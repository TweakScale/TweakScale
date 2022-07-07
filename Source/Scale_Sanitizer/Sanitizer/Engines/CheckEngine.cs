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
using System;
using System.Collections.Generic;

namespace TweakScale.Sanitizer.Engine
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

			public Job(ConfigNode cn)
			{
				this.name = cn.GetValue("name");
				this.descriptionText = cn.GetValue("description_text");
				this.issueText = cn.GetValue("issue_text");
				this.issueLink = System.Uri.UnescapeDataString(cn.GetValue("issue_link"));
				this.supportLink = System.Uri.UnescapeDataString(cn.GetValue("support_link"));
				this.module = cn.GetValue("module_affected");
				this.conflicts = cn.GetValues("module_conflicting");
				this.dependencies = cn.GetValues("module_dependency");
			}
		}

		public class Result
		{
			public readonly Job job;
			public readonly AvailablePart availablePart;
			public readonly Part prefab;
			public string[] Conflicts { get; private set;}
			public string[] MissingDependencies  { get; private set;}

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
				if(this.prefab.Modules.Contains("TweakScale"))    // Better safe than sorry. Someone eventually will call this without TweakScale installed!
				{
					foreach(string module in this.job.conflicts) if(this.prefab.Modules.Contains(module))
						conflicts.Add(module);

					foreach(string module in this.job.dependencies) if(!this.prefab.Modules.Contains(module))
						missing.Add(module);
				}
				this.Conflicts = conflicts.ToArray();
				this.MissingDependencies = missing.ToArray();
			}

			public override string ToString()
			{
				if (0 != this.Conflicts.Length && 0 != this.MissingDependencies.Length)
					return string.Format("{0} ({1}) passed the sanity check {2}", this.availablePart.name, this.availablePart.title, this.job.name);
				return string.Format("{0} ({1}) failed the sanity check {2} due {3}", this.availablePart.name, this.availablePart.title, this.job.name, this.ToProblems());
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
				string.Format("have a Conflict between {0} and {1}", this.job.module, string.Join(",", this.Conflicts));

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
			Log.detail(r.ToString());
			return r;
		}

	}
}
