#region

using System.Runtime.InteropServices;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     Levensthein algorithm providing similarity measurement <see cref="ISimilarity" />
	///     and distance measurement <see cref="IDistance" />
	/// </summary>
	public class Levenshtein : IDistance, ISimilarity
	{

		#region IDistance Members

		/// <summary>
		///     Levenshtein distance returns the number of edit operations
		///     (addition, deletation, substition) which are needed for transformation
		///     from one string to another
		///     <example>
		///         return 1 for deletation character: ABC => AC
		///         return 1 for substitution character ABC => AXC
		///         return 1 for addition character ABC => ABCD
		///     </example>
		/// </summary>
		/// <param name="first">the first string</param>
		/// <param name="second">the second string</param>
		/// <returns>returns the number of edit distance</returns>
		public int GetDistance(NormalizedString first, NormalizedString second)
		{
			return LevDist(first.Value, second.Value);
		}

		/// <summary>
		///     Levenshtein distance returns the number of edit operations
		///     (addition, deletation, substition) which are needed for transformation
		///     from one string to another
		///     <example>
		///         return 1 for deletation character: ABC => AC
		///         return 1 for substitution character ABC => AXC
		///         return 1 for addition character ABC => ABCD
		///     </example>
		/// </summary>
		/// <param name="first">the first string</param>
		/// <param name="second">the second string</param>
		/// <returns>returns the number of edit distance</returns>
		public int GetDistance(string first, string second)
		{
			return LevDist(first, second);
		}

		#endregion

		#region ISimilarity Members

		/// <summary>
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public double GetSimilarity(string first, string second)
		{
			return NormLevSim(first, second);
		}

		/// <summary>
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return NormLevSim(first.Value, second.Value);
		}

		#endregion

		#region Methods (private)

		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.LevenshteinDistanceEntry, CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		private static extern int LevDist([In] string first, [In] string second);

		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.LevenshteinSimilarityEntry, CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		private static extern double NormLevSim([In] string first, [In] string second);

		#endregion
	}
}