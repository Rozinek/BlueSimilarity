#region

using System.Linq;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;
using BlueSimilarity.Indexing;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     TF-IDF
	/// </summary>
// ReSharper disable once InconsistentNaming
	public class TFIDF : IBagOfWordsSimilarity, ISemantic
	{
		#region Constructors

		/// <summary>
		/// Initializate 
		/// </summary>
		/// <param name="semanticVocabulary">The semantic vocabulary.</param>
		public TFIDF(SemanticVocabulary semanticVocabulary)
		{
			Vocabulary = semanticVocabulary;
		}

		#endregion

		#region IBagOfWordsSimilarity Members

		/// <summary>
		/// Gets the similarity between array of tokens. The position of token in array
		/// doesn't have an impact on resulting score.
		/// </summary>
		/// <param name="tokensPattern">The tokens pattern</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>
		/// the score between 0 and 1
		/// </returns>
		public double GetSimilarity(string[] tokensPattern, string[] tokensTarget)
		{
			var patternWeights = Vocabulary.GetSemanticWeight(tokensPattern);
			var targetWeights = Vocabulary.GetSemanticWeight(tokensTarget);

			return NativeEntryPoint.TFIDFNative(tokensPattern, patternWeights, tokensPattern.Length,
				tokensTarget, targetWeights, tokensTarget.Length);
		}

		/// <summary>
		/// Gets the similarity between array of tokens. The position of token in array
		/// doesn't have an impact on resulting score.
		/// </summary>
		/// <param name="tokensPattern">The tokens pattern.</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>
		/// the score between 0 and 1
		/// </returns>
		public double GetSimilarity(NormalizedString[] tokensPattern, NormalizedString[] tokensTarget)
		{
			return GetSimilarity(tokensPattern.Select(x => x.Value).ToArray(), tokensTarget.Select(x => x.Value).ToArray());
		}

		/// <summary>
		/// Gets the internal token similarity between tokens.
		/// </summary>
		/// <value>
		/// The internal token similarity.
		/// </value>
		public TokenSimilarity InternalTokenSimilarity { get { return TokenSimilarity.Exact; } }

		/// <summary>
		/// Indicates whether the bag of words similarity is symmetric. The symmetric similarity
		/// doesn't matter if the tokens passes in argument tokensPattern or tokensTarget. This satisfies
		/// metric symmetry axiom sim(x, y) = sim(y, x). Otherwise if is not symmetric then the tokensPattern will be matched
		/// as pattern.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is symmetric; otherwise, <c>false</c>.
		/// </value>
		public bool IsSymmetric
		{
			get { return true; }
		}

		/// <summary>
		/// Gets the similarity.
		/// </summary>
		/// <param name="patternTokenizer">The pattern tokenizer.</param>
		/// <param name="targetTokenizer">The target tokenizer.</param>
		/// <returns></returns>
		public double GetSimilarity(ITokenizer patternTokenizer, ITokenizer targetTokenizer)
		{
			return GetSimilarity(patternTokenizer.ToArray(), targetTokenizer.ToArray());
		}

		#endregion

		#region ISemantic Members

		/// <summary>
		/// Gets the vocabulary.
		/// </summary>
		/// <value>
		/// The vocabulary.
		/// </value>
		public SemanticVocabulary Vocabulary { get; private set; }

		#endregion
	}
}