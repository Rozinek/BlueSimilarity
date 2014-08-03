using BlueSimilarity.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.Test
{
	[TestClass]
	public class LevenshteinTest
	{
		private static Levenshtein _levensthein;

		[TestInitialize]
		public void Initialize()
		{
			_levensthein = new Levenshtein();
		}

		[TestMethod]
		public void GetDistanceTest()
		{
			
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";

			var resultAdd = _levensthein.GetDistance(addFirst, addSecond);
			var resultAddNorm = _levensthein.GetDistance(new NormalizedString(addFirst), new NormalizedString(addSecond));			
			Assert.AreEqual(1, resultAdd);
			Assert.AreEqual(1, resultAddNorm);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			var resultDel = _levensthein.GetDistance(delFirst, delSecond);
			var resultDelNorm = _levensthein.GetDistance(new NormalizedString(delFirst),
				new NormalizedString(delSecond));
			Assert.AreEqual(1, resultDel);
			Assert.AreEqual(1, resultDelNorm);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			var resultSub = _levensthein.GetDistance(subFirst, subSecond);
			var resultSubNorm = _levensthein.GetDistance(new NormalizedString(subFirst),
				new NormalizedString(subSecond));
			Assert.AreEqual(1, resultSub);
			Assert.AreEqual(1, resultSubNorm);

			// substitution and deletation together
			var resultMixture = _levensthein.GetDistance("abcdxyz", "zbcdxy");
			Assert.AreEqual(2, resultMixture);
		}

		[TestMethod]
		public void GetSimilarityTest()
		{
			const double errorTollerance = 1E-05;

			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			var resultAdd = _levensthein.GetSimilarity(addFirst, addSecond);
			var resultAddNorm = _levensthein.GetSimilarity(new NormalizedString(addFirst), new NormalizedString(addSecond));
			Assert.AreEqual(0.8, resultAdd, errorTollerance);
			Assert.AreEqual(0.8, resultAddNorm, errorTollerance);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			var resultDel = _levensthein.GetSimilarity(delFirst, delSecond);
			var resultDelNorm = _levensthein.GetSimilarity(new NormalizedString(delFirst),
				new NormalizedString(delSecond));
			Assert.AreEqual(0.75, resultDel, errorTollerance);
			Assert.AreEqual(0.75, resultDelNorm, errorTollerance);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			var resultSub = _levensthein.GetSimilarity(subFirst, subSecond);
			var resultSubNorm = _levensthein.GetSimilarity(new NormalizedString(subFirst),
				new NormalizedString(subSecond));
			Assert.AreEqual(0.75, resultSub);
			Assert.AreEqual(0.75, resultSubNorm);

			// substitution and deletation together
			var resultMixture = _levensthein.GetSimilarity("abcdxyz", "zbcdxy");
			Assert.AreEqual(0.714, resultMixture, 0.001);
		}
	}
}
