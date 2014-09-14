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
		#region Static and contants fields

		private const string JaccardCoefficientEntry = "Jaccard";
		private const string DiceCoefficientEntry = "Dice";
		private const string OverlapCoefficientEntry = "Overlap";
		private const string LevenshteinDistanceEntry = "LevDist";
		private const string LevenshteinSimilarityEntry = "NormLevSim";
		private const string DamerauLevenshteinDistanceEntry = "DamLevDist";
		private const string DamerauLevenshteinSimilarityEntry = "NormDamLevSim";
		private const string JaroEntry = "Jaro";
		private const string JaroWinklerEntry = "JaroWinkler";

		/// <summary>
		///     The name of the interop nativ assembly name
		/// </summary>
		private const string BlueSimilarityInteropName = "BlueSimilarity.Interop.dll";

		/// <summary>
		///     Calling convention to the native entry point
		/// </summary>
		private const CallingConvention InteropCallingConvention = CallingConvention.StdCall;

		#endregion

		#region Constructors

		static NativeEntryPoint()
		{
			//Console.WriteLine("Initializate native entry point");
			LoadLibraryIfExists(BlueSimilarityInteropName);
		}

		#endregion

		#region Methods (internal)

		[DllImport(BlueSimilarityInteropName, EntryPoint = DamerauLevenshteinDistanceEntry,
			CallingConvention = InteropCallingConvention)]
		internal static extern int DamLevDist([In] string first, [In] string second);

		[DllImport(BlueSimilarityInteropName, EntryPoint = DiceCoefficientEntry,
			CallingConvention = InteropCallingConvention)]
		internal static extern double Dice([In] string first, [In] string second, int qgramLength);

		[DllImport(BlueSimilarityInteropName, EntryPoint = JaccardCoefficientEntry,
			CallingConvention = InteropCallingConvention)]
		internal static extern double Jaccard([In] string first, [In] string second, int qgramLength);

		[DllImport(BlueSimilarityInteropName, EntryPoint = JaroEntry,
			CallingConvention = InteropCallingConvention)]
		internal static extern double JaroNative([In] string first, [In] string second);

		[DllImport(BlueSimilarityInteropName, EntryPoint = JaroWinklerEntry,
			CallingConvention = InteropCallingConvention)]
		internal static extern double JaroWinklerNative([In] string first, [In] string second);

		[DllImport(BlueSimilarityInteropName, EntryPoint = LevenshteinDistanceEntry,
			CallingConvention = InteropCallingConvention)]
		internal static extern int LevDist([In] string first, [In] string second);

		[DllImport(BlueSimilarityInteropName, EntryPoint = DamerauLevenshteinSimilarityEntry,
			CallingConvention = InteropCallingConvention)]
		internal static extern double NormDamLevSim([In] string first, [In] string second);

		[DllImport(BlueSimilarityInteropName, EntryPoint = LevenshteinSimilarityEntry,
			CallingConvention = InteropCallingConvention)]
		internal static extern double NormLevSim([In] string first, [In] string second);

		/// <summary>
		///     Overlap coefficient native method
		/// </summary>
		[DllImport(BlueSimilarityInteropName, EntryPoint = OverlapCoefficientEntry,
			CallingConvention = InteropCallingConvention)]
		internal static extern double Overlap([In] string first, [In] string second, int qgramLength);

		#endregion

		#region Methods (private)

		[DllImport("Kernel32.dll")]
		private static extern IntPtr LoadLibrary(string path);

		/// <summary>
		///     Load the native library
		///     <example>
		///         e.g. unmanaged.dll
		///     </example>
		/// </summary>
		/// <param name="dllName">the file name for native dll libary</param>
		private static void LoadLibraryIfExists(string dllName)
		{
			var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
			var path = Path.GetDirectoryName(assembly.Location);
			path = Path.Combine(path, Environment.Is64BitProcess ? "x64" : "x86", dllName);

			if (File.Exists(path))
			{
				LoadLibrary(path);
				//Debug.WriteLine("Loaded native library {0} ", dllName);
			}
			else
				throw new DllNotFoundException(string.Format("Not found the native dll libary in {0}", path));
		}

		#endregion
	}
}