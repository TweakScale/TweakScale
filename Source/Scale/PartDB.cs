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
using System.Collections.Generic;
using System.Linq;
using TweakScale.Annotations;
using UnityEngine;

namespace TweakScale
{
	public class PartDB
	{
		public readonly Part part;
		protected TweakScale ts;
		protected readonly bool IsOnKSP19 = KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.FindByVersion(1,9,0);
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

		protected PartDB(Part prefab, Part part, ScaleType scaleType, TweakScale ts)
		{
			this.prefab = prefab;
			this.part = part;
			this.ScaleNodes = scaleType.ScaleNodes;
			this.ts = ts;
			this.defaultScale = this.currentScale = this.previousScale = scaleType.DefaultScale;
			if (null != this.ts) this.currentScale = this.previousScale = this.ts.currentScale;
		}
		internal static PartDB Create(Part prefab, Part part, ScaleType scaleType, TweakScale ts = null)
		{
			bool hasVariants = prefab.Modules.Contains("ModulePartVariants");
			return null == ts
				? new PartDB(prefab, part, scaleType, ts)
				: hasVariants ? (PartDB)new VariantPartScaler(prefab, part, scaleType, ts) : (PartDB)new StandardPartScaler(prefab, part, scaleType, ts)
			;
		}

		internal virtual double CalculateDryCost()
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

		internal bool enabled => null != this.ts && this.ts.enabled;
		internal bool IsMine(Part part) => null != part && this.part.GetInstanceID() == part.GetInstanceID();

		public float RescaleFactor => this.part.rescaleFactor / this.prefab.rescaleFactor;

		public float ModuleCost
		{
			get
			{
				double r = this.ts.DryCost - this.part.partInfo.cost;
				Log.dbg("Module Cost without resources {0} {1}", this.ts.InstanceID, r);
				r += this.ts.ignoreResourcesForCost
					? 0.0
					: part.Resources.Cast<PartResource>().Aggregate(0.0, (a, b) => a + b.maxAmount * b.info.unitCost)
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

		internal void FirstUpdate()
		{
			Log.dbg("FirstUpdate for {0}", this.ts.InstanceID);
			this.ScaleDragCubes(true);
			if (HighLogic.LoadedSceneIsEditor)						// cloned parts and loaded crafts seem to need this (otherwise the node positions revert)
				if (this.IsOnKSP19) this.FirstScalePartKSP19();		// This is needed by (surprisingly!) KSP 1.9
				else this.ScalePart(false, true);					// This was originally shoved on Update() for KSP 1.2 on commit 09d7744
		}

		internal void Scale()
		{
			Log.dbg("Scale for {0}", this.ts.InstanceID);
			this.ScalePart(true, false);
			this.ScaleDragCubes(false);
			this.OnChange();
		}

		internal void Rescale()
		{
			Log.dbg("Rescale for {0}", this.ts.InstanceID);
			this.ScalePart(true, true);
			this.ScaleDragCubes(true); // I'm unsure if I should enable this. FIXME: TEST, TEST, TEST!
			this.OnChange();
		}

		internal virtual PartDB Destroy() {
			Log.dbg("{0}.Destroy {1} ", this.GetType().Name, this.ts.InstanceID);
			return null;
		}

		//
		// None of these makes any sense for Prefab!
		//
		protected virtual void OnChange() { } 
		protected virtual void FirstScalePartKSP19() { }
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
		private void ScalePart(bool moveParts, bool absolute)
		{
			this.ScalePartModelTransform();
			this.MoveSurfaceAttachment(moveParts, absolute);
			this.MoveAttachmentNodes(moveParts, absolute);
			this.MoveSurfaceAttachedParts();
		}
	}

	internal class StandardPartScaler : PartDB
	{
		internal StandardPartScaler(Part prefab, Part part, ScaleType scaleType, TweakScale ts) : base(prefab, part, scaleType, ts)
		{
			if (HighLogic.LoadedSceneIsEditor) this.OnEditorIn();
			GameEventGameSceneSwitchListener.Instance.Add(this);
		}

		internal void OnGameSceneSwitchRequested(GameEvents.FromToAction<GameScenes, GameScenes> action)
		{
			if (GameScenes.EDITOR == action.to) this.OnEditorIn();
			if (GameScenes.EDITOR == action.from) this.OnEditorOut();
		}

		internal override PartDB Destroy() {
			GameEventGameSceneSwitchListener.Instance.Remove(this);
			return base.Destroy();
		}

		protected override void FirstScalePartKSP19()
		{
			this.ScalePartModelTransform();
			this.MoveSurfaceAttachment(false, true);
			this.MoveAttachmentNodes(false, true);
		}

		/// <summary>
		/// Moves <paramref name="node"/> to reflect the new scale. If <paramref name="movePart"/> is true, also moves attached parts.
		/// </summary>
		/// <param name="node">The node to move.</param>
		/// <param name="baseNode">The same node, as found on the prefab part.</param>
		/// <param name="movePart">Whether or not to move attached parts.</param>
		/// <param name="absolute">Whether to use absolute or relative scaling.</param>
		protected void MoveNode(AttachNode node, AttachNode baseNode, bool movePart, bool absolute)
		{
			if (baseNode == null)
			{
				baseNode = node;
				absolute = false;
			}

			Vector3 oldPosition = node.position;

			node.position = absolute
				? baseNode.position * this.ts.ScalingFactor.absolute.linear
				: node.position * this.ts.ScalingFactor.relative.linear
			;

			Vector3 deltaPos = node.position - oldPosition;
			if (movePart && null != node.attachedPart)
				this.MovePart(deltaPos
					, node
					, absolute ? this.ts.ScalingFactor.absolute.linear : this.ts.ScalingFactor.relative.linear
				);

			this.ScaleAttachNode(node, baseNode);
		}

		protected void MovePart(Vector3 deltaPos, AttachNode node, float linearScale)
		{
			if (node.attachedPart == this.part.parent)
			{
				this.part.transform.Translate(-deltaPos, this.part.transform);
			}
			else
			{
				Vector3 oldAttPos = node.attachedPart.attPos;
				node.attachedPart.attPos *= linearScale;

				Vector3 offset = node.attachedPart.attPos - oldAttPos;
				node.attachedPart.transform.Translate(deltaPos + offset, node.attachedPart.transform);
			}
		}

		/// <summary>
		/// Change the size of <paramref name="node"/> to reflect the new size of the part it's attached to.
		/// </summary>
		/// <param name="node">The node to resize.</param>
		/// <param name="baseNode">The same node, as found on the prefab part.</param>
		protected void ScaleAttachNode(AttachNode node, AttachNode baseNode)
		{
			if (this.ts.isFreeScale || this.ScaleNodes == null || this.ScaleNodes.Length == 0)
			{
				float tmpBaseNodeSize = baseNode.size;
				if (tmpBaseNodeSize == 0)
				{
					tmpBaseNodeSize = 0.5f;
				}
				node.size = (int)(tmpBaseNodeSize * this.ts.tweakScale / this.ts.defaultScale + 0.49);
			}
			else
			{
				node.size = baseNode.size + (1 * this.ScaleNodes[this.ts.tweakName]);
			}

			if (node.size < 0)
			{
				node.size = 0;
			}
		}

		protected override void MoveAttachmentNodes(bool moveParts, bool absolute)
		{
			int len = this.part.attachNodes.Count;
			for (int i = 0; i < len; i++) {
				AttachNode node = this.part.attachNodes[i];

				AttachNode[] nodesWithSameId = this.FindNodesWithSameId(node);
				AttachNode[] baseNodesWithSameId = this.FindBaseNodesWithSameId(node);

				// FIXME: This is really necessary? The AttachNode arrays is (almost) surelly 1 entry only...
				int idIdx = Array.FindIndex(nodesWithSameId, a => a == node);

				if (idIdx < baseNodesWithSameId.Length) {
					AttachNode baseNode = baseNodesWithSameId[idIdx];
					this.MoveNode(node, baseNode, moveParts, absolute);
				} else {
					Log.warn("Error scaling part. Node {0} does not have counterpart in base part.", node.id);
				}
			}
		}

		protected AttachNode[] FindNodesWithSameId(AttachNode node)
		{
			AttachNode[] nodesWithSameId = this.part.attachNodes
				.Where(a => a.id == node.id)
				.ToArray();

			return nodesWithSameId;
		}

		protected virtual AttachNode[] FindBaseNodesWithSameId(AttachNode node)
		{
			AttachNode[] baseNodesWithSameId = this.prefab.attachNodes
				.Where(a => a.id == node.id)
				.ToArray();

			return baseNodesWithSameId;
		}

		protected override void MoveSurfaceAttachment(bool moveParts, bool absolute)
		{
			if (null != this.part.srfAttachNode)
				this.MoveNode(this.part.srfAttachNode, this.prefab.srfAttachNode, moveParts, absolute);
		}

		protected override void MoveSurfaceAttachedParts()
		{
			int numChilds = this.part.children.Count;
			for (int i = 0; i < numChilds; i++)
			{
				Part child = this.part.children [i];
				if (child.srfAttachNode == null || child.srfAttachNode.attachedPart != part)
					continue;

				Vector3 attachedPosition = child.transform.localPosition + child.transform.localRotation * child.srfAttachNode.position;
				Vector3 targetPosition = attachedPosition * this.ts.ScalingFactor.relative.linear;
				child.transform.Translate(targetPosition - attachedPosition, this.part.transform);
			}
		}

		protected override void ScalePartModelTransform()
		{
			this.part.rescaleFactor = this.prefab.rescaleFactor * this.ts.ScalingFactor.absolute.linear;

			Transform trafo = this.part.partTransform.Find("model");
			if (trafo != null)
			{
				if (this.ts.defaultTransformScale.x == 0.0f)
				{
					this.ts.defaultTransformScale = trafo.localScale;
				}

				// check for flipped signs
				if (this.ts.defaultTransformScale.x * trafo.localScale.x < 0)
				{
					this.ts.defaultTransformScale.x *= -1;
				}
				if (this.ts.defaultTransformScale.y * trafo.localScale.y < 0)
				{
					this.ts.defaultTransformScale.y *= -1;
				}
				if (this.ts.defaultTransformScale.z * trafo.localScale.z < 0)
				{
					this.ts.defaultTransformScale.z *= -1;
				}

				trafo.localScale = this.ts.ScalingFactor.absolute.linear * this.ts.defaultTransformScale;
				trafo.hasChanged = true;
				this.part.partTransform.hasChanged = true;
			}
		}

		/**
		 * Used to scale the Drag Curves.
		 * 
		 * Use absolute = true when you are sure KSP had reset the DragCurves to the default and need to scale them backl.
		 */
		protected override void ScaleDragCubes(bool absolute)
		{
			ScalingFactor.FactorSet factor = absolute
				? this.ts.ScalingFactor.absolute
				: this.ts.ScalingFactor.relative
			;

			if (factor.linear == 1)
				return;

			int len = this.part.DragCubes.Cubes.Count;
			for (int ic = 0; ic < len; ic++)
			{
				DragCube dragCube = this.part.DragCubes.Cubes[ic];
				dragCube.Size *= factor.linear;
				for (int i = 0; i < dragCube.Area.Length; i++)
					dragCube.Area[i] *= factor.quadratic;

				for (int i = 0; i < dragCube.Depth.Length; i++)
					dragCube.Depth[i] *= factor.linear;
			}
			this.part.DragCubes.ForceUpdate(true, true);
		}

		/**
		 * Used to reset and scale the Drag Curves.
		 * 
		 * To be used when you don't know the state of the Drag Curves, but yet needs to be sure to have them scaled to the current factor
		 */
		protected override void RescaleDragCubes()
		{
			ScalingFactor.FactorSet factor = this.ts.ScalingFactor.absolute; // Rescaling it's always absolute!

			int len = this.part.DragCubes.Cubes.Count;

			for (int i = 0; i < len; ++i) {
				DragCube part = this.part.DragCubes.Cubes[i];
				DragCube prefab = this.prefab.DragCubes.Cubes[i];
				part.Size = prefab.Size;

				for (int j = 0; j < part.Area.Length; ++j)
					part.Area[j] = prefab.Area[j];

				for (int j = 0; j < part.Depth.Length; ++i)
					part.Depth[j] = prefab.Depth[j];
			}

			if (factor.linear == 1) return;

			for (int ic = 0; ic < len; ic++)
			{
				DragCube dragCube = this.part.DragCubes.Cubes[ic];
				dragCube.Size *= factor.linear;
				for (int i = 0; i < dragCube.Area.Length; i++)
					dragCube.Area[i] *= factor.quadratic;

				for (int i = 0; i < dragCube.Depth.Length; i++)
					dragCube.Depth[i] *= factor.linear;
			}

			this.part.DragCubes.ForceUpdate(true, true);
		}

		protected override void OnEditorIn() { Log.dbg("{0}:{1} OnEditorIn", this.GetType().Name, this.ts.InstanceID); }
		protected override void OnEditorOut() { Log.dbg("{0}:{1} OnEditorOut", this.GetType().Name, this.ts.InstanceID); }
	}

	internal class VariantPartScaler : StandardPartScaler
	{
		private PartVariant previousVariant;
		private PartVariant currentVariant;

		internal VariantPartScaler(Part prefab, Part part, ScaleType scaleType, TweakScale ts) : base(prefab, part, scaleType, ts)
		{
			this.previousVariant = this.currentVariant = part.variants.SelectedVariant;
		}

		internal PartVariant SetVariant(PartVariant partVariant)
		{
			PartVariant r = this.previousVariant;
			this.previousVariant = this.currentVariant;
			this.currentVariant = partVariant;
			return r;
		}

		private void ReCalculateCostAndMass()
		{
			float costFactor = (float)this.ts.DryCostFactor;
			float massFactor = (float)this.ts.MassFactor;

			foreach (PartVariant p in this.part.variants.variantList)
			{
				PartVariant prefab = this.prefab.variants.variantList[this.prefab.variants.GetVariantIndex(p.Name)];
				p.Cost = prefab.Cost * costFactor;
				p.Mass = prefab.Mass * massFactor;
			}
		}

		internal override PartDB Destroy()
		{
			GameEventEditorVariantAppliedListener.Instance.Remove(this);
			return base.Destroy();
		}

		protected override void FirstScalePartKSP19()
		{
			this.ScalePartModelTransform();
			this.MoveSurfaceAttachment(false, true);
			this.MoveAttachmentNodes(false, true);
		}

		internal override double CalculateDryCost()
		{
			double dryCost = this.currentVariant.Cost + base.CalculateDryCost();
			Log.dbg("CalculateDryCostWithVariant {0} {1}", null == this.ts ? this.part.name : this.ts.InstanceID, dryCost);

			if (dryCost < 0) {
				dryCost = 0;
				Log.error("CalculateDryCostWithVariant: negative dryCost: part={0}, DryCost={1}", null == this.ts ? this.part.name : this.ts.InstanceID, dryCost);
			}
			return dryCost;
		}

		protected override void OnEditorIn()
		{
			base.OnEditorIn();
			GameEventEditorVariantAppliedListener.Instance.Add(this);
		}

		protected override void OnEditorOut()
		{
			GameEventEditorVariantAppliedListener.Instance.Remove(this);
			base.OnEditorOut();
		}

		protected override void OnChange()
		{
			base.OnChange();
			this.ReCalculateCostAndMass();
		}

		internal void OnEditorVariantApplied(Part part, PartVariant partVariant)
		{
			Log.dbg("OnEditorVariantApplied {0} {1}", this.ts.InstanceID, partVariant.Name);
			this.SetVariant(partVariant);
			if (!this.ts.IsScaled) return;

			// When an variant is applied, all the attachment nodes are replaced by "vanilla" ones
			// So we need to rescale them using absolute scales.
			this.MoveAttachmentNodes(false, true);

			// And since the super's Move code doesn't works right when you are "scaling back" a part, we need to
			// "fix" them now
			this.MoveParts();

			this.ScaleDragCubes(true); // I'm unsure if I should enable this. FIXME: TEST, TEST, TEST!
			this.OnChange();
		}

		protected override AttachNode[] FindBaseNodesWithSameId(AttachNode node)
		{
			return this.FindBaseNodesWithSameId(node, this.currentVariant);
		}

		protected AttachNode[] FindBaseNodesWithSameId(AttachNode node, PartVariant variant)
		{
			AttachNode [] baseNodesWithSameId = this.prefab.variants.variantList[this.prefab.variants.GetVariantIndex(variant.Name)].AttachNodes
				.Where(a => a.id == node.id)
				.ToArray();

			if (0 == baseNodesWithSameId.Length)
				baseNodesWithSameId = this.prefab.attachNodes
					.Where(a => a.id == node.id)
					.ToArray();

			return baseNodesWithSameId;
		}

		protected void MoveParts()
		{
			int len = this.part.attachNodes.Count;
			for (int i = 0; i < len; i++) {
				AttachNode node = this.part.attachNodes[i];

				if (null == node.attachedPart)
				{
					Log.dbg("{0}'s node {1} has not attached part.", this.part.name, node.id);
					continue;
				}

				AttachNode[] currentNodesWithSameId = this.FindNodesWithSameId(node);									// The node was scaled correctly, we can use the node as is
				AttachNode[] previousBaseNodesWithSameId = this.FindBaseNodesWithSameId(node, this.previousVariant);	// This is where the part was
				AttachNode[] currentBaseNodesWithSameId = this.FindBaseNodesWithSameId(node, this.currentVariant);		// This is where the part should be

				if (currentNodesWithSameId.Length > 0 && previousBaseNodesWithSameId.Length > 0 && currentBaseNodesWithSameId.Length > 0)
				{
					Vector3 currentPosition = this.part.partTransform.InverseTransformPoint(node.attachedPart.partTransform.position);	// Where we are
					Vector3 desiredPosition = currentNodesWithSameId[0].position;														// Where we should be
					Vector3 deltaPos = desiredPosition - currentPosition;

					bool isAttachedParent = node.attachedPart == this.part.parent;
					if (isAttachedParent) {
						deltaPos = -deltaPos;
						this.part.transform.Translate(deltaPos, this.part.transform);
					} else {
						node.attachedPart.transform.Translate(deltaPos, node.attachedPart.transform);
					}

					Log.dbg("Moving {0}'s node {1} attached part {2}{3} from {4} to {5} by {6}."
						, this.part.name, node.id, node.attachedPart.name
						, isAttachedParent ? " those attachment is his parent" : ""
						, currentPosition, desiredPosition, deltaPos);
				} else
					Log.error("Error moving part on Variant. Node {0} does not have counterpart in part variants {1} and/or {2}.", node.id, this.previousVariant.Name, this.currentVariant.Name);
			}
		}
	}

	internal class GameEventGameSceneSwitchListener : MonoBehaviour
	{
		private static GameEventGameSceneSwitchListener instance;
		internal static GameEventGameSceneSwitchListener Instance { get {
			if (null != instance) return instance;
			GameObject ob = new GameObject();
			instance = ob.AddComponent<GameEventGameSceneSwitchListener>();
			return instance;
		} }

		private readonly HashSet<StandardPartScaler> listeners = new HashSet<StandardPartScaler>();

		internal void Add(StandardPartScaler listener)
		{
			this.listeners.Add(listener);
		}

		internal void Remove(StandardPartScaler listener)
		{
			if (this.listeners.Contains(listener)) this.listeners.Remove(listener);
		}

		[UsedImplicitly]
		private void Awake()
		{
			GameEvents.onGameSceneSwitchRequested.Add(this.GameSceneSwitchHandler);
		}

		[UsedImplicitly]
		private void Destroy()
		{
			GameEvents.onGameSceneSwitchRequested.Remove(this.GameSceneSwitchHandler);
			this.listeners.Clear();
		}

		[UsedImplicitly]
		private void GameSceneSwitchHandler(GameEvents.FromToAction<GameScenes, GameScenes> action)
		{
			foreach (StandardPartScaler ps in this.listeners) if (ps.enabled)
				ps.OnGameSceneSwitchRequested(action);
		}
	}

	internal class GameEventEditorVariantAppliedListener : MonoBehaviour
	{
		private static GameEventEditorVariantAppliedListener instance;
		internal static GameEventEditorVariantAppliedListener Instance { get {
			if (null != instance) return instance;
			GameObject ob = new GameObject();
			instance = ob.AddComponent<GameEventEditorVariantAppliedListener>();
			return instance;
		} }

		private readonly HashSet<VariantPartScaler> listeners = new HashSet<VariantPartScaler>();

		internal void Add(VariantPartScaler listener)
		{
			this.listeners.Add(listener);
		}

		internal void Remove(VariantPartScaler listener)
		{
			if (this.listeners.Contains(listener)) this.listeners.Remove(listener);
		}

		[UsedImplicitly]
		private void Awake()
		{
			GameEvents.onEditorVariantApplied.Add(this.EditorVariantAppliedHandler);
		}

		[UsedImplicitly]
		private void Destroy()
		{
			GameEvents.onEditorVariantApplied.Remove(this.EditorVariantAppliedHandler);
			this.listeners.Clear();
		}

		[UsedImplicitly]
		internal void EditorVariantAppliedHandler(Part part, PartVariant partVariant)
		{
			foreach (VariantPartScaler ps in this.listeners) if (ps.enabled && ps.IsMine(part))
				ps.OnEditorVariantApplied(part, partVariant);
		}
	}

}
