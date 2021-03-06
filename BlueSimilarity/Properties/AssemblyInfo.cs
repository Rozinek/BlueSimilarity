﻿#region

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

#endregion

[assembly: AssemblyTitle("BlueSimilarity")]
[assembly:
	AssemblyDescription(
		"Similarity string metric library e.g. edit distance - Levenshtein, Damerau-Levenshtein, Jaro, Jaro-Winkler, Jaccard, Dice, Overlap and bag of tokens similarity"
		)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Ondrej Rozinek")]
[assembly: AssemblyProduct("BlueSimilarity")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("4a397941-6fcd-41a7-9be5-84270934536b")]

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

[assembly: AssemblyVersion("1.0.2.0")]
[assembly: AssemblyFileVersion("1.0.2.0")]


// add internal visibility for unit test project
[assembly: InternalsVisibleTo("BlueSimilarity.Test")]