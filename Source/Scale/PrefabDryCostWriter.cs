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
using System.Collections;
using System.Collections.Generic;
using System.Text;

using KSPe;
using KSPe.Annotations;

namespace TweakScale
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    internal class PrefabDryCostWriter : SingletonBehavior<PrefabDryCostWriter>
    {
        private static readonly int WAIT_ROUNDS = 120; // @60fps, would render 2 secs.
        
        internal static bool isConcluded = false;

        [UsedImplicitly]
        private void Start()
        {
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
            StartCoroutine("WriteDryCost");
        }

        private IEnumerator WriteDryCost()
        {
            PrefabDryCostWriter.isConcluded = false;
            Log.info("WriteDryCost: Started");

            {  // Toe Stomping Fest prevention
                for (int i = WAIT_ROUNDS; i >= 0 && null == PartLoader.LoadedPartsList; --i)
                {
                    yield return null;
                    if (0 == i) Log.warn("Timeout waiting for PartLoader.LoadedPartsList!!");
                }
    
    			 // I Don't know if this is needed, but since I don't know that this is not needed,
    			 // I choose to be safe than sorry!
                {
                    int last_count = int.MinValue;
                    for (int i = WAIT_ROUNDS; i >= 0; --i)
                    {
                        if (last_count == PartLoader.LoadedPartsList.Count) break;
                        last_count = PartLoader.LoadedPartsList.Count;
                        yield return null;
                        if (0 == i) Log.warn("Timeout waiting for PartLoader.LoadedPartsList.Count!!");
                    }
                }
            }

            List<Sanitizer.SanityCheck> checksAvailable = new List<Sanitizer.SanityCheck>();
            {
                IEnumerable<Type> ts = KSPe.Util.SystemTools.Type.Search.By(typeof(Sanitizer.SanityCheck));
                foreach (Type t in ts) if (!t.IsAbstract)
                    checksAvailable.Add((Sanitizer.SanityCheck)System.Activator.CreateInstance(t));
            }

            int total_count = 0;
            int drycost_failures_count = 0;
            int unscalable_count = 0;

            foreach (AvailablePart p in PartLoader.LoadedPartsList)
            {
                for (int i = WAIT_ROUNDS; i >= 0 && (null == p.partPrefab || null == p.partPrefab.Modules); --i)
                {
                    yield return null;
                    if (0 == i) Log.error("Timeout waiting for {0}.prefab.Modules!!", p.name);
                }
              
                Part prefab;
                { 
                    // Historically, we had problems here.
                    // However, that co-routine stunt appears to have solved it.
                    // But we will keep this as a ghinea-pig in the case the problem happens again.
                    int retries = WAIT_ROUNDS;
                    bool containsTweakScale = false;
                    Exception culprit = null;
                    
                    prefab = p.partPrefab; // Reaching the prefab here in the case another Mod recreates it from zero. If such hypothecical mod recreates the whole part, we're doomed no matter what.
                    
                    while (retries > 0)
                    { 
                        bool should_yield = false;
                        try 
                        {
                            containsTweakScale = prefab.Modules.Contains("TweakScale"); // Yeah. This while stunt was done just to be able to do this. All the rest is plain clutter! :D 
                            break;
                        }
                        catch (Exception e)
                        {
                            culprit = e;
                            --retries;
                            should_yield = true;
                        }
                        if (should_yield) // This stunt is needed as we can't yield from inside a try-catch!
                            yield return null;
                    }

                    if (0 == retries)
                    {
                        Log.error("Exception on {0}.prefab.Modules.Contains: {1}", p.name, culprit);
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
                        foreach (Sanitizer.SanityCheck sc in checksAvailable) if (i == sc.Priority)
                            if (abort = sc.Check(p, prefab)) break;
                    }
                }

                // Run the Show Stopper checks. It's run at last so the Sanity Checks has a chance of act before blowing it up.
                foreach (Sanitizer.SanityCheck sc in checksAvailable) if (Sanitizer.Priority.ShowStopper == sc.Priority)
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
                foreach (Sanitizer.SanityCheck sc in checksAvailable)
                {
                    unscalable_count += sc.Unscalable;
                    m.Add(sc.Summary);
                }
                m.Add(string.Format("{0} unscalable parts", unscalable_count));
                Log.info("WriteDryCost Concluded : {0}.", string.Join("; ", m.ToArray()));
            }

            PrefabDryCostWriter.isConcluded = true;

            {   // Shows the Error Messages (only if there's no Show Stoppers).
                // The Dialogs should be positioned in a way that they could be all displayed at once.
                bool showStopper = false;
                foreach (Sanitizer.SanityCheck sc in checksAvailable) if (Sanitizer.Priority.ShowStopper == sc.Priority)
                    // Only the first Show Stopper is emitted. There's no point on flooding the screen with more than one.
                    if (showStopper = sc.EmmitMessageIfNeeded(ModuleManagerListener.shouldShowWarnings))
                        yield break;
                if (!showStopper) // If a Show Stopper as emiited, nothing else matters.
                    for (Sanitizer.Priority i = Sanitizer.Priority.Critical; i < Sanitizer.Priority.ShowStopper; ++i)
                        foreach (Sanitizer.SanityCheck sc in checksAvailable) if (i == sc.Priority)
                            sc.EmmitMessageIfNeeded(ModuleManagerListener.shouldShowWarnings);
            }
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
}
