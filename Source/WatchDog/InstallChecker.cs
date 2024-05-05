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
using System.Collections.Generic;
using System.Linq;

namespace TweakScale.WatchDog
{
	internal class InstallChecker
	{
		// Ugly hack. :)
		internal class EnvironmentSaneException : Exception
		{
			public EnvironmentSaneException(string message) : base(message) {  }
		}

		internal void Execute()
		{
			try
			{
				// Always check for being the unique Assembly loaded. This will avoid problems in the future.
				String msg = this.CheckMyself();
				if (null != msg)
					Log.detail("{0} is present and correctly installed.", this.GetType().Name);

				if (null == msg)
					 msg = this.CheckScaleRedist();
				else
					Log.detail("Scale_Redist is present and correctly installed.");

				if (null == msg)
					 msg = this.CheckTweakScale();
				else
					Log.detail("TweakScale is present and correctly installed.");

				if (null == msg)	// If MMWD is installed, there's nothing we need to do.
					msg = this.CheckModuleManagerWatchDog();
				else
					Log.detail("ModuleManagerWatchDog is present and correctly installed.");

				if (null == msg)
					msg = this.CheckModuleManager();
				else
					Log.detail("ModuleManager is present and correctly installed.");

				Handle(msg);
			}
			catch (EnvironmentSaneException e)
			{
				Log.detail("System considered sane due \"{0}\". No further testings neeed.", e.Message);
				return;
			}
			catch (Exception e)
			{
				Log.error(e.ToString());
				GUI.Dialogs.ShowStopperAlertBox.Show(e.ToString());
			}
		}

		private void Handle(string msg)
		{
			if (null != msg)
				GUI.Dialogs.ShowStopperAlertBox.Show(msg);
		}

		private const string MM_ASSEMBLY_NAME = "ModuleManager";
		private const string MM_MYFORK_ASMTITTLE = "Module Manager /L";
		private const string MMWD_ASSEMBLY_NAME = "ModuleManagerWatchDog";
		private const string TSREDIST_ASSEMBLY_NAME = "Scale_Redist";
		private const string TSREDIST_ASSEMBLY_FILENAME = "999_Scale_Redist.dll";
		private const string TS_ASSEMBLY_NAME = "Scale";

		private string CheckMyself()
		{
			IEnumerable<AssemblyLoader.LoadedAssembly> loaded = SanityLib.FetchLoadedAssembliesByName(this.GetType().Assembly.GetName().Name);

#if DEBUG
			Log.dbg("CheckMyself");
			foreach (AssemblyLoader.LoadedAssembly la in loaded)
				Log.dbg("{0} :: {1}", la.assembly.FullName, la.assembly.Location);
#endif

			// Obviously, would be pointless to check for it not being installed! (0 == count). :)
			if (1 != loaded.Count()) return ErrorMessage.ERR_TSWD_DUPLICATED;
			if (!SanityLib.CheckIsOnRightPlace(loaded.First()))
				return ErrorMessage.ERR_TSWD_WRONGPLACE;
			return null;
		}

		private string CheckModuleManagerWatchDog()
		{
			IEnumerable<AssemblyLoader.LoadedAssembly> loaded = SanityLib.FetchLoadedAssembliesByName(MMWD_ASSEMBLY_NAME);

#if DEBUG
			Log.dbg("CheckModuleManagerWatchDog");
			foreach (AssemblyLoader.LoadedAssembly la in loaded)
				Log.dbg("{0} :: {1}", la.assembly.FullName, la.assembly.Location);
#endif

			if (0 == loaded.Count()) return ErrorMessage.ERR_MM_ABSENT;
			if (this.IsModuleManagerMyFork()) throw new EnvironmentSaneException("MMWD is present");

			return null;
		}

		private string CheckModuleManager()
		{
			IEnumerable<AssemblyLoader.LoadedAssembly> loaded = SanityLib.FetchLoadedAssembliesByName(MM_ASSEMBLY_NAME);

#if DEBUG
			Log.dbg("CheckModuleManager");
			foreach (AssemblyLoader.LoadedAssembly la in loaded)
				Log.dbg("{0} :: {1}", la.assembly.FullName, la.assembly.Location);
#endif

			if (0 == loaded.Count()) return ErrorMessage.ERR_MM_ABSENT;
			if (this.IsModuleManagerMyFork()) throw new EnvironmentSaneException("MM/L is present");

			return null;
		}

		private bool IsModuleManagerMyFork()
		{
			IEnumerable<System.Reflection.Assembly> loaded = SanityLib.FetchAssembliesByName(MM_ASSEMBLY_NAME);
#if DEBUG
			Log.dbg("IsModuleManagerMyFork");
			foreach (System.Reflection.Assembly a in loaded)
				Log.dbg("{0} :: {1}", a.FullName, a.Location);
#endif
			System.Reflection.Assembly assembly = loaded.First();
			System.Reflection.AssemblyTitleAttribute attributes = (System.Reflection.AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(System.Reflection.AssemblyTitleAttribute), false);
			string assemblyTittle = attributes.Title ?? "";
			Log.dbg("First ({0}) = {1} :: {2}", assemblyTittle, assembly.FullName, assembly.Location);
			return (assemblyTittle.StartsWith(MM_MYFORK_ASMTITTLE));
		}

		private string CheckScaleRedist()
		{
			IEnumerable<AssemblyLoader.LoadedAssembly> loaded = SanityLib.FetchLoadedAssembliesByName(TSREDIST_ASSEMBLY_NAME);

#if DEBUG
			Log.dbg("CheckScaleRedist");
			foreach (AssemblyLoader.LoadedAssembly la in loaded)
				Log.dbg("{0} :: {1}", la.assembly.FullName, la.assembly.Location);
#endif

			if (0 == loaded.Count()) return ErrorMessage.ERR_TSREDIST_ABSENT;
			if (1 != loaded.Count()) return ErrorMessage.ERR_TSREDIST_DUPLICATED;
			if (!SanityLib.CheckIsOnGameData(loaded.First(), TSREDIST_ASSEMBLY_FILENAME))
				return ErrorMessage.ERR_TSREDIST_WRONGPLACE;
			if (!SanityLib.MatchTradeMark(loaded.First(), "TweakScale™ /L Redistributable"))
				return ErrorMessage.ERR_TSREDIST_ALIEN;
			if (!SanityLib.MatchAssemblyVersion(loaded.First(), TweakScale.RedistVersion.AssemblyVersion))
				return ErrorMessage.ERR_TSREDIST_DEPRECATED;

			return null;
		}

		private string CheckTweakScale()
		{
			IEnumerable<AssemblyLoader.LoadedAssembly> loaded = SanityLib.FetchLoadedAssembliesByName(TS_ASSEMBLY_NAME);

#if DEBUG
			Log.dbg("CheckTweakScale");
			foreach (AssemblyLoader.LoadedAssembly la in loaded)
				Log.dbg("{0} :: {1}", la.assembly.FullName, la.assembly.Location);
#endif

			if (0 == loaded.Count()) return ErrorMessage.ERR_TS_ABSENT;
			if (1 != loaded.Count()) return ErrorMessage.ERR_TS_DUPLICATED;
			if (!SanityLib.MatchTradeMark(loaded.First(), "TweakScale /L"))
				return ErrorMessage.ERR_TS_ALIEN;

			return null;
		}

	}
}
