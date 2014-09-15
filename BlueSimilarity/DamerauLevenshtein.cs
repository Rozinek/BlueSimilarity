#region

using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     Damerau - Levenshtein algorithm providing similarity measurement <see cref="ISimilarity" />
	///     and distance measurement <see cref="IDistance" />
	///     <a href="http://en.wikipedia.org/wiki/Damerau–Levenshtein_distance">here</a>
	/// </summary>
	public class DamerauLevenshtein : IDistance, ISimilarity
	{
		#region IDistance Members

		/// <summary>
		///     Offers the same behavior as <see cref="Levenshtein.GetDistance(string, string)" />
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
			return NativeEntryPoint.DamLevDist(first, second);
		}

		/// <summary>
		///     Offers the same behavior as <see cref="Levenshtein.GetDistance(NormalizedString, NormalizedString)" />
		///     and extends for transposition of two character that will have only 1 distance
		/// </summary>
		/// <example>
		///     return 1 for transposition two character ABC => ACB
		/// </example>
		/// <param name="first">the first normalized string</param>
		/// <param name="second">the second normalized string</param>
		/// <returns>return the number of edit distance</returns>
		public int GetDistance(NormalizedString first, NormalizedString second)
		{
			return NativeEntryPoint.DamLevDist(first.Value, second.Value);
		}


		/// <summary>
		///     Offers the same behavior as <see cref="Levenshtein.GetDistance(Token, Token)" />
		///     and extends for transposition of two character that will have only 1 distance
		/// </summary>
		/// <example>
		///     return 1 for transposition two character ABC => ACB
		/// </example>
		/// <param name="first">the first normalized string</param>
		/// <param name="second">the second normalized string</param>
		/// <returns>return the number of edit distance</returns>
		public int GetDistance(Token first, Token second)
		{
			return NativeEntryPoint.DamLevDist(first.Value, second.Value);
		}

		#endregion

		#region ISimilarity Members

		/// <summary>
		///     Get the similarity score
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(Token first, Token second)
		{
			return NativeEntryPoint.NormDamLevSim(first.Value, second.Value);
		}


		/// <summary>
		///     Get the similarity score
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(string first, string second)
		{
			return NativeEntryPoint.NormDamLevSim(first, second);
		}

		/// <summary>
		///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return NativeEntryPoint.NormDamLevSim(first.Value, second.Value);
		}

		#endregion
	}
}