#region

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

#endregion

namespace BlueSimilarity.Definitions
{
	internal static class NativeEntryPoint
	{
	    static  NativeEntryPoint()
	    {
			LoadLibraryIfExists(BlueSimilarityInteropName);
		}

	   /// <summary>
	   /// Load the native library
	   /// <example>
	   /// e.g. unmanaged.dll
	   /// </example>
	   /// </summary>
	   /// <param name="dllName">the file name for native dll libary</param>
	   public static void LoadLibraryIfExists(string dllName)
	   {
			var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
			var path = Path.GetDirectoryName(assembly.Location);
			path = Path.Combine(path, Environment.Is64BitProcess ? "x64" : "x86", dllName);

			if (File.Exists(path))
				LoadLibrary(path);
			else
				throw new DllNotFoundException( string.Format("Not found the native dll libary in {0}", path));
		}

		[DllImport("Kernel32.dll")]
		private static extern IntPtr LoadLibrary(string path);


		#region Static and contants fields

		internal const string JaccardCoefficientEntry = "Jaccard";
		internal const string DiceCoefficientEntry = "Dice";
		internal const string OverlapCoefficientEntry = "Overlap";
		internal const string LevenshteinDistanceEntry = "LevDist";
		internal const string LevenshteinSimilarityEntry = "NormLevSim";
		internal const string DamerauLevenshteinDistanceEntry = "DamLevDist";
		internal const string DamerauLevenshteinSimilarityEntry = "NormDamLevSim";
		internal const string JaroEntry = "Jaro";
		internal const string JaroWinklerEntry = "JaroWinkler";

		/// <summary>
		///     The name of the interop nativ assembly name
		/// </summary>
		internal const string BlueSimilarityInteropName = "BlueSimilarity.Interop.dll";

		/// <summary>
		///     Calling convention to the native entry point
		/// </summary>
		internal const CallingConvention InteropCallingConvention = CallingConvention.StdCall;

		#endregion
	}
}