#region

using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class DiceCoefficientTest
	{
		#region Private fields

		private DiceCoefficient<Unigram> _diceCoefUnigram;
		private DiceCoefficient<Bigram> _diceCoefBigram;
		private DiceCoefficient<Trigram> _diceCoefTrigram;

		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefUnigram, addFirst, addSecond, GetExpectedDiceCoefficient(4, 4, 5));
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefBigram, addFirst, addSecond, GetExpectedDiceCoefficient(3, 3, 4));
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefTrigram, addFirst, addSecond, GetExpectedDiceCoefficient(2, 2, 3));

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefUnigram, delFirst, delSecond, GetExpectedDiceCoefficient(3, 4, 3));
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefBigram, delFirst, delSecond, GetExpectedDiceCoefficient(2, 3, 2));
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefTrigram, delFirst, delSecond, GetExpectedDiceCoefficient(1, 2, 1));

			// substitution edit distance testd
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefUnigram, subFirst, subSecond, GetExpectedDiceCoefficient(3, 4, 4));
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefBigram, subFirst, subSecond, GetExpectedDiceCoefficient(1, 3, 3));
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefTrigram, subFirst, subSecond, GetExpectedDiceCoefficient(0, 2, 2));

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefUnigram, mixFirst, mixSecond, GetExpectedDiceCoefficient(6, 7, 6));
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefBigram, mixFirst, mixSecond, GetExpectedDiceCoefficient(4, 6, 5));
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoefTrigram, mixFirst, mixSecond, GetExpectedDiceCoefficient(3, 5, 4));
		}

		[TestInitialize]
		public void Initializate()
		{
			_diceCoefUnigram = new DiceCoefficient<Unigram>();
			_diceCoefBigram = new DiceCoefficient<Bigram>();
			_diceCoefTrigram = new DiceCoefficient<Trigram>();
		}

		#endregion

		#region Methods (private)

		private static double GetExpectedDiceCoefficient(int intersection, int firstElements, int secondElements)
		{
			return 2*intersection/(double) (firstElements + secondElements);
		}

		#endregion
	}
}