/*
	This file is part of TweakScale /L
		© 2018-2022 LisiasT

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
	internal class HotFixes : Abstract
	{
		private int failures = 0;
		private int count = 0;

		public override Priority Priority => Priority.Low;
		public override int Ocurrences => this.count;
		public override int Unscalable => 0;
		public override int Failures => this.failures;
		public override string Summary => string.Format("{0} parts with hotfixes", this.count);
		public override bool HasRules => true;

		protected override bool DoCheck(AvailablePart p, Part prefab)
		{
			try
			{
				string r = null;

				if (null != (r = this.CheckForHotFixes(prefab)))
				{   // Warns about hot-fixes
					// Hot fixes are not that bad as overrules, but they are brute force solutions for specific problems,
					// and cam bit if the environment changes - as a new installed add-on being written off.
					Log.warn("Part {0} ({1}) has a hot-fix. See link {2} for details.", p.name, p.title, r);
					++this.count;
					return true; // Abort the check chain for this part.
				}
			}
			catch (Exception e)
			{
				++failures;
				Log.error("part={0} ({1}) Exception on Sanity Checks: {2}", p.name, p.title, e);
			}
			return false; // Do not abort check chain.
		}

		public override bool EmmitMessageIfNeeded(bool showMessageBox) {
			bool r = this.count > 0;
			if (r) GUI.HotFixAdviseBox.Show(this.count, showMessageBox);
			return r;
		}

		private string CheckForHotFixes(Part p) {
			Log.dbg("Checking Hotfixes for {0} at {1}", p.name, p.partInfo.partUrl);
			ConfigNode part = Abstract.GetMeThatConfigNode(p);
			if (null == part) return null; // Let the this.checkForShowStoppers do the job.

			{
				foreach (ConfigNode basket in part.GetNodes("MODULE"))
				{
					if ("TweakScale" != basket.GetValue("name")) continue;
					if (basket.HasValue("HOTFIX"))
						return System.Uri.UnescapeDataString(basket.GetValue("HOTFIX"));
				}
			}

			return null;
		}

	}
}
