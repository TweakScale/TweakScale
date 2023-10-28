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
using UnityEngine;

namespace TweakScale.PartDB
{
	public class StandardPartScaler : Scaler
	{
		public StandardPartScaler(Part prefab, Part part, ScaleType scaleType, TweakScale ts) : base(prefab, part, scaleType, ts)
		{
		}

		protected override void DoScale()
		{
			base.DoScale();
			this.ScalePart(true, true);
			this.ScaleDragCubes(false);
			this.OnChange();
		}

		protected override void DoRestore()
		{
			this.ScalePart(true, true);
			this.OnChange();
		}

		protected override void DoFirstUpdate()
		{
			base.DoFirstUpdate();
			this.ScaleDragCubes(true);
			if (HighLogic.LoadedSceneIsEditor)							// cloned parts and loaded crafts seem to need this (otherwise the node positions revert)
				this.ScalePart(false, true);							// This was originally shoved on Update() for KSP 1.2 on commit 09d7744
		}

		protected override void DoClone()
		{
			base.DoClone();
			Log.dbg("orgPos, attPos, attPos0 {0} {1} {2}", this.part.orgPos, this.part.attPos, this.part.attPos0);
			Log.dbg("orgRot, attRotation, attRotation0 {0} {1} {2}", this.part.orgRot, this.part.attRotation, this.part.attRotation0);
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
			Log.dbg("StandardPartScaler.MoveNode {0} {1} {2}", node.id, baseNode.id, movePart, absolute);

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
			Log.dbg("StandardPartScaler.MovePart {0} {1} {2}", deltaPos, node.id, linearScale);

			if (node.attachedPart == this.part.parent)
			{
				this.part.transform.Translate(-deltaPos, this.part.transform);
			}
			else
			{
				Vector3 oldAttPos = node.attachedPart.attPos;
				node.attachedPart.attPos *= linearScale;

				Vector3 offset = node.attachedPart.attPos - oldAttPos;
				node.attachedPart.transform.Translate(deltaPos + offset, this.part.transform);
			}
		}

		/// <summary>
		/// Change the size of <paramref name="node"/> to reflect the new size of the part it's attached to.
		/// </summary>
		/// <param name="node">The node to resize.</param>
		/// <param name="baseNode">The same node, as found on the prefab part.</param>
		protected void ScaleAttachNode(AttachNode node, AttachNode baseNode)
		{
			Log.dbg("StandardPartScaler.ScaleAttachNode {0} {1}", node.id, baseNode.id);

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
			base.MoveAttachmentNodes(moveParts, absolute);

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
			Log.dbg("StandardPartScaler.FindNodesWithSameId {0}", node.id);

			AttachNode[] nodesWithSameId = this.part.attachNodes
				.Where(a => a.id == node.id)
				.ToArray();

			return nodesWithSameId;
		}

		protected virtual AttachNode[] FindBaseNodesWithSameId(AttachNode node)
		{
			Log.dbg("StandardPartScaler.FindBaseNodesWithSameId {0}", node.id);

			AttachNode[] baseNodesWithSameId = this.prefab.attachNodes
				.Where(a => a.id == node.id)
				.ToArray();

			return baseNodesWithSameId;
		}

		protected override void MoveSurfaceAttachment(bool moveParts, bool absolute)
		{
			base.MoveSurfaceAttachment(moveParts, absolute);

			if (null != this.part.srfAttachNode)
				this.MoveNode(this.part.srfAttachNode, this.prefab.srfAttachNode, moveParts, absolute);
		}

		protected override void MoveSurfaceAttachedParts()
		{
			base.MoveSurfaceAttachedParts();

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
			base.ScalePartModelTransform();

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
			base.ScaleDragCubes(absolute);
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
			base.RescaleDragCubes();

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

		protected static string InstanceID(Part p) => string.Format("{0}:{1:X}", p.name, p.GetInstanceID());
		protected string InstanceID() => null == this.ts ? this.part.name : this.ts.InstanceID;
	}

}
