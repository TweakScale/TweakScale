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

namespace TweakScale.GUI
{
    internal static class ShowStopperAlertBox
    {
        private const string MSG = @"TweakScale found {0} **FATAL** issue(s) with your KSP install! This will probably corrupt your saves!

Your KSP.log lists every problematic part in your install; look for lines containing '[TweakScale] ERROR: **FATAL**'. Note that these parts are not the culprits, but innocent victims. No automated fix is possible for these problems.";

        private const string AMSG = @"close KSP, then ask for help with diagnosing the problem mod on the TweakScale forum thread. Please upload your KSP.log to a file share service and share a link to it in your post";

        internal static void Show(int failure_count)
        {
            KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
                string.Format(MSG, failure_count),
                AMSG,
                () => { Application.OpenURL("https://forum.kerbalspaceprogram.com/index.php?/topic/179030-*"); Application.Quit(); }
            );
            Log.force("\"Houston, we have a problem!\" about show stoppers on patching was displayed");
        }
    }
}