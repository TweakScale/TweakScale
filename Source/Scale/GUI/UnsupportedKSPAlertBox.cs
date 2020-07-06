/*
		This file is part of TweakScale /L
		© 2018-2020 LisiasT
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
using UnityEngine;
using System;
using KSPe;

namespace TweakScale.GUI
{
    internal static class UnsupportedKSPAlertBox
    {
        private static readonly string MSG = @"Unfortunately TweakScale is currently not known to work correctly on KSP {0} (and newer)!

It's not certain that it will not work fine, it's **NOT KNOWN** and if anything goes wrong, KSP will inject bad information on your savegames corrupting parts with TwekScale.";

        private static readonly string AMSG = @"check the GitHub issue #{1} (KSP will close) to be updated on any news about KSP {0}";

        internal static void Show(string kspVersion, string issueNumber)
        {
            KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
                string.Format(MSG, kspVersion, issueNumber),
                AMSG,
                () => { Application.OpenURL(string.Format("https://github.com/net-lisias-ksp/TweakScale/issues/{0}", issueNumber)); Application.Quit(); }
            );
            Log.force("\"Houston, we have a Problem!\" about KSP {0} was displayed", kspVersion);
        }
    }
}