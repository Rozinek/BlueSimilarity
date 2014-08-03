using System.CodeDom;
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
			SimilarityHelpers.DistanceInterfaceTest(_levensthein, addFirst, addSecond, 1);


			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.DistanceInterfaceTest(_levensthein, delFirst, delSecond, 1);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.DistanceInterfaceTest(_levensthein, subFirst, subSecond, 1);

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.DistanceInterfaceTest(_levensthein, mixFirst, mixSecond, 2);
		}

		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_levensthein, addFirst, addSecond, 0.8);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_levensthein, delFirst, delSecond, 0.75);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_levensthein, subFirst, subSecond, 0.75);

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_levensthein, mixFirst, mixSecond, 0.714);
		}
	}
}
