#region

using System.Diagnostics.Contracts;
using BlueSimilarity.Contracts;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Definitions
{
	/// <summary>
	///     Methods for measurement similarity between 0 and 1 where 0 is total dissimilarity
	///     and 1 are full similar strings with regards to used algorithm
	/// </summary>
	[ContractClass(typeof (SimilarityContract))]
	public interface ISimilarity
	{
		#region Methods (public)

		/// <summary>
		///     Get the similarity score
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		double GetSimilarity(string first, string second);

		/// <summary>
		///     Get the similarity score
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		double GetSimilarity(NormalizedString first, NormalizedString second);

		/// <summary>
		///     Get the similarity score
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		double GetSimilarity(Token first, Token second);

		#endregion
	}
}