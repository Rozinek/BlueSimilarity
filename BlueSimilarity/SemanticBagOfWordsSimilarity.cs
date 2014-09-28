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
	/// The semantic similarity between more words. Important for semantic functionality is to have well learned <seealso cref="SemanticVocabulary"/>
	/// </summary>
	public class SemanticBagOfWordsSimilarity : IBagOfTokenSimilarity
	{
		#region Static and contants fields

		private const TokenSimilarity DefaultTokenSimilarity = TokenSimilarity.Levenshtein;
		private const bool DefaultIsSymmetric = false;

		#endregion

		#region Constructors


		public SemanticBagOfWordsSimilarity(SemanticVocabulary learnedVocabulary)
			: this(learnedVocabulary, DefaultTokenSimilarity, DefaultIsSymmetric)
		{
		}


		public SemanticBagOfWordsSimilarity(SemanticVocabulary learnedVocabulary, TokenSimilarity tokenSimilarity)
			: this(learnedVocabulary, tokenSimilarity, DefaultIsSymmetric)
		{
		}


		public SemanticBagOfWordsSimilarity(SemanticVocabulary learnedVocabulary, TokenSimilarity tokenSimilarity,
			bool isSymmetric)
		{
			InternalTokenSimilarity = tokenSimilarity;
			IsSymmetric = isSymmetric;
			Vocabulary = learnedVocabulary;
		}

		#endregion

		#region Properties and Indexers

		/// <summary>
		/// Gets the vocabulary.
		/// </summary>
		/// <value>The vocabulary.</value>
		public SemanticVocabulary Vocabulary { get; private set; }

		#endregion

		#region IBagOfTokenSimilarity Members

		/// <summary>
		/// Gets the semantic similarity between array of tokens. The position of token in array
		/// doesn't have an impact on resulting score.
		/// </summary>
		/// <param name="tokensPattern">The tokens pattern</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>the score between 0 and 1</returns>
		public double GetSimilarity(string[] tokensPattern, string[] tokensTarget)
		{
			var patternWeights = Vocabulary.GetSemanticWeight(tokensPattern);
			var targetWeights = Vocabulary.GetSemanticWeight(tokensTarget);

			return NativeEntryPoint.SemanticBagOfTokensSim(tokensPattern, patternWeights, tokensPattern.Length, tokensTarget,
				targetWeights, tokensTarget.Length, InternalTokenSimilarity, IsSymmetric);
		}

		/// <summary>
		/// Gets the similarity between array of tokens. The position of token in array
		/// doesn't have an impact on resulting score.
		/// </summary>
		/// <param name="tokensPattern">The tokens pattern.</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>the score between 0 and 1</returns>
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
		/// <returns>the score between 0 and 1</returns>
		public double GetSimilarity(ITokenizer tokensPattern, ITokenizer tokensTarget)
		{
			return GetSimilarity(tokensPattern.ToArray(), tokensTarget.ToArray());
		}

		public bool IsSymmetric { get; private set; }

		public TokenSimilarity InternalTokenSimilarity { get; private set; }

		#endregion
	}
}