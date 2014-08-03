using System;
using BlueSimilarity.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.Test
{
	[TestClass]
	public class OverlapCoefficientTest
	{
		#region Private fields

		private OverlapCoefficient _overlapdCoef;

		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityTest()
		{
			const double errorTollerance = 0.001;

			var result = _overlapdCoef.GetSimilarity("abcd", "abcd");

			Assert.AreEqual(1.0, result, errorTollerance);

			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			var resultAdd = _overlapdCoef.GetSimilarity(addFirst, addSecond);
			var resultAddNorm = _overlapdCoef.GetSimilarity(new NormalizedString(addFirst), new NormalizedString(addSecond));

			var expectedAdd = GetExpectedOverlapCoefficient(3, 3, 4);
			Assert.AreEqual(expectedAdd, resultAdd, errorTollerance);
			Assert.AreEqual(expectedAdd, resultAddNorm, errorTollerance);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			var resultDel = _overlapdCoef.GetSimilarity(delFirst, delSecond);
			var resultDelNorm = _overlapdCoef.GetSimilarity(new NormalizedString(delFirst), new NormalizedString(delSecond));

			var expectedDel = GetExpectedOverlapCoefficient(2, 3, 2);
			Assert.AreEqual(expectedDel, resultDel, errorTollerance);
			Assert.AreEqual(expectedDel, resultDelNorm, errorTollerance);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			var resultSub = _overlapdCoef.GetSimilarity(subFirst, subSecond);
			var resultSubNorm = _overlapdCoef.GetSimilarity(new NormalizedString(subFirst),
				new NormalizedString(subSecond));

			var expectedSub = GetExpectedOverlapCoefficient(1, 3, 3);
			Assert.AreEqual(expectedSub, resultSub);
			Assert.AreEqual(expectedSub, resultSubNorm);

			// substitution and deletation together
			var resultMixture = _overlapdCoef.GetSimilarity("abcdxyz", "zbcdxy");

			var expectedMixture = GetExpectedOverlapCoefficient(4, 6, 5);
			Assert.AreEqual(expectedMixture, resultMixture, 0.001);
		}

		[TestInitialize]
		public void Initializate()
		{
			_overlapdCoef = new OverlapCoefficient();
		}

		#endregion

		#region Methods (private)

		private static double GetExpectedOverlapCoefficient(int intersection, int firstElements, int secondElements)
		{
			return intersection / (double)(Math.Min(firstElements, secondElements));
		}

		#endregion
	}
}
