using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Scale_Redist")]
[assembly: AssemblyDescription("TweakScale /L Redistributable Assembly")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(TweakScale.Redist.LegalMamboJambo.Company)]
[assembly: AssemblyProduct(TweakScale.Redist.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright(TweakScale.Redist.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark(TweakScale.Redist.LegalMamboJambo.Trademark)]
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
 *	* 1.0
 *		+ Original Interfaces from Biotronic times.
 *			- IRescalable & IRescalable<>
 */
[assembly: AssemblyVersion(TweakScale.RedistVersion.AssemblyVersion)]
[assembly: AssemblyFileVersion(TweakScale.RedistVersion.Number)]
