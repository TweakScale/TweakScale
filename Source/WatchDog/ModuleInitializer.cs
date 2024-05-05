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
namespace TweakScale.WatchDog
{
	public static class ModuleInitializer
	{
		public static void Initialize()
		{
			Log.force("Initialize {0}", TweakScale.WatchDog.Version.Text);
			InstallChecker ic = new InstallChecker();
			ic.Execute();
		}
	}
}
