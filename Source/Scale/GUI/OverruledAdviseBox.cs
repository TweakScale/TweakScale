﻿/*
	This file is part of TweakScale /L
		© 2018-2024 LisiasT
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
using System;
using UnityEngine;
using KSPe.UI;

namespace TweakScale.GUI
{
	internal class OverruledAdviseBox : CommonBox
	{
		private static readonly string MSG = @"TweakScale has applied overrules to {0} parts.

These parts have known problems with TweakScale, so TweakScale has applied patches to fix them.

Do not start a new save or share crafts, as overruled parts make your vessels non-standard and unsafe to share. Use them only to keep exising saves going.";

		internal static void show(int overrule_count)
		{
			GameObject go = new GameObject("TweakScale.AdviseBox");
			TimedMessageBox dlg = go.AddComponent<TimedMessageBox>();

			GUIStyle win = createWinStyle(Color.white);
			GUIStyle text = createTextStyle();

			if (ModuleManagerListener.shouldShowWarnings)
				dlg.Show(
					"TweakScale advises",
					String.Format(MSG, overrule_count),
					30, 0, -1,
					win, text
				);
			Log.force("\"TweakScale advises\" about overrules was {0}", ModuleManagerListener.shouldShowWarnings ? "displayed" : "suppressed");
		}
	}
}
