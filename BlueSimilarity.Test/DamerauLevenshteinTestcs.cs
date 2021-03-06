﻿#region

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class DamerauLevenshteinTestcs
	{
		#region Private fields

		private DamerauLevenshtein _damerauLevenshtein;

		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetDistanceTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			const int addResultExpectd = 1;
			SimilarityHelpers.DistanceInterfaceTest(_damerauLevenshtein, addFirst, addSecond, addResultExpectd);

			// deletation edit distance test
			SimilarityHelpers.DistanceInterfaceTest(_damerauLevenshtein, "abcd", "abc", 1);

			// substitution edit distance test
			SimilarityHelpers.DistanceInterfaceTest(_damerauLevenshtein, "abcd", "axcd", 1);

			// substitution and deletation together
			SimilarityHelpers.DistanceInterfaceTest(_damerauLevenshtein, "abcdxyz", "zbcdxy", 2);

			// transposition
			SimilarityHelpers.DistanceInterfaceTest(_damerauLevenshtein, "abcd", "acbd", 1);
			SimilarityHelpers.DistanceInterfaceTest(_damerauLevenshtein, "abcde", "baced", 2);
		}

		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_damerauLevenshtein, addFirst, addSecond, 0.8);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_damerauLevenshtein, delFirst, delSecond, 0.75);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_damerauLevenshtein, subFirst, subSecond, 0.75);

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_damerauLevenshtein, mixFirst, mixSecond, 0.714);

			// transposition
			const string transFirst = "abcd";
			const string transSecond = "acbd";
			SimilarityHelpers.SimilarityInterfaceTest(_damerauLevenshtein, transFirst, transSecond, 0.75);
		}

		[TestInitialize]
		public void Initializate()
		{
			_damerauLevenshtein = new DamerauLevenshtein();
		}

		#endregion
	}
}