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
	along with TweakScale /L. If not, see <https://www.gnu.org/licenses/>.
*/

namespace TweakScale.PartDB
{
	internal partial class VariantPartScaler : StandardPartScaler
	{
		private readonly bool HasKSP19bug = KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.FindByVersion(1,9,0);

		protected override void DoFirstUpdate()
		{
			base.DoFirstUpdate();
			if (this.HasKSP19bug) this.FirstScalePartKSP19();			// This is needed by (surprisingly!) KSP 1.9
			else this.ScalePart(true, true);							// This was originally shoved on Update() for KSP 1.2 on commit 09d7744
		}

		private void FirstScalePartKSP19()
		{
			Log.dbg("VariantPartScaler.FirstScalePartKSP19");

			this.ScalePartModelTransform();
			this.MoveSurfaceAttachment(true, true);
			this.MoveAttachmentNodes(false, true);
		}
	}
}
