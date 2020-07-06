using System;
using UnityEngine;
using KSPe;

namespace TweakScale.GUI
{
    internal static class UnsupportedKSPAlertBox
    {
        private static readonly string MSG = @"Unfortunately TweakScale is currently not known to work correctly on KSP {0} (and newer)!

It's not certain that it will not work fine, it's **NOT KNOWN** and if anything goes wrong, KSP will inject bad information on your savegames corrupting parts with TwekScale.";

        private static readonly string AMSG = @"check the GitHub issue #{1} (KSP will close) to be updated on any news about KSP {0}";

        internal static void Show(string kspVersion, string issueNumber)
        {
            KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
                string.Format(MSG, kspVersion, issueNumber),
                AMSG,
                () => { Application.OpenURL(string.Format("https://github.com/net-lisias-ksp/TweakScale/issues/{0}", issueNumber)); Application.Quit(); }
            );
            Log.force("\"Houston, we have a Problem!\" about KSP {0} was displayed", kspVersion);
        }
    }
}