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
using System.Collections.Generic;
using System.Text;
using System.Linq;

using UnityEngine;

namespace TweakScale.GUI
{
	internal static class DeprecatedCompanionFatalError 
	{
		private const string URL = "https://github.com/TweakScale/Companion/releases";

		private static readonly string MSG = @"Deprecated Companion(s) was(were) found!

The following Companion(s) **NEED** to be removed, having this(these) oldies lingering around is detrimental to KSP's health:

{0}
Alternatively, you may want to remove everything under `GameData/TweakScaleCompanion` and install the lastest ÜberPaket with everything and the kitchen's sink included!";

		private static readonly string AMSG = @"close KSP, remove and then reinstall the mentioned Companion(s)";

		internal static void Show(Dictionary<string,string> companions) {
			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<string, string> p in companions)
				sb.Append(string.Format("* {0} on {1}\n", p.Key, p.Value));
			string msg = sb.ToString();
			KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
				string.Format(MSG, msg),
				AMSG,
				() => { KSPe.Util.UrlTools.OpenURL(URL); }
			);
			Log.force("\"Houston, we have a problem!\" about the deprecated Companions found {0}:", string.Join(", ", companions.Keys.ToArray()));
		}
	}
}
