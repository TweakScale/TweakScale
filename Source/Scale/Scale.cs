/*
		This file is part of TweakScale /L
		© 2018-2020 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

		TweakScale /L is double licensed, as follows:

		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

		And you are allowed to choose the License that better suit your needs.

		TweakScale /L is distributed in the hope that it will be useful,
		but WITHOUT ANY WARRANTY; without even the implied warranty of
		MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

		You should have received a copy of the SKL Standard License 1.0
		along with TweakScale /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

		You should have received a copy of the GNU General Public License 2.0
		along with TweakScale /L If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
//using ModuleWheels;

using TweakScale.Annotations;
using System.Collections.Generic;

namespace TweakScale
{    
	public class TweakScale : PartModule, IPartCostModifier, IPartMassModifier
    {
        /// <summary>
        /// The selected scale. Different from currentScale only for destination single update, where currentScale is set to match this.
        /// </summary>
        [KSPField(isPersistant = false, guiActiveEditor = true, guiName = "#TweakScale_GUI_Scale", guiFormat = "0.000", guiUnits = "m")]//Scale
        [UI_ScaleEdit(scene = UI_Scene.Editor)]
// ReSharper disable once InconsistentNaming
        public float tweakScale = -1;

        /// <summary>
        /// Index into scale values array.
        /// </summary>
        [KSPField(isPersistant = false, guiActiveEditor = true, guiName = "#TweakScale_GUI_Scale")]//Scale
        [UI_ChooseOption(scene = UI_Scene.Editor)]
// ReSharper disable once InconsistentNaming
        public int tweakName = 0;

        /// <summary>
        /// The scale to which the part currently is scaled.
        /// </summary>
        [KSPField(isPersistant = true)]
// ReSharper disable once InconsistentNaming
        public float currentScale = -1;

        /// <summary>
        /// The default scale, i.e. the number by which to divide tweakScale and currentScale to get the relative size difference from when the part is used without TweakScale.
        /// </summary>
        [KSPField(isPersistant = true)]
// ReSharper disable once InconsistentNaming
        public float defaultScale = -1;

        /// <summary>
        /// Whether the part should be freely scalable or limited to destination list of allowed values.
        /// </summary>
        [KSPField(isPersistant = false)]
// ReSharper disable once InconsistentNaming
        public bool isFreeScale = false;

        /// <summary>
        /// The scale exponentValue array. If isFreeScale is false, the part may only be one of these scales.
        /// </summary>
        protected float[] ScaleFactors = { 0.625f, 1.25f, 2.5f, 3.75f, 5f };
        
        /// <summary>
        /// PartDB - KSP Part data abstraction layer
        ///</summary>
        protected PartDB partDB;

        /// <summary>
        /// Cached scale vector, we need this because the game regularly reverts the scaling of the IVA overlay
        /// </summary>
        private Vector3 _savedIvaScale;

        /// <summary>
        /// The exponentValue by which the part is scaled by default. When destination part uses MODEL { scale = ... }, this will be different from (1,1,1).
        /// </summary>
        [KSPField(isPersistant = true)]
// ReSharper disable once InconsistentNaming
        public Vector3 defaultTransformScale = new Vector3(0f, 0f, 0f);

        private bool _firstUpdate = true;
        private bool is_duplicate = false;
        public bool scaleMass = true;

        /// <summary>
        /// Updaters for different PartModules.
        /// </summary>
        private IRescalable[] _updaters = new IRescalable[0];

        /// <summary>
        /// Cost of unscaled, empty part.
        /// </summary>
        [KSPField(isPersistant = true)]
        public float DryCost = 0f;  // Default value, so missing DryCost from the Config will be calculated by the PrefabCostWriter.

        /// <summary>
        /// Original Crew Capacity
        /// </summary>
        [KSPField(isPersistant = true)]
        public int OriginalCrewCapacity;

        /// <summary>
        /// Flag to tell TweakScale to plain ignore the part's resources on the costs calculation.
		/// Needed for Modules that does their own Resources Management instead of using stock's one.
        /// </summary>
        [KSPField(isPersistant = false)]
        public bool ignoreResourcesForCost = false;

        /// <summary>
        /// scaled mass
        /// </summary>
        [KSPField(isPersistant = false)]
        public float MassScale = 1;

        private Hotkeyable _chainingEnabled;

        /// <summary>
        /// The ScaleType for this part.
        /// </summary>
        public ScaleType ScaleType { get; private set; }

        public bool IsScaled => (Math.Abs(currentScale / defaultScale - 1f) > 1e-5f);
        private bool IsChanged => currentScale != (isFreeScale ? tweakScale : ScaleFactors [tweakName]);

        /// <summary>
        /// The current scaling factor.
        /// </summary>
        public ScalingFactor ScalingFactor => new ScalingFactor(tweakScale / defaultScale, tweakScale / currentScale, isFreeScale ? -1 : tweakName);

        protected virtual void SetupPrefab(Part prefabPart)
        {
            Log.dbg("SetupPrefab {0}", this.InstanceID);
			ConfigNode PartNode = GameDatabase.Instance.GetConfigs("PART").FirstOrDefault(c => c.name.Replace('_', '.') == part.name).config;
			ConfigNode ModuleNode = PartNode.GetNodes("MODULE").FirstOrDefault(n => n.GetValue("name") == moduleName);

            this.ScaleType = new ScaleType(ModuleNode);
            this.SetupFromConfig(ScaleType);
            this.partDB = PartDB.Create(prefabPart, this.part, ScaleType);
            tweakScale = currentScale = defaultScale;

            tfInterface = Type.GetType("TestFlightCore.TestFlightInterface, TestFlightCore", false);
        }

        /// <summary>
        /// Sets up values from ScaleType, creates updaters, and sets up initial values.
        /// </summary>
        protected virtual void Setup() // For compatibility to Legacy.
        {
            this.Setup(this.part);
        }
        protected virtual void Setup(Part part)
        {
            Log.dbg("Setup {0}", this.InstanceID);

            {
                Part prefab = part.partInfo.partPrefab;
                ScaleType = (prefab.Modules["TweakScale"] as TweakScale).ScaleType;
                SetupFromConfig(ScaleType);
                this.partDB = PartDB.Create(prefab, part, ScaleType, this);     // This need to be reworked. I calling this twice. :(
            }

            _updaters = TweakScaleUpdater.CreateUpdaters(part).ToArray();
            this.SetupCrewManifest();
            this.HandleChildrenScaling(); // Wrongplace?

            if (!isFreeScale && ScaleFactors.Length != 0)
            {
                tweakName = Tools.ClosestIndex(tweakScale, ScaleFactors);
                tweakScale = ScaleFactors[tweakName];
            }
        }

        internal void RescaleIfNeededAndUpdate()
        {
            if (!IsScaled) return;
            this.RescaleAndUpdate();
            this.NotifyListeners();
        }

        internal void ScaleAndUpdate()
        {
            this.partDB.Scale();
            try {
                this.CallUpdaters();
            } catch (Exception exception) {
                Log.error("Exception on ScaleAndUpdate: {0}", exception);
            }
        }

        internal void RescaleAndUpdate()
        {
            this.partDB.Rescale();
            try {
                this.CallUpdaters();
            } catch (Exception exception) {
                Log.error("Exception on RescaleAndUpdate: {0}", exception);
            }
        }

        internal void CalculateDryCostIfNeeded()    // Needed by PrefabDryCostWriter
        {
            Log.dbg("CalculateDryCostIfNeeded {0}", this.InstanceID);
            if (0f == this.DryCost)
                this.DryCost = (float)this.partDB.CalculateDryCost();
        }

        /// <summary>
        /// Loads settings from <paramref name="scaleType"/>.
        /// </summary>
        /// <param name="scaleType">The settings to use.</param>
        private void SetupFromConfig(ScaleType scaleType)
        {
            Log.dbg("SetupFromConfig for {0}", this.part.name);
            if (ScaleType == null) Log.error("Scaletype==null! part={0}", part.name);

            isFreeScale = scaleType.IsFreeScale;
            if (-1 == defaultScale)
                defaultScale = scaleType.DefaultScale;

            if (-1 == currentScale)
                currentScale = defaultScale;

            if (-1 == tweakScale)
                tweakScale = currentScale;

            Fields["tweakScale"].guiActiveEditor = false;
            Fields["tweakName"].guiActiveEditor = false;
            ScaleFactors = scaleType.ScaleFactors;
            if (ScaleFactors.Length <= 0)
                return;

            if (isFreeScale)
            {
                Fields["tweakScale"].guiActiveEditor = true;
				UI_ScaleEdit range = (UI_ScaleEdit)Fields["tweakScale"].uiControlEditor;
                range.intervals = scaleType.ScaleFactors;
                range.incrementSlide = scaleType.IncrementSlide;
                range.unit = scaleType.Suffix;
                range.sigFigs = 3;
                Fields["tweakScale"].guiUnits = scaleType.Suffix;
            }
            else
            {
                Fields["tweakName"].guiActiveEditor = scaleType.ScaleFactors.Length > 1;
				UI_ChooseOption options = (UI_ChooseOption)Fields["tweakName"].uiControlEditor;
                options.options = scaleType.ScaleNames;
            }
        }

# region Event Handlers
        [UsedImplicitly]
        public override void OnLoad(ConfigNode node)
        {
            Log.dbg("OnLoad {0}", this.InstanceID);

            base.OnLoad(node);

            if (null == part.partInfo)
            {
                // Loading of the prefab from the part config
                this.SetupPrefab(part);
            }
            else
            {
                // Loading of the part from a saved craft
                tweakScale = currentScale;
                if (HighLogic.LoadedSceneIsEditor || IsScaled)
                { 
                    this.Setup(part);

					// That's the problem - somewhere in the not so near past, KSP implemented a stunt called
					// UpgradePipeline. This thing acts after the PartModule's OnLoad handler, and it injects
					// back default values from prefab into the part on loading. This was intended to allow older
					// savegames to be loaded on newer KSP (as it would inject default values on missing atributes
					// present only on the new KSP version - or to reset new defaults when things changed internally),
					// but also ended up trashing changes and atributes only available on runtime for some custom modules.
					// So we need to check and upgrade things **before** KSP mangles them, otherwise the old values will
					// be trashed by KSP and we will not be able to detect and fix the old data ourselves.
                    {   // Act only on craft loadings from file.
                        KSPe.ConfigNodeWithSteroids cn = KSPe.ConfigNodeWithSteroids.from(node);
                        if (!this.IsPartMatchesPrefab(cn))
                            this.FixPartScaling(node, cn);
                    }
                    this.RescaleIfNeededAndUpdate();
                }
                else
                    this.enabled = false;
            }
        }

		[UsedImplicitly]
        public override void OnSave(ConfigNode node)
        {
            Log.dbg("OnSave {0}", this.InstanceID);

            if (this.is_duplicate)
            {   // Hack to prevent duplicated entries (and duplicated modules) persisting on the craft file
                node.SetValue("name", "TweakScaleRogueDuplicate", 
                    "Programatically tainted due duplicity. Only one single instance above should exist, usually the first one. ",
                    false);
                Log.warn("Part {0} has a Rogue Duplicated TweakScale!", part.name);
            }

            base.OnSave(node);
        }

        [UsedImplicitly]
        public override void OnAwake()
        {
            Log.dbg("OnAwake {0}", this.InstanceID);

            base.OnAwake();
            if (HighLogic.LoadedSceneIsEditor) this.Setup(this.part);
        }

        [UsedImplicitly]
        public override void OnStart(StartState state)
        {
            if (this.FailsIntegrity()) return;

            Log.dbg("OnStart {0}", this.InstanceID);

            base.OnStart(state);

			{
				UI_ScaleEdit ui = (this.Fields["tweakScale"].uiControlEditor as UI_ScaleEdit);
				ui.onFieldChanged += this.OnTweakScaleChanged;
			}
			{
				UI_ChooseOption ui = (this.Fields["tweakName"].uiControlEditor as UI_ChooseOption);
				ui.onFieldChanged += this.OnTweakScaleChanged;
			}

            if (HighLogic.LoadedSceneIsEditor)
            {
                if (this.partDB.HasCrew)
                {
                    GameEvents.onEditorShipModified.Add(OnEditorShipModified);
                    this.wasOnEditorShipModifiedAdded = true;
                }

                _chainingEnabled = HotkeyManager.Instance.AddHotkey(
                    "Scale chaining", new[] {KeyCode.LeftShift}, new[] {KeyCode.LeftControl, KeyCode.K}, false
                    );
            }

            // scale IVA overlay
            if (HighLogic.LoadedSceneIsFlight && this.enabled && (part.internalModel != null))
            {
                _savedIvaScale = part.internalModel.transform.localScale * ScalingFactor.absolute.linear;
                part.internalModel.transform.localScale = _savedIvaScale;
                part.internalModel.transform.hasChanged = true;
            }
        }

		[UsedImplicitly]
		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}", this._InstanceID); // Something bad is happening inside KSP guts before this being called,
														// so I had to cache the InstanceID because the part's data are inconsistent at this point.

			if (null != this.partDB) this.partDB = this.partDB.Destroy();
			if (this.wasOnEditorShipModifiedAdded) GameEvents.onEditorShipModified.Remove(this.OnEditorShipModified);
		}


		private void OnTweakScaleChanged(BaseField field, object what)
		{
			this.OnTweakScaleChanged();
			foreach (Part p in this.part.symmetryCounterparts)
				p.FindModuleImplementing<TweakScale>().OnTweakScaleChanged();
		}

		/// <summary>
		/// Scale has changed!
		/// </summary>
		private void OnTweakScaleChanged()
        {
            Log.dbg("OnTweakScaleChanged {0}", this.InstanceID);

            if (!isFreeScale)
            {
                tweakScale = ScaleFactors[tweakName];
            }

            this.partDB.SetScale(tweakScale);

            if ((_chainingEnabled != null) && _chainingEnabled.State)
            {
                this.HandleChildrenScaling();
            }

            this.ScaleAndUpdate();
            this.MarkWindowDirty();

            currentScale = tweakScale;

            this.NotifyListeners();
        }

        private bool wasOnEditorShipModifiedAdded = false;
        [UsedImplicitly]
        private void OnEditorShipModified(ShipConstruct ship)
        {
            Log.dbg("OnEditorShipModified {0}", this.InstanceID);

            if (HighLogic.LoadedSceneIsEditor) //only run the following block in the editor; it updates the crew-assignment GUI
                this.UpdateCrewManifest(); 
        }

        [UsedImplicitly]
        public void Update()
        {
            Log.dbgOnce("Update {0}", this.InstanceID);

            if (_firstUpdate)
            {
                _firstUpdate = false;
                if (this.FailsIntegrity()) return;
                if (this.IsScaled) this.partDB.FirstUpdate();
            }

            if (HighLogic.LoadedSceneIsFlight)
            {
                // flight scene frequently nukes our OnStart resize some time later
                if ((part.internalModel != null) && (part.internalModel.transform.localScale != _savedIvaScale))
                {
                    part.internalModel.transform.localScale = _savedIvaScale;
                    part.internalModel.transform.hasChanged = true;
                }
            }

            this.CallUpdateables();
        }

#endregion

        private void CallUpdaters()
        {
            // two passes, to depend less on the order of this list
            {
                int len = _updaters.Length;
                for (int i = 0; i < len; i++) {
                    // first apply the exponents
                    IRescalable updater = _updaters [i];
                    if (updater is TSGenericUpdater) {
                        float oldMass = part.mass;  // Why resetting the mass? What happens if I write a updater for the Mass?
                                                    // I suspect I found the source of some mass related idiossyncrasies - todo: INVESTIGATE
                        try {
                            updater.OnRescale(ScalingFactor);
                        } catch (Exception e) {
                            Log.error("Exception on rescale while TSGenericUpdater: {0}", e);
                        } finally {
                            part.mass = oldMass; // make sure we leave this in a clean state
                        }
                    }
                }
            }

            // Why this code was here? We already registered it on the EditorOnChange. Perhaps for older KSP?
            //if (_prefabPart.CrewCapacity > 0 && HighLogic.LoadedSceneIsEditor)
            //    UpdateCrewManifest();

            if (part.Modules.Contains("ModuleDataTransmitter"))
                UpdateAntennaPowerDisplay();

            // MFT support
            UpdateMftModule();

            // TF support
            updateTestFlight();

            {
                int len = _updaters.Length;
                for (int i = 0; i < len; i++) {
                    IRescalable updater = _updaters [i];
                    // then call other updaters (emitters, other mods)
                    if (updater is TSGenericUpdater)
                        continue;

                    try {
                        updater.OnRescale(ScalingFactor);
                    } catch (Exception e) {
                        Log.error("Exception on rescale while ¬TSGenericUpdater: {0}", e);
                    }
                }
            }
        }

        private void CallUpdateables()
        {
            int len = _updaters.Length;
            for (int i = 0; i < len; i++)
            {
                if (_updaters[i] is IUpdateable)
                    (_updaters[i] as IUpdateable).OnUpdate();
            }
        }

        private void NotifyListeners()
        {
            // Problem: We don't have the slightest idea if the OnPartScaleChanged was already handled or not.
            // If it didn't, this event may induce Recall to cache the part's resource before he could finish his business.
            // So whoever has received that event, he will need to handle OnPartResourceChanged too after, even by us doing it here.

            // send Resource Changed message to KSP Recall if needed
            if (0 != this.partDB.part.Resources.Count) this.NotifyPartResourcesChanged();

            // send AttachNodes Changed message to KSP Recall if needed
            if (0 != this.partDB.part.attachNodes.Count) this.NotifyPartAttachmentNodesChanged();

            this.NotifyPartSurfaceAttachmentChanged(); // This is not working on KSP 1.9, apparently Editor overwrites us before we send the event here!

            // send scaling part message
            this.NotifyPartScaleChanged();

            // Notify the Word we changed the ship.
            GameEvents.onEditorShipModified.Fire(EditorLogic.fetch.ship);
        }

        private void SetupCrewManifest()
        {
            // Restores the original Crew Capacity, as the Pregab is mangled.
            this.partDB.part.CrewCapacity = this.OriginalCrewCapacity;

            VesselCrewManifest vcm = ShipConstruction.ShipManifest;
            if (vcm == null) { return; }
            PartCrewManifest pcm = vcm.GetPartCrewManifest(part.craftID);
            if (pcm == null) { return; }

            if (pcm.partCrew.Length != this.partDB.part.CrewCapacity)
                this.SetCrewManifestSize(pcm, this.partDB.part.CrewCapacity);
        }

        //only run the following block in the editor; it updates the crew-assignment GUI
        private void UpdateCrewManifest()
        {
            Log.dbg("UpdateCrewManifest {0}", this.InstanceID);

#if !CREW_SCALE_UP
            // Small safety guard.
            if (part.CrewCapacity >= this.partDB.prefab.CrewCapacity) { return; }
#endif

            try // Preventing this thing triggering an eternal loop on the event handling!
            {
                VesselCrewManifest vcm = ShipConstruction.ShipManifest;
                if (vcm == null) return;
                PartCrewManifest pcm = vcm.GetPartCrewManifest(part.craftID);
                if (pcm == null) return;

                int len = pcm.partCrew.Length;
                //int newLen = Math.Min(part.CrewCapacity, _prefabPart.CrewCapacity);
                int newLen = part.CrewCapacity;
                if (len == newLen) return;

                Log.dbg("UpdateCrewManifest current {0}; new {1}", len, newLen);

                this.partDB.part.CrewCapacity  = newLen;
#if CREW_SCALE_UP
    #if PREFAB_SCALE_HACK
                // Workaround to try to force KSP to accept bigger crew manifests at editting time, as apparently it only respects the prefab's value, bluntly ignoring the part's data!
                this.partDB.prefab.CrewCapacity = Math.Max(this.partDB.prefab.CrewCapacity, this.partDB.part.CrewCapacity);
    #endif
#else
                this.partDB.part.CrewCapacity = Math.Min(this.partDB.part.CrewCapacity, this.partDB.prefab.CrewCapacity);
#endif
                if (EditorLogic.fetch.editorScreen == EditorScreen.Crew)
                    EditorLogic.fetch.SelectPanelParts();

                this.SetCrewManifestSize(pcm, newLen);

                ShipConstruction.ShipManifest.SetPartManifest(part.craftID, pcm);
            }
            catch (Exception e)
            {
                Log.error(e, this);
            }
        }

        private void SetCrewManifestSize(PartCrewManifest pcm, int crewCapacity)
        {
            string[] newpartCrew = new string[crewCapacity];
            {
                for (int i = 0; i < newpartCrew.Length; ++i)
                    newpartCrew[i] = string.Empty;

                int SIZE = Math.Min(pcm.partCrew.Length, newpartCrew.Length);
                for (int i = 0; i < SIZE; ++i)
                    newpartCrew[i] = pcm.partCrew[i];

                for (int i = SIZE; i < pcm.partCrew.Length; ++i)
                    pcm.RemoveCrewFromSeat(i);
            }
            pcm.partCrew = newpartCrew;
        }

        private void UpdateMftModule()
        {
            try
            {
                if (this.partDB.prefab.Modules.Contains("ModuleFuelTanks"))
                {
                    scaleMass = false;
					PartModule m = this.partDB.prefab.Modules["ModuleFuelTanks"];
                    FieldInfo fieldInfo = m.GetType().GetField("totalVolume", BindingFlags.Public | BindingFlags.Instance);
                    if (fieldInfo != null)
                    {
                        double oldVol = (double)fieldInfo.GetValue(m) * 0.001d;
						BaseEventDetails data = new BaseEventDetails(BaseEventDetails.Sender.USER);
                        data.Set<string>("volName", "Tankage");
                        data.Set<double>("newTotalVolume", oldVol * ScalingFactor.absolute.cubic);
                        part.SendEvent("OnPartVolumeChanged", data, 0);
                    }
                    else Log.warn("MFT interaction failed (fieldinfo=null)");
                }
            }
            catch (Exception e)
            {
                Log.warn("Exception during MFT interaction" + e.ToString());
            }
        }

        public static Type tfInterface = null;
        private void updateTestFlight()
        {
            if (null == tfInterface) return;
            BindingFlags tBindingFlags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
            string _name = "scale";
            string value = ScalingFactor.absolute.linear.ToString();
            string owner = "TweakScale";

            bool valueAdded = (bool)tfInterface.InvokeMember("AddInteropValue", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new System.Object[] { part, _name, value, owner });
            Log.dbg("TF: valueAdded={0}, value={1}", valueAdded, value);
        }

        private void UpdateAntennaPowerDisplay()
        {
			ModuleDataTransmitter m = part.Modules["ModuleDataTransmitter"] as ModuleDataTransmitter;
            double p = m.antennaPower / 1000;
            Char suffix = 'k';
            if (p >= 1000)
            {
                p /= 1000f;
                suffix = 'M';
                if (p >= 1000)
                {
                    p /= 1000;
                    suffix = 'G';
                }
            }
            p = Math.Round(p, 2);
            string str = p.ToString() + suffix;
            if (m.antennaCombinable) { str += " (Combinable)"; }
            m.powerText = str;
        }

		/// <summary>
		/// Propagate relative scaling factor to children.
		/// </summary>
		private void HandleChildrenScaling()
		{
			this.HandleChildrenScaling(this.part);
		}

		private void HandleChildrenScaling(Part father)
		{
			HashSet<Part> rejected = new HashSet<Part>();
			HashSet<Part> parts = new HashSet<Part>();
			{
				int len = father.children.Count;
				for (int i = 0; i < len; ++i) parts.Add(father.children[i]);
			}
			while (parts.Count > 0) {
				foreach (Part p in parts) {
					if (!this.HandleChildScaling(p)) {
						// if we get here, the part is not scalable. So we need to handle their children ourselves
						int len = p.children.Count;
						for (int i = 0; i < len; ++i) rejected.Add(p.children[i]);
					}
				}
				parts.Clear();
				parts.UnionWith(rejected);
                rejected.Clear();
			}
		}

		private bool HandleChildScaling(Part child)
		{
			TweakScale b = child.GetComponent<TweakScale>();
			if (null == b) {
				Log.dbg("Ignoring child scaling {0}:{1}", child.name, child.persistentId);
				return false;
			}
			Log.dbg("Handling child scaling {0}:{1}", child.name, child.persistentId);
			float factor = ScalingFactor.relative.linear;
			if (Math.Abs(factor - 1) <= 1e-4f) return true;

			b.tweakScale *= factor;
			if (!b.isFreeScale && (b.ScaleFactors.Length > 0)) {
				b.tweakName = Tools.ClosestIndex(b.tweakScale, b.ScaleFactors);
			}
			b.OnTweakScaleChanged();
            return true;
		}

		/// <summary>
		/// Disable TweakScale module if something is wrong.
		/// </summary>
		/// <returns>True if something is wrong, false otherwise.</returns>
		private bool FailsIntegrity()
        {
            if (this != part.Modules.GetModules<TweakScale>().First())
            {
                enabled = false; // disable TweakScale module
                this.is_duplicate = true; // Flags this as not persistent
                Fields["tweakScale"].guiActiveEditor = false;
                Fields["tweakName"].guiActiveEditor = false;
                Log.warn("Duplicate TweakScale module on part [{0}] {1}", part.partInfo.name, part.partInfo.title);
                return true;
            }
            if (ScaleFactors.Length == 0)
            {
                enabled = false; // disable TweakScale module
                Log.warn("{0}({1}) has no valid scale factors. This is probably caused by an invalid TweakScale configuration for the part.", part.name, part.partInfo.title);
                Log.dbg(this.ToString());
                Log.dbg(ScaleType.ToString());
                return true;
            }
            return false;
        }

        private bool IsPartMatchesPrefab(KSPe.ConfigNodeWithSteroids node)
        {
            float prefabDefaultScale = this.partDB.prefab.Modules.GetModule<TweakScale>(0).defaultScale;
            float currentDefaultScale = node.GetValue<float>("defaultScale", prefabDefaultScale);
            return Math.Abs(prefabDefaultScale - currentDefaultScale) < 0.001f;
        }

        // ConfigNodeWithSteroids is not complete yet, lots of work to do!
        // So I had to give the source node to be fixed together the fancy one with some nice helpers,
        // as it currently doesn't updates the node used to create it (by design, the idea is to create an
        // "commit" command - so exception handling would be made easier.
        private ConfigNode FixPartScaling(ConfigNode source, KSPe.ConfigNodeWithSteroids node)
        {
            float prefabDefaultScale = this.partDB.prefab.Modules.GetModule<TweakScale>(0).defaultScale;
            float craftDefaultScale = node.GetValue<float>("defaultScale", prefabDefaultScale);
            float craftScale = node.GetValue<float>("currentScale", prefabDefaultScale);
            float craftRelativeScale = craftScale / craftDefaultScale;
            float newCurrentScale = prefabDefaultScale * craftRelativeScale;

            source.SetValue("currentScale", newCurrentScale);
            source.SetValue("defaultScale", prefabDefaultScale);

            Log.warn("Invalid defaultScale! Craft {0} had the part {1} defaultScale changed from {2:F3} to {3:F3} and was rescaled to {4:F3}"
                , this.partDB.part.craftID, this.InstanceID
                , craftDefaultScale, prefabDefaultScale, newCurrentScale
                );
            return source;
        }

        /// <summary>
        /// Marks the right-click window as dirty (i.e. tells it to update).
        /// </summary>
        private void MarkWindowDirty() // redraw the right-click window with the updated stats
        {
            foreach (UIPartActionWindow win in FindObjectsOfType<UIPartActionWindow>().Where(win => win.part == part))
            {
                // This causes the slider to be non-responsive - i.e. after you click once, you must click again, not drag the slider.
                win.displayDirty = true;
            }
        }

		#region Interface Implementation

        float IPartCostModifier.GetModuleCost(float defaultCost, ModifierStagingSituation situation) // TODO: This makes any sense? What's situation anyway?
        {
            Log.dbg("IPartCostModifier.GetModuleCost {0} IsScaled? {1}", this.InstanceID, this.IsScaled);
            float r = this.IsScaled ? this.partDB.ModuleCost : 0;
            Log.dbg("IPartCostModifier.GetModuleCost {0} {1}", this.InstanceID, r);
            return r;
        }

        ModifierChangeWhen IPartCostModifier.GetModuleCostChangeWhen()
        {
            return ModifierChangeWhen.FIXED;
        }

        float IPartMassModifier.GetModuleMass(float defaultMass, ModifierStagingSituation situation)
        {
            if (IsScaled && scaleMass)
              return this.partDB.prefab.mass * (MassScale - 1f);
            else
              return 0;
        }

        ModifierChangeWhen IPartMassModifier.GetModuleMassChangeWhen()
        {
            return ModifierChangeWhen.FIXED;
        }

		#endregion


		#region Public Interface

		//
		// These are meant for use with an unloaded part (so you only have the persistent data
		// but the part is not alive). In this case get currentScale/defaultScale and call
		// this method on the prefab part.
		//

		public double MassFactor => this.getMassFactor((double)(this.tweakScale / this.defaultScale));
        public double getMassFactor(double rescaleFactor)
        {
            double exponent = ScaleExponents.getMassExponent(this.ScaleType.Exponents);
            return Math.Pow(rescaleFactor, exponent);
        }

        public double DryCostFactor => this.getDryCostFactor((double)(this.tweakScale / this.defaultScale));
        public double getDryCostFactor(double rescaleFactor)
        {
            double exponent = ScaleExponents.getDryCostExponent(ScaleType.Exponents);
            return Math.Pow(rescaleFactor, exponent);
        }

        public double VolumeFactor => this.getVolumeFactor((double)(this.tweakScale / this.defaultScale));
        public double getVolumeFactor(double rescaleFactor)
        {
            return Math.Pow(rescaleFactor, 3); //NOTE: Volume is **always** 3 dimensional.
        }

        public double AreaFactor => this.getAreaFactor((double)(this.tweakScale / this.defaultScale));
        public double getAreaFactor(double rescaleFactor)
        {
            return Math.Pow(rescaleFactor, 2); //NOTE: Area is **always** 2 dimensional.
        }

        public float CurrentScaleFactor => this.partDB.RescaleFactor;

        #endregion


        # region Event Senders

        private void NotifyPartScaleChanged()
        {
            BaseEventDetails data = new BaseEventDetails(BaseEventDetails.Sender.USER);
            data.Set<float>("factorAbsolute", ScalingFactor.absolute.linear);
            data.Set<float>("factorRelative", ScalingFactor.relative.linear);
            this.part.SendEvent("OnPartScaleChanged", data, 0);
        }

        private void NotifyPartAttachmentNodesChanged()
        {
            BaseEventDetails data = new BaseEventDetails(BaseEventDetails.Sender.USER);
            data.Set<int>("InstanceID", this.part.GetInstanceID());
            data.Set<Type>("issuer", this.GetType());
            this.part.SendEvent("NotifyPartAttachmentNodesChanged", data, 0);
        }

        private void NotifyPartSurfaceAttachmentChanged()
        {
            BaseEventDetails data = new BaseEventDetails(BaseEventDetails.Sender.USER);
            data.Set<int>("InstanceID", this.part.GetInstanceID());
            data.Set<Type>("issuer", this.GetType());
            data.Set<AttachNode>("srfAttachNode", this.part.srfAttachNode);
            this.part.SendEvent("OnPartSurfaceAttachmentChanged", data, 0);
        }

        private void NotifyPartResourcesChanged()
        {
            BaseEventDetails data = new BaseEventDetails(BaseEventDetails.Sender.USER);
            data.Set<int>("InstanceID", this.part.GetInstanceID());
            data.Set<Type>("issuer", this.GetType());
            this.part.SendEvent("OnPartResourcesChanged", data, 0);
        }

        #endregion


		// This was borking on OnDestroy, so I decided to cache the information and save a NRE there.
		private string _InstanceID = null;
		public string InstanceID => this._InstanceID = string.Format("{0}:{1:X}", this.part.name, this.part.GetInstanceID());

        public override string ToString()
        {
            string result = string.Format("TweakScale:{0} {{", this.InstanceID);
            result += "; isFreeScale = " + isFreeScale;
            result += "; " + ScaleFactors.Length  + " scaleFactors = ";
            foreach (float s in ScaleFactors)
                result += s + "  ";
            result += "; tweakScale = "   + tweakScale;
            result += "; currentScale = " + currentScale;
            result += "; defaultScale = " + defaultScale;
            result += "; scaleNodes = " + this.ScaleType.ScaleNodes;
            //result += "; minValue = " + MinValue;
            //result += "; maxValue = " + MaxValue;
            return result + "}";
        }

#if DEBUG
        [KSPEvent(guiActive = false, active = true)]
        internal void OnPartScaleChanged(BaseEventDetails data)
        {
            float factorAbsolute = data.Get<float>("factorAbsolute");
            float factorRelative = data.Get<float>("factorRelative");
            Log.dbg("PartMessage: OnPartScaleChanged:"
                + "\npart=" + part.name
                + "\nfactorRelative=" + factorRelative.ToString()
                + "\nfactorAbsolute=" + factorAbsolute.ToString());

        }

        [KSPEvent(guiActive = true, guiActiveEditor = true, guiName = "Debug")]
        internal void debugOutput()
        {
            Log.dbg("<debugOutput for {0}>", this.InstanceID);
            AvailablePart ap = part.partInfo;
            Log.dbg("prefabCost={0}, dryCost={1}, prefabDryCost={2}", ap.cost, DryCost, (this.partDB.prefab.Modules["TweakScale"] as TweakScale).DryCost);

            if (this.part.Modules.Contains("ModuleKISItem"))
                Log.dbg("kisVolOvr={0}", part.Modules["ModuleKISItem"].Fields["volumeOverride"].GetValue(part.Modules["ModuleKISItem"]));

            Log.dbg("ignoreResourcesForCost={0}, ResourceCost={1}", this.ignoreResourcesForCost, (part.Resources.Cast<PartResource>().Aggregate(0.0, (a, b) => a + b.maxAmount * b.info.unitCost) ));

            {
                TweakScale ts = part.partInfo.partPrefab.Modules ["TweakScale"] as TweakScale;
                Log.dbg("massFactor={0}", ts.MassFactor);
                Log.dbg("costFactor={0}", ts.DryCostFactor);
                Log.dbg("volFactor={0}", ts.VolumeFactor);
            }

            Collider x = part.collider;
            Log.dbg("C: {0}, enabled={1}", x.name, x.enabled);
            if (part.Modules.Contains("ModuleRCSFX")) {
                Log.dbg("RCS power={0}", (part.Modules["ModuleRCSFX"] as ModuleRCSFX).thrusterPower);
            }
            if (part.Modules.Contains("ModuleEnginesFX"))
            {
                Log.dbg("Engine thrust={0}", (part.Modules["ModuleEnginesFX"] as ModuleEnginesFX).maxThrust);
            }
            Log.dbg("</debugOutput>");

        }

        public new bool enabled {
            get { return base.enabled; }
            set {
                if (base.enabled != value)
                {
                    System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                    Log.detail("Enabled set to {0} {1}", value, t);
                }
                base.enabled = value;
            }
        }
#endif

    }
}
