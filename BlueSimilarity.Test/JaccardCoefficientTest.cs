#region

using BlueSimilarity.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class JaccardCoefficientTest
	{
		#region Private fields

		private JaccardCoefficient _jaccardCoef;

		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityTest()
		{
			const double errorTollerance = 0.001;

			var result = _jaccardCoef.GetSimilarity("abcd", "abcd");

			Assert.AreEqual(1.0, result, errorTollerance);

			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			var resultAdd = _jaccardCoef.GetSimilarity(addFirst, addSecond);
			var resultAddNorm = _jaccardCoef.GetSimilarity(new NormalizedString(addFirst), new NormalizedString(addSecond));

			var expectedAdd = GetExpectedJaccardCoefficient(3, 3, 4);
			Assert.AreEqual(expectedAdd, resultAdd, errorTollerance);
			Assert.AreEqual(expectedAdd, resultAddNorm, errorTollerance);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			var resultDel = _jaccardCoef.GetSimilarity(delFirst, delSecond);
			var resultDelNorm = _jaccardCoef.GetSimilarity(new NormalizedString(delFirst), new NormalizedString(delSecond));

			var expectedDel = GetExpectedJaccardCoefficient(2, 3, 2);
			Assert.AreEqual(expectedDel, resultDel, errorTollerance);
			Assert.AreEqual(expectedDel, resultDelNorm, errorTollerance);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			var resultSub = _jaccardCoef.GetSimilarity(subFirst, subSecond);
			var resultSubNorm = _jaccardCoef.GetSimilarity(new NormalizedString(subFirst),
				new NormalizedString(subSecond));

			var expectedSub = GetExpectedJaccardCoefficient(1, 3, 3);
			Assert.AreEqual(expectedSub, resultSub);
			Assert.AreEqual(expectedSub, resultSubNorm);

			// substitution and deletation together
			var resultMixture = _jaccardCoef.GetSimilarity("abcdxyz", "zbcdxy");

			var expectedMixture = GetExpectedJaccardCoefficient(4, 6, 5);
			Assert.AreEqual(expectedMixture, resultMixture, 0.001);
		}

		[TestInitialize]
		public void Initializate()
		{
			_jaccardCoef = new JaccardCoefficient();
		}

		#endregion

		#region Methods (private)

		private static double GetExpectedJaccardCoefficient(int intersection, int firstElements, int secondElements)
		{
			return intersection/(double) (firstElements + secondElements - intersection);
		}

		#endregion
	}
}