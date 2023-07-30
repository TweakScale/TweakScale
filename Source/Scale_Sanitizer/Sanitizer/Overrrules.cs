/*
	This file is part of TweakScale /L
		© 2018-2023 LisiasT

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

namespace TweakScale.Sanitizer
{
	internal class Overrules : Abstract
	{
		private int failures = 0;
		private int count = 0;

		public override Priority Priority => Priority.Low;
		public override int Ocurrences => this.count;
		public override int Unscalable => 0;
		public override int Failures => this.failures;
		public override string Summary => string.Format("{0} parts with issues overruled", this.count);
		public override bool HasRules => true;

		protected override bool DoCheck(AvailablePart p, Part prefab)
		{
			try
			{
				string r = null;

				// This one is for my patches that "break things again" in a controlled way to salvage already running savegames
				// that would be lost by fixing things right. Sometimes, it's possible to keep the badly patched parts ongoing, as
				// as the nastiness will not crash KSP (besides still corrupting savegames and craft files in a way that would not
				// allow the user to share them).
				// Since we are overruling the checks, we abort the remaining ones. Yes, this allows abuse, but whatever... I can't
				// save the World, just the savegames. :)
				if (null != (r = this.CheckForOverrules(prefab)))
				{   // This is for detect and log the Breaking Parts patches.
					// See issue [#56]( https://github.com/TweakScale/TweakScale/issues/56 ) for details.
					// This is **FAR** from a good measure, but it's the only viable.
					Log.warn("Part {0} ({1}) has the issue(s) overrule(s) {2}. See [#56]( https://github.com/TweakScale/TweakScale/issues/56 ) for details.", p.name, p.title, r);
					++this.count;
					return true; // Abort the check chain for this part.
				}
			}
			catch (Exception e)
			{
				++this.failures;
				Log.error("part={0} ({1}) Exception on Sanity Checks: {2}", p.name, p.title, e);
			}
			return false; // Do not abort check chain.
		}

		public override bool EmmitMessageIfNeeded(bool showMessageBox)
		{
			bool r = this.count > 0;
			if (r) GUI.OverrulledAdviseBox.Show(this.count, showMessageBox);
			return r;
		}

		private string CheckForOverrules(Part p)
		{
			Log.dbg("Checking Issue Overrule for {0} at {1}", p.name, p.partInfo.partUrl);
			ConfigNode part = Abstract.GetMeThatConfigNode(p);
			if (null == part) return null; // Let the this.checkForShowStoppers do the job.

			{
				foreach (ConfigNode basket in part.GetNodes("MODULE"))
				{
					if ("TweakScale" != basket.GetValue("name")) continue;
					if (basket.HasValue("ISSUE_OVERRULE"))
						return basket.GetValue("ISSUE_OVERRULE");
				}
			}

			return null;
		}
	}
}