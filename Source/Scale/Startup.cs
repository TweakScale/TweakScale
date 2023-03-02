﻿/*
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
using UnityEngine;
using KSPe.Annotations;
using System.Security.Cryptography;

using ASSET = KSPe.IO.Asset<TweakScale.Startup>;
using System.Collections.Generic;
using System.Linq;

namespace TweakScale
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	internal class Startup : MonoBehaviour
	{
		internal static readonly Dictionary<string,string> COMPANIONS_AVAILABLE = new Dictionary<string, string>();
		internal static readonly HashSet<string> COMPANIONS_INSTALLED = new HashSet<string>();

		[UsedImplicitly]
		private void Start() {
			Log.force("Version {0}", Version.Text);

			try	// Check for critical artefacts first!
			{
				using (KSPe.Util.SystemTools.Assembly.Loader<TweakScale> a = new KSPe.Util.SystemTools.Assembly.Loader<TweakScale>())
				{
					if (KSPe.Util.KSP.Version.Current < KSPe.Util.KSP.Version.GetVersion(1, 4, 0))
						a.LoadAndStartup("Scale.PartDB.13x");
					else if (KSPe.Util.KSP.Version.Current < KSPe.Util.KSP.Version.GetVersion(1, 4, 4))
						a.LoadAndStartup("Scale.PartDB.14x");
					else if (KSPe.Util.KSP.Version.Current < KSPe.Util.KSP.Version.GetVersion(1, 9, 0))
						a.LoadAndStartup("Scale.PartDB.15x");
					else
						a.LoadAndStartup("Scale.PartDB.19x");
				}

				// Check if the needed Classes are available...
				KSPe.Util.SystemTools.Type.Find.ByQualifiedName("TweakScale.PartDB.StandardPartScaler");
				if (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.GetVersion(1, 4, 0))
					KSPe.Util.SystemTools.Type.Find.ByQualifiedName("TweakScale.PartDB.VariantPartScaler");
			}
			catch (System.Exception e)
			{
				Log.error(e.ToString());
				GUI.MissingDLLAlertBox.Show(e.Message);
			}

			try
			{
				KSPe.Util.Compatibility.Check<Startup>(typeof(Version), typeof(Configuration));
				KSPe.Util.Installation.Check<Startup>("Scale", "TweakScale", null, true);

				if (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.FindByVersion(1, 4, 3))
				{
					Type calledType = Type.GetType("KSP_Recall.Version, KSP-Recall", false, false);
					if (null == calledType)
					{
						GUI.NoRecallAlertBox.Show();
						return;
					}
				}

				if (KSPe.Util.KSP.Version.Current > KSPe.Util.KSP.Version.FindByVersion(1, 12, 4))
				{
					GUI.UnsupportedKSPAdviseBox.Show(KSPe.Util.KSP.Version.Current.ToString());
					return;
				}
			}
			catch (KSPe.Util.InstallmentException e)
			{
				Log.error(e.ToShortMessage());
				KSPe.Common.Dialogs.ShowStopperAlertBox.Show(e);
			}

			try
			{
				this.ensureCompanionDataIntegrity();
				this.readCompanionData();
				HashSet<string> needed = new HashSet<string>();
				using (System.IO.StreamReader reader = ASSET.StreamReader.CreateFor(ADDONS_FILE))
				{
					string[] headers = reader.ReadLine().Split('\t');
					while (!reader.EndOfStream)
					{
						string[] data = reader.ReadLine().Split('\t');
						string dir = data[0].Replace("GameData::", "").Replace("::", KSPe.IO.Path.DirectorySeparatorStr);
						string addon = data[1];
						string companion = data[2];
						if (KSPe.IO.Directory.Exists(
								KSPe.IO.Hierarchy.GAMEDATA.Solve(dir)
							))
						{
							if(!COMPANIONS_INSTALLED.Contains(companion))
							{
								Log.warn("{0} is installed, but the respective Companion {1} is not!", addon, companion);
								needed.Add(COMPANIONS_AVAILABLE[companion]);
							}
						}
					}
				}
				if (0 != needed.Count)
					GUI.MissingCompanionAlertBox.Show(needed.ToArray());
			}
			catch (Exception e)
			{
				KSPe.Common.Dialogs.ShowStopperAlertBox.Show(e.Message, "Close KSP and Reinstall TweakScale", () => { Application.Quit(); });
			}
		}

		private void ensureCompanionDataIntegrity()
		{
			byte[] hashvalue;
			using (SHA512 sha = SHA512.Create())
			{
				using (System.IO.FileStream fs = ASSET.FileStream.CreateFor(KSPe.IO.FileMode.Open, ADDONS_FILE))
				{
					hashvalue = sha.ComputeHash(fs);
				}
				if (!ADDONS_SHA.SequenceEqual(hashvalue))
					throw new System.IO.FileNotFoundException("Could not read the TweakScale Companion definition files.");
			}

			using (SHA512 sha = SHA512.Create())
			{
				using (System.IO.FileStream fs = ASSET.FileStream.CreateFor(KSPe.IO.FileMode.Open, COMPANIONS_FILE))
				{
					hashvalue = sha.ComputeHash(fs);
				}
				if (!COMPANIONS_SHA.SequenceEqual(hashvalue))
					throw new System.IO.FileNotFoundException("Could not read the TweakScale Companion definition files.");
			}
		}

		private void readCompanionData()
		{
			using (System.IO.StreamReader reader = ASSET.StreamReader.CreateFor(COMPANIONS_FILE))
			{
				string[] headers = reader.ReadLine().Split('\t');
				while (!reader.EndOfStream)
				{
					string[] data = reader.ReadLine().Split('\t');
					string dir = data[2].Replace("GameDatabase::", "").Replace("::", KSPe.IO.Path.DirectorySeparatorStr);
					COMPANIONS_AVAILABLE.Add(data[0], data[1]);
					if (KSPe.IO.Directory.Exists(
							KSPe.IO.Hierarchy.GAMEDATA.Solve(dir)
						))
					{
						COMPANIONS_INSTALLED.Add(data[0]);
						Log.detail("{0} is installed.", data[1]);
					}
				}
			}
		}

		private const string	ADDONS_FILE = "AddOns-v1_0.csv";
		private readonly byte[] ADDONS_SHA = new byte[] {36, 52, 20, 166, 60, 107, 36, 166, 132, 203, 33, 187, 64, 170, 3, 67, 228, 72, 36, 82, 0, 183, 40, 48, 206, 0, 179, 6, 248, 128, 107, 211, 229, 196, 107, 100, 55, 196, 203, 71, 110, 251, 137, 212, 77, 142, 139, 54, 186, 86, 146, 105, 39, 87, 23, 24, 77, 243, 72, 101, 74, 40, 82, 120};
		private const string	COMPANIONS_FILE = "Companions-v1_0.csv";
		private readonly byte[] COMPANIONS_SHA = new byte[] {198, 40, 206, 135, 60, 0, 10, 174, 236, 101, 197, 106, 74, 131, 238, 163, 40, 133, 133, 42, 112, 61, 193, 145, 9, 45, 139, 49, 135, 62, 173, 204, 33, 103, 6, 106, 232, 60, 35, 167, 166, 88, 248, 21, 147, 43, 182, 100, 86, 108, 123, 213, 113, 133, 238, 133, 118, 48, 22, 153, 19, 207, 17, 231};
	}
}
