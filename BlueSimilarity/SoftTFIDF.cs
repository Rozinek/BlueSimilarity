#region

using System;
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
        private const double DefaultThreshold = 0.9;

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

		    SimMetric simMetric = SimHelpers.GetSimMetric(InternalTokenSimilarity);

		    // unit vectorizing
		    Utils.UnitVectorizing(patternWeights);
		    Utils.UnitVectorizing(targetWeights);

		    double finalScore = 0;
		    for (int p = 0; p < patternTokens.Length; p++)
		    {
		        var pattern = patternTokens[p];

		        if (pattern == null)
		        {
		            continue;
		        }

		        double maxOverToken = SimHelpers.MinimumScore;
		        double weightOverToken = 0;
		        for (int t = 0; t < targetTokens.Length; t++)
		        {
		            var target = targetTokens[t];

		            if (target == null)
		            {
		                continue;
		            }

		            double currentScore = simMetric(pattern, target);

		            if (currentScore > DefaultThreshold && currentScore > maxOverToken)
		            {
		                maxOverToken = currentScore;
		                weightOverToken = patternWeights[p] * targetWeights[t];
		            }

		            // if score achieves maximum score then breaks the loop and increases the performance
		            if (Utils.Equals(currentScore, SimHelpers.MaximumScore))
		            {
		                break;
		            }
		        }
		        finalScore += weightOverToken * maxOverToken;
		    }

		    return finalScore;
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