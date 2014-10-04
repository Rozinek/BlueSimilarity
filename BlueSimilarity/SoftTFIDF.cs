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
	/// SoftTFIDF
	/// </summary>
	public class SoftTFIDF : IBagOfWordsSimilarity, ISemantic
	{

		private const TokenSimilarity DefaultTokenSimilarity = TokenSimilarity.Jaro;
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SoftTFIDF"/> class.
		/// </summary>
		/// <param name="learnedVocabulary">The learned vocabulary.</param>
		public SoftTFIDF(SemanticVocabulary learnedVocabulary)
			: this(learnedVocabulary, DefaultTokenSimilarity)
		{			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SoftTFIDF"/> class.
		/// </summary>
		/// <param name="learnedVocabulary">The learned vocabulary.</param>
		/// <param name="tokenSimilarity">The token similarity.</param>
		public SoftTFIDF(SemanticVocabulary learnedVocabulary, TokenSimilarity tokenSimilarity)
		{
			Vocabulary = learnedVocabulary;
			InternalTokenSimilarity = tokenSimilarity;
		}

		#endregion

		#region IBagOfWordsSimilarity Members

		/// <summary>
		/// Gets the similarity.
		/// </summary>
		/// <param name="patternTokens">The pattern tokens.</param>
		/// <param name="targetTokens">The target tokens.</param>
		/// <returns></returns>
		public double GetSimilarity(string[] patternTokens, string[] targetTokens)
		{
			var patternWeights = Vocabulary.GetSemanticWeight(patternTokens);
			var targetWeights = Vocabulary.GetSemanticWeight(targetTokens);

			return NativeEntryPoint.SoftTFIDFNative(
					patternTokens, patternWeights, patternTokens.Length,
					targetTokens, targetWeights, targetTokens.Length,
					InternalTokenSimilarity);
		}

		public bool IsSymmetric
		{
			get { return true; }
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
		/// Gets the similarity between array of tokens. The position of token in array
		/// doesn't have an impact on resulting score.
		/// </summary>
		/// <param name="tokensPattern">The tokens pattern.</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>
		/// the score between 0 and 1
		/// </returns>
		public double GetSimilarity(ITokenizer tokensPattern, ITokenizer tokensTarget)
		{
			return GetSimilarity(tokensPattern.ToArray(), tokensTarget.ToArray());
		}

		/// <summary>
		/// Gets the internal token similarity between tokens.
		/// </summary>
		/// <value>
		/// The internal token similarity.
		/// </value>
		public TokenSimilarity InternalTokenSimilarity { get; private set; }

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