/*
	This file is part of TweakScale Watch Dog
		© 2023 Lisias T : http://lisias.net <support@lisias.net>

	TweakScale Watch Dog is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	TweakScale Watchdog is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with Module Manager Watch Dog. If not, see
	<https://ksp.lisias.net/SKL-1_0.txt>.

*/
namespace TweakScale.WatchDog
{
	internal static class ErrorMessage
	{
		public static readonly string ERR_TSWD_DUPLICATED = "There're more than one TweakScale Watch Dog on this KSP installment! Please delete all but the one you intend to use!";
		public static readonly string ERR_TSWD_WRONGPLACE = "TweakScale.WatchDog.dll <b>must be</b> on GameData/TweakScale/Plugins and not anywhere else. You need to fix your installation!";
		public static readonly string ERR_TSREDIST_ABSENT = "There's no Scale_Redist on this KSP installment! You need to properly install TweakScale!";
		public static readonly string ERR_TSREDIST_DUPLICATED = "There're more than one Scale_Redist on this KSP installment! Please delete all but the GameData/999_Scale_Redist.dll one.";
		public static readonly string ERR_TSREDIST_WRONGPLACE = "999_Scale_Redist.dll <b>must be</b> directly on GameData and not inside any subfolder or on other filename. You need to fix your installation!";
		public static readonly string ERR_MM_ABSENT = "There's no Module Manager on this KSP installment! You need to install Module Manager!";
		public static readonly string ERR_MM_WRONGPLACE = "ModuleManager.dll <b>must be</b> directly on GameData and not inside any subfolder. Please move ModuleManager.dll directly into GameData.";
	}
}
