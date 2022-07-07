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

namespace TweakScale.Sanitizer.Engine
{
	public class Fix : Check
	{
		public new class Job : Check.Job
		{
			public enum Correction
			{
				RemoveTweakScaleModule,	// Terminal Task
				RemoveOffendingModule
			}

			public readonly Correction correction;

			public Job(ConfigNode cn):base(cn)
			{
				string action = cn.GetValue("action");
				if ("REMOVE_TWEAKSCALE".Equals(action)) this.correction = Correction.RemoveTweakScaleModule;
				else if ("REMOVE_OFFENDING".Equals(action)) this.correction = Correction.RemoveOffendingModule;
				else throw new ArgumentException(string.Format("Action {0} not recognized!", action));
			}
		}

		public new class Result
		{
			private readonly Check.Result result;
			public Job job => (Job)this.result.job;
			public AvailablePart availablePart => this.result.availablePart;
			public Part prefab => this.result.prefab;
			public string[] Conflicts => this.result.Conflicts;
			public string[] MissingDependencies => this.result.MissingDependencies;
			public bool CorrectionApplied { get; private set;}
			public bool IsTerminal => this.CorrectionApplied && Job.Correction.RemoveTweakScaleModule == job.correction;

			public Result(Check.Result r)
			{
				this.result = r;
			}

			public void Execute()
			{
				this.CorrectionApplied = false;
				if (0 == this.Conflicts.Length || 0 == this.MissingDependencies.Length) return;

				switch(job.correction)
				{
					case Job.Correction.RemoveTweakScaleModule:
						RemoveModuleFrom(this.availablePart, this.prefab, "TweakScale");
						this.CorrectionApplied = true;
						break;
					case Job.Correction.RemoveOffendingModule:
						if (0 != this.result.MissingDependencies.Length) throw new InvalidOperationException(string.Format("Can't fix {0} as it miss the folliowing dependencies {1}!", this.prefab.partName, this.MissingDependencies));
						this.CorrectionApplied = 0 != this.Conflicts.Length;
						foreach (string m in this.Conflicts)
							RemoveModuleFrom(this.availablePart, this.prefab, m);
						break;
					default:
						throw new NotImplementedException(string.Format("Action {0} not implemented", job.correction));
				}
				if (this.CorrectionApplied)	Log.detail("Corrections were applied by {0} on {1} ({2})", this.job.name, this.availablePart.name, this.availablePart.title);
			}

			public override string ToString() => this.result.ToString();
			public string ToProblems() => this.result.ToProblems();
		}

		private static Fix INSTANCE = null;
		internal static new Fix Instance = INSTANCE??(INSTANCE = new Fix());
		protected Fix() {}

		public Result Execute(Job job, AvailablePart p, Part prefab)
		{
			Result r = new Result(base.Execute(job, p, prefab));
			r.Execute();
			if (r.CorrectionApplied)
				Log.detail(string.Format("Corrections were applied by {0} on {1} ({2})", r.job.name, r.availablePart.name, r.availablePart.title));
			return r;
		}

		public static void RemoveModuleFrom(AvailablePart p, Part prefab, string module)
		{
			Log.warn("Removing {0} support for {1} ({2}).", module, p.name, p.title);
			PartModule m = prefab.Modules[module];
			prefab.RemoveModule(m);
			if (KSPe.Util.KSP.Version.Current < KSPe.Util.KSP.Version.FindByVersion(1, 8, 0))
				UnityEngine.Object.Destroy(m);  // Kill the bastard so it doesn't came back from nowhere to bite our ass!
		}
	}
}
