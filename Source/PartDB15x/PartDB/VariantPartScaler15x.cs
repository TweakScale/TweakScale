/*
	This file is part of TweakScale /L
		© 2018-2024 LisiasT
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
using System.Collections;
using System.Linq;
using UnityEngine;

namespace TweakScale.PartDB
{
	internal partial class VariantPartScaler : StandardPartScaler
	{
		private void ReCalculateCostAndMass()
		{
			Log.dbg("VariantPartScaler.ReCalculateCostAndMass");

			float costFactor = (float)this.ts.DryCostFactor;
			float massFactor = (float)this.ts.MassFactor;

			foreach (PartVariant p in this.part.variants.variantList)
			{
				PartVariant prefab = this.prefab.variants.variantList[this.prefab.variants.GetVariantIndex(p.Name)];
				p.Cost = prefab.Cost * costFactor;
				p.Mass = prefab.Mass * massFactor;
			}
		}

		protected override double CalculateDryCost()
		{
			Log.dbg("VariantPartScaler.CalculateDryCost");

			double dryCost = this.currentVariant.Cost + base.CalculateDryCost();
			Log.dbg("CalculateDryCostWithVariant {0} {1}", this.InstanceID(), dryCost);

			if (dryCost < 0) {
				dryCost = 0;
				Log.error("CalculateDryCostWithVariant: negative dryCost: part={0}, DryCost={1}", this.InstanceID(), dryCost);
			}
			return dryCost;
		}

		protected override void OnChange()
		{
			base.OnChange();
			this.ReCalculateCostAndMass();
		}

		internal void OnEditorVariantApplied(Part part, PartVariant partVariant)
		{
			if(!this.ts.IsScaled) return;
			Log.dbg("VariantPartScaler.OnEditorVariantApplied {0} {1}", this.ts.InstanceID, partVariant.Name);
			this.SetVariant(partVariant);

			this.ts.StartCoroutine(this.DelayedRescale());
		}

		private IEnumerator DelayedRescale ()
		{
			int i = 2;
			while (--i > 0) yield return null;

		// Note to my future self!
		//
		// This is not a fix, is a hell of a clutch to get rid of the #307 problem on the legacy branch
		// https://github.com/TweakScale/TweakScale/issues/307
		//
		// The fact is that the legacy branch (this one) still have a lot of gambiarras que are bitting my arse badly. The beta
		// branch was refactored for a reason.
		//
		// Since this worked on beta, it's a proper fix, and not working here is due the clutches that still linger. Porting the beta
		// gradually as I was doing while #307 wasn't fixed is not feasible on the long run, and it is not viable anymore on the short.
		// So this clutch end up being the less worst of the short term fixes I'm able to do now to push 2.4.8.0 ahead and focus
		// on finishing Beta and promoting it to mainstream.
		//
		// The consequences of this clutch is that variants that change resouces and other physical characteristics will not be properly
		// handled, and some features may be left unscaled after the variant is applyed. Eventually a full Scaling will be aplied on
		// the craft (by undo, by saving and loading, or by launching) so this is not a show stopper, but it will be annoying for sure.
		//
		// Corolarry: I need to promote beta to the mainstream ASAP.
		//
			// Rescale everything, as new variants may have different resources definitions, etc.
			// I will trust (or hope) that any changes made by the Variant is already applied.
			//this.ts.Rescale();

			this.MoveAttachmentNodes(false, true);
			yield break;
		}


	}
}
