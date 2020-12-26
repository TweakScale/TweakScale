/*
		This file is part of TweakScale /L
		© 2018-2020 LisiasT
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
		along with TweakScale /L If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

using TweakScale.Annotations;

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

            int total_count = 0;
            int check_failures_count = 0;
            int sanity_failures_count = 0;
            int showstoppers_count = 0;
            int overrules_count = 0;
            int hotfixes_count = 0;
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
                try {
                    string r = null;
                    
                    // We check for fixable problems first, in the hope to prevent by luck a ShowStopper below.
                    // These Offending Parts never worked before, or always ends in crashing KSP, so the less worse
                    // line of action is to remove TweakScale from them in order to allow the player to at least keep
                    // playing KSP. Current savegames can break, but they were going to crash and loose everything anyway!!
                    if (null != (r = this.checkForSanity(prefab)))
                    {   // There are some known situations where TweakScale is capsizing. If such situations are detected, we just
                        // refuse to scale it. Sorry.
                        Log.error("Part {0} ({1}) didn't passed the sanity check due {2}.", p.name, p.title, r);
                        Log.warn("Removing TweakScale support for {0} ({1}).", p.name, p.title);

                        PartModule m = prefab.Modules["TweakScale"];
                        prefab.RemoveModule(m);
                        if (KSPe.Util.KSP.Version.Current < KSPe.Util.KSP.Version.FindByVersion(1,8,0))
                            UnityEngine.Object.Destroy(m);  // Kill the bastard so it doesn't came back from nowhere to bite our ass!

                        ++sanity_failures_count;
                        ++unscalable_count; // Since this part is not scalable, we must account it as non scalable!
                        continue;
                    }

                    // This one is for my patches that "break things again" in a controlled way to salvage already running savegames
                    // that would be lost by fixing things right. Sometimes, it's possible to keep the badly patched parts ongoing, as
                    // as the nastiness will not crash KSP (besides still corrupting savegames and craft files in a way that would not
                    // allow the user to share them).
                    // Since we are overruling the checks, we abort the remaining ones. Yes, this allows abuse, but whatever... I can't
                    // save the World, just the savegames. :)
                    if (null != (r = this.checkForOverrules(prefab)))
                    {   // This is for detect and log the Breaking Parts patches.
                        // See issue [#56]( https://github.com/net-lisias-ksp/TweakScale/issues/56 ) for details.
                        // This is **FAR** from a good measure, but it's the only viable.
                        Log.warn("Part {0} ({1}) has the issue(s) overrule(s) {2}. See [#56]( https://github.com/net-lisias-ksp/TweakScale/issues/56 ) for details.", p.name, p.title, r);
                        ++overrules_count;
                    }
                    if (null != (r = this.checkForHotFixes(prefab)))
                    {   // Warns about hot-fixes
                        // Hot fixes are not that bad as overrules, but they are brute force solutions for specific problems,
                        // and cam bit if the environment changes - as a new installed add-on being written off.
                        Log.warn("Part {0} ({1}) has a hot-fix. See link {2} for details.", p.name, p.title, r);
                        ++hotfixes_count;
                    }
                    // And now we check for the ShowStoppers.
                    // These ones happens due rogue patches, added after a good installment could starts savegames, what ends up corrupting them!
                    // Since we don't have how to know when this happens, and since originally the part was working fine, we don't know
                    // how to proceeed. So the only sensible option is to scare the user enough to make him/her go to the Forum for help
                    // so we can identify the offending patch and then provide a solution that would preserve his savegame.
                    // We also stops any further processing, as we could damage something that is already damaged.
                    else if (null != (r = this.checkForShowStoppers(prefab)))
                    {   // This are situations that we should not allow the KSP to run to prevent serious corruption.
                        // This is **FAR** from being a good measure, but it's the only viable.
                        Log.error("**FATAL** Part {0} ({1}) has a fatal problem due {2}.", p.name, p.title, r);
                        ++showstoppers_count;
                        continue;
                    }

                }
                catch (Exception e)
                {
                    ++check_failures_count;
                    Log.error("part={0} ({1}) Exception on Sanity Checks: {2}", p.name, p.title, e);
                }

                // If we got here, the part is good to go, or was overruled into a sane configuration that would allow us to proceed.

                try
                {
                    TweakScale m = prefab.Modules["TweakScale"] as TweakScale;
                    m.OriginalCrewCapacity = prefab.CrewCapacity;
                    m.CalculateDryCostIfNeeded();
                    Log.dbg("Part {0} ({1}) has drycost {2} and OriginalCrewCapacity {3}",  p.name, p.title, m.DryCost, m.OriginalCrewCapacity);
                }
                catch (Exception e)
                {
                    ++check_failures_count;
                    Log.error("part={0} ({1}) Exception on writeDryCost: {2}", p.name, p.title, e);
                }
            }

            Log.info("WriteDryCost Concluded : {0} parts found ; {1} checks failed ; {2} parts with hotfixes ; {3} parts with issues overruled ; {4} Show Stoppers found; {5} Sanity Check failed; {6} unscalable parts.", total_count, check_failures_count, hotfixes_count, overrules_count, showstoppers_count, sanity_failures_count, unscalable_count);
            PrefabDryCostWriter.isConcluded = true;

            if (showstoppers_count > 0)
            {
                GUI.ShowStopperAlertBox.Show(showstoppers_count);
            }
            else
            {
                if (overrules_count > 0)            GUI.OverrulledAdviseBox.show(overrules_count);
                if (hotfixes_count > 0)             GUI.HotFixAdviseBox.show(hotfixes_count);

                if (sanity_failures_count > 0)      GUI.SanityCheckAlertBox.show(sanity_failures_count);
                if (check_failures_count > 0)       GUI.CheckFailureAlertBox.show(check_failures_count);
            }
        }

        private string checkForSanity(Part p)
        {
            Log.dbg("Checking Sanity for {0} at {1}", p.name, p.partInfo.partUrl);
            
            try {
                TweakScale m = p.Modules.GetModule<TweakScale>();
                if (m.Fields["tweakScale"].guiActiveEditor == m.Fields["tweakName"].guiActiveEditor)
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

        private string checkForShowStoppers(Part p)
        {
            Log.dbg("Checking ShowStopper for {0} at {1}", p.name, p.partInfo.partUrl);
            ConfigNode part = this.GetMeThatConfigNode(p);
            {
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
            }
            return null;
        }

        private string checkForOverrules(Part p)
        {
            Log.dbg("Checking Issue Overrule for {0} at {1}", p.name, p.partInfo.partUrl);
            ConfigNode part = this.GetMeThatConfigNode(p);
            {
                foreach (ConfigNode basket in part.GetNodes("MODULE"))
                {
                    if ("TweakScale" != basket.GetValue("name")) continue;
                    if (basket.HasValue("ISSUE_OVERRULE"))
                        return basket.GetValue("ISSUE_OVERRULE");
                }
            }
            return null;
        }

        private string checkForHotFixes(Part p)
        {
            Log.dbg("Checking Hotfixes for {0} at {1}", p.name, p.partInfo.partUrl);
            ConfigNode part = this.GetMeThatConfigNode(p);
            {
                foreach (ConfigNode basket in part.GetNodes("MODULE"))
                {
                    if ("TweakScale" != basket.GetValue("name")) continue;
                    if (basket.HasValue("HOTFIX"))
                        return System.Uri.UnescapeDataString(basket.GetValue("HOTFIX"));
                }
            }
            return null;
        }

        private ConfigNode GetMeThatConfigNode(Part p)
        {
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
                Log.info("NULL ConfigNode for {0} (unholy characters on the name?). Trying partConfig instead!", p.partInfo.partUrl);
                r = p.partInfo.partConfig;
            }
            return r;
        }
    }
}
