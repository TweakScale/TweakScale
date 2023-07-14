/*
	This file is part of TweakScale /L
		© 2018-2023 LisiasT
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
using Err = KSPe.Common.Dialogs.ErrorHandling;

using SEngine = TweakScale.Sanitizer.Engine;

namespace TweakScale
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	internal class PrefabDryCostWriter : SingletonBehavior<PrefabDryCostWriter>
	{
		internal static bool isConcluded = false;

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
				if (KSPe.Util.SystemTools.Assembly.Exists.ByName("Scale_Sanitizer"))
				{
					System.Reflection.Assembly asm = KSPe.Util.SystemTools.Assembly.Find.ByName("Scale_Sanitizer");
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
			GameEvents.onGameSceneSwitchRequested.Remove(this.OnGameSceneSwitchRequested);
			Updater.Registry.Init();
			this.WriteDryCost();
		}

		private void WriteDryCost()
        {
            PrefabDryCostWriter.isConcluded = false;
            Log.info("WriteDryCost: Started");

            int total_count = 0;
            int drycost_failures_count = 0;
            int unscalable_count = 0;

            foreach (AvailablePart p in PartLoader.LoadedPartsList)
            {
                Log.detail("Procesing part named {0} ; title {1}.", p.name, p.title);
                { 
                    bool containsTweakScale = false;

                    try 
                    {
                        containsTweakScale = p.partPrefab.Modules.Contains("TweakScale");
                    }
                    catch (Exception e)
                    {
                        Log.error("Exception on {0}.prefab.Modules.Contains: {1}", p.name, e.Message);
                        Log.detail("{0}", p.partPrefab.Modules);
                        continue;
                    }

                    ++total_count;
                    if (!containsTweakScale)
                    {
                        Log.dbg("The part named {0} ; title {1} doesn't supports TweakScale. Skipping.", p.name, p.title);
                        ++unscalable_count;
                        continue;
                    }
                }
#if DEBUG
                {
                    Log.dbg("Found part named {0} ; title {1}:", p.name, p.title);
                    foreach (PartModule m in p.partPrefab.Modules)
                        Log.dbg("\tPart {0} has module {1}", p.name, m.moduleName);
                }
#endif
                SEngine.Instance.Check(p);
                try
                {   // Now we can try to calculate the DryCost. Safely.
                    TweakScale m = p.partPrefab.Modules["TweakScale"] as TweakScale;
                    m.CalculateDryCostIfNeeded();
                    Log.dbg("Part {0} ({1}) has drycost {2} and OriginalCrewCapacity {3}",  p.name, p.title, m.DryCost, p.partPrefab.CrewCapacity);
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
                m.AddRange(SEngine.Instance.GetSummary(unscalable_count));
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
                // (but I have, by now, no hard evidences yet) - but I try first the config file nevertheless. There's no point
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
