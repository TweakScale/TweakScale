/*
	This file is part of TweakScale™ /L
		© 2018-2024 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

	TweakScale™ /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	TweakScale™ /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with TweakScale™ /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with TweakScale™ /L. If not, see <https://www.gnu.org/licenses/>.
*/
using System.Text;

using UnityEngine;

namespace TweakScale.GUI
{
	internal static class MissingCompanionFatalError 
	{
		private const string URL = "https://github.com/TweakScale/Companion/releases";
		private static readonly string MSG = @"Supported 3rd parties were found, but the respective Companion™ weren't.

The following Companions™ **NEED** to be installed, as the targets Add'Ons they support are known to play havoc with TweakScale™ and they fix or workaroud the known problems:

{0}
Alternatively, you may want to install the ÜberPaket™ with everything and the kitchen's sink included!";

		private static readonly string AMSG = @"close KSP, then install the Companion(s)";

		internal static void Show(string[] companions) {
			StringBuilder sb = new StringBuilder();
			foreach (string s in companions)
				sb.Append(string.Format("* {0} \n", s));
			string msg = sb.ToString();
			KSPe.Common.Dialogs.ShowStopperErrorBox.Show(
				string.Format(MSG, msg),
				AMSG,
				() => { KSPe.Util.UrlTools.OpenURL(URL); }
			);
			Log.force("\"Houston, we have a problem!\" about the need to install the following Companions™ {0}:", string.Join(", ", companions));
		}
	}
}
