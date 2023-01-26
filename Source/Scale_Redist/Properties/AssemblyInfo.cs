using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Scale_Redist")]
[assembly: AssemblyDescription("TweakScale /L Redistributable Assembly")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("L Aerospace/KSP Division")]
[assembly: AssemblyProduct("TweakScale /L Redistributable")]
[assembly: AssemblyCopyright("Copyright © 2018-2023 LisiasT")]
[assembly: AssemblyTrademark("TweakScale™ by Gaius Goodspeed, Biotronic, Pellinor, LisiasT")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("01c8d239-4233-4a83-ae50-3e1a12cff502")]

/*
 * This is the API definition of TweakScale.
 * 
 * Originally, other mods could include this in their distribution and compile against it without depending on a TweakScale version,
 * but having more than one copy of the ScaleRedist (or any other Assembly) is known to cause performance issues on KSP < 1.8, and to
 * plain cause problems on KSP >= 1.8.
 * 
 * So from now on, only one copy should exist on the GameData's root.
 *
 * Change History:
 *	* 1.2
 *		+ Added Priorizable IRescaler and IRescaler<> alternatives:
 *			- IPriorityRescalable & IPriorityRescalable<> for Recalers that should have precedence on everything else
 *			- ISecondaryRescalable & ISecondaryRescalable<> for Recalers that should be executed after everything else
 *		+ Added IUpdateable
 *			- To be implemented by Rescalers that need to be called on every OnUpdate
 *		+ Added TweakScale.Updater.Abstract class
 *			- An optional template to be used by Custom Rescalers
 *			- TweakScale's default ones use it.
 *	* 1.1
 *		+ Added ISanityCheck
 *			- Interface to allow implementing Custom Sanity Checks to be called by TweakScale automatically.
 *		+ Dependency on the KSP's Assembly-CSharp was added.
 *	* 1.0
 *		+ Original Interfaces from Biotronic times.
 *			- IRescalable & IRescalable<>
 */
[assembly: AssemblyVersion("1.2")]
[assembly: AssemblyFileVersion(TweakScale.Version.Number)]
