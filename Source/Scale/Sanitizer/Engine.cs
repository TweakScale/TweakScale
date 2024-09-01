/*
	This file is part of TweakScale™ /L
		© 2018-2024 LisiasT

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
using System;
using System.Collections.Generic;

using KSPe.Annotations;

namespace TweakScale.Sanitizer
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	internal class Results : SingletonBehavior<Results>
	{
		// Shows the Error Messages (only if there's no Show Stoppers). 
		// The Dialogs should be positioned in a way that they could be all displayed at once.
		[UsedImplicitly]
		private void Start()
		{
			bool showStopper = false;
			foreach (Sanitizer.ISanityCheck sc in Engine.Instance.CHECKS_AVAILABLE) if (Sanitizer.Priority.ShowStopper == sc.Priority)
				// Only the first Show Stopper is emitted. There's no point on flooding the screen with more than one.
				if (showStopper = sc.EmmitMessageIfNeeded(ModuleManagerListener.shouldShowWarnings))
					break;
			if (!showStopper) // If a Show Stopper as emitted, nothing else matters. Otherwise, notify user about the lesser problems.
				for (Priority i = 0; i < Priority.__SIZE; ++i)
					foreach (ISanityCheck sc in Engine.Instance.CHECKS_AVAILABLE) if (i == sc.Priority)
						sc.EmmitMessageIfNeeded(ModuleManagerListener.shouldShowWarnings);
		}

		[UsedImplicitly]
		new protected void OnDestroy()
		{
			base.OnDestroy();
			// Free some memory:
			Engine.Instance.Destroy();
		}
	}

	internal class Engine
	{
		private static Engine INSTANCE;
		internal static Engine Instance => INSTANCE??(INSTANCE = new Engine());

		internal readonly List<ISanityCheck> CHECKS_AVAILABLE = new List<ISanityCheck>();

		private Engine()
		{
			IEnumerable<Type> ts = KSPe.Util.SystemTools.Type.Search.By(typeof(ISanityCheck));
			foreach(Type t in ts) if (!t.IsAbstract)
			{
				ISanityCheck sc = (ISanityCheck)System.Activator.CreateInstance(t);
				if (sc.HasRules) CHECKS_AVAILABLE.Add(sc);
			}
		}
		internal void Destroy()
		{
			this.CHECKS_AVAILABLE.Clear();
			INSTANCE = null;
		}

		internal List<string> GetSummary(int unscalable_count)
		{
			List<string> m = new List<string>();
			int failure_count = 0;
			foreach(Sanitizer.ISanityCheck sc in Engine.Instance.CHECKS_AVAILABLE)
			{
				unscalable_count += sc.Unscalable;
				failure_count += sc.Failures;
				m.Add(sc.Summary);
			}
			m.Add(string.Format("{0} parts failed being checked", failure_count));
			m.Add(string.Format("{0} unscalable parts", unscalable_count));
			return m;
		}

		internal void Check(AvailablePart ap)
		{
			Log.detail("Sanity Checks for part {0} ({1}) started.", ap.name, ap.title);
			{	// Run all the Sanity Checks (but Show Stoppers), priorized.
				for(Priority i = 0; i < Priority.__SIZE; ++i)
					foreach(ISanityCheck sc in CHECKS_AVAILABLE) if (i == sc.Priority)
						if (sc.Check(ap, ap.partPrefab)) break;
			}

			// Run the Show Stopper checks. It's run at last so the Sanity Checks has a chance of act before blowing everything up.
			foreach(ISanityCheck sc in CHECKS_AVAILABLE) if (Priority.ShowStopper == sc.Priority)
				if (sc.Check(ap, ap.partPrefab)) return; // If anyone from the show stoppers kicks, it's game over for this part. It's the reason they are called Show Stoppers!
		}
	}
}
