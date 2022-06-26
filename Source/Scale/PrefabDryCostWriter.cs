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

using KSPe;
using KSPe.Annotations;

namespace TweakScale
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	internal class PrefabDryCostWriter : SingletonBehavior<PrefabDryCostWriter>
	{
		internal static bool isConcluded = false;
		internal static readonly List<Sanitizer.SanityCheck> CHECKS_AVAILABLE = new List<Sanitizer.SanityCheck>();

		[UsedImplicitly]
		private void Start()
		{
			using (KSPe.Util.SystemTools.Assembly.Loader<TweakScale> a = new KSPe.Util.SystemTools.Assembly.Loader<TweakScale>()) try
			{
				if (KSPe.IO.File<TweakScale>.Asset.Exists("Scale_Sanitizer.dll"))
					a.LoadAndStartup("Scale_Sanitizer");
			}
			catch (System.Exception e)
			{
				Log.error(e.ToString());
				GUI.MissingDLLAlertBox.Show(e.Message);
			}

			{
				bool sane = false;
				if (KSPe.Util.SystemTools.Assembly.Finder.ExistsByName("Scale_Sanitizer"))
				{
					System.Reflection.Assembly asm = KSPe.Util.SystemTools.Assembly.Finder.FindByName("Scale_Sanitizer");
					System.Diagnostics.FileVersionInfo myVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(this.GetType().Assembly.Location);
					System.Diagnostics.FileVersionInfo hisVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(asm.Location);

					sane = KSPe.IO.Path.GetDirectoryName(asm.Location).StartsWith(
							KSPe.IO.Path.GetDirectoryName(this.GetType().Assembly.Location)
						);
					sane &= myVersionInfo.CompanyName.Equals(hisVersionInfo.CompanyName);
					sane &= myVersionInfo.ProductName.Equals(hisVersionInfo.ProductName);
					sane &= myVersionInfo.LegalCopyright.Equals(hisVersionInfo.LegalCopyright);
					sane &= myVersionInfo.LegalTrademarks.Equals(hisVersionInfo.LegalTrademarks);
				}

				if (!sane)
				{
					Log.force(@"
**********************************************************************
***          THIS TWEAKSCALE INSTALLATION IS UNSUPPORTED           ***
**                                                                  **
**  Scale_Sanitizer, a DLL  TweakScale needs  in  order  to  check  **
**  for   known  problems  and  prevent  disastrous   consequences  **
**  in your rig, was not found.                                     **
**                                                                  **
**  This means that TweakScale can't check if it's safe to run!     **
**                                                                  **
**  Proceed at your own risk. TweakScale's maintainer **WILL NOT**  **
**  accept  bug  reports, neither  will help  diagnosing  problems  **
**  without Scale_Sanitiser present.                                **
***                                                                ***
**********************************************************************
");
					GUI.MissingSanitizerAlertBox.Show();
				}
			}
			GameEvents.onGameSceneSwitchRequested.Add(this.OnGameSceneSwitchRequested);
		}

		private void OnGameSceneSwitchRequested(GameEvents.FromToAction<GameScenes, GameScenes> data) {
			Log.detail("Switching scene from {0} to {1}.", data.from, data.to);
			this.WriteDryCost();
		}

		private void WriteDryCost()
        {
            PrefabDryCostWriter.isConcluded = false;
            Log.info("WriteDryCost: Started");

            {
                IEnumerable<Type> ts = KSPe.Util.SystemTools.Type.Search.By(typeof(Sanitizer.SanityCheck));
                foreach (Type t in ts) if (!t.IsAbstract)
                    CHECKS_AVAILABLE.Add((Sanitizer.SanityCheck)System.Activator.CreateInstance(t));
            }

            int total_count = 0;
            int drycost_failures_count = 0;
            int unscalable_count = 0;

            foreach (AvailablePart p in PartLoader.LoadedPartsList)
            {
                Part prefab;
                { 
                    bool containsTweakScale = false;

                    prefab = p.partPrefab; // Reaching the prefab here in the case another Mod recreates it from zero. If such hypothecical mod recreates the whole part, we're doomed no matter what.
                    try 
                    {
                        containsTweakScale = prefab.Modules.Contains("TweakScale"); // Yeah. This while stunt was done just to be able to do this. All the rest is plain clutter! :D 
                    }
                    catch (Exception e)
                    {
                        Log.error("Exception on {0}.prefab.Modules.Contains: {1}", p.name, e.Message);
                        Log.detail("{0}", prefab.Modules);
                        continue;
                    }

                    ++total_count;
                    if (!containsTweakScale)
                    {
                        Log.dbg("The part named {0} ; title {1} doesn't supports TweakScale. Skipping.", p.name, p.title);
                        ++unscalable_count;
                        continue;
                    }

                    // End of hack. Ugly, uh? :P
                }
#if DEBUG
                {
                    Log.dbg("Found part named {0} ; title {1}:", p.name, p.title);
                    foreach (PartModule m in prefab.Modules)
                        Log.dbg("\tPart {0} has module {1}", p.name, m.moduleName);
                }
#endif
                {   // Run all the Sanity Checks (but Show Stoppers), priorized.
                    bool abort = false;
                    for (Sanitizer.Priority i = Sanitizer.Priority.__MIN; i < Sanitizer.Priority.__SIZE; ++i)
                    {
                        if (abort) break;
                        foreach (Sanitizer.SanityCheck sc in CHECKS_AVAILABLE) if (i == sc.Priority)
                            if (abort = sc.Check(p, prefab)) break;
                    }
                }

                // Run the Show Stopper checks. It's run at last so the Sanity Checks has a chance of act before blowing it up.
                foreach (Sanitizer.SanityCheck sc in CHECKS_AVAILABLE) if (Sanitizer.Priority.ShowStopper == sc.Priority)
                    if (sc.Check(p, prefab)) break;

                try
                {   // Now we can try to calculate the DryCost.
                    TweakScale m = prefab.Modules["TweakScale"] as TweakScale;
                    m.OriginalCrewCapacity = prefab.CrewCapacity;
                    m.CalculateDryCostIfNeeded();
                    Log.dbg("Part {0} ({1}) has drycost {2} and OriginalCrewCapacity {3}",  p.name, p.title, m.DryCost, m.OriginalCrewCapacity);
                }
                catch (Exception e)
                {
                    ++drycost_failures_count;
                    Log.error("part={0} ({1}) Exception on writeDryCost: {2}", p.name, p.title, e);
                }
            }

            {   // Log the summary info
                List<string> m = new List<string>();
                m.Add(string.Format("{0} parts found", total_count));
                m.Add(string.Format("{0} DryCost failed", drycost_failures_count));
                foreach (Sanitizer.SanityCheck sc in CHECKS_AVAILABLE)
                {
                    unscalable_count += sc.Unscalable;
                    m.Add(sc.Summary);
                }
                m.Add(string.Format("{0} unscalable parts", unscalable_count));
                Log.info("WriteDryCost Concluded : {0}.", string.Join("; ", m.ToArray()));
            }

            PrefabDryCostWriter.isConcluded = true;
        }

        private ConfigNode GetMeThatConfigNode(Part p)
        {
            AvailablePart ap = p.partInfo;
            if (null == ap)
            {
                Log.warn("NULL partInfo for {0}!! Something is *really* messed up with this part!!", p.partName);
                return null;
            }

            // Check the forum for the rationale:
            //      https://forum.kerbalspaceprogram.com/index.php?/topic/7542-the-official-unoffical-quothelp-a-fellow-plugin-developerquot-thread/&do=findComment&comment=3631853
            //      https://forum.kerbalspaceprogram.com/index.php?/topic/7542-the-official-unoffical-quothelp-a-fellow-plugin-developerquot-thread/&do=findComment&comment=3631908
            //      https://forum.kerbalspaceprogram.com/index.php?/topic/7542-the-official-unoffical-quothelp-a-fellow-plugin-developerquot-thread/&do=findComment&comment=3632139

            // First try the canonnical way - there must be a config file somewhere!
            ConfigNode r = GameDatabase.Instance.GetConfigNode(p.partInfo.partUrl);
            if (null == r)
            {
                // But if that doesn't works, let's try the partConfig directly.
                //
                // I have reasons to believe that partConfig may not be an identical copy from the Config since Making History
                // (but I have, by now, no hard evidences yet) - but I try first the config file nevertheless. There's no point]
                // on risking pinpointing something that cannot be found on the config file.
                //
                // What will happen if the problems start to appear on the partConfig and not in the config file is something I
                // don't dare to imagine...
                Log.warn("NULL ConfigNode for {0} (unholy characters on the name?). Trying partConfig instead!", p.partInfo.partUrl);
                r = p.partInfo.partConfig;
            }

            return r;
        }
    }

	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	internal class PrefabDryCostWriterResults : SingletonBehavior<PrefabDryCostWriterResults>
	{
		// Shows the Error Messages (only if there's no Show Stoppers). 
		// The Dialogs should be positioned in a way that they could be all displayed at once.
		[UsedImplicitly]
		private void Start()
		{
			bool showStopper = false;
			foreach (Sanitizer.SanityCheck sc in PrefabDryCostWriter.CHECKS_AVAILABLE) if (Sanitizer.Priority.ShowStopper == sc.Priority)
				// Only the first Show Stopper is emitted. There's no point on flooding the screen with more than one.
				if (showStopper = sc.EmmitMessageIfNeeded(ModuleManagerListener.shouldShowWarnings))
					break;
			if (!showStopper) // If a Show Stopper as emiited, nothing else matters.
				for (Sanitizer.Priority i = Sanitizer.Priority.__MIN; i < Sanitizer.Priority.__SIZE; ++i)
					foreach (Sanitizer.SanityCheck sc in PrefabDryCostWriter.CHECKS_AVAILABLE) if (i == sc.Priority)
							sc.EmmitMessageIfNeeded(ModuleManagerListener.shouldShowWarnings);
			// Free some memory:
			PrefabDryCostWriter.CHECKS_AVAILABLE.Clear();
		}
	}
}
