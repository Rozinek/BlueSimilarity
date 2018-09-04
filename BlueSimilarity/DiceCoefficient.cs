#region

using System.Collections.Generic;
using System.Linq;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     Dice coefficient see description: http://en.wikipedia.org/wiki/Sørensen–Dice_coefficient
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DiceCoefficient<T> : ISimilarity where T : IQgram
	{
		#region Private fields

		private readonly int _qgramLength;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DiceCoefficient{T}" /> class.
        /// </summary>
        public DiceCoefficient()
		{
			_qgramLength = TypeConversion.GetQgramLength<T>();
		}

		#endregion

		#region ISimilarity Members

		/// <summary>
		///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
		/// </summary>
		/// <param name="first">pattern string</param>
		/// <param name="second">text string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(Token first, Token second)
		{
			return GetSimilarity(first.Value, second.Value);
		}

		/// <summary>
		///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
		/// </summary>
		/// <param name="pattern">pattern string</param>
		/// <param name="text">text string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(string pattern, string text)
		{
		    List<string> string1_qgrams = Utils.GetQgramsVector(pattern, _qgramLength);
		    List<string> string2_qgrams = Utils.GetQgramsVector(text, _qgramLength);
		    List<string> intersection = string1_qgrams.Intersect(string2_qgrams).ToList();

		    // calculate dice coefficient
		    double dice = intersection.Count * 2 / (double)(string1_qgrams.Count + string2_qgrams.Count);

		    return dice;
        }

		/// <summary>
		///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
		/// </summary>
		/// <param name="first">pattern string</param>
		/// <param name="second">text string</param>
		/// <returns>returns the similarity score between 0 and 1</returns>
		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return GetSimilarity(first.Value, second.Value);
		}

		#endregion
	}
}