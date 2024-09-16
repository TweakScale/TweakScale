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
	internal static class UnsuitedForCkanErrorBox
	{
		private const string URL = "https://ksp.lisias.netblogs/tech-support/CKAN/";
		private static readonly string MSG = @"Oh, dear! You downloaded TweakScale™ from the wrong place!

This version of TweakScale is not compatible with CKAN due changes on the GameData management. You need to download it from SpaceDock or from the KSP-CKAN mirror on Archive.org!

Please download it from: https://spacedock.info/mod/127/TweakScale or <a href=""https://archive.org/details/kspckanmods?tab=collection&query=TweakScale+Everything&sort=-publicdate\"">Archive.org</a>";

		private static readonly string AMSG = @"close KSP, download TweakScale™ from SpaceDock and then reinstall it";

		internal static void Show() {
			KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
				string.Format(MSG, MSG),
				AMSG,
				() => { Application.OpenURL(URL); Application.Quit(); }
			);
			Log.force("\"Houston, we have a problem!\" about this package being unsuit for being used on CKAN was displayed");
		}
	}
}
