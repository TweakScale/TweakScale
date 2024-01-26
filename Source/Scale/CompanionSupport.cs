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

using ASSET = KSPe.IO.Asset<TweakScale.Startup>;

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

		private const string    ADDONS_FILE = "AddOns-v1_1.csv";
		private readonly byte[] ADDONS_SHA = new byte[] {106, 54, 8, 141, 64, 128, 146, 1, 194, 192, 29, 196, 47, 87, 47, 167, 200, 88, 186, 48, 108, 175, 2, 224, 170, 63, 53, 104, 3, 63, 94, 61, 71, 222, 213, 153, 203, 210, 239, 196, 227, 149, 109, 228, 181, 237, 8, 242, 199, 39, 244, 8, 40, 172, 157, 38, 214, 79, 238, 169, 55, 242, 38, 31, };
		private const string    COMPANIONS_FILE = "Companions-v1_0.csv";
		private readonly byte[] COMPANIONS_SHA = new byte[] {191, 131, 69, 11, 239, 205, 197, 151, 238, 107, 90, 162, 110, 127, 73, 103, 44, 247, 253, 177, 71, 82, 207, 33, 30, 198, 183, 82, 236, 117, 24, 234, 214, 117, 131, 43, 237, 140, 40, 18, 51, 227, 21, 145, 193, 165, 251, 107, 56, 255, 61, 45, 76, 68, 192, 207, 187, 196, 203, 157, 140, 66, 236, 184, };

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
			using (System.IO.StreamReader reader = ASSET.StreamReader.CreateFor(ADDONS_FILE))
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
							Log.warn("{0} is installed, but the respective Companion {1} is not! (mandatory?={2})", addon, companion, is_mandatory);
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
