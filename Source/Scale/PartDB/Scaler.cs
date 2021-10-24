/*
		This file is part of TweakScale /L
		© 2018-2021 LisiasT
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
using System.Collections.Generic;
using System.Linq;

namespace TweakScale.PartDB
{
	public class Scaler
	{
		public readonly Part part;
		protected TweakScale ts;
		protected float currentScale;
		protected float previousScale;
		protected readonly float defaultScale;

		/// <summary>
		/// The node scale array. If node scales are defined the nodes will be resized to these values.
		///</summary>
		protected readonly int[] ScaleNodes = { };

		/// <summary>
		/// The unmodified prefab part. From this, default values are found.
		/// </summary>
		public readonly Part prefab;

		/// <summary>
		/// Initialises the PartDB thingy.
		/// </summary>
		/// <param name="prefab"></param> the prefab data fetch from the KSP's OnLoad(null) call
		/// <param name="part"></param> the part 
		/// <param name="scaleType"></param> the default scaleType
		/// <param name="ts"></param> the TweakScale object that own us. **MUST BE NULL** when starting up KSP (Loading Scene).
		protected Scaler(Part prefab, Part part, ScaleType scaleType, TweakScale ts)
		{
			this.prefab = prefab;
			this.part = part;
			this.ScaleNodes = scaleType.ScaleNodes;
			this.ts = ts;
			this.defaultScale = this.currentScale = this.previousScale = scaleType.DefaultScale;
			if (null != this.ts) this.currentScale = this.previousScale = this.ts.currentScale;

			if (HighLogic.LoadedSceneIsEditor) this.OnEditorIn();
			GameEventGameSceneSwitchListener.Instance.Add(this);
		}

		internal static Scaler Create(Part prefab, Part part, ScaleType scaleType, TweakScale ts = null)
		{
			bool hasVariants = prefab.Modules.Contains("ModulePartVariants");
			return null == ts
				? new Scaler(prefab, part, scaleType, ts)
				: hasVariants
					? FindAndCreate("VariantPartScaler", prefab, part, scaleType, ts)
					: FindAndCreate("StandardPartScaler", prefab, part, scaleType, ts)
			;
		}

		private static Scaler FindAndCreate(string name, Part prefab, Part part, ScaleType scaleType, TweakScale ts) {
			string qualifiedName = "TweakScale.PartDB." + name;
			Type type = KSPe.Util.SystemTools.TypeFinder.FindByQualifiedName(qualifiedName);
			return (Scaler)Activator.CreateInstance(type, prefab, part, scaleType, ts);
		}

		internal double GetDryCost() { return this.CalculateDryCost(); }
		protected virtual double CalculateDryCost()
		{
			Log.dbg("CalculateDryCost {0}", null == this.ts ? this.part.name : this.ts.InstanceID);
			double dryCost = (this.part.partInfo.cost - this.prefab.Resources.Cast<PartResource>().Aggregate (0.0, (a, b) => a + b.maxAmount * b.info.unitCost));
			Log.dbg("CalculateDryCost {0} {1}", null == this.ts ? this.part.name : this.ts.InstanceID, dryCost);
			if (dryCost < 0) {
				dryCost = 0;
				Log.error("CalculateDryCost: negative dryCost: part={0}, DryCost={1}", null == this.ts ? this.part.name : this.ts.InstanceID, dryCost);
			}
			return dryCost;
		}

		internal bool HasCrew
		{
			get
			{
				return this.prefab.CrewCapacity > 0;
			}
		}

		public bool enabled => null != this.ts && this.ts.enabled;
		public bool IsMine(Part part) => null != part && this.part.GetInstanceID() == part.GetInstanceID();

		public float RescaleFactor => this.part.rescaleFactor / this.prefab.rescaleFactor;

		public float ModuleCost
		{
			get
			{
				Log.dbg("Get Module Cost {0}", this.ts.InstanceID);
				double r = this.ts.DryCost - this.part.partInfo.cost;
				Log.dbg("Module Cost without resources {0} {1}", this.ts.InstanceID, r);
				r += this.ts.ignoreResourcesForCost
					? 0.0
					: this.part.Resources.Cast<PartResource>().Aggregate(0.0, (a, b) => a + b.maxAmount * b.info.unitCost)
				;
				Log.dbg("Module Cost *WITH* resources {0} {1}", this.ts.InstanceID, r);
				return (float)r;
			}
		}

		internal float SetScale(float newScale)
		{
			float r = this.previousScale;
			this.previousScale = this.currentScale;
			this.currentScale = newScale;
			return r;
		}

		internal virtual void FirstUpdate()
		{
			Log.dbg("FirstUpdate for {0}", this.ts.InstanceID);
			this.ScaleDragCubes(true);
			if (HighLogic.LoadedSceneIsEditor)								// cloned parts and loaded crafts seem to need this (otherwise the node positions revert)
				this.FirstScale();
		}

		protected virtual void FirstScale()
		{
			this.ScalePart(false, true);							// This was originally shoved on Update() for KSP 1.2 on commit 09d7744
		}

		internal void Scale()
		{
			Log.dbg("Scale for {0}", this.ts.InstanceID);
			this.ScalePart(true, false);
			this.ScaleDragCubes(false);
			this.OnChange();
		}

		internal void Restore()
		{
			Log.dbg("Restore for {0}", this.ts.InstanceID);
			this.ScalePart(true, true);
			this.OnChange();
		}

		internal Scaler Destroy() { return this.DestroyYourself(); }
		protected virtual Scaler DestroyYourself() {
			Log.dbg("{0}.Destroy {1} ", this.GetType().Name, (null!=this.ts) ? this.ts.InstanceID : "<no TweakScale instance>");
			GameEventGameSceneSwitchListener.Instance.Remove(this);
			return null;
		}

		internal void OnGameSceneSwitchRequested(GameEvents.FromToAction<GameScenes, GameScenes> action)
		{
			if (GameScenes.EDITOR == action.to) this.OnEditorIn();
			if (GameScenes.EDITOR == action.from) this.OnEditorOut();
		}

		//
		// None of these makes any sense for Prefab!
		//
		protected virtual void OnChange() { } 
		protected virtual void ScalePartModelTransform() { }
		protected virtual void ScaleDragCubes(bool absolute) { }
		protected virtual void RescaleDragCubes() { }
		protected virtual void MoveSurfaceAttachment(bool moveParts, bool absolute) { }
		protected virtual void MoveAttachmentNodes(bool moveParts, bool absolute) { }
		protected virtual void MoveSurfaceAttachedParts() { }
		protected virtual void OnEditorIn() { }
		protected virtual void OnEditorOut() { }

		/// <summary>
		/// Updates properties that change linearly with scale.
		/// </summary>
		/// <param name="moveParts">Whether or not to move attached parts.</param>
		/// <param name="absolute">Whether to use absolute or relative scaling.</param>
		protected void ScalePart(bool moveParts, bool absolute)
		{
			this.ScalePartModelTransform();
			this.MoveSurfaceAttachment(moveParts, absolute);
			this.MoveAttachmentNodes(moveParts, absolute);
			this.MoveSurfaceAttachedParts();
		}
	}

}
