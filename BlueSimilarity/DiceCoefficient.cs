#region

using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	/// Dice coefficient <see cref="http://en.wikipedia.org/wiki/Sørensen–Dice_coefficient"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DiceCoefficient<T> : ISimilarity where T : IQgram
	{
		#region Private fields

		private readonly int _qgramLength;

		#endregion

		#region Constructors

		public DiceCoefficient()
		{
			_qgramLength = TypeConversion.GetQgramLength<T>();
		}

		#endregion

		#region ISimilarity Members

		public double GetSimilarity(Token first, Token second)
		{
			return GetSimilarity(first.Value, second.Value);
		}

		public double GetSimilarity(string first, string second)
		{
			return NativeEntryPoint.Dice(first, second, _qgramLength);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return GetSimilarity(first.Value, second.Value);
		}

		#endregion
	}
}