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
	/// The semantic similarity between more words. Important for semantic functionality is to have well learned <seealso cref="SemanticVocabulary"/>
	/// </summary>
	public class SemanticBagOfWordsSimilarity : IBagOfWordsSimilarity
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

		    SimMetric simMetric = SimHelpers.GetSimMetric(InternalTokenSimilarity);

		    int pLen = tokensPattern.Length;
		    int tLen = tokensTarget.Length;

		    // re-calculate similarity symmetric vs. not symmetric
		    if (IsSymmetric && pLen > tLen)
		    {
		        Utils.Swap(ref tokensPattern, ref tokensTarget);
		        Utils.Swap(ref patternWeights, ref targetWeights);
		        Utils.Swap(ref pLen, ref tLen);
		    }

		    double sumOverTokens = 0;
		    double sumWeights = 0;
		    for (int p = 0; p < tokensPattern.Length; p++)
		    {
		        string pattern = tokensPattern[p];
		        double pWeight = patternWeights[p];

		        if (pattern == null)
		        {
		            continue;
		        }

		        double maxOverToken = SimHelpers.MinimumScore;
		        double weightOverToken = Math.Pow(pWeight, 2.0);

		        for (int t = 0; t < tokensTarget.Length; p++)
		        {
		            string target = tokensTarget[t];
		            double tWeight = targetWeights[t];

                    if (target == null)
		            {
		                continue;
		            }

		            double currentScore = simMetric(pattern, target);

		            if (currentScore > maxOverToken)
		            {
		                maxOverToken = currentScore;
		                weightOverToken = pWeight * tWeight;
		            }

		            // if score achieves maximum score then breaks the loop and increases the performance
		            if (Utils.Equals(currentScore, SimHelpers.MaximumScore))
		            {
		                break;
		            }

		        }
		        sumOverTokens += weightOverToken * maxOverToken;
		        sumWeights += weightOverToken;
		    }

		    return sumOverTokens / sumWeights;

        }

        /// <summary>
        /// Gets the similarity between array of tokens. The position of token in array
        /// doesn't have an impact on resulting score.
        /// </summary>
        /// <param name="tokensPattern">The tokens pattern.</param>
        /// <param name="tokensTarget">The tokens target.</param>
        /// <returns>The score between 0 and 1</returns>
        public double GetSimilarity(NormalizedString[] tokensPattern, NormalizedString[] tokensTarget)
		{
			return GetSimilarity(tokensPattern.Select(x => x.Value).ToArray(), tokensTarget.Select(x => x.Value).ToArray());
		}

		/// <summary>
		/// Gets the similarity between array of tokens. The position of the token in array
		/// doesn't have an impact on resulting score.
		/// </summary>
		/// <param name="tokensPattern">The tokens pattern.</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>The score between 0 and 1</returns>
		public double GetSimilarity(ITokenizer tokensPattern, ITokenizer tokensTarget)
		{
			return GetSimilarity(tokensPattern.ToArray(), tokensTarget.ToArray());
		}

		public bool IsSymmetric { get; private set; }

		public TokenSimilarity InternalTokenSimilarity { get; private set; }

		#endregion
	}
}