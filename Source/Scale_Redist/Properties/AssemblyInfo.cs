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
[assembly: AssemblyCopyright("Copyright © 2018-2022 LisiasT")]
[assembly: AssemblyTrademark("TweakScale™ by Gaius Goodspeed, Biotronic, Pellinor, LisiasT")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("01c8d239-4233-4a83-ae50-3e1a12cff502")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

// this is the API definition of TweakScale.
// Other mods can include this in their distribution and compile against it without depending on a TweakScale version.
// It does not depend on KSP and has not been touched since biotronic wrote it.
// So its version should stay at 1.0, to make clear that other mods do not need to compile against a new version
// (pellinor)
[assembly: AssemblyVersion("1.0")]
[assembly: AssemblyFileVersion(TweakScale.Version.Number)]
