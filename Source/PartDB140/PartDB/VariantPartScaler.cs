﻿/*
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

		internal PartVariant SetVariant(PartVariant partVariant)
		{
			Log.dbg("VariantPartScaler.SetVariant from {0} to {1}", this.previousVariant.DisplayName, partVariant.DisplayName);

			PartVariant r = this.previousVariant;
			this.previousVariant = this.currentVariant;
			this.currentVariant = partVariant;
			return r;
		}

		protected override Scaler DestroyYourself()
		{
			GameEventEditorVariantAppliedListener.Instance.Remove(this);
			return base.DestroyYourself();
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
	}
}
