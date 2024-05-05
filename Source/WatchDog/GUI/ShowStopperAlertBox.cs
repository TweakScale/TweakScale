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
using UnityEngine;

namespace TweakScale.WatchDog.GUI.Dialogs
{
	internal static class ShowStopperAlertBox
	{
		private const string URL = "https://ksp.lisias.net/add-ons/TweakScale/WatchDog/KNOWN_ISSUES";
		private static readonly string AMSG = @"to get instructions about how to Download and Install Module Manager.";

		internal static void Show(string msg)
		{
			KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
				msg,
				AMSG,
				() => { Application.OpenURL(URL); Application.Quit(); }
			);
			Log.detail("\"Houston, we have a Problem!\" was displayed about : {0}", msg);
		}
	}
}
