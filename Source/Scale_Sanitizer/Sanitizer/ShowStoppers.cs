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
	internal class ShowStoppers : Abstract
	{
		private int failures = 0;
		private int count = 0;

		public override Priority Priority => Priority.ShowStopper;
		public override int Ocurrences => this.count;
		public override int Unscalable => this.count;
		public override int Failures => this.failures;
		public override string Summary => string.Format("{0} Show Stoppers found", this.count);

		private readonly List<Engine.Check.Job> AVAILABLE_CHECKS = new List<Engine.Check.Job>();

		internal ShowStoppers()
		{
			UrlDir.UrlConfig urlc = GameDatabase.Instance.GetConfigs("TWEAKSCALE")[0];
			ConfigNode sanityNodes = urlc.config.GetNode("SANITY");
			foreach (ConfigNode cn in sanityNodes.GetNodes("CHECK"))
			{
				if (!cn.HasValue("priority") || this.Priority.ToString().Equals(cn.GetValue("priority"))) continue;
				AVAILABLE_CHECKS.Add(new Engine.Check.Job(KSPe.ConfigNodeWithSteroids.from(cn)));
			}
		}

		protected override bool DoCheck(AvailablePart p, Part prefab)
		{
			try
			{
				string r = null;

				// And now we check for the ShowStoppers.
				// These ones happens due rogue patches, added after a good installment could starts savegames, what ends up corrupting them!
				// Since we don't have how to know when this happens, and since originally the part was working fine, we don't know
				// how to proceeed. So the only sensible option is to scare the user enough to make him/her go to the Forum for help
				// so we can identify the offending patch and then provide a solution that would preserve his savegame.
				// We also stops any further processing, as we could damage something that is already damaged.
				if (null != (r = this.CheckForShowStoppers(prefab)))
				{   // This are situations that we should not allow the KSP to run to prevent serious corruption.
					// This is **FAR** from being a good measure, but it's the only viable.
					Log.error("**FATAL** Part {0} ({1}) has a fatal problem due {2}.", p.name, p.title, r);
					++this.count;
					return true; // Abort the check chain for this part.
				}

				{
					List<Engine.Check.Result> checksFailed = this.CheckIntegrity(p, prefab);
					if(0 != checksFailed.Count)
					{ 
						r = string.Join("; ", checksFailed.Select(s => s.ToProblems()).ToArray<string>());
						Log.error("**FATAL** Part {0} ({1}) has a fatal problem due {2}.", p.name, p.title, r);
						return true;
					}
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
			if (r) GUI.ShowStopperAlertBox.Show(this.count, showMessageBox);
			return r;
		}

		private string CheckForShowStoppers(Part p)
		{
			Log.dbg("Checking ShowStopper for {0} at {1}", p.name, p.partInfo.partUrl);
			ConfigNode part = Abstract.GetMeThatConfigNode(p);
			if (null == part) return "having a part without a partInfo! - see issue [#237]( https://github.com/net-lisias-ksp/TweakScale/issues/237";

			foreach (ConfigNode basket in part.GetNodes("MODULE"))
			{
				string moduleName = basket.GetValue("name");
				if ("TweakScale" != moduleName) continue;
				if (basket.HasValue("ISSUE_OVERRULE")) continue; // TODO: Check if the issue overrule is for #34 or any other that is checked here.
				Log.dbg("\tModule {0}", moduleName);
				foreach (ConfigNode.Value property in basket.values)
				{
					Log.dbg("\t\t{0} = {1}", property.name, property.value);
					if (1 != basket.GetValues(property.name).Length)
						return "having duplicated properties - see issue [#34]( https://github.com/net-lisias-ksp/TweakScale/issues/34 )";
				}
			}

			return null;
		}

		private List<Engine.Check.Result> CheckIntegrity(AvailablePart p, Part prefab)
		{
			List<Engine.Check.Result> checksFailed = new List<Engine.Check.Result>();
			foreach (Engine.Check.Job j in AVAILABLE_CHECKS)
			{
				Engine.Check.Result r = Engine.Check.Instance.Execute(j, p, prefab);
				if (r.IsProblematic)
				{
					++this.count;
					checksFailed.Add(r);
				}
			}
			return checksFailed;
		}
	}
}
