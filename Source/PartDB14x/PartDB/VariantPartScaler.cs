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

namespace TweakScale.PartDB
{
	internal partial class VariantPartScaler : StandardPartScaler
	{
		private PartVariant previousVariant;
		private PartVariant currentVariant;

		public VariantPartScaler(Part prefab, Part part, ScaleType scaleType, TweakScale ts) : base(prefab, part, scaleType, ts)
		{
			this.previousVariant = this.currentVariant = part.variants.SelectedVariant;
		}

		protected override void DoRestore()
		{
			// This fixes (again) https://github.com/TweakScale/TweakScale/issues/307
			// To tell you the true, it's really only needed when the root part is a VARIANT one,
			// but anyway...
			this.ScalePart(true, !HighLogic.LoadedSceneIsEditor);
			this.OnChange();
		}

		protected override void DoClone()
		{
			this.ScalePart(true, !HighLogic.LoadedSceneIsEditor);
			Log.dbg("orgPos, attPos, attPos0 {0} {1} {2}", this.part.orgPos, this.part.attPos, this.part.attPos0);
			Log.dbg("orgRot, attRotation, attRotation0 {0} {1} {2}", this.part.orgRot, this.part.attRotation, this.part.attRotation0);
		}

		internal PartVariant SetVariant(PartVariant partVariant)
		{
			Log.dbg("VariantPartScaler.SetVariant from {0} to {1}", this.previousVariant.DisplayName, partVariant.DisplayName);

			PartVariant r = this.previousVariant;
			this.previousVariant = this.currentVariant;
			this.currentVariant = partVariant;
			return r;
		}

		protected override Scaler DestroyMyself()
		{
			GameEventEditorVariantAppliedListener.Instance.Remove(this);
			return base.DestroyMyself();
		}

		protected override void DoOnEditorIn()
		{
			GameEventEditorVariantAppliedListener.Instance.Add(this);
		}

		protected override void DoOnEditorOut()
		{
			GameEventEditorVariantAppliedListener.Instance.Remove(this);
		}

		internal void OnEditorVariantApplied(Part part, PartVariant partVariant)
		{
			if(!this.ts.IsScaled) return;
			Log.dbg("VariantPartScaler.OnEditorVariantApplied {0} {1}", this.ts.InstanceID, partVariant.Name);
			this.SetVariant(partVariant);

			// Rescale everything, as new variants may have different resources definitions, etc.
			// I will trust (or hope) that any changes made by the Variant is already applied.
			this.ts.Rescale();
		}
	}
}
