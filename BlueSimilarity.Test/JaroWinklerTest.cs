#region

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class JaroWinklerTest
	{
		#region Static and contants fields

		private static JaroWinkler _jaroWinkler;

		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_jaroWinkler, addFirst, addSecond, 0.960);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_jaroWinkler, delFirst, delSecond, 0.942);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_jaroWinkler, subFirst, subSecond, 0.850);

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_jaroWinkler, mixFirst, mixSecond, 0.849);
		}


		[TestInitialize]
		public void Initializate()
		{
			_jaroWinkler = new JaroWinkler();
		}

		[TestMethod]
		public void RealCasesTest()
		{
			// test from wikipedia
			const string wikiFirst = "martha";
			const string wikiSecond = "marhta";
			SimilarityHelpers.SimilarityInterfaceTest(_jaroWinkler, wikiFirst, wikiSecond, 0.961);

			// test from wikipedia
			const string wiki2First = "dwayne";
			const string wiki2Second = "duane";
			SimilarityHelpers.SimilarityInterfaceTest(_jaroWinkler, wiki2First, wiki2Second, 0.84);
		}

		#endregion
	}
}