using System.Diagnostics.Contracts;
using BlueSimilarity.Containers;
using BlueSimilarity.Types;

namespace BlueSimilarity.Definitions
{
	/// <summary>
	/// Defines similarity between more tokens
	/// </summary>
	[ContractClass(typeof(BagOfTokenSimilarityContract))]
	public interface IBagOfTokenSimilarity
	{
		/// <summary>
		/// Gets the similarity between array of tokens. The position of token in array
		/// doesn't have an impact on resulting score.
		/// </summary>
		/// <param name="tokensPattern">The tokens pattern</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>the score between 0 and 1</returns>
		double GetSimilarity(string[] tokensPattern, string[] tokensTarget);

		/// <summary>
		/// Gets the similarity between array of tokens. The position of token in array
		/// doesn't have an impact on resulting score.
		/// </summary>
		/// <param name="tokensPattern">The tokens pattern.</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>the score between 0 and 1</returns>
		double GetSimilarity(NormalizedString[] tokensPattern, NormalizedString[] tokensTarget);


		/// <summary>
		/// Gets the similarity between array of tokens. The position of token in array
		/// doesn't have an impact on resulting score.
		/// </summary>
		/// <param name="tokensPattern">The tokens pattern.</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>the score between 0 and 1</returns>
		double GetSimilarity(Tokenizer tokensPattern, Tokenizer tokensTarget);

		/// <summary>
		/// Indicates whether the bag of tokens similarity is symmetric. The symmetric similarity
		/// doesn't matter if the tokens passes in argument tokensPattern or tokensTarget. This satisfies
		/// metric symmetry axiom sim(x, y) = sim(y, x). Otherwise if is not symmetric then the tokensPattern will be matched
		/// as pattern.
		/// </summary>
		/// <value><c>true</c> if this instance is symmetric; otherwise, <c>false</c>.</value>
		bool IsSymmetric { get; }


		/// <summary>
		/// Gets the internal token similarity between tokens.
		/// </summary>
		/// <value>The internal token similarity.</value>
	    TokenSimilarity InternalTokenSimilarity { get; }

	}
}