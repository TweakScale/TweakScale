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

namespace TweakScale.WatchDog
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	internal class Startup : MonoBehaviour
	{
		private void Start()
		{
			Log.force("Version {0}", TweakScale.WatchDog.Version.Text);

			if (!TweakScale.Version.Text.Contains("BETA") && Util.CkanTools.CheckCkanInstalled() && Util.CkanTools.CheckCkanRepository())
				//GUI.Dialogs.UnsuitedForCkanErrorBox.Show();
				Log.force("**YOUR ATTENTION PLEASE!** This TweakScale™ packaging is not meant to be used under CKAN. It's usually a bad idea to do manual installings when using CKAN, futurelly this may break things for you! Please CKAN to install TweakScale™ on CKAN managed rigs.");
		}
	}
}
