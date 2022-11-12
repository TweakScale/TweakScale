/*
	This file is part of TweakScale /L
		© 2018-2022 LisiasT
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
using UnityEngine;
using KSPe.UI;

namespace TweakScale.GUI
{
	internal class UnsupportedKSPAdviseBox : CommonBox
	{
		private static readonly string MSG = @"TweakScale has not been confirmed to work correctly on KSP {0} or later!

It is NOT KNOWN whether it will work correctly.

If you choose to continue running KSP, your saves may be unrecoverably corrupted, even if it seems to be working. Make back-ups now! You may find the S.A.V.E mod helpful for this.";

		internal static void Show(string currentVersion)
		{
			GameObject go = new GameObject("TweakScale.AdviseBox");
			TimedMessageBox dlg = go.AddComponent<TimedMessageBox>();

			GUIStyle win = createWinStyle(Color.white);
			GUIStyle text = createTextStyle();

			dlg.Show(
				"TweakScale advises",
				string.Format(MSG, currentVersion),
				30, 0, 0,
				win, text
			);
			Log.force("\"TweakScale advises\" about KSP was displayed.");
		}
	}
}
