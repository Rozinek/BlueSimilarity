﻿#region

using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	/// Jaccard coefficient <see cref="http://en.wikipedia.org/wiki/Jaccard_coefficient"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class JaccardCoefficient<T> : ISimilarity where T : IQgram
	{
		#region Private fields

		private readonly int _qgramLength;

		#endregion

		#region Constructors

		public JaccardCoefficient()
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
			return NativeEntryPoint.Jaccard(first, second, _qgramLength);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return GetSimilarity(first.Value, second.Value);
		}

		#endregion

	}
}