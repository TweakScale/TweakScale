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
	internal class CriticalFixes : Abstract
	{
		private int failures = 0;
		private int count = 0;
		private int unscalable = 0;

		public override Priority Priority => Priority.Critical;
		public override int Ocurrences => this.count;
		public override int Unscalable => this.unscalable;
		public override int Failures => this.failures;
		public override string Summary => string.Format("{0} critical fixes applied", this.count);

		private readonly List<Engines.Fix.Job> AVAILABLE_FIXES = new List<Engines.Fix.Job>();
		public override bool HasRules => 0 != this.AVAILABLE_FIXES.Count;

		public CriticalFixes()
		{
			UrlDir.UrlConfig urlc = GameDatabase.Instance.GetConfigs("TWEAKSCALE")[0];
			ConfigNode sanityNodes = urlc.config.GetNode("SANITY");
			foreach (ConfigNode cn in sanityNodes.GetNodes("FIX"))
			{
				// All fixes must be executed on the Critical Priority.
				if (cn.HasValue("priority")) cn.RemoveValues("priority");
				cn.SetValue("priority", Priority.Critical.ToString());
				AVAILABLE_FIXES.Add(new Engines.Fix.Job(KSPe.ConfigNodeWithSteroids.from(cn)));
			}
			Log.dbg("{0} has {1} available fixes.", this.Priority, this.AVAILABLE_FIXES.Count);
		}

		protected override bool DoCheck(AvailablePart p, Part prefab)
		{
			try
			{
				string r = null;

				// These Offending Parts never worked before, or always ends in crashing KSP, so the less worse
				// line of action is to remove TweakScale from them in order to allow the player to at least keep
				// playing KSP. Current savegames can break, but they were going to crash and loose everything anyway!!
				if (null != (r = this.CheckForSanity(p, prefab)))
				{   // There are some known situations where TweakScale is capsizing. If such situations are detected, we just
					// refuse to scale it. Sorry.
					Engines.Fix.RemoveModuleFrom(p, prefab, "TweakScale");
					Log.error("Part {0} ({1}) didn't passed the sanity check due {2}.", p.name, p.title, r);

					++this.count;
					++this.unscalable;
					return true; // Abort the check chain for this part.
				}

				// We check for fixable problems first, in the hope to prevent by luck a ShowStopper later.
				{
					List<Engines.Fix.Result> fixesApplied = this.ApplyFixes(p, prefab);
					r = string.Join("; ", fixesApplied.Select(s => s.ToProblems()).ToArray<string>());
					Log.error("Part {0} ({1}) didn't passed the sanity check due {2}.", p.name, p.title, r);
					if (fixesApplied.Any(s => s.IsTerminal))
						return true;
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
			if (r) GUI.SanityCheckAlertBox.Show(this.count, showMessageBox);
			return r;
		}

		private string CheckForSanity(AvailablePart p, Part prefab)
		{
			Log.dbg("Checking Sanity for {0} ({1}) at {2}", p.name, p.title, p.partUrl);

			try
			{
				TweakScale m = prefab.Modules.GetModule<TweakScale>();
				if (m.active && m.available && m.Fields["tweakScale"].guiActiveEditor == m.Fields["tweakName"].guiActiveEditor)
					return "not being correctly initialized - see issue [#30]( https://github.com/net-lisias-ksp/TweakScale/issues/30 )";
			}
			catch (System.NullReferenceException)
			{
				return "having missed attributes - see issue [#30]( https://github.com/net-lisias-ksp/TweakScale/issues/30 )";
			}

			return null;
		}

		private List<Engines.Fix.Result> ApplyFixes(AvailablePart p, Part prefab)
		{
			List<Engines.Fix.Result> fixesApplied = new List<Engines.Fix.Result>();
			foreach (Engines.Fix.Job j in AVAILABLE_FIXES) if (Engines.Fix.Job.Correction.RemoveOffendedModule == j.correction)
			{
				Engines.Fix.Result r = Engines.Fix.Instance.Execute(j, p, prefab);
				if (r.CorrectionApplied)
				{
					++this.count;
					++this.unscalable;
					fixesApplied.Add(r);
					return fixesApplied;		// We removed TweakScale from the part. There's nothing else we can do.
				}
			}
			foreach (Engines.Fix.Job j in AVAILABLE_FIXES) if (Engines.Fix.Job.Correction.RemoveOffendedModule != j.correction)
			{
				Engines.Fix.Result r = Engines.Fix.Instance.Execute(j, p, prefab);
				if (r.CorrectionApplied)
				{
					++this.count;
					fixesApplied.Add(r);
				}
			}
			return fixesApplied;
		}
	}
}
