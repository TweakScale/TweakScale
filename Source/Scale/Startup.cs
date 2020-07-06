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
