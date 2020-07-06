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
using KSPe.UI;

namespace TweakScale.GUI
{
    internal class CheckFailureAlertBox : CommonBox
    { 
        private static readonly string MSG = @"TweakScale found {0} parts that broke the checking process! See KSP.log for details.

This does not means that the part(s) has(have) a TweakScale problem, it(they) can be alright to be scaled. But since TweakScale cannot know for sure, it's a concern.

This usually happens due Third Parties Add'On, a DLC getting into the way or some really botched patches - ending up borking the check and sometimes breaking something else.

Please report, we are working hard to fix these problems.";

        internal static void show(int check_failures)
        {
            GameObject go = new GameObject("TweakScale.WarningBox");
            TimedMessageBox dlg = go.AddComponent<TimedMessageBox>();
            
            GUIStyle win = createWinStyle(Color.yellow);
            GUIStyle text = createTextStyle();
            
            if (ModuleManagerListener.shouldShowWarnings)
                dlg.Show(
                    "TweakScale Warning", 
                    String.Format(MSG, check_failures),
                    30, 1, 1,
                    win, text
                );
            Log.force("\"TweakScale Warning\" about check failures was {0}", ModuleManagerListener.shouldShowWarnings ? "displayed" : "suppressed");
        }
    }
}