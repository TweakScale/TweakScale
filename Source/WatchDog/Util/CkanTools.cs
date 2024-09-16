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
using SIO = System.IO;

namespace TweakScale.WatchDog.Util
{
	public static class CkanTools
	{
		private static bool? is_ckan_installed = null;
		public static bool CheckCkanInstalled()
		{
			if (null != is_ckan_installed) return (bool)is_ckan_installed;

			string path = SIO.Path.Combine(KSPUtil.ApplicationRootPath, "CKAN");
			is_ckan_installed = SIO.Directory.Exists(path);
			path = SIO.Path.Combine(path, "registry.json");
			is_ckan_installed &= SIO.File.Exists(path);
			return (bool)is_ckan_installed;
		}

		private static bool? is_ckan_repository = null;
		public static bool CheckCkanRepository()
		{
			if (null != is_ckan_repository) return (bool)is_ckan_repository;

			is_ckan_repository = CheckCkanInstalled();
			if ((bool)is_ckan_repository)
			{
				string path = SIO.Path.Combine(KSPUtil.ApplicationRootPath, "CKAN");
				path = SIO.Path.Combine(path, "registry.json");

				string text = SIO.File.ReadAllText(path);
				is_ckan_repository = text.Contains("https://github.com/KSP-CKAN/");
			}

			return (bool)is_ckan_repository;
		}
	}
}
