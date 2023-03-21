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
using System.Text;

using UnityEngine;
using KSPe.UI;

namespace TweakScale.GUI
{
	internal class MissingCompanionAdviseBox : CommonBox
	{
		private static readonly string MSG = @"Supported 3rd parties were found, but the respective Companion weren't.

Installing such companions will bring you the full power of TweakScale, consider installing the following Companions:

{0}
Alternatively, you may want to install the ÜberPaket with everything and the kitchen's sink included!";

		internal static void Show(string[] companions)
		{
			GameObject go = new GameObject("TweakScale.GUI.MissingCompanionAdviseBox");
			TimedMessageBox dlg = go.AddComponent<TimedMessageBox>();

			GUIStyle win = createWinStyle(Color.white);
			GUIStyle text = createTextStyle();

			StringBuilder sb = new StringBuilder();
			foreach (string s in companions)
				sb.Append(string.Format("* {0} \n", s));
			string msg = sb.ToString();

			if (ModuleManagerListener.shouldShowWarnings)
				dlg.Show(
					"TweakScale advises",
					string.Format(MSG, msg),
					30, 0, 0,
					win, text
				);
			Log.force("\"TweakScale Advise\" was {0} suggesting to install the following Companions {1}:", ModuleManagerListener.shouldShowWarnings ? "displayed" : "supressed",  string.Join(", ", companions));
		}
	}
}
