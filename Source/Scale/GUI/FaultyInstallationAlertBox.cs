/*
	This file is part of TweakScale™ /L
		© 2018-2024 LisiasT
		© 2015-2018 pellinor
		© 2014 Gaius Godspeed and Biotronic

	TweakScale™ /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	TweakScale™ /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with TweakScale™ /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with TweakScale™ /L. If not, see <https://www.gnu.org/licenses/>.
*/
using UnityEngine;

namespace TweakScale.GUI
{
	internal static class FaultyInstallationAlertBox
	{
		private static readonly string MSG = @"TweakScale™ is not installed correctly! Some of its required data files files are missing.

There is no safe way to proceed; without these files, TweakScale™ will not work properly!

Reason reported: {0}";

		private static readonly string AMSG = @"close KSP, then reinstall TweakScale™";

		internal static void Show(string msg) {
			KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
				string.Format(MSG, msg),
				AMSG,
				() => { Application.Quit(); }
			);
			Log.force("\"Houston, we have a problem!\" about missing data files was displayed. Reason reported: {0}", msg);
		}
	}
}
