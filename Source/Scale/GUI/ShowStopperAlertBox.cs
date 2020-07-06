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
using System;
using UnityEngine;
using KSPe;

namespace TweakScale.GUI
{
    internal static class ShowStopperAlertBox
    {
        private static readonly string MSG = @"Unfortunately TweakScale found {0} **FATAL** issue(s) on your KSP installment! This probably will corrupt your savagames sooner or later!

The KSP.log is listing every compromised part(s) on your installment, look for lines with '[TweakScale] ERROR: **FATAL**' on the log line. Be aware that the parts being reported are not the culprits, but the Screaming Victims. There's no possible automated fix for these.";

        private static readonly string AMSG = @"call for help on the TweakScale thread on the Forum (KSP will close). We will help you on diagnosing the Add'On that is troubling you. Publish your KSP.log on some file share service and mention it on the post";

        internal static void Show(int failure_count)
        {
            KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
                string.Format(MSG, failure_count),
                AMSG,
                () => { Application.OpenURL("https://forum.kerbalspaceprogram.com/index.php?/topic/179030-*"); Application.Quit(); }
            );
            Log.force("\"Houston, we have a Problem!\" about show stoppers on patching was displayed");
        }
    }
}