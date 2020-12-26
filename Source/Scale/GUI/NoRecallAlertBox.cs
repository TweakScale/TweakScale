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
    internal static class NoRecallAlertBox
    {
        private static readonly string MSG = @"KSP Recall was not found!

On KSP 1.9.x KSP Recall **is needed** to fix problems on Resources (not only on TweakScale, it only happens that TweakScale is the first known victim of the problem).";

        private static readonly string AMSG = @"download KSP Recall from the Forum's page (KSP will close)";

        internal static void Show()
        {
            KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
                MSG,
                AMSG,
                () => { Application.OpenURL("https://forum.kerbalspaceprogram.com/index.php?/topic/192048-*"); Application.Quit(); }
            );
            Log.force("\"Houston, we have a Problem!\" about KSP-Recall was displayed");
        }
    }
}