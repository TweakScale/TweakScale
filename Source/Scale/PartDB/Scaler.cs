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

namespace TweakScale.PartDB
{
	public class Scaler : IDisposable
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
			Type type = KSPe.Util.SystemTools.Type.Find.ByQualifiedName(qualifiedName);
			return (Scaler)Activator.CreateInstance(type, prefab, part, scaleType, ts);
		}

		public virtual double CalculateResourcesCost() => this.prefab.Resources.Cast<PartResource>().Aggregate (0.0, (a, b) => a + b.maxAmount * b.info.unitCost);
		public virtual double CalculateDryCost()
		{
			Log.dbg("CalculateDryCost {0}", null == this.ts ? this.part.name : this.ts.InstanceID);
			double dryCost = (this.part.partInfo.cost - this.CalculateResourcesCost());
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

		public bool enabled => null != this.ts && this.ts.enabled && this.ts.active;
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

		internal void FirstUpdate()		{ Log.dbg("{0}:{1}.FirstUpdate", this.GetType().Name, this.ts.InstanceID);	this.DoFirstUpdate(); }
		internal void CopyUpdate()		{ Log.dbg("{0}:{1}.CopyUpdate", this.GetType().Name, this.ts.InstanceID);	this.DoCopyUpdate(); }
		internal void Scale()			{ Log.dbg("{0}:{1}.Scale", this.GetType().Name, this.ts.InstanceID);		this.DoScale(); }
		internal void Restore()			{ Log.dbg("{0}:{1}.Restore", this.GetType().Name, this.ts.InstanceID);		this.DoRestore(); }
		internal void Clone()			{ Log.dbg("{0}:{1}.OnClone", this.GetType().Name, this.ts.InstanceID);		this.DoClone(); }

		protected void OnChange()		{ Log.dbg("{0}:{1}.OnChange", this.GetType().Name, this.ts.InstanceID);		this.DoOnChange();} 
		protected void OnEditorIn()		{ Log.dbg("{0}:{1}.OnEditorIn", this.GetType().Name, this.ts.InstanceID);	this.DoOnEditorIn(); }
		protected void OnEditorOut()	{ Log.dbg("{0}:{1}.OnEditorOut", this.GetType().Name, this.ts.InstanceID);	this.DoOnEditorOut(); }

		void IDisposable.Dispose()		{ this.DestroyMyself(); }
		protected virtual Scaler DestroyMyself() {
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
		protected virtual void DoScale()		{ }
		protected virtual void DoRestore()		{ }
		protected virtual void DoFirstUpdate()	{ }
		protected virtual void DoCopyUpdate()	{ }
		protected virtual void DoClone()		{ }

		protected virtual void DoOnChange()		{ }
		protected virtual void DoOnEditorIn()	{ }
		protected virtual void DoOnEditorOut()	{ }

		protected virtual void ScalePartModelTransform()		{ Log.dbg("{0}.ScalePartModelTransform", this.GetType().Name); }
		protected virtual void ScaleDragCubes(bool absolute)	{ Log.dbg("{0}.ScalePartModelTransform", this.GetType().Name); }
		protected virtual void RescaleDragCubes()				{ Log.dbg("{0}.RescaleDragCubes", this.GetType().Name); }
		protected virtual void MoveSurfaceAttachment(bool moveParts, bool absolute)	{ Log.dbg("{0}.MoveSurfaceAttachment", this.GetType().Name);  }
		protected virtual void MoveAttachmentNodes(bool moveParts, bool absolute)	{ Log.dbg("{0}.MoveAttachmentNodes {0} {1}", this.GetType().Name, moveParts, absolute); }
		protected virtual void MoveSurfaceAttachedParts()	{ Log.dbg("{0}.MoveSurfaceAttachedParts", this.GetType().Name); }

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
