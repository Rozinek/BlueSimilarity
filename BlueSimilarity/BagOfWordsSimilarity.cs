#region

using System.Linq;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     The similarity between more tokens (words)
	/// </summary>
	public class BagOfWordsSimilarity : IBagOfWordsSimilarity
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
		/// <param name="tokensPattern">The tokens pattern.</param>
		/// <param name="tokensTarget">The tokens target.</param>
		/// <returns>System.Double.</returns>
		public double GetSimilarity(string[] tokensPattern, string[] tokensTarget)
		{
			return NativeEntryPoint
				.BagOfTokensSim(tokensPattern, tokensPattern.Length, tokensTarget, tokensTarget.Length,
					InternalTokenSimilarity, IsSymmetric);
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
			return NativeEntryPoint.BagOfTokensSimStruct(tokensPattern, tokensPattern.Length,
				tokensTarget, tokensTarget.Length,
				InternalTokenSimilarity, IsSymmetric);
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
		///     doesn't matter if the tokens passes in argument tokensPattern or tokensTarget. This satisfies
		///     metric symmetry axiom sim(x, y) = sim(y, x). Otherwise if is not symmetric then the tokensPattern will be matched
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