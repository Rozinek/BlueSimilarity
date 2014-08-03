#region

using BlueSimilarity.Containers;
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
			const double errorTollerance = 0.001;

			var result = _diceCoef.GetSimilarity("abcd", "abcd");

			Assert.AreEqual(1.0, result, errorTollerance);

			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			var resultAdd = _diceCoef.GetSimilarity(addFirst, addSecond);
			var resultAddNorm = _diceCoef.GetSimilarity(new NormalizedString(addFirst), new NormalizedString(addSecond));

			var expectedAdd = GetExpectedDiceCoefficient(3, 3, 4);
			Assert.AreEqual(expectedAdd, resultAdd, errorTollerance);
			Assert.AreEqual(expectedAdd, resultAddNorm, errorTollerance);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			var resultDel = _diceCoef.GetSimilarity(delFirst, delSecond);
			var resultDelNorm = _diceCoef.GetSimilarity(new NormalizedString(delFirst), new NormalizedString(delSecond));

			var expectedDel = GetExpectedDiceCoefficient(2, 3, 2);
			Assert.AreEqual(expectedDel, resultDel, errorTollerance);
			Assert.AreEqual(expectedDel, resultDelNorm, errorTollerance);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			var resultSub = _diceCoef.GetSimilarity(subFirst, subSecond);
			var resultSubNorm = _diceCoef.GetSimilarity(new NormalizedString(subFirst),
				new NormalizedString(subSecond));

			var expectedSub = GetExpectedDiceCoefficient(1, 3, 3);
			Assert.AreEqual(expectedSub, resultSub);
			Assert.AreEqual(expectedSub, resultSubNorm);

			// substitution and deletation together
			var resultMixture = _diceCoef.GetSimilarity("abcdxyz", "zbcdxy");

			var expectedMixture = GetExpectedDiceCoefficient(4, 6, 5);
			Assert.AreEqual(expectedMixture, resultMixture, 0.001);
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