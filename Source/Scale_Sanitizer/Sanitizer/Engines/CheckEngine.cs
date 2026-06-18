/*
	This file is part of TweakScale /L
		© 2018-2026 LisiasT
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
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using KSPe;

namespace TweakScale.Sanitizer.Engines
{
	public class Check
	{
		public class Job
		{
			public readonly string name;
			public readonly string descriptionText;
			public readonly string issueText;
			public readonly string issueLink;
			public readonly string supportLink;
			public readonly string module;
			public readonly string[] conflicts;
			public readonly string[] dependencies;

			public readonly string[] conflictsPartUrlPrefix;
			public readonly Regex[] conflictsPartUrlRx;

			public readonly string[] conflictsPartName;
			public readonly Regex[] conflictsPartNameRx;


			public Job(ConfigNodeWithSteroids cn)
			{
				this.name = cn.GetValue("name");
				this.descriptionText = cn.GetValue("description_text", "");
				this.issueText = cn.GetValue("issue_text", "");
				this.issueLink = System.Uri.UnescapeDataString(cn.GetValue("issue_link",""));
				this.supportLink = System.Uri.UnescapeDataString(cn.GetValue("support_link",""));
				this.module = cn.GetValue("module_affected");
				this.conflicts = cn.GetValues("module_conflicting");
				this.dependencies = cn.GetValues("module_dependency");

				this.conflictsPartUrlPrefix = cn.GetValues("parturl_conflict_prefix");
				{
					string[] sa = cn.GetValues("parturl_conflict_regex");
					List<Regex> r = new List<Regex>();
					for (int i = 0; i < sa.Length; ++i)
						r.Add(new Regex(sa[i]));
					this.conflictsPartUrlRx = r.ToArray();
				}

				this.conflictsPartName = cn.GetValues("partname_conflict");
				{
					string[] sa = cn.GetValues("partname_conflict_regex");
					List<Regex> r = new List<Regex>();
					for (int i = 0; i < sa.Length; ++i)
						r.Add(new Regex(sa[i]));
					this.conflictsPartNameRx = r.ToArray();
				}
			}

			public string GetLogDescription(string defaultDesc)
			{
				string r = string.IsNullOrEmpty(this.descriptionText) ? defaultDesc : this.descriptionText;
				if (!string.IsNullOrEmpty(this.issueText)) r += string.Format(". {0}", this.issueText);
				if (!string.IsNullOrEmpty(this.issueLink)) r += string.Format("({0})", this.issueLink);
				return r;
			}

			public string GetScreenDescription(string defaultDesc)
			{
				string r = string.IsNullOrEmpty(this.descriptionText) ? defaultDesc : this.descriptionText;
				if (!string.IsNullOrEmpty(this.supportLink)) r += string.Format(". Visit <a href=\"{0}\">this link</a> for further information", this.issueText);
				return r;
			}
		}

		public class Result
		{
			public readonly Job job;
			public readonly AvailablePart availablePart;
			public readonly Part prefab;
			public string[] Conflicts { get; private set;}
			public string[] MissingDependencies  { get; private set;}
			public bool IsProblematic => !(0 == this.Conflicts.Length && 0 == this.MissingDependencies.Length);

			public Result(Job job, AvailablePart p, Part prefab)
			{
				this.job = job;
				this.availablePart = p;
				this.prefab = prefab;
			}

			public void Execute()
			{
				List<string> conflicts = new List<string>();
				List<string> missing = new List<string>();
				if (this.prefab.Modules.Contains(this.job.module))	// The potentially offended module is installed? Otherwise we have nothing to do.
				{
					{
						string[] list = this.job.dependencies;
						for (int i = 0; i < list.Length; ++i) if (!this.prefab.Modules.Contains(list[i]))
							missing.Add(list[i]);
					}
					{
						string[] list = this.job.conflicts;
						for (int i = 0; i < list.Length; ++i) if (this.prefab.Modules.Contains(list[i]))
							conflicts.Add(list[i]);
					}
					this.checkPartUrl(conflicts);
					this.checkPartName(conflicts);
				}
				this.Conflicts = conflicts.ToArray();
				this.MissingDependencies = missing.ToArray();
			}

			private void checkPartUrl(List<string> conflicts)
			{
				string partUrl = this.availablePart.partUrl;
				{
					Regex[] list = this.job.conflictsPartUrlRx;
					for (int i = 0; i < list.Length; ++i)
					{
						MatchCollection m = list[i].Matches(partUrl);
						if (0 != m.Count) conflicts.Add(partUrl);
					}
				}
				{
					string[] list = this.job.conflictsPartUrlPrefix;
					for (int i = 0; i < list.Length; ++i) if (partUrl.StartsWith(list[i]))
						conflicts.Add(partUrl);
				}
			}

			private void checkPartName(List<string> conflicts)
			{
				string partName = this.availablePart.name;
				{
					Regex[] list = this.job.conflictsPartNameRx;
					for (int i = 0; i < list.Length; ++i)
					{
						MatchCollection m = list[i].Matches(partName);
						if (0 != m.Count) conflicts.Add(partName);
					}
				}
				{
					string[] list = this.job.conflictsPartUrlPrefix;
					for (int i = 0; i < list.Length; ++i) if (partName.StartsWith(list[i]))
						conflicts.Add(partName);
				}
			}

			public string ToLog()
			{
				if (this.IsProblematic)
					return string.Format("{0} ({1}) failed the sanity check {2} due {3}", this.availablePart.name, this.availablePart.title, this.job.name, this.job.GetLogDescription(this.ToProblems()));
				return string.Format("{0} ({1}) passed the sanity check {2}", this.availablePart.name, this.availablePart.title, this.job.name);
			}

			public string ToScreen()
			{
				if (this.IsProblematic)
					return string.Format("{0} ({1}) failed the sanity check {2} due {3}", this.availablePart.name, this.availablePart.title, this.job.name, this.job.GetScreenDescription(this.ToProblems()));
				return string.Format("{0} ({1}) passed the sanity check {2}", this.availablePart.name, this.availablePart.title, this.job.name);
			}

			public string ToProblems()
			{
				if (0 != this.Conflicts.Length && 0 != this.MissingDependencies.Length)
					return this.ToConflicts() + " and " + this.ToMissingDependencies();
				if (0 != this.Conflicts.Length)
					return this.ToConflicts();
				if (0 != this.MissingDependencies.Length)
					return this.ToMissingDependencies();
				return "";
			}

			private string ToMissingDependencies() =>
				string.Format("have a conflict between {0} and {1}", this.job.module, string.Join(",", this.Conflicts));

			private string ToConflicts() =>
				string.Format("{0} didn't met dependency(ies) {1}", this.job.module, string.Join(",", this.MissingDependencies));
		}

		private static Check INSTANCE = null;
		internal static Check Instance = INSTANCE??(INSTANCE = new Check());
		protected Check() {}

		public Result Execute(Job job, AvailablePart p, Part part)
		{
			Result r = new Result(job, p, part);
			r.Execute();
			Log.trace(r.ToLog());
			return r;
		}

	}
}
