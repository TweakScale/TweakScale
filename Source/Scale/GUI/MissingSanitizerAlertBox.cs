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
	internal static class MissingSanitizerAlertBox
	{
		private static readonly string MSG = @"THIS TWEAKSCALE™ INSTALLATION IS UNSUPPORTED!

Scale_Sanitizer, a DLL TweakScale™ needs to check for known problems and prevent disastrous consequences in your rig, was not found. This means that TweakScale™ can't check if it's safe to run!

Proceed at your own risk. TweakScale™'s maintainer will <B>NOT</B> accept bug reports, neither will help diagnosing problems without it.";

		private static readonly string AMSG = @"fully reinstall TweakScale™";

		internal static void Show() {
			if (ModuleManagerListener.shouldShowWarnings)
				KSPe.Common.Dialogs.ShowStopperErrorBox.Show(
					MSG,
					AMSG,
					() => { Application.Quit(); }
				);
			Log.force("\"Houston, we have a problem!\" about missing Scale_Sanitizer was {0}", ModuleManagerListener.shouldShowWarnings ? "displayed" : "suppressed");
		}
	}
}