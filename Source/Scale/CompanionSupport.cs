/*
	This file is part of TweakScale /L
		© 2018-2024 LisiasT
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
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;

using HIERARCHY = KSPe.IO.Hierarchy<TweakScale.Startup>;

namespace TweakScale
{
	internal class CompanionSupport
	{
		internal class MandatoryCompanionsException : Exception
		{
			internal readonly string[] companions;
			internal MandatoryCompanionsException(HashSet<string> companions)
			{
				this.companions = companions.ToArray<string>();
			}
		}

		internal class DeprecatedCompanionsException : Exception
		{
			internal readonly Dictionary<string,string> companions;
			internal DeprecatedCompanionsException(Dictionary<string,string> companions)
			{
				this.companions = companions;
			}
		}

		internal class NeededCompanionsException : Exception
		{
			internal readonly string[] companions;
			internal NeededCompanionsException(HashSet<string> companions)
			{
				this.companions = companions.ToArray<string>();
			}
		}

		private const string    ADDONS_FILENAME = "AddOns-v1_1.csv";
		private readonly string ADDONS_FILE_PATH = HIERARCHY.GAMEDATA.Solve("Plugins", "PluginData", ADDONS_FILENAME);
		private readonly byte[] ADDONS_SHA = new byte[] {143, 1, 6, 134, 112, 118, 6, 41, 153, 202, 123, 146, 69, 134, 25, 251, 123, 78, 246, 11, 24, 88, 181, 221, 162, 127, 28, 10, 240, 67, 45, 137, 171, 200, 2, 242, 109, 101, 248, 68, 144, 105, 54, 95, 220, 161, 49, 89, 40, 182, 87, 140, 96, 192, 200, 98, 116, 147, 147, 86, 117, 159, 64, 150, };
		private const string    COMPANIONS_FILENAME = "Companions-v1_0.csv";
		private readonly string COMPANIONS_FILE_PATH = HIERARCHY.GAMEDATA.Solve("Plugins", "PluginData", COMPANIONS_FILENAME);
		private readonly byte[] COMPANIONS_SHA = new byte[] {242, 7, 50, 149, 126, 122, 52, 18, 43, 97, 142, 16, 135, 125, 36, 125, 230, 154, 25, 182, 197, 241, 29, 192, 187, 18, 61, 0, 201, 248, 162, 0, 205, 74, 135, 41, 201, 192, 153, 153, 105, 21, 4, 214, 66, 199, 101, 60, 112, 86, 56, 161, 10, 69, 68, 171, 225, 13, 128, 47, 70, 0, 40, 15, };

		private readonly Dictionary<string,string> COMPANIONS_AVAILABLE = new Dictionary<string, string>();
		private readonly HashSet<string> COMPANIONS_INSTALLED = new HashSet<string>();
		private readonly Dictionary<string,string> DEPRECATED_FOUND = new Dictionary<string,string>();
		private readonly bool shouldRun;

		internal CompanionSupport()
		{
			this.shouldRun = this.checkCompanionPresenseAndAge();
			if (this.shouldRun)
			{ 
				this.ensureCompanionDataIntegrity();
				this.readCompanionData();
			}
		}

		internal void Execute()
		{
			if (!this.shouldRun) return;

			if (0 != DEPRECATED_FOUND.Count)
				throw new DeprecatedCompanionsException(DEPRECATED_FOUND);

			HashSet<string> needed = new HashSet<string>();
			HashSet<string> mandatory = new HashSet<string>();
			using (System.IO.StreamReader reader = new System.IO.StreamReader(ADDONS_FILE_PATH))
			{
				string[] headers = reader.ReadLine().Split('\t');
				while (!reader.EndOfStream)
				{
					string[] data = reader.ReadLine().Split('\t');
					string dir = data[0].Replace("GameData::", "").Replace("::", KSPe.IO.Path.DirectorySeparatorStr);
					string addon = data[1];
					string companion = data[2];
					bool is_mandatory = "1" == data[6].Trim();

					if (COMPANIONS_AVAILABLE.ContainsKey(companion) && KSPe.IO.Directory.Exists(
							KSPe.IO.Hierarchy.GAMEDATA.Solve(dir)
						))
					{
						if (!COMPANIONS_INSTALLED.Contains(companion))
						{
							Log.warn("{0} is installed, but the respective Companion {1} is not! (mandatory?={2}", addon, companion, is_mandatory);
							if (is_mandatory)	mandatory.Add(COMPANIONS_AVAILABLE[companion]);
							else				needed.Add(COMPANIONS_AVAILABLE[companion]);
						}
					}
				}
			}

			if (0 != mandatory.Count)	throw new MandatoryCompanionsException(mandatory);
			else if (0 != needed.Count)	throw new NeededCompanionsException(needed);
		}

		private bool checkCompanionPresenseAndAge()
		{
			try
			{
				KSPe.Util.AddOnVersionChecker.Version v = KSPe.Util.AddOnVersionChecker.LoadVersion("TweakScaleCompanion");
				KSPe.Util.AddOnVersionChecker.Versioning today = new KSPe.Util.AddOnVersionChecker.Versioning(
					DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0
				);
				return !KSPe.Util.SystemTools.Assembly.Exists.ByName("TweakScaleCompanion") || today >= v.VERSION;
			}
			catch (Exception e)
			{
				Log.error("Could not check the presense of TweakScale Companion due {0}", e.Message);
			}
			return true;
		}

		private void ensureCompanionDataIntegrity()
		{
			byte[] hashvalue;
			using (SHA512 sha = SHA512.Create())
			{
				using (System.IO.FileStream fs = new System.IO.FileStream(ADDONS_FILE_PATH, System.IO.FileMode.Open))
				{
					hashvalue = sha.ComputeHash(fs);
				}
				if (!ADDONS_SHA.SequenceEqual(hashvalue))
					throw new System.IO.FileNotFoundException("Could not read the TweakScale Companion definition files.");
			}

			using (SHA512 sha = SHA512.Create())
			{
				using (System.IO.FileStream fs = new System.IO.FileStream(COMPANIONS_FILE_PATH, System.IO.FileMode.Open))
				{
					hashvalue = sha.ComputeHash(fs);
				}
				if (!COMPANIONS_SHA.SequenceEqual(hashvalue))
					throw new System.IO.FileNotFoundException("Could not read the TweakScale Companion definition files.");
			}
		}

		private void readCompanionData()
		{
			using (System.IO.StreamReader reader = new System.IO.StreamReader(COMPANIONS_FILE_PATH))
			{
				string[] headers = reader.ReadLine().Split('\t');
				while (!reader.EndOfStream)
				{
					string[] data = reader.ReadLine().Split('\t');
					string name = data[0];
					string friendly_name = data[1];
					string dir = data[2].Replace("GameDatabase::", "").Replace("::", KSPe.IO.Path.DirectorySeparatorStr);
					string status = data[5];

					if ("unreleased".Equals(status)) continue;

					if ("deprecated".Equals(status) && KSPe.IO.Directory.Exists(
							KSPe.IO.Hierarchy.GAMEDATA.Solve(dir)
						))
					{
						COMPANIONS_INSTALLED.Add(name);
						Log.error("Deprecated {0} was found! You need to remove this directory: {1}", friendly_name, dir);
						DEPRECATED_FOUND[name] = dir;
						continue;
					}

					COMPANIONS_AVAILABLE.Add(name, friendly_name);
					if (KSPe.IO.Directory.Exists(
							KSPe.IO.Hierarchy.GAMEDATA.Solve(dir)
						))
					{
						COMPANIONS_INSTALLED.Add(name);
						Log.detail("{0} is installed.", friendly_name);
					}
				}
			}
		}
	}
}
