#region

using System.Runtime.InteropServices;

#endregion

namespace BlueSimilarity.Definitions
{
	internal static class NativeEntryPoint
	{
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