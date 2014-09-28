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
	/// </summary>
	public class SemanticBagOfWordsSimilarity : IBagOfTokenSimilarity
	{
		#region Static and contants fields

		private const TokenSimilarity DefaultTokenSimilarity = TokenSimilarity.Levenshtein;
		private const bool DefaultIsSymmetric = false;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="BagOfTokensSimilarity" /> class
		///     with default values <seealso cref="TokenSimilarity.Levenshtein" /> and <seealso cref="IsSymmetric" />
		///     false.
		/// </summary>
		public SemanticBagOfWordsSimilarity(SemanticVocabulary learnedVocabulary)
			: this(learnedVocabulary, DefaultTokenSimilarity, DefaultIsSymmetric)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="BagOfTokensSimilarity" /> class
		///     ith default value <seealso cref="IsSymmetric" /> false.
		/// </summary>
		/// <param name="learnedVocabulary"></param>
		/// <param name="tokenSimilarity">The token similarity.</param>
		public SemanticBagOfWordsSimilarity(SemanticVocabulary learnedVocabulary, TokenSimilarity tokenSimilarity)
			: this(learnedVocabulary, tokenSimilarity, DefaultIsSymmetric)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="BagOfTokensSimilarity" /> class.
		/// </summary>
		/// <param name="learnedVocabulary"></param>
		/// <param name="tokenSimilarity">The token similarity.</param>
		/// <param name="isSymmetric">if set to <c>true</c> [is symmetric].</param>
		public SemanticBagOfWordsSimilarity(SemanticVocabulary learnedVocabulary, TokenSimilarity tokenSimilarity,
			bool isSymmetric)
		{
			InternalTokenSimilarity = tokenSimilarity;
			IsSymmetric = isSymmetric;
			Vocabulary = learnedVocabulary;
		}

		#endregion

		#region Properties and Indexers

		public SemanticVocabulary Vocabulary { get; private set; }

		#endregion

		#region IBagOfTokenSimilarity Members

		public double GetSimilarity(string[] tokensPattern, string[] tokensTarget)
		{
			var patternWeights = Vocabulary.GetSemanticWeight(tokensPattern);
			var targetWeights = Vocabulary.GetSemanticWeight(tokensTarget);

			return NativeEntryPoint.SemanticBagOfTokensSim(tokensPattern, patternWeights, tokensPattern.Length, tokensTarget,
				targetWeights, tokensTarget.Length, InternalTokenSimilarity, IsSymmetric);
		}

		public double GetSimilarity(NormalizedString[] tokensPattern, NormalizedString[] tokensTarget)
		{
			return GetSimilarity(tokensPattern.Select(x => x.Value).ToArray(), tokensTarget.Select(x => x.Value).ToArray());
		}

		public double GetSimilarity(ITokenizer tokensPattern, ITokenizer tokensTarget)
		{
			return GetSimilarity(tokensPattern.ToArray(), tokensTarget.ToArray());
		}

		public bool IsSymmetric { get; private set; }
		public TokenSimilarity InternalTokenSimilarity { get; private set; }

		#endregion
	}
}