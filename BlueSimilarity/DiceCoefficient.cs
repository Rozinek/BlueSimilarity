#region

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;

#endregion

namespace BlueSimilarity
{
	public class DiceCoefficient : ISimilarity
	{
		#region Static and contants fields

		private const int QgramLengthDefault = 2;

		#endregion

		#region Private fields

		private readonly int _qgramLength;

		#endregion

		#region Constructors

		public DiceCoefficient()
			: this(QgramLengthDefault)
		{
		}

		public DiceCoefficient(int qgramLength)
		{
			Contract.Requires<ArgumentOutOfRangeException>(qgramLength > 0, "The q-gram length must be positive integer.");
			_qgramLength = qgramLength;
		}

		#endregion

		#region ISimilarity Members

		public double GetSimilarity(string first, string second)
		{
			return Dice(first, second, _qgramLength);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return GetSimilarity(first.Value, second.Value);
		}

		#endregion

		#region Methods (private)

		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = "Dice", CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		private static extern double Dice([In] string first, [In] string second, int qgramLength);

		#endregion
	}
}