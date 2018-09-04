using System;
using System.Linq;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

namespace BlueSimilarity
{
    public class BagOfWordsSimilarity
    {
        #region Static and contants fields

        private const TokenSimilarity DefaultTokenSimilarity = TokenSimilarity.Levenshtein;
        private const bool DefaultIsSymmetric = false;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BagOfWordsSimilarity" /> class
        /// with default values <seealso cref="TokenSimilarity.Levenshtein"/> and <seealso cref="IsSymmetric"/>
        /// false.
        /// </summary>
        public BagOfWordsSimilarity() : this(DefaultTokenSimilarity, DefaultIsSymmetric)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BagOfWordsSimilarity" /> class
        /// ith default value <seealso cref="IsSymmetric"/> false.
        /// </summary>
        /// <param name="tokenSimilarity">The token similarity.</param>
        public BagOfWordsSimilarity(TokenSimilarity tokenSimilarity) : this(tokenSimilarity, DefaultIsSymmetric)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BagOfWordsSimilarity" /> class.
        /// </summary>
        /// <param name="tokenSimilarity">The token similarity.</param>
        /// <param name="isSymmetric">if set to <c>true</c> [is symmetric].</param>
        public BagOfWordsSimilarity(TokenSimilarity tokenSimilarity, bool isSymmetric)
        {
            InternalTokenSimilarity = tokenSimilarity;
            IsSymmetric = isSymmetric;
        }

        #endregion

        #region IBagOfTokenSimilarity Members

       

        /// <summary>
        ///     Gets the similarity.
        /// </summary>
        /// <param name="patternTokens">The tokens pattern.</param>
        /// <param name="targetTokens">The tokens target.</param>
        /// <returns>System.Double.</returns>
        public double GetSimilarity(string[] patternTokens, string[] targetTokens)
        {
            int m = patternTokens.Length;
            int n = targetTokens.Length;

            
            SimMetric refSimilarity = SimHelpers.GetSimMetric(InternalTokenSimilarity);

            // re-calculate similarity symmetric vs. not symmetric
            if (IsSymmetric && m > n)
            {
                Utils.Swap(ref patternTokens, ref targetTokens);
                Utils.Swap(ref m, ref n);
            }

            double sumOverTokens = 0;
            foreach (var pattern in patternTokens)
            {
                if (pattern == null) continue;

                double maxOverToken = 0;
                foreach (var target in targetTokens)
                {
                    if (target == null) continue;

                    double currentScore = refSimilarity(pattern, target);
                    
                    // if score achieves maximum score then breaks the loop and increases the performance
                    if (Utils.Equals(currentScore, SimHelpers.MaximumScore))
                    {
                        maxOverToken = SimHelpers.MaximumScore;
                        break;
                    }

                    maxOverToken = Math.Max(maxOverToken, currentScore);
                }
                sumOverTokens += maxOverToken;
            }

            return sumOverTokens / m;
        }


        /// <summary>
        ///     Gets the similarity between array of tokens. The position of token in array
        ///     doesn't have an impact on resulting score.
        /// </summary>
        /// <param name="tokensPattern">The tokens pattern.</param>
        /// <param name="tokensTarget">The tokens target.</param>
        /// <returns>the score between 0 and 1</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public double GetSimilarity(NormalizedString[] tokensPattern, NormalizedString[] tokensTarget)
        {
            return GetSimilarity(tokensPattern.Select(x => x.Value).ToArray(), tokensTarget.Select(y => y.Value).ToArray());
        }

        /// <summary>
        ///     Gets the similarity between array of tokens. The position of token in array
        ///     doesn't have an impact on resulting score.
        /// </summary>
        /// <param name="tokensPattern">The tokens pattern.</param>
        /// <param name="tokensTarget">The tokens target.</param>
        /// <returns>the score between 0 and 1</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public double GetSimilarity(ITokenizer tokensPattern, ITokenizer tokensTarget)
        {
            return GetSimilarity(tokensPattern.ToArray(), tokensTarget.ToArray());
        }

        /// <summary>
        ///     Indicates whether the bag of tokens similarity is symmetric. The symmetric similarity
        ///     doesn't matter if the tokens passes in argument patternTokens or targetTokens. This satisfies
        ///     metric symmetry axiom sim(x, y) = sim(y, x). Otherwise if is not symmetric then the patternTokens will be matched
        ///     as pattern.
        /// </summary>
        /// <value><c>true</c> if this instance is symmetric; otherwise, <c>false</c>.</value>
        public bool IsSymmetric { get; set; }


        /// <summary>
        ///     Gets the internal token similarity between tokens.
        /// </summary>
        /// <value>The internal token similarity.</value>
        public TokenSimilarity InternalTokenSimilarity { get; set; }

        #endregion
    }
}
