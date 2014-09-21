#region

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class LevenshteinTest
	{
		#region Static and contants fields

		private static Levenshtein _levenshtein;

		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetDistanceTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.DistanceInterfaceTest(_levenshtein, addFirst, addSecond, 1);


			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.DistanceInterfaceTest(_levenshtein, delFirst, delSecond, 1);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.DistanceInterfaceTest(_levenshtein, subFirst, subSecond, 1);

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.DistanceInterfaceTest(_levenshtein, mixFirst, mixSecond, 2);
		}

		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_levenshtein, addFirst, addSecond, 0.8);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_levenshtein, delFirst, delSecond, 0.75);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_levenshtein, subFirst, subSecond, 0.75);

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_levenshtein, mixFirst, mixSecond, 0.714);
		}

		[TestInitialize]
		public void Initialize()
		{
			_levenshtein = new Levenshtein();
		}

		#endregion
	}
}