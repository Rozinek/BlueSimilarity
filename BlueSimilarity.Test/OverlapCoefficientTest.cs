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
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_overlapdCoef, addFirst, addSecond, GetExpectedOverlapCoefficient(3, 3, 4));

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_overlapdCoef, delFirst, delSecond, GetExpectedOverlapCoefficient(2, 3, 2));

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_overlapdCoef, subFirst, subSecond, GetExpectedOverlapCoefficient(1, 3, 3));

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_overlapdCoef, mixFirst, mixSecond, GetExpectedOverlapCoefficient(4, 6, 5));
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
