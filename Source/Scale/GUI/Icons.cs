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
using KSPe.Annotations;

using UnityEngine;
using H = KSPe.IO.Hierarchy<TweakScale.Startup>;
using T = KSPe.Util.Image.Texture2D;

namespace TweakScale.GUI
{
	internal static class Icons
	{
		private const string ICONSDIR = "Icons";
		private static Texture2D _ScaleOff = null;
		internal static Texture2D ScaleOff = _ScaleOff ?? (_ScaleOff = T.LoadFromFile(H.GAMEDATA.Solve("PluginData", ICONSDIR, "Scale_Off")));

		private static Texture2D _ScaleOn = null;
		internal static Texture2D ScaleOn = _ScaleOn ?? (_ScaleOn = T.LoadFromFile(H.GAMEDATA.Solve("PluginData", ICONSDIR, "Scale_On")));

		private static Texture2D _ScaleAuto = null;
		internal static Texture2D ScaleAuto = _ScaleAuto ?? (_ScaleAuto = T.LoadFromFile(H.GAMEDATA.Solve("PluginData", ICONSDIR, "Scale_Auto")));

		private static Texture2D _ScaleUnsupported = null;
		internal static Texture2D ScaleUnsupported = _ScaleUnsupported ?? (_ScaleUnsupported = T.LoadFromFile(H.GAMEDATA.Solve("PluginData", ICONSDIR, "Scale_Unsupported")));
	}

	[KSPAddon(KSPAddon.Startup.SpaceCentre, true)]
	internal class IconPreloader : MonoBehaviour
	{
		[UsedImplicitly]
		private void Start()
		{   // Preload the icons on Space Center to avoid halting the Editor at first entry.
			Log.force("BLAH");
			Icons.ScaleOn.GetInstanceID();
			Icons.ScaleOff.GetInstanceID();
			Icons.ScaleAuto.GetInstanceID();
			Icons.ScaleUnsupported.GetInstanceID();
		}
	}
}
