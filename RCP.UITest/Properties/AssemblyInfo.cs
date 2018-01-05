using NUnit.Framework;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("RCP.UITest")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("RCP.UITest")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
// Note that the following settings can be inserted at the test fixture/class level and also at the test method level, instead of setting the entire project
// in the assembly
// Setting for running tests parallel across fixtures/classes only
[assembly: Parallelizable(ParallelScope.Fixtures)]
// Setting for running tests parallel across test methods
//[assembly: Parallelizable(ParallelScope.Children)]
// Setting the max amount of tests to be run in parallel
[assembly: LevelOfParallelism(12)]
// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("110d50ca-3ad4-4e58-a25b-2714e658cec0")]

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
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
