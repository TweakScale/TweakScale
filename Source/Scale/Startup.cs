using System;

using UnityEngine;

using TweakScale.Annotations;

namespace TweakScale
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    internal class Startup : MonoBehaviour
	{
        [UsedImplicitly]
        private void Start()
        {
            Log.init();
            Log.force("Version {0}", Version.Text);

/*            if (1 == KSPe.Util.KSP.Version.Current.MAJOR && KSPe.Util.KSP.Version.Current.MINOR >= 10)
            {
                GUI.UnsupportedKSPAlertBox.Show("1.10", "115");
            }
            else*/ if (1 == KSPe.Util.KSP.Version.Current.MAJOR && KSPe.Util.KSP.Version.Current.MINOR == 9)
            {
                Type calledType = Type.GetType("KSP_Recall.Version, KSP-Recall", false, false);
                if (null == calledType) GUI.NoRecallAlertBox.Show();
            }
            else try
            {
                KSPe.Util.Compatibility.Check<Startup>(typeof(Version), typeof(Configuration));
                KSPe.Util.Installation.Check<Startup>(typeof(Version));
            }
            catch (KSPe.Util.InstallmentException e)
            {
                Log.error(e.ToShortMessage());
                KSPe.Common.Dialogs.ShowStopperAlertBox.Show(e);
            }
        }
	}

    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    internal class ModuleManagerListener : MonoBehaviour
    {
        internal static bool shouldShowWarnings = true;

        private const string MMEXPCACHE = "PluginData/ModuleManager/ConfigCache.cfg";
        private const string MMCACHE = "GameData/ModuleManager.ConfigCache";

        [UsedImplicitly]
        public static void ModuleManagerPostLoad()
        {
            try
            {
                string path = System.IO.Path.Combine(KSPUtil.ApplicationRootPath, MMEXPCACHE);
                if (!System.IO.File.Exists(path))
                {
                    path = System.IO.Path.Combine(KSPUtil.ApplicationRootPath, MMCACHE);
                    if (!System.IO.File.Exists(path)) throw new System.IO.FileNotFoundException("Module Manager cache not found!");
                }
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                System.DateTime lastmodified = fi.LastWriteTime;
                double hours = (System.DateTime.Now - lastmodified).TotalHours;
                shouldShowWarnings = (hours < 1);
            }
            catch (System.Exception e)
            {
                Log.error("Error [{0}] while checking Module Manager cache age!", e);
            }
            Log.detail("ModuleManagerPostLoad handled! shouldShowWarnings is {0}", shouldShowWarnings);
        }
    }

}
