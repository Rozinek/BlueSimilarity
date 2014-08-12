﻿#region

using System.Runtime.InteropServices;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     Damerau - Levensthein algorithm providing similarity measurement <see cref="ISimilarity" />
	///     and distance measurement <see cref="IDistance" />
	/// </summary>
	public class DamerauLevenshtein : IDistance, ISimilarity
	{
		#region IDistance Members

		/// <summary>
		///     Offers the same behaviour as <see cref="Levenshtein.GetDistance(string, string)" />
		///     and extends for transposition of two character that will have only 1 distance
		/// </summary>
		/// <example>
		///     return 1 for transposition two character ABC => ACB
		/// </example>
		/// <param name="first">the first string</param>
		/// <param name="second">the second string</param>
		/// <returns>return the number of edit distance</returns>
		public int GetDistance(string first, string second)
		{
			return DamLevDist(first, second);
		}

		/// <summary>
		///     Offers the same behaviour as <see cref="Levenshtein.GetDistance(NormalizedString, NormalizedString)" />
		///     and extends for transposition of two character that will have only 1 distance
		/// </summary>
		/// <example>
		///     return 1 for transposition two character ABC => ACB
		/// </example>
		/// <param name="first">the first string</param>
		/// <param name="second">the second string</param>
		/// <returns>return the number of edit distance</returns>
		public int GetDistance(NormalizedString first, NormalizedString second)
		{
			return DamLevDist(first.Value, second.Value);
		}

		public int GetDistance(Token first, Token second)
		{
			return DamLevDist(first.Value, second.Value);
		}

		#endregion

		#region ISimilarity Members

		public double GetSimilarity(Token first, Token second)
		{
			return NormDamLevSim(first.Value, second.Value);
		}

		public double GetSimilarity(string first, string second)
		{
			return NormDamLevSim(first, second);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return NormDamLevSim(first.Value, second.Value);
		}

		#endregion

		#region Methods (private)

		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.DamerauLevenshteinDistanceEntry,
			CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		private static extern int DamLevDist([In] string first, [In] string second);

		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.DamerauLevenshteinSimilarityEntry,
			CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		private static extern double NormDamLevSim([In] string first, [In] string second);

		#endregion
	}
}