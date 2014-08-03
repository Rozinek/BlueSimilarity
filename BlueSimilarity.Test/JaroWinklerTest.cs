using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueSimilarity.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.Test
{
	[TestClass]
	 public class JaroWinklerTest
	{
		private static JaroWinkler _jaroWinkler;


		[TestInitialize]
		public void Initializate()
		{
			_jaroWinkler = new JaroWinkler();
		}

		/// <summary>
		/// <seealso cref="http://en.wikipedia.org/wiki/Jaro%E2%80%93Winkler_distance"/>
		/// </summary>
		[TestMethod]
		public void JaroWinklerSimilarityTest()
		{
			const double errorTolerance = 0.001;

			// test from wikipedia
			const string wikiFirst = "martha";
			const string wikiSecond = "marhta";
			var resultWiki = _jaroWinkler.GetSimilarity(wikiFirst, wikiSecond);
			var resultWikiNorm = _jaroWinkler.GetSimilarity(new NormalizedString(wikiFirst), new NormalizedString(wikiSecond));
			Assert.AreEqual(0.961, resultWiki, errorTolerance);
			Assert.AreEqual(0.961, resultWikiNorm, errorTolerance);

			// test from wikipedia
			const string wiki2First = "dwayne";
			const string wiki2Second = "duane";
			var result2Wiki = _jaroWinkler.GetSimilarity(wiki2First, wiki2Second);
			var result2WikiNorm = _jaroWinkler.GetSimilarity(new NormalizedString(wiki2First), new NormalizedString(wiki2Second));
			Assert.AreEqual(0.84, result2Wiki, errorTolerance);
			Assert.AreEqual(0.84, result2WikiNorm, errorTolerance);


			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			var resultAdd = _jaroWinkler.GetSimilarity(addFirst, addSecond);
			var resultAddNorm = _jaroWinkler.GetSimilarity(new NormalizedString(addFirst), new NormalizedString(addSecond));
			Assert.AreEqual(0.960, resultAdd, errorTolerance);
			Assert.AreEqual(0.960, resultAddNorm, errorTolerance);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			var resultDel = _jaroWinkler.GetSimilarity(delFirst, delSecond);
			var resultDelNorm = _jaroWinkler.GetSimilarity(new NormalizedString(delFirst),
				new NormalizedString(delSecond));
			Assert.AreEqual(0.942, resultDel, errorTolerance);
			Assert.AreEqual(0.942, resultDelNorm, errorTolerance);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			var resultSub = _jaroWinkler.GetSimilarity(subFirst, subSecond);
			var resultSubNorm = _jaroWinkler.GetSimilarity(new NormalizedString(subFirst),
				new NormalizedString(subSecond));
			Assert.AreEqual(0.850, resultSub, errorTolerance);
			Assert.AreEqual(0.850, resultSubNorm, errorTolerance);

			// substitution and deletation together
			var resultMixture = _jaroWinkler.GetSimilarity("abcdxyz", "zbcdxy");
			Assert.AreEqual(0.849, resultMixture, errorTolerance);
			
		}
	}
}
