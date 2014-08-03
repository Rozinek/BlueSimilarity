#region

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class DiceCoefficientTest
	{
		#region Private fields

		private DiceCoefficient _diceCoef;

		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoef, addFirst, addSecond, GetExpectedDiceCoefficient(3, 3, 4));

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoef, delFirst, delSecond, GetExpectedDiceCoefficient(2, 3, 2));

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoef, subFirst, subSecond, GetExpectedDiceCoefficient(1, 3, 3));

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_diceCoef, mixFirst, mixSecond, GetExpectedDiceCoefficient(4, 6, 5));
		}

		[TestInitialize]
		public void Initializate()
		{
			_diceCoef = new DiceCoefficient();
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