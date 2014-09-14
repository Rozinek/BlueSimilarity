#region

using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	/// Overlap coefficeint <see cref="http://en.wikipedia.org/wiki/Overlap_coefficient"/>  
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class OverlapCoefficient<T> : ISimilarity where T : IQgram
	{
		#region Private fields

		private readonly int _qgramLength;

		#endregion

		#region Constructors

		public OverlapCoefficient()
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
			return NativeEntryPoint.Overlap(first, second, _qgramLength);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return GetSimilarity(first.Value, second.Value);
		}

		#endregion
	}
}