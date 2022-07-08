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
using System.Collections.Generic;
using System.Linq;

namespace TweakScale.Sanitizer
{
	internal abstract class AbstractCheck : Abstract
	{
		private int failures = 0;
		private int count = 0;

		private readonly Priority priority;
		public override Priority Priority => this.priority;
		public override int Ocurrences => this.count;
		public override int Unscalable => 0;
		public override int Failures => this.failures;
		public override string Summary => string.Format("{0} {1} checks failed", this.count, this.Priority.ToString());

		protected AbstractCheck(Priority priority)
		{
			this.priority = priority;
		}

		protected override bool DoCheck(AvailablePart p, Part prefab)
		{
			string r;
			try
			{
				List<Engine.Check.Result> checksFailed = this.CheckIntegrity(p, prefab);
				r = string.Join("; ", checksFailed.Select(s => s.ToProblems()).ToArray<string>());
				Log.error("Part {0} ({1}) didn't passed the sanity check due {2}.", p.name, p.title, r);
				return false;	// Non Show Stoppers checks are not meant to halt the process chain. They are warnings.
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
			if (r) GUI.SanityCheckAlertBox.Show(this.count, showMessageBox);
			return r;
		}

		private List<Engine.Check.Result> CheckIntegrity(AvailablePart p, Part prefab)
		{
			List<Engine.Check.Result> checksFailed = new List<Engine.Check.Result>();
			{
				List<Engine.Check.Job> checks = new List<Engine.Check.Job>();
				{ 
					UrlDir.UrlConfig urlc = GameDatabase.Instance.GetConfigs("TWEAKSCALE")[0];
					ConfigNode sanityNodes = urlc.config.GetNode("SANITY");
					foreach (ConfigNode cn in sanityNodes.GetNodes("CHECK"))
					{
						if (!cn.HasValue("priority") || this.priority.ToString().Equals(cn.GetValue("priority"))) continue;
						checks.Add(new Engine.Check.Job(KSPe.ConfigNodeWithSteroids.from(cn)));
					}
				}
				foreach (Engine.Check.Job j in checks)
				{
					Engine.Check.Result r = Engine.Check.Instance.Execute(j, p, prefab);
					if (r.IsProblematic)
					{
						++this.count;
						checksFailed.Add(r);
					}
				}
			}
			return checksFailed;
		}
	}
}
