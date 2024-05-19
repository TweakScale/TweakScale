/*
	This file is part of TweakScale Watch Dog
		© 2024 Lisias T : http://lisias.net <support@lisias.net>

	TweakScale Watch Dog is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	TweakScale Watchdog is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with Module Manager Watch Dog. If not, see
	<https://ksp.lisias.net/SKL-1_0.txt>.

*/
using System;

using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Reflection;

namespace TweakScale.WatchDog
{
	public static class SanityLib
	{
		/**
		 * If you need to fetch Assemblies being loaded or not (i.e., including the ones that KSP
		 * didn't managed to finish the loading by faulty dependencies), you need to use this one.
		 */
		public static IEnumerable<System.Reflection.Assembly> FetchAssembliesByName(string assemblyName)
		{
			return from a in System.AppDomain.CurrentDomain.GetAssemblies()
					where assemblyName == a.GetName().Name
					orderby a.Location ascending
					select a
				;
		}

		/**
		 * If you are interested only on assemblies that were properly loaded by KSP, this is the one you want.
		 */
		public static IEnumerable<AssemblyLoader.LoadedAssembly> FetchLoadedAssembliesByName(string assemblyName)
		{ 
			return from a in AssemblyLoader.loadedAssemblies
					let ass = a.assembly
					where assemblyName == ass.GetName().Name
					orderby a.path ascending
					select a
				;
		}

		public static bool CheckIsOnGameData(AssemblyLoader.LoadedAssembly loadedAssembly, string filename = null)
		{
			string fullpath = Path.GetFullPath(loadedAssembly.path);
			string directory = Path.GetDirectoryName(fullpath);
			string gamedata = Path.GetFullPath(GetPathFor("GameData"));
			return directory.Equals(gamedata)
				&& ((null == filename) || filename == Path.GetFileName(fullpath));
		}

		public static bool CheckIsOnRightPlace(AssemblyLoader.LoadedAssembly loadedAssembly, string filename = null)
		{
			string fullpath = Path.GetFullPath(loadedAssembly.path);
			string directory = Path.GetDirectoryName(fullpath);
			string gamedata = Path.GetFullPath(GetPathFor("GameData"));
			string tweakscale = Path.Combine(gamedata, "TweakScale");
			string plugins = Path.Combine(tweakscale, "Plugins");
			return directory.Equals(plugins)
				&& ((null == filename) || filename == Path.GetFileName(fullpath));
		}

		public static string[] GetFromConfig(string nodeName, string valueName)
		{
			nodeName = nodeName.Replace("_", "");
			ConfigNode cn = GameDatabase.Instance.GetConfigNode("ModuleManagerWatchDog/Plugins/"+nodeName+"/"+nodeName);
			if (null == cn) return new string[]{};
			if ("WatchDog" != cn.name) return new string[]{};
			return cn.GetValues(valueName);
		}

		public static string GetPathFor(string path, params string[] paths)
		{
			string r = Path.GetFullPath(KSPUtil.ApplicationRootPath);
			r = Path.Combine(r, path);
			foreach (string p in paths)
				r = Path.Combine(r, p);
			return r;
		}

		internal static bool MatchTradeMark(AssemblyLoader.LoadedAssembly asm, string asmProd)
		{
			string assemblyCompany = null;
			string assemblyProduct = null;
			string assemblyTrademark = null;

			try
			{ 
				object[] attribs = asm.assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
				if (attribs.Length < 1) return false;
				assemblyCompany = ((AssemblyCompanyAttribute)attribs[0]).Company;
				Log.detail("assemblyCompany {0}", assemblyCompany);
			}
			catch (Exception e)
			{
				Log.error("{0}\n{1}", e.ToString(), e.StackTrace);
			}

			try
			{ 
				object[] attribs = asm.assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), true);
				if (attribs.Length < 1) return false;
				assemblyProduct = ((AssemblyProductAttribute)attribs[0]).Product;
				Log.detail("assemblyProduct {0}", assemblyProduct);
			}
			catch (Exception e)
			{
				Log.error("{0}\n{1}", e.ToString(), e.StackTrace);
			}

			try
			{ 
				object[] attribs = asm.assembly.GetCustomAttributes(typeof(AssemblyTrademarkAttribute), true);
				if (attribs.Length < 1) return false;
				assemblyTrademark = ((AssemblyTrademarkAttribute)attribs[0]).Trademark;
				Log.detail("assemblyTrademark {0}", assemblyTrademark);
			}
			catch (Exception e)
			{
				Log.error("{0}\n{1}", e.ToString(), e.StackTrace);
			}

			bool r = true;
			r = r && TweakScale.WatchDog.LegalMamboJambo.Company.Equals(assemblyCompany);
			Log.dbg("{0}", r);
			r = r && asmProd.Equals(assemblyProduct);
			Log.dbg("{0}", r);
			r = r && assemblyTrademark.Contains("TweakScale™");
			Log.dbg("{0}", r);
			r = r && assemblyTrademark.Contains("LisiasT");
			Log.dbg("{0}", r);

			return r;
		}

		internal static bool MatchAssemblyVersion(AssemblyLoader.LoadedAssembly asm, string asmVersion)
		{
			string assemblyVersion = null;
			try
			{ 
				assemblyVersion = asm.assembly.GetName().Version.ToString();
				Log.detail("assemblyVersion {0}", assemblyVersion);
			}
			catch (Exception e)
			{
				Log.error("{0}\n{1}", e.ToString(), e.StackTrace);
			}

			return assemblyVersion.StartsWith(asmVersion);
		}
	}
}
