/*
	This file is part of TweakScale /L
		© 2018-2023 LisiasT
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
	along with TweakScale /L. If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

using KSPe.Annotations;

namespace TweakScale
{
	[KSPAddon(KSPAddon.Startup.EditorAny, false)]
	internal class TweakScaleHotKeys : MonoBehaviour
	{
		private void Awake()
		{
			Features.AutoScale.Init();
			Features.ScaleChaining.Init();
		}

		private void OnDestroy()
		{
			Features.ScaleChaining.DeInit();
			Features.AutoScale.DeInit();
		}
	}

	public class TweakScale : PartModule, IPartCostModifier, IPartMassModifier
	{
		// Checks if the running KSP has the Upgrade Pipeline feature, so TweakScale can omit itself from craft files when not used,
		// as the module will be injected back on loading when needed.
		private static readonly bool UPGRADE_PILELINED_KSP = KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.GetVersion(1,4,0);

		[KSPField(isPersistant = true, guiActiveEditor = false)]
		public string type = "";

		/// <summary>
		/// Tells if TweakScale is active or not. When inactiva, it will be completely uselees, as it was not installed on this part at all
		/// </summary>
		[KSPField(isPersistant = true, guiActiveEditor = true, guiName = "TweakScale is")]
		[UI_Toggle(disabledText = "Inactive", enabledText = "Active", scene = UI_Scene.Editor)]
		public bool active = true;

		/// <summary>
		/// Allows to hide the TweakScale widgets from the PAW when set to false, so the current selections can't be changed by the user.
		/// Usefull to soft remove tweakscale from a part for challenges by using active = false above together this one.
		/// Obviously, there's no PAW widget for this. :)
		/// </summary>
		[KSPField(isPersistant = true)]
		public bool available = true;

		/// <summary>
		/// To tell the user when TweakScale is unavailable, to prevent him/her to think TweakScale was deinstalled.
		/// </summary>
		[KSPField(isPersistant = false, guiActiveEditor = false, guiName = "TweakScale is")]
        [UI_Label(scene = UI_Scene.Editor)]
		public string availabilityStatus = " Unavailable";

        /// <summary>
        /// The selected scale. Different from currentScale only for destination single update, where currentScale is set to match this.
        /// </summary>
        [KSPField(isPersistant = false, guiActiveEditor = true, guiName = "Scale", guiFormat = "0.000", guiUnits = "m")]//#TweakScale_GUI_Scale
        [UI_ScaleEdit(scene = UI_Scene.Editor)]
        public float tweakScale = -1;

        /// <summary>
        /// Index into scale values array.
        /// </summary>
        [KSPField(isPersistant = false, guiActiveEditor = true, guiName = "Scale")]//#TweakScale_GUI_Scale
        [UI_ChooseOption(scene = UI_Scene.Editor)]
        public int tweakName = 0;

        /// <summary>
        /// The scale to which the part currently is scaled.
        /// </summary>
        [KSPField(isPersistant = true)]
        public float currentScale = -1;

        /// <summary>
        /// The default scale, i.e. the number by which to divide tweakScale and currentScale to get the relative size difference from when the part is used without TweakScale.
        /// </summary>
        [KSPField(isPersistant = true)]
        public float defaultScale = -1;

        /// <summary>
        /// Whether the part should be freely scalable or limited to destination list of allowed values.
        /// </summary>
        [KSPField(isPersistant = false)]
        public bool isFreeScale = false;

        /// <summary>
        /// The scale exponentValue array. If isFreeScale is false, the part may only be one of these scales.
        /// </summary>
        internal float[] ScaleFactors = { 0.625f, 1.25f, 2.5f, 3.75f, 5f };
        
        /// <summary>
        /// PartDB - KSP Part data abstraction layer
        ///</summary>
		internal PartDB.Scaler scaler;

        /// <summary>
        /// Cached scale vector, we need this because the game regularly reverts the scaling of the IVA overlay
        /// </summary>
        private Vector3 savedIvaScale = Vector3.zero;

        /// <summary>
        /// The exponentValue by which the part is scaled by default. When destination part uses MODEL { scale = ... }, this will be different from (1,1,1).
        /// </summary>
        [KSPField(isPersistant = true)]
        public Vector3 defaultTransformScale = new Vector3(0f, 0f, 0f);

        private bool _firstUpdate = true;
        private bool _firstUpdateAfterCopy = false;
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

        /// <summary>
        /// The ScaleType for this part.
        /// </summary>
        public ScaleType ScaleType { get; private set; }

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
			this.scaler = PartDB.Scaler.Create(prefabPart, this.part, ScaleType);
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
		protected virtual void Setup(Part part, UpgradePipelineStatus status = null)
		{
			Log.dbg("Setup {0} {1}", this.InstanceID, status);

			{
				Part prefab = this.part.partInfo.partPrefab;
				this.ScaleType = (prefab.Modules["TweakScale"] as TweakScale).ScaleType;
				this.SetupFromConfig(ScaleType, status);
				this.scaler = PartDB.Scaler.Create(prefab, this.part, this.ScaleType, this);     // This need to be reworked. I calling this twice. :(

				this.part.OnEditorAttach += OnEditorAttach;
				this.wasOnEditorAttachAdded = true;
			}

            _updaters = TweakScaleUpdater.CreateUpdaters(part).ToArray();
            this.SetupCrewManifest();

			if (!this.isFreeScale && 0 != this.ScaleFactors.Length)
			{
				this.tweakName = Tools.ClosestIndex(tweakScale, ScaleFactors);
				this.tweakScale = ScaleFactors[tweakName];
			}
		}

        internal void RestoreScaleIfNeededAndUpdate()
        {
            if (!IsScaled) return;
            this.RescaleAndUpdate();
            this.NotifyListeners(false);
        }

        internal void ScaleAndUpdate()
        {
			this.scaler.Scale();
            try {
                this.CallUpdaters();
            } catch (Exception exception) {
                Log.error("Exception on ScaleAndUpdate: {0}", exception);
            }
        }

        internal void RescaleAndUpdate()
        {
			Log.dbg("RescaleAndUpdate for {0} : {1}, {2}. {3}", this.part.name, defaultScale, currentScale, tweakScale);
			this.scaler.Restore();
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
				this.DryCost = (float)this.scaler.GetDryCost();
        }

        /// <summary>
        /// Loads settings from <paramref name="scaleType"/>.
        /// </summary>
        /// <param name="scaleType">The settings to use.</param>
        private void SetupFromConfig(ScaleType scaleType, UpgradePipelineStatus status = null)
        {
			Log.dbg("SetupFromConfig for {0} : {1}, {2}. {3}", this.part.name, defaultScale, currentScale, tweakScale);
            if (ScaleType == null) Log.error("Scaletype==null! part={0}", part.name);

            isFreeScale = scaleType.IsFreeScale;
            if (-1 == defaultScale || null != status)
                defaultScale = status?.newDefaultScale??scaleType.DefaultScale;

            if (-1 == currentScale || null != status)
                currentScale = defaultScale;

            if (-1 == tweakScale || null != status)
                tweakScale = status?.newCurrentScale??currentScale;

            this.SetupWidgets(scaleType);
			Log.dbg("SetupFromConfig for {0} : {1}, {2}, {3} FINAL", this.part.name, defaultScale, currentScale, tweakScale);
        }

		private void SetupWidgets(ScaleType scaleType)
		{
			ScaleFactors = scaleType.ScaleFactors;
			this.SetupWidgetsVisibility();
			if (ScaleFactors.Length <= 0) return;

			if (isFreeScale) {
				BaseField field = Fields["tweakScale"];
				UI_ScaleEdit range = (UI_ScaleEdit)field.uiControlEditor;
				range.intervals = scaleType.ScaleFactors;
				range.incrementSlide = scaleType.IncrementSlide;
				range.unit = scaleType.Suffix;
				range.sigFigs = 3;
				field.guiUnits = scaleType.Suffix; 
			} else {
				UI_ChooseOption options = (UI_ChooseOption)Fields["tweakName"].uiControlEditor;
				options.options = scaleType.ScaleNames;
			}
		}

		private void SetupWidgetsVisibility()
		{
			{
				BaseField field = Fields["active"];
				field.guiActiveEditor = this.available;
			}

			Fields["availabilityStatus"].guiActiveEditor = !this.available;

			{
				BaseField field = Fields["tweakScale"];
				field.guiActiveEditor = this.active && this.available && this.isFreeScale;
			}

			{
				BaseField field = Fields["tweakName"];
				field.guiActiveEditor = this.active && this.available && (!this.isFreeScale && this.ScaleFactors.Length > 1);
			}
		}

	#region KSP Event Handlers

        public override void OnLoad(ConfigNode node)
        {
            Log.dbg("OnLoad {0}", this.InstanceID);

            if (null == part.partInfo)
            {
				base.OnLoad(node);
                // Loading of the prefab from the part config
                this.SetupPrefab(part);
            }
            else
            {
				UpgradePipelineStatus status = this.ExecuteMyUpgradePipeline(node);
				base.OnLoad(node);
				this.Setup(part, status);

                // Loading of the part from a saved craft
                tweakScale = currentScale;
				this.RestoreScaleIfNeededAndUpdate();
				this.enabled = this.IsScaled;
            }
        }

        /// <summary>
		/// Ensures new attributes will be added when loading older configs
		/// </summary>
		/// <param name="node"></param>
		[UsedImplicitly]
		private void OnLoadDefaults(ConfigNode node)
		{
			Log.dbg("OnLoadDefaults before {0}", node.ToString());
			if(!node.HasValue("active")) node.AddValue("active", this.active);
			if(!node.HasValue("available")) node.AddValue("available", this.available);
			Log.dbg("OnLoadDefaults after {0}", node.ToString());
		}

		[UsedImplicitly]
        public override void OnSave(ConfigNode node)
        {
            Log.dbg("OnSave {0}", this.InstanceID);

			if (this.is_duplicate)
			{	// Hack to prevent duplicated entries (and duplicated modules) persisting on the craft file
				Log.warn("Part {0} has a Rogue Duplicated TweakScale!", part.name);
				node.SetValue("name", "TweakScaleRogueDuplicate",
					"Programatically tainted due duplicity. Only one single instance above should exist, usually the first one. ",
					false);
				node.SetValue("isEnabled", "false",
					"Programatically disabled",	// In the unlikelly event that someone thinks it's a good idea to create a 
					false);						// `TweakScaleRogueDuplicate` module to screw with me. :)
				this.type = "Rogue";
				base.OnSave(node);
				return;
			}

			// Preventing saving the TweakScale module data on the craft file when TweakScale is not active neither in use for this
			// part.
			//
			// Aims to declutter the craft files, allowing the user to publish unscaled crafts on KerbalX et all without cluttering
			// it with unneeded dependencies.
			//
			// Will also save the user the need to build a TweakScaleless KSP installment to play Challenges where TweakScale is not
			// allowed.
			//
			// Theoretically this could be used too on savegames, but for what? Savegames are not usually shared, and when they do,
			// the one getting it need to have all the add'ons installed anyway. (and mangling savegames without a real need send me
			// shivers down my spine.
			//
			// This can only be applied on KSP >= 1.4, where the Upgrade Pipeline is there to inject back the module on laoding,
			// otherwise the part will get TweakScale permanently ripped off until being removed and a new one attached to the
			// editting craft.
			//
			if (Globals.Instance.AllowStealthSave
					&& UPGRADE_PILELINED_KSP
					&& HighLogic.LoadedSceneIsEditor
					&& !this.IsScaled
					&& this.IsSaveMode()
				)
			{
				Log.detail("Part {0} is being saved without TweakScale as it is not used or active.", this.part.name);

				// Besides aborting the method (what makes de node to be persisted as an empty node called PARTDATA)
				// I'm mangling the node anyway.
				node.ClearData();
				node.comment = "Nothing to see here. Please ignore me.";
				//node.name = ""; // Pushing my luck a bit.
				node.name = "i";// KCT rereads the craft file after saving, but a possible bug prevents it
								// from correctly reading NODES without a name, as it appears.
								// (not necessarily a bug on KCT, the problem appears to be on ConfigNode.CopyToRecursive,
								// but it worths to mention that crafts decluttered by this tool are loaded fine
								// by KSP downto KSP 1.4.0. Perhaps due UpgradePipeline?
				return; // Let's think different... (yep, it worked).
			}

			this.type = this.ScaleType?.Name??"NULL"; // Broken TweakScale modules doesn't have the `this.ScaleType` initialised!

			base.OnSave(node);
		}

		// Prevents mangling ConfigNodes when not saving the thing into a craft file.
		private bool IsSaveMode()
		{
			System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
			foreach (System.Diagnostics.StackFrame frame in st.GetFrames())
			{
				string classname = frame.GetMethod().DeclaringType.Name;
				string methodname = frame.GetMethod().ToString();
				Log.dbg("IsSaveMode {0} {1}", classname, methodname);
				if ("ShipConstruct".Equals(classname) && "ConfigNode SaveShip()".Equals(methodname))
					return true;
			}
			return false;
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
				BaseField field = Fields["active"];
				field.uiControlEditor.onFieldChanged += this.OnActiveFieldChange;
			}
			{
				UI_ScaleEdit ui = (this.Fields["tweakScale"].uiControlEditor as UI_ScaleEdit);
				ui.onFieldChanged += this.OnTweakScaleChanged;
				ui.onFieldChanged += this.OnFieldChange;
			}
			{
				UI_ChooseOption ui = (this.Fields["tweakName"].uiControlEditor as UI_ChooseOption);
				ui.onFieldChanged += this.OnTweakScaleChanged;
				ui.onFieldChanged += this.OnFieldChange;
			}

            if (HighLogic.LoadedSceneIsEditor)
            {
				if (this.scaler.HasCrew)
                {
					GameEvents.onEditorShipModified.Add(this.OnEditorShipModified);
                    this.wasOnEditorShipModifiedAdded = true;
                }
            }

			if (this.IsIVAScalable) this.ScaleIVA();
        }

		public override void OnCopy(PartModule partModule)
		{
			Log.dbg("OnCopy {0}", this.InstanceID);
			base.OnCopy(partModule);
			this._firstUpdateAfterCopy = true;
		}

		[UsedImplicitly]
		private void OnDestroy()
		{
			Log.dbg("OnDestroy {0}", this._InstanceID); // Something bad is happening inside KSP guts before this being called,
														// so I had to cache the InstanceID because the part's data are inconsistent at this point.

			if (this.wasOnEditorAttachAdded) this.part.OnEditorAttach -= this.OnEditorAttach;
			if (this.wasOnEditorShipModifiedAdded) GameEvents.onEditorShipModified.Remove(this.OnEditorShipModified);
			if (null != this.scaler) this.scaler = this.scaler.Destroy();
		}

		private string _getInfo = null;
		public override string GetInfo()
		{
			if (!this.active) return "Disabled.";
			if (!this.available) return "Unavailable for user tweaking.";
			if (null == this._getInfo)
			{
				if (this.ScaleType.IsFreeScale && (1.0f == this.ScaleType.DefaultScale || 100f == this.ScaleType.DefaultScale))
					this._getInfo = string.Format(
								"<b>Scale Type</b> : {0}\n"
							, this.ScaleType.Name
						);
				else
					this._getInfo = string.Format(
								"<b>Scale Type</b> : {0}\n"
								+ "<b>Default Scale</b> : {1}\n"
								+ "<b>Scales</b> : {2}\n"
								+ "<b>{3}\n"
							, this.ScaleType.Name
							, this.ScaleType.DefaultScaleString
							, this.ScaleType.ScaleNamesString
							, this.ScaleType.IsFreeScale ? "Allows Free Scaling" : ""
						);
			}
			return this._getInfo;
		}


		private void OnTweakScaleChanged(BaseField field, object what)
		{
			Log.dbg("OnTweakScaleChanged {0}", this.InstanceID);
			this.OnTweakScaleChanged();
		}

		/// <summary>
		/// Scale has changed!
		/// </summary>
		internal void OnTweakScaleChanged()
		{
			this.HandleTweakScaleChanged();
			foreach (Part p in this.part.symmetryCounterparts)
				p.FindModuleImplementing<TweakScale>().HandleTweakScaleChanged();
		}

		private void HandleTweakScaleChanged()
        {
            if (!isFreeScale)
            {
                tweakScale = ScaleFactors[tweakName];
            }

			this.scaler.SetScale(tweakScale);

			if (Features.ScaleChaining.Enabled)
				Features.ScaleChaining.Execute(this);

            this.ScaleAndUpdate();
            this.MarkWindowDirty();

            currentScale = tweakScale;

            this.NotifyListeners();
        }

		private bool wasOnEditorShipModifiedAdded = false;
		[UsedImplicitly]
		private void OnEditorShipModified(ShipConstruct ship)
		{
			if (!HighLogic.LoadedSceneIsEditor && this.IsScaled) return;
			Log.dbg("OnEditorShipModified {0}", this.InstanceID);

			if (HighLogic.LoadedSceneIsEditor) 
				this.UpdateCrewManifest(); 
		}

		private bool wasOnEditorAttachAdded = false;
		[UsedImplicitly]
		private void OnEditorAttach()
		{
			if (!(HighLogic.LoadedSceneIsEditor && this.IsScaled)) return;
			Log.dbg("OnEditorAttach {0}", this.InstanceID);

            if (null == this.part.parent) return; // This should be impossible, but better safe than sorry...
            TweakScale module = this.part.parent.GetComponent<TweakScale>();
			if (null != module && Features.AutoScale.Enabled)
				Features.AutoScale.Execute(module, this);
		}

        [UsedImplicitly]
        public void Update()
        {
            Log.dbgOnce("Update {0}", this.InstanceID);

            if (_firstUpdate)
            {
                _firstUpdate = false;
                if (this.FailsIntegrity()) return;
				if (this.IsScaled) this.scaler.FirstUpdate();
            }

			if (_firstUpdateAfterCopy)
			{
				this._firstUpdateAfterCopy = false;
				if (this.IsScaled) this.scaler.CopyUpdate();
			}

			// flight scene frequently nukes our OnStart resize some time later
			if(this.IsIVAScalable) this.RestoreIVAScaling();

			// FixMe: This is being called every single Frame. We really need to do it? This wastes CPU cycles...
			this.CallUpdateables();
        }

	#endregion


		private void ResetTweakScale()
		{
			this.tweakScale = this.defaultScale;
			this.OnTweakScaleChanged();
		}

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

        private void NotifyListeners(bool fireShipModified = true)
        {
            // Problem: We don't have the slightest idea if the OnPartScaleChanged was already handled or not.
            // If it didn't, this event may induce Recall to cache the part's resource before he could finish his business.
            // So whoever has received that event, he will need to handle OnPartResourceChanged too after, even by us doing it here.

            Log.dbg("send Resource Changed message to KSP Recall if needed");
			if (0 != this.scaler.part.Resources.Count) this.NotifyPartResourcesChanged();

            Log.dbg("send AttachNodes Changed message to KSP Recall if needed");
			if (0 != this.scaler.part.attachNodes.Count) this.NotifyPartAttachmentNodesChanged();

            this.NotifyPartSurfaceAttachmentChanged(); // This is not working on KSP 1.9, apparently Editor overwrites us before we send the event here!

            Log.dbg("send scaling part message");
            this.NotifyPartScaleChanged();

            Log.dbg("Notify the World we changed the ship ? {0}", fireShipModified);
            if (fireShipModified) GameEvents.onEditorShipModified.Fire(EditorLogic.fetch.ship);
        }

        private void SetupCrewManifest()
        {
            // Restores the original Crew Capacity, as the Pregab is mangled.
			this.scaler.part.CrewCapacity = this.OriginalCrewCapacity;

            VesselCrewManifest vcm = ShipConstruction.ShipManifest;
            if (vcm == null) { return; }
            PartCrewManifest pcm = vcm.GetPartCrewManifest(part.craftID);
            if (pcm == null) { return; }

			if (pcm.partCrew.Length != this.scaler.part.CrewCapacity)
				this.SetCrewManifestSize(pcm, this.scaler.part.CrewCapacity);
        }

        //only run the following block in the editor; it updates the crew-assignment GUI
        private void UpdateCrewManifest()
        {
            Log.dbg("UpdateCrewManifest {0}", this.InstanceID);

#if !CREW_SCALE_UP
            // Small safety guard.
			if (part.CrewCapacity > this.scaler.prefab.CrewCapacity) part.CrewCapacity = this.scaler.prefab.CrewCapacity;
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

                Log.dbg("UpdateCrewManifest current {0}; new {1}; prefab {2}", len, newLen, this.scaler.prefab.CrewCapacity);

                this.scaler.part.CrewCapacity  = newLen;
#if CREW_SCALE_UP
    #if PREFAB_SCALE_HACK
                // Workaround to try to force KSP to accept bigger crew manifests at editting time, as apparently it only respects the prefab's value, bluntly ignoring the part's data!
				this.scaler.prefab.CrewCapacity = Math.Max(this.scaler.prefab.CrewCapacity, this.scaler.part.CrewCapacity);
    #endif
#else
				this.scaler.part.CrewCapacity = Math.Min(this.scaler.part.CrewCapacity, this.scaler.prefab.CrewCapacity);
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
                if (this.scaler.prefab.Modules.Contains("ModuleFuelTanks"))
                {
                    scaleMass = false;
					PartModule m = this.scaler.prefab.Modules["ModuleFuelTanks"];
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
		private static readonly BindingFlags tBindingFlags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
        private void updateTestFlight()
        {
            if (null == tfInterface) return;
            string _name = "scale";
            string value = ScalingFactor.absolute.linear.ToString();
            string owner = "TweakScale";

			bool valueAdded = (bool)tfInterface.InvokeMember("AddInteropValue", tBindingFlags, null, null, new System.Object[] { part, _name, value, owner });
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

		// scale IVA overlay
		protected void ScaleIVA()
		{
			this.savedIvaScale = part.internalModel.transform.localScale * ScalingFactor.absolute.linear;
			this.RestoreIVAScaling();
		}
		protected void RestoreIVAScaling()
		{
			if(this.savedIvaScale.IsZero()) return;
			if(this.savedIvaScale == part.internalModel.transform.localScale) return;

			part.internalModel.transform.localScale = this.savedIvaScale;
			part.internalModel.transform.hasChanged = true;
		}
		protected bool IsIVAScalable => (HighLogic.LoadedSceneIsFlight && (null != this.part.internalModel) && this.IsScaled);

		/// <summary>
		/// Disable TweakScale module if something is wrong.
		/// </summary>
		/// <returns>True if something is wrong, false otherwise.</returns>
		private bool FailsIntegrity()
        {
            if (this != part.Modules.GetModules<TweakScale>().First())
            {
                this.DisableEverything();
                this.is_duplicate = true; // Flags this as not persistent
                Log.warn("Duplicate TweakScale module on part [{0}] {1}", part.partInfo.name, part.partInfo.title);
                return true;
            }
            if (ScaleFactors.Length == 0)
            {
                this.DisableEverything();
                Log.warn("{0}({1}) has no valid scale factors. This is probably caused by an invalid TweakScale configuration for the part.", part.name, part.partInfo.title);
                Log.dbg(this.ToString());
                Log.dbg(ScaleType.ToString());
                return true;
            }
            return false;
        }

		private void DisableEverything()
		{
			enabled = false; // disable TweakScale module
			this.active = false;
			this.available = false;
			Fields["tweakScale"].guiActiveEditor = false;
			Fields["tweakName"].guiActiveEditor = false;
			Fields["active"].guiActiveEditor = false;
			Fields["available"].guiActiveEditor = false;
			this.availabilityStatus = " in error!";
		}

		// Note to myself: struts are passed **BY VALUE** on methods!
		// Objects are passed by reference.
		protected class UpgradePipelineStatus
		{
			internal bool sameScaleType;
			internal bool sameDefaultScale;
			internal TweakScale prefab;

			internal float newDefaultScale;
			internal float newCurrentScale;

			public override string ToString()
			{
				return string.Format("{0}{{sameScaleType:{1}, sameDefaultScale:{2}, newDefaultScale:{3}, newCurrentScale:{4}}}",
						this.GetType().Name,
						sameScaleType, sameDefaultScale,
						newDefaultScale, newCurrentScale
					);
			}
		}

		private UpgradePipelineStatus IsPartMatchesPrefab(KSPe.ConfigNodeWithSteroids node)
		{
			TweakScale prefab = this.part.partInfo.partPrefab.Modules.GetModule<TweakScale>(0);
			UpgradePipelineStatus r = new UpgradePipelineStatus();
			r.prefab = prefab;
			r.sameScaleType = prefab.ScaleType.Name.Equals(this.type);
			{
				float currentDefaultScale = node.GetValue<float>("defaultScale", prefab.defaultScale);
				r.sameDefaultScale = Math.Abs(prefab.defaultScale - currentDefaultScale) < 0.001f;
			}
			return r;
		}

		private ConfigNode FixPartScaling(UpgradePipelineStatus status, ConfigNode source, KSPe.ConfigNodeWithSteroids node)
		{
			Log.dbg("FixPartScaling");

			source.SetValue("type", status.prefab.type);

			status.newDefaultScale = status.prefab.ScaleType.DefaultScale;
			source.SetValue("defaultScale", status.newDefaultScale);

			float craftDefaultScale = node.GetValue<float>("defaultScale", status.newDefaultScale);
			float craftScale = node.GetValue<float>("currentScale", status.newDefaultScale);

			float craftRelativeScale = craftScale / craftDefaultScale;

			string prefabSuffix = status.prefab.ScaleType.Suffix??"";
			Log.dbg("FixPartScaling os using suffix [{0}]", prefabSuffix);
			if ("%".Equals(prefabSuffix) && 100f == status.newDefaultScale)			// Handles percentage scaling scheme
			{
				status.newCurrentScale = 100f * craftRelativeScale;
			}
			else if ("".Equals(prefabSuffix) && 1.0f == status.newDefaultScale)		// Handles normalized scaling scheme
			{
				status.newCurrentScale = craftRelativeScale;
			}
			else if ("m".Equals(prefabSuffix.ToLower()))								// Handles Metric scaling scheme
			{
				status.newCurrentScale = status.newDefaultScale * craftRelativeScale;
			}
			else// Sounds stupid, but sooner or later someone will try to scale things in Imperial Units and I need to change something here. :)
				// TODO: Cook a way to allow customizable Migrations, instead of brute forcing my way on the problem as done here.
			{
				Log.warn("Unrecognized Measuring Unit on scaling scheme for {0} used by {1}.", status.prefab.ScaleType, this.part);
				status.newCurrentScale = status.newDefaultScale * craftRelativeScale;
			}
			source.SetValue("currentScale", status.newCurrentScale);

			Log.warn("Upgrading ScaleType! Craft {0} had the part {1} scaling changed"
						+ " from ({2}: default={3:F3}, current={4:F3})"
						+ " to ({5}: default={6:F3}, current={7:F3})"
					, this.part.craftID, this.InstanceID    // note: this.part.vessel.vesselName is not available yet at this point.
					, this.type, craftDefaultScale, craftScale
					, status.prefab.type, status.newDefaultScale, status.newCurrentScale
				);

			return source;
		}

		private ConfigNode FixPartScalingSameType(UpgradePipelineStatus status, ConfigNode source, KSPe.ConfigNodeWithSteroids node)
		{
			Log.dbg("FixPartScalingSameType");

			status.newDefaultScale = status.prefab.defaultScale;
			float craftDefaultScale = node.GetValue<float>("defaultScale", status.newDefaultScale);
			float craftScale = node.GetValue<float>("currentScale", status.newDefaultScale);
			float craftRelativeScale = craftScale / craftDefaultScale;
			status.newCurrentScale = status.newDefaultScale * craftRelativeScale;

			source.SetValue("currentScale", status.newCurrentScale);
			source.SetValue("defaultScale", status.newDefaultScale);

			Log.warn("Upgrading defaultScale! Craft {0} had the part {1} defaultScale changed from {2:F3} to {3:F3} and was rescaled to {4:F3}"
					, this.part.craftID, this.InstanceID    // note: this.part.vessel.vesselName is not available yet at this point.
					, craftDefaultScale, status.newDefaultScale, status.newCurrentScale
				);
			return source;
		}

		private UpgradePipelineStatus ExecuteMyUpgradePipeline(ConfigNode node)
		{
			Log.dbg("ExecuteMyUpgradePipeline before {0}", node);
			UpgradePipelineStatus r = null;

			// That's the problem - somewhere in the not so near past, KSP implemented a stunt called
			// UpgradePipeline. This thing acts after the PartModule's OnLoad handler, and it injects
			// back default values from prefab into the part on loading. This was intended to allow older
			// savegames to be loaded on newer KSP (as it would inject default values on missing atributes
			// present only on the new KSP version - or to reset new defaults when things changed internally),
			// but also ended up trashing changes and atributes only available on runtime for some custom modules.
			//
			// So we need to check and upgrade things **before** KSP mangles them, otherwise the old values will
			// be trashed by KSP and we will not be able to detect and fix the old data ourselves.
			{
				// ConfigNodeWithSteroids is not complete yet, lots of work to do!
				//
				// So I had to give the source node to be fixed together the fancy one with some nice helpers,
				// as it currently doesn't updates the node used to create it (by design, the idea is to create an
				// "commit" command - so exception handling would be made easier.
				KSPe.ConfigNodeWithSteroids cn = KSPe.ConfigNodeWithSteroids.from(node);

				UpgradePipelineStatus data = this.IsPartMatchesPrefab(cn);
				if (data.sameDefaultScale && data.sameScaleType)    return null;

				if (!data.sameScaleType)                            this.FixPartScaling(data, node, cn);
				if (data.sameScaleType && !data.sameDefaultScale)   this.FixPartScalingSameType(data, node, cn);

				r = data;
			}
			Log.dbg("ExecuteMyUpgradePipeline after {0}", node);
			return r;
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
			float r = this.IsScaled ? this.scaler.ModuleCost : 0;
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
				return this.scaler.prefab.mass * (MassScale - 1f);
            else
              return 0;
        }

        ModifierChangeWhen IPartMassModifier.GetModuleMassChangeWhen()
        {
            return ModifierChangeWhen.FIXED;
        }

		#endregion


		#region Public Interface

		public bool IsScaled => this.active && !this.is_duplicate && (Math.Abs(currentScale / defaultScale - 1f) > 1e-5f);
		public bool IsChanged => this.active && currentScale != (isFreeScale ? tweakScale : ScaleFactors[tweakName]);

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

		public float CurrentScaleFactor => this.scaler.RescaleFactor;

		public void SetState(bool active, bool available)
		{
			Log.stackdump("SetState {0} to active = {1}, available = {2})", this.InstanceID, active, available);
			this.SetStateInternal(active, available);
			foreach (Part p in this.part.symmetryCounterparts)
				p.FindModuleImplementing<TweakScale>().SetStateInternal(active, available);
		}

		// Helper to a 3rd party be able to force a complete rescaling.
		// It's up to the caller to prevent an infinit loop, do not call this inside a TweakScale message handler!
		public void Rescale()
		{
			this.ScaleAndUpdate();
		}

		#endregion

		internal void SetStateInternal(bool active, bool available)
		{
			this.active = active;
			this.enabled = this.IsScaled;
			this.available = available;
			this.SetupWidgetsVisibility();
			if (!this.active) this.ResetTweakScale();
		}

		#region Event Handlers

		private void OnActiveFieldChange(BaseField field, object previous)
		{
			Log.dbg("OnActiveFieldChange {0}:{1:X} from {2} to {3}", field.name, this.part.GetInstanceID(), previous, this.active);
			this.SetState(this.active, this.available);
			if (null != GUI.ToolbarSupport.Instance) GUI.ToolbarSupport.Instance.UpdateIcon(active, available);
		}

		private void OnFieldChange(BaseField field, object previous)
		{
			if (null != GUI.ToolbarSupport.Instance) GUI.ToolbarSupport.Instance.UpdateIcon();
		}

		#endregion


		#region Event Senders

        private void NotifyPartScaleChanged()
        {
            BaseEventDetails data = new BaseEventDetails(BaseEventDetails.Sender.USER);
            data.Set<int>("InstanceID", this.part.GetInstanceID());
            data.Set<Type>("issuer", this.GetType());
            data.Set<float>("factorAbsolute", ScalingFactor.absolute.linear);
            data.Set<float>("factorRelative", ScalingFactor.relative.linear);
            this.part.SendEvent("OnPartScaleChanged", data, 0);
        }

        private void NotifyPartAttachmentNodesChanged()
        {
            BaseEventDetails data = new BaseEventDetails(BaseEventDetails.Sender.USER);
            data.Set<int>("InstanceID", this.part.GetInstanceID());
            data.Set<Type>("issuer", this.GetType());
            this.part.SendEvent("OnPartAttachmentNodesChanged", data, 0);
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
			Log.dbg("prefabCost={0}, dryCost={1}, prefabDryCost={2}", ap.cost, DryCost, (this.scaler.prefab.Modules["TweakScale"] as TweakScale).DryCost);

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
                    Log.stackdump("Enabled set to {0}", value);
                base.enabled = value;
            }
        }
#endif

    }
}
