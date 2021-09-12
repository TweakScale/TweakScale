﻿/*
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
using System.Linq;
using UnityEngine;

namespace TweakScale.PartDB
{
	internal class VariantPartScaler : StandardPartScaler
	{
		private PartVariant previousVariant;
		private PartVariant currentVariant;

		public VariantPartScaler(Part prefab, Part part, ScaleType scaleType, TweakScale ts) : base(prefab, part, scaleType, ts)
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

		protected override Scaler DestroyYourself()
		{
			GameEventEditorVariantAppliedListener.Instance.Remove(this);
			return base.DestroyYourself();
		}

		protected override double CalculateDryCost()
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

				if (0 != this.part.symmetryCounterparts.Count)
					this.MovePartSymetry(node);
				else
					this.MovePart(node);
			}
		}

		protected void MovePart(AttachNode node)
		{
			AttachNode[] currentNodesWithSameId = this.FindNodesWithSameId(node);									// The node was scaled correctly, we can use the node as is
			AttachNode[] previousBaseNodesWithSameId = this.FindBaseNodesWithSameId(node, this.previousVariant);	// This is where the part was
			AttachNode[] currentBaseNodesWithSameId = this.FindBaseNodesWithSameId(node, this.currentVariant);		// This is where the part should be
			AttachNode[] attachedPartNode = this.FindAttachingNode(this.part, node.attachedPart);

			if (currentNodesWithSameId.Length > 0 && previousBaseNodesWithSameId.Length > 0 && currentBaseNodesWithSameId.Length > 0)
			{
				Vector3 currentPosition = this.part.partTransform.InverseTransformPoint(node.attachedPart.partTransform.position);	// Where we are
				Vector3 desiredPosition = currentNodesWithSameId[0].position;														// Where we should be
				Vector3 deltaPos = desiredPosition - currentPosition - attachedPartNode[0].position;

				bool isAttachedParent = node.attachedPart == this.part.parent;
				if (isAttachedParent) {
					deltaPos = -deltaPos + this.part.attPos;
					this.part.transform.Translate(deltaPos, this.part.parent.transform);
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

		protected void MovePartSymetry(AttachNode node)
		{

		}

		private AttachNode[] FindAttachingNode(Part part, Part attachedPart)
		{
			AttachNode [] attachingNodes = attachedPart.attachNodes
				.Where(a => a.attachedPart == part)
				.ToArray();

			return attachingNodes;
		}
	}
}