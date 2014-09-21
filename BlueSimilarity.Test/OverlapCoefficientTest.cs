#region

using System;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class OverlapCoefficientTest
	{
		#region Private fields

		private OverlapCoefficient<Bigram>   _overlapdCoefBigram;
		private OverlapCoefficient<Unigram>  _overlapCoefUnigram;
		private OverlapCoefficient<Trigram>  _overlapCoefTrigram;

	#endregion

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_overlapCoefUnigram, addFirst, addSecond, GetExpectedOverlapCoefficient(4, 4, 5));
			SimilarityHelpers.SimilarityInterfaceTest(_overlapdCoefBigram, addFirst, addSecond, GetExpectedOverlapCoefficient(3, 3, 4));
			SimilarityHelpers.SimilarityInterfaceTest(_overlapCoefTrigram, addFirst, addSecond, GetExpectedOverlapCoefficient(2, 2, 3));

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";

			SimilarityHelpers.SimilarityInterfaceTest(_overlapCoefUnigram, delFirst, delSecond, GetExpectedOverlapCoefficient(3, 4, 3));
			SimilarityHelpers.SimilarityInterfaceTest(_overlapdCoefBigram, delFirst, delSecond, GetExpectedOverlapCoefficient(2, 3, 2));
			SimilarityHelpers.SimilarityInterfaceTest(_overlapCoefTrigram, delFirst, delSecond, GetExpectedOverlapCoefficient(1, 2, 1));
			

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_overlapCoefUnigram, subFirst, subSecond, GetExpectedOverlapCoefficient(3, 4, 4));
			SimilarityHelpers.SimilarityInterfaceTest(_overlapdCoefBigram, subFirst, subSecond, GetExpectedOverlapCoefficient(1, 3, 3));
			SimilarityHelpers.SimilarityInterfaceTest(_overlapCoefTrigram, subFirst, subSecond, GetExpectedOverlapCoefficient(0, 2, 2));

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_overlapCoefUnigram, mixFirst, mixSecond, GetExpectedOverlapCoefficient(6, 7, 6));
			SimilarityHelpers.SimilarityInterfaceTest(_overlapdCoefBigram, mixFirst, mixSecond, GetExpectedOverlapCoefficient(4, 6, 5));
			SimilarityHelpers.SimilarityInterfaceTest(_overlapCoefTrigram, mixFirst, mixSecond, GetExpectedOverlapCoefficient(3, 5, 4));
		}

		[TestInitialize]
		public void Initializate()
		{
			_overlapdCoefBigram = new OverlapCoefficient<Bigram>();
			_overlapCoefUnigram = new OverlapCoefficient<Unigram>();
			_overlapCoefTrigram = new OverlapCoefficient<Trigram>();
		}

		#endregion

		#region Methods (private)

		private static double GetExpectedOverlapCoefficient(int intersection, int firstElements, int secondElements)
		{
			return intersection/(double) (Math.Min(firstElements, secondElements));
		}

		#endregion
	}
}