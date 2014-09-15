#region

using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     Jaro-Winkler method <a href="http://en.wikipedia.org/wiki/Jaro–Winkler_distance">here</a>
	/// </summary>
	public class JaroWinkler : ISimilarity
	{
		#region ISimilarity Members

		/// <summary>
		///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(Token first, Token second)
		{
			return NativeEntryPoint.JaroWinklerNative(first.Value, second.Value);
		}

		/// <summary>
		///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(string first, string second)
		{
			return NativeEntryPoint.JaroWinklerNative(first, second);
		}

		/// <summary>
		///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return NativeEntryPoint.JaroWinklerNative(first.Value, second.Value);
		}

		#endregion
	}
}