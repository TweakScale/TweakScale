using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Tests.Checks
{
	internal static class CompanionCheck
	{
		internal static readonly Dictionary<string,string> COMPANIONS_AVAILABLE = new Dictionary<string, string>();

		private const string ADDONS_FILE = "/Users/lisias/Workspaces/KSP/GIT/net-lisias/ksp/TweakScale/GameData/TweakScale/Plugins/PluginData/AddOns-v1_1.csv";
		private const string COMPANIONS_FILE = "/Users/lisias/Workspaces/KSP/GIT/net-lisias/ksp/TweakScale/GameData/TweakScale/Plugins/PluginData/Companions-v1_0.csv";

		internal static void createDataIntegrity()
		{
			byte[] hashvalue;
			using (SHA512 sha = SHA512.Create())
			{
				using (System.IO.FileStream fs = new System.IO.FileStream(ADDONS_FILE, System.IO.FileMode.Open))
				{
					hashvalue = sha.ComputeHash(fs);
				}
				Console.WriteLine(string.Format("private const string	ADDONS_FILE = \"AddOns-v1_1.csv\";"));
				Console.WriteLine(string.Format("private readonly byte[] ADDONS_SHA = new byte[] {{{0}}};", toString(hashvalue)));
			}

			using (SHA512 sha = SHA512.Create())
			{
				using (System.IO.FileStream fs = new System.IO.FileStream(COMPANIONS_FILE, System.IO.FileMode.Open))
				{
					hashvalue = sha.ComputeHash(fs);
				}
				Console.WriteLine(string.Format("private const string	COMPANIONS_FILE = \"Companions-v1_0.csv\";"));
				Console.WriteLine(string.Format("private readonly byte[] COMPANIONS_SHA = new byte[] {{{0}}};", toString(hashvalue)));
			}
		}

		internal static void checkDataConsistency()
		{
			using (System.IO.StreamReader reader = new System.IO.StreamReader(COMPANIONS_FILE))
			{
				string[] headers = reader.ReadLine().Split('\t');
				while (!reader.EndOfStream)
				{
					string[] data = reader.ReadLine().Split('\t');
					string dir = data[2].Replace("GameDatabase::", "").Replace("::", "/");
					COMPANIONS_AVAILABLE.Add(data[0], data[1]);
				}
			}

			using (System.IO.StreamReader reader = new System.IO.StreamReader(ADDONS_FILE))
			{
				string[] headers = reader.ReadLine().Split('\t');
				while (!reader.EndOfStream)
				{
					string[] data = reader.ReadLine().Split('\t');
					string addon_name = data[2];
					Console.WriteLine(COMPANIONS_AVAILABLE[addon_name]);
				}
			}
		}

		private static string toString(byte[] hashvalue)
		{
			StringBuilder sb = new StringBuilder();
			foreach (byte b in hashvalue)
				sb.Append(string.Format("{0}, ", b));
			return sb.ToString();
		}
	}
}
