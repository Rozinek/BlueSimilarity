#region

using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     Overlap coefficient see description: http://en.wikipedia.org/wiki/Overlap_coefficient
	/// </summary>
	/// <typeparam name="T"><see cref="Unigram" />, <see cref="Bigram" />, <see cref="Trigram" /></typeparam>
	public class OverlapCoefficient<T> : ISimilarity where T : IQgram
	{
		#region Private fields

		private readonly int _qgramLength;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="OverlapCoefficient{T}" /> class.
		/// </summary>
		public OverlapCoefficient()
		{
			_qgramLength = TypeConversion.GetQgramLength<T>();
		}

		#endregion

		#region ISimilarity Members

		/// <summary>
		///     Get the similarity score
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(Token first, Token second)
		{
			return GetSimilarity(first.Value, second.Value);
		}

		/// <summary>
		///     Get the similarity score
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(string first, string second)
		{
			return NativeEntryPoint.Overlap(first, second, _qgramLength);
		}

		/// <summary>
		///     Get the similarity score
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return GetSimilarity(first.Value, second.Value);
		}

		#endregion
	}
}