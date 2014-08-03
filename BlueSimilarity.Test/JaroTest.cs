#region

using BlueSimilarity.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class JaroTest
	{
		#region Static and contants fields

		private static Jaro _jaro;

		#endregion

		#region Methods (public)

		[TestInitialize]
		public void Initialize()
		{
			_jaro = new Jaro();
		}

		/// <summary>
		/// <seealso cref="http://en.wikipedia.org/wiki/Jaro%E2%80%93Winkler_distance"/>
		/// </summary>
		[TestMethod]
		public void JaroSimilarityTest()
		{
			const double errorTolerance = 0.001;

		    // test from wikipedia
			const string wikiFirst = "martha";
			const string wikiSecond = "marhta";
			var resultWiki = _jaro.GetSimilarity(wikiFirst, wikiSecond);
			var resultWikiNorm = _jaro.GetSimilarity(new NormalizedString(wikiFirst), new NormalizedString(wikiSecond));
			Assert.AreEqual(0.944, resultWiki, errorTolerance);
			Assert.AreEqual(0.944, resultWikiNorm, errorTolerance);


			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			var resultAdd = _jaro.GetSimilarity(addFirst, addSecond);
			var resultAddNorm = _jaro.GetSimilarity(new NormalizedString(addFirst), new NormalizedString(addSecond));
			Assert.AreEqual(0.933, resultAdd, errorTolerance);
			Assert.AreEqual(0.933, resultAddNorm, errorTolerance);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			var resultDel = _jaro.GetSimilarity(delFirst, delSecond);

			var resultDelNorm = _jaro.GetSimilarity(new NormalizedString(delFirst),
				new NormalizedString(delSecond));
			Assert.AreEqual(0.917, resultDel, errorTolerance);
			Assert.AreEqual(0.917, resultDelNorm, errorTolerance);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			var resultSub = _jaro.GetSimilarity(subFirst, subSecond);
			var resultSubNorm = _jaro.GetSimilarity(new NormalizedString(subFirst),
				new NormalizedString(subSecond));
			Assert.AreEqual(0.833, resultSub, errorTolerance);
			Assert.AreEqual(0.833, resultSubNorm, errorTolerance);

			// substitution and deletation together
			var resultMixture = _jaro.GetSimilarity("abcdxyz", "zbcdxy");
			Assert.AreEqual(0.849, resultMixture, errorTolerance);
		}

		#endregion
	}
}