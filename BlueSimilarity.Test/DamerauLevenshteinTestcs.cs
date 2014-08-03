using BlueSimilarity.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.Test
{

	[TestClass]
	public class DamerauLevenshteinTestcs
	{
		private DamerauLevenshtein _damerauLevenshtein;

		[TestInitialize]
		public void Initializate()
		{
			_damerauLevenshtein = new DamerauLevenshtein();
		}

		[TestMethod]
		public void DamerauLevenshteinDistanceTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			var resultAdd = _damerauLevenshtein.GetDistance(addFirst, addSecond);
			var resultAddNorm = _damerauLevenshtein.GetDistance(new NormalizedString(addFirst), new NormalizedString(addSecond));
			Assert.AreEqual(1, resultAdd);
			Assert.AreEqual(1, resultAddNorm);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			var resultDel = _damerauLevenshtein.GetDistance(delFirst, delSecond);
			var resultDelNorm = _damerauLevenshtein.GetDistance(new NormalizedString(delFirst),
				new NormalizedString(delSecond));
			Assert.AreEqual(1, resultDel);
			Assert.AreEqual(1, resultDelNorm);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			var resultSub = _damerauLevenshtein.GetDistance(subFirst, subSecond);
			var resultSubNorm = _damerauLevenshtein.GetDistance(new NormalizedString(subFirst),
				new NormalizedString(subSecond));
			Assert.AreEqual(1, resultSub);
			Assert.AreEqual(1, resultSubNorm);

			// substitution and deletation together
			var resultMixture = _damerauLevenshtein.GetDistance("abcdxyz", "zbcdxy");
			Assert.AreEqual(2, resultMixture);

			// transposition
			var resultTransposition = _damerauLevenshtein.GetDistance("abcd", "acbd");
			Assert.AreEqual(1,resultTransposition);
		}

		[TestMethod]
		public void NormalizedDamerauLevenshteinDistanceTest()
		{
			const double errorTollerance = 1E-05;

			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			var resultAdd = _damerauLevenshtein.GetSimilarity(addFirst, addSecond);
			var resultAddNorm = _damerauLevenshtein.GetSimilarity(new NormalizedString(addFirst), new NormalizedString(addSecond));
			Assert.AreEqual(0.8, resultAdd, errorTollerance);
			Assert.AreEqual(0.8, resultAddNorm, errorTollerance);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			var resultDel = _damerauLevenshtein.GetSimilarity(delFirst, delSecond);
			var resultDelNorm = _damerauLevenshtein.GetSimilarity(new NormalizedString(delFirst),
				new NormalizedString(delSecond));
			Assert.AreEqual(0.75, resultDel, errorTollerance);
			Assert.AreEqual(0.75, resultDelNorm, errorTollerance);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			var resultSub = _damerauLevenshtein.GetSimilarity(subFirst, subSecond);
			var resultSubNorm = _damerauLevenshtein.GetSimilarity(new NormalizedString(subFirst),
				new NormalizedString(subSecond));
			Assert.AreEqual(0.75, resultSub);
			Assert.AreEqual(0.75, resultSubNorm);

			// substitution and deletation together
			var resultMixture = _damerauLevenshtein.GetSimilarity("abcdxyz", "zbcdxy");
			Assert.AreEqual(0.714, resultMixture, 0.001);

			// transposition
			var resultTransposition = _damerauLevenshtein.GetSimilarity("abcd", "acbd");
			Assert.AreEqual(0.75, resultTransposition);
		}
	}
}
