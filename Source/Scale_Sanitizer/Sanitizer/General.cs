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
	public class General : Abstract
	{
		private int failures = 0;
		private int count = 0;

		public override Priority Priority => Priority.Critical;
		public override int Ocurrences => this.count;
		public override int Unscalable => this.count;
		public override int Failures => this.failures;
		public override string Summary => string.Format("{0} checks failed", this.count);

		protected override bool DoCheck(AvailablePart p, Part prefab)
		{
			try
			{
				string r = null;

				// We check for fixable problems first, in the hope to prevent by luck a ShowStopper below.
				// These Offending Parts never worked before, or always ends in crashing KSP, so the less worse
				// line of action is to remove TweakScale from them in order to allow the player to at least keep
				// playing KSP. Current savegames can break, but they were going to crash and loose everything anyway!!
				if (null != (r = this.CheckForSanity(prefab)))
				{   // There are some known situations where TweakScale is capsizing. If such situations are detected, we just
					// refuse to scale it. Sorry.
					Log.error("Part {0} ({1}) didn't passed the sanity check due {2}.", p.name, p.title, r);
					Log.warn("Removing TweakScale support for {0} ({1}).", p.name, p.title);

					PartModule m = prefab.Modules["TweakScale"];
					prefab.RemoveModule(m);
					if (KSPe.Util.KSP.Version.Current < KSPe.Util.KSP.Version.FindByVersion(1, 8, 0))
						UnityEngine.Object.Destroy(m);  // Kill the bastard so it doesn't came back from nowhere to bite our ass!

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
			if (r) GUI.SanityCheckAlertBox.Show(this.count, showMessageBox);
			return r;
		}

		private string CheckForSanity(Part p)
		{
			Log.dbg("Checking Sanity for {0} at {1}", p.name, p.partInfo.partUrl);

			try
			{
				TweakScale m = p.Modules.GetModule<TweakScale>();
				if (m.active && m.available && m.Fields["tweakScale"].guiActiveEditor == m.Fields["tweakName"].guiActiveEditor)
					return "not being correctly initialized - see issue [#30]( https://github.com/net-lisias-ksp/TweakScale/issues/30 )";
			}
			catch (System.NullReferenceException)
			{
				return "having missed attributes - see issue [#30]( https://github.com/net-lisias-ksp/TweakScale/issues/30 )";
			}

			if (p.Modules.Contains("FSbuoyancy") && !p.Modules.Contains("TweakScalerFSbuoyancy"))
				return "using FSbuoyancy module without TweakScaleCompanion for Firespitter installed - see issue [#1] from TSC_FS ( https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1 )";

			if (p.Modules.Contains("ModuleB9PartSwitch"))
			{
				if (p.Modules.Contains("FSfuelSwitch"))
					return "having ModuleB9PartSwitch together FSfuelSwitch - see issue [#12]( https://github.com/net-lisias-ksp/TweakScale/issues/12 )";
				if (p.Modules.Contains("ModuleFuelTanks"))
					return "having ModuleB9PartSwitch together ModuleFuelTanks - see issue [#12]( https://github.com/net-lisias-ksp/TweakScale/issues/12 )";
			}

			return null;
		}
	}
}
