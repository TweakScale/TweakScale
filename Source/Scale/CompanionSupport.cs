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
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;

using ASSET = KSPe.IO.Asset<TweakScale.Startup>;

namespace TweakScale
{
	internal class CompanionSupport
	{
		internal class MandatoryCompanions : Exception
		{
			internal readonly string[] companions;
			internal MandatoryCompanions(HashSet<string> companions)
			{
				this.companions = companions.ToArray<string>();
			}
		}

		internal class NeededCompanions : Exception
		{
			internal readonly string[] companions;
			internal NeededCompanions(HashSet<string> companions)
			{
				this.companions = companions.ToArray<string>();
			}
		}

		private const string    ADDONS_FILE = "AddOns-v1_1.csv";
		private readonly byte[] ADDONS_SHA = new byte[] {201, 248, 76, 226, 223, 157, 113, 98, 126, 91, 35, 56, 127, 82, 100, 201, 196, 47, 105, 44, 41, 94, 197, 136, 138, 80, 126, 147, 96, 209, 227, 212, 123, 91, 77, 20, 57, 202, 38, 62, 209, 62, 94, 63, 93, 125, 45, 90, 231, 159, 45, 198, 93, 77, 150, 53, 251, 28, 220, 84, 164, 119, 171, 164, };
		private const string    COMPANIONS_FILE = "Companions-v1_0.csv";
		private readonly byte[] COMPANIONS_SHA = new byte[] {188, 10, 151, 173, 205, 109, 182, 12, 255, 157, 57, 233, 99, 39, 74, 236, 137, 135, 56, 27, 21, 99, 165, 201, 158, 70, 178, 70, 170, 160, 119, 44, 179, 247, 112, 241, 183, 28, 112, 160, 65, 164, 35, 28, 7, 179, 181, 86, 249, 137, 79, 40, 253, 49, 121, 156, 106, 178, 151, 14, 125, 3, 208, 136, };

		private readonly Dictionary<string,string> COMPANIONS_AVAILABLE = new Dictionary<string, string>();
		private readonly HashSet<string> COMPANIONS_INSTALLED = new HashSet<string>();
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
							Log.warn("{0} is installed, but the respective Companion {1} is not! (mandatory?={2}", addon, companion, is_mandatory);
							if (is_mandatory)	mandatory.Add(COMPANIONS_AVAILABLE[companion]);
							else				needed.Add(COMPANIONS_AVAILABLE[companion]);
						}
					}
				}
			}

			if (0 != mandatory.Count)	throw new MandatoryCompanions(mandatory);
			else if (0 != needed.Count)	throw new NeededCompanions(needed);
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

					COMPANIONS_AVAILABLE.Add(name, friendly_name);
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
	}
}
