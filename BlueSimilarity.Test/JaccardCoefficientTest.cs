#region

using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class JaccardCoefficientTest
	{
		#region Private fields

		private JaccardCoefficient<Unigram> _jaccardCoefUnigram; 
		private JaccardCoefficient<Bigram> _jaccardCoefBigram;
		private JaccardCoefficient<Trigram> _jaccardCoefTrigram; 
		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefUnigram, addFirst, addSecond, GetExpectedJaccardCoefficient(4, 4, 5));
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefBigram, addFirst, addSecond, GetExpectedJaccardCoefficient(3, 3, 4));
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefTrigram, addFirst, addSecond, GetExpectedJaccardCoefficient(2, 2, 3));

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefUnigram, delFirst, delSecond, GetExpectedJaccardCoefficient(3, 4, 3));
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefBigram, delFirst, delSecond, GetExpectedJaccardCoefficient(2, 3, 2));
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefTrigram, delFirst, delSecond, GetExpectedJaccardCoefficient(1, 2, 1));

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefUnigram, subFirst, subSecond, GetExpectedJaccardCoefficient(3, 4, 4));
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefBigram, subFirst, subSecond, GetExpectedJaccardCoefficient(1, 3, 3));
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefTrigram, subFirst, subSecond, GetExpectedJaccardCoefficient(0, 2, 2));

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefUnigram, mixFirst, mixSecond, GetExpectedJaccardCoefficient(6, 7, 6));
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefBigram, mixFirst, mixSecond, GetExpectedJaccardCoefficient(4, 6, 5));
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoefTrigram, mixFirst, mixSecond, GetExpectedJaccardCoefficient(3, 5, 4));
		}

		[TestInitialize]
		public void Initializate()
		{
			 _jaccardCoefUnigram = new JaccardCoefficient<Unigram>(); 
			_jaccardCoefBigram = new JaccardCoefficient<Bigram>();
			_jaccardCoefTrigram = new JaccardCoefficient<Trigram>();
		}

		#endregion

		#region Methods (private)

		private static double GetExpectedJaccardCoefficient(int intersection, int firstElements, int secondElements)
		{
			return intersection/(double) (firstElements + secondElements - intersection);
		}

		#endregion
	}
}