using System;

using Tests.Checks;

namespace Tests
{
	class MainClass
	{
		public static void Main(string[] args) {
			Console.WriteLine("Hello World!");
			EscapeDataString("https://ksp.lisias.net/add-ons/TweakScale/Support/SanityChecks/B9PartSwitch-Conflicts");
			EscapeDataString("https://ksp.lisias.net/add-ons/TweakScale/Support/SanityChecks/Firespitter-Buoyancy");
			EscapeDataString("https://github.com/net-lisias-ksp/TweakScaleCompantion_FS/issues/1");
			EscapeDataString("https://github.com/net-lisias-ksp/TweakScale/issues/12");
			EscapeDataString("https://github.com/net-lisias-ksp/TweakScaleCompantion_FuelSwitches/issues/2");
			EscapeDataString("https://ksp.lisias.net/add-ons/TweakScale/Support/SanityChecks/Configurable-Containers-Needs-Companion");
			EscapeDataString("https://ksp.lisias.net/add-ons/TweakScale/Support/SanityChecks/BlueDog_DB-Needs-Companion");
			EscapeDataString("https://ksp.lisias.net/add-ons/TweakScale/Support/SanityChecks/Tantares-Needs-Companion");

			CompanionCheck.createDataIntegrity();
			CompanionCheck.checkDataConsistency();
		}

		private static void EscapeDataString(string v) {
			Console.WriteLine(string.Format("{0} -> {1}", v, System.Uri.EscapeDataString(v)));
		}
	}
}
