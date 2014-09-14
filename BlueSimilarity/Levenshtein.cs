#region

using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     Levensthein algorithm <seealso cref="http://en.wikipedia.org/wiki/Levenshtein_distance" /> providing similarity
	///     measurement <see cref="ISimilarity" /> and distance measurement <see cref="IDistance" />
	/// </summary>
	public class Levenshtein : IDistance, ISimilarity
	{
		#region IDistance Members

		/// <summary>
		///     <see cref="GetDistance(string, string)" />
		/// </summary>
		/// <param name="first">the first token</param>
		/// <param name="second">the second token</param>
		/// <returns>returns the number of edit distance</returns>
		public int GetDistance(Token first, Token second)
		{
			return NativeEntryPoint.LevDist(first.Value, second.Value);
		}

		/// <summary>
		///     <see cref="GetDistance(string, string)" />
		/// </summary>
		/// <param name="first">the first normalized string</param>
		/// <param name="second">the second normalized string</param>
		/// <returns>returns the number of edit distance</returns>
		public int GetDistance(NormalizedString first, NormalizedString second)
		{
			return NativeEntryPoint.LevDist(first.Value, second.Value);
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
			return NativeEntryPoint.LevDist(first, second);
		}

		#endregion

		#region ISimilarity Members

		/// <summary>
		///     Normalized similarity from 0 to 1 where the 1 is total similarity
		/// </summary>
		/// <param name="first">the first token</param>
		/// <param name="second">the second token</param>
		/// <returns>returns the number of edit distance</returns>
		public double GetSimilarity(Token first, Token second)
		{
			return NativeEntryPoint.NormLevSim(first.Value, second.Value);
		}

		/// <summary>
		///     Normalized similarity from 0 to 1 where the 1 is total similarity
		/// </summary>
		/// <param name="first">the first token</param>
		/// <param name="second">the second token</param>
		/// <returns>returns the number of edit distance</returns>
		public double GetSimilarity(string first, string second)
		{
			return NativeEntryPoint.NormLevSim(first, second);
		}

		/// <summary>
		///     Normalized similarity from 0 to 1 where the 1 is total similarity
		/// </summary>
		/// <param name="first">the first normalized string</param>
		/// <param name="second">the second normalized string</param>
		/// <returns>returns the number of edit distance</returns>
		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return NativeEntryPoint.NormLevSim(first.Value, second.Value);
		}

		#endregion
	}
}