/*
	This file is part of TweakScale Watch Dog
		© 2024 Lisias T : http://lisias.net <support@lisias.net>

	TweakScale Watch Dog is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	TweakScale Watchdog is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with Module Manager Watch Dog. If not, see
	<https://ksp.lisias.net/SKL-1_0.txt>.

*/
using System;
using UnityEngine;
using KSPe.UI;

namespace KSPe.Common.Dialogs
{
	internal class ShowStopperAlertBox : AbstractDialog
	{
		private static readonly string aMSG = "close KSP and then fix the problem described above";

		private static readonly string MSG = @"{0}

This is a Show Stopper problem. Your best line of action is to click the OK button to {1}.

If you choose to ignore this message and click Cancel to proceed, be advised that your savegames can get corrupted at any time, even when things appear to work by now - and the salvage can be harder.

Backup everything <b>*NOW*</b> if you choose to ignore this message and proceed.

Your KSP is running from:
<b>{2}</b>.";

		internal static void Show(string errorMessage)
		{
			Show(errorMessage, aMSG, Application.Quit);
		}

		internal static void Show(string errorMessage, string actionMessage, Action lambda)
		{
			GameObject go = new GameObject("KSPe.Common.Diallgs.ShowStopperAlertBox");
			MessageBox dlg = go.AddComponent<MessageBox>();

			//GUIStyle win = new GUIStyle(HighLogic.Skin.window)
			GUIStyle win = new GUIStyle("Window")
			{
				fontSize = 26,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.UpperCenter,
				richText = true,
				wordWrap = false
			};
			win.normal.textColor = Color.red;
			win.border.top = 0;
			win.padding.top = -5;
			SetWindowBackground(win);
			win.active.background =	win.focused.background = win.normal.background;

			GUIStyle text = new GUIStyle("Label")
			{
				fontSize = 18,
				fontStyle = FontStyle.Normal,
				alignment = TextAnchor.MiddleLeft,
				richText = true,
				wordWrap = true
			};
			text.normal.textColor = Color.white;
			text.padding.top = 8;
			text.padding.bottom = text.padding.top;
			text.padding.left = text.padding.top;
			text.padding.right = text.padding.top;
			SetTextBackground(text);

			dlg.Show(
				"Houston, we have a Problem!",
				String.Format(MSG, errorMessage, actionMessage, AppRoot()),
				lambda,
				win, text,
				false
			);
		}

		private static string AppRoot()
		{
			string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
			location = location.Substring(0, location.IndexOf("GameData"));
			location = System.IO.Path.GetFullPath(location);
			return location;
		}
	}
}
