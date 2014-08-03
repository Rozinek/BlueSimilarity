#region

using BlueSimilarity.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class JaccardCoefficientTest
	{
		#region Private fields

		private JaccardCoefficient _jaccardCoef;

		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoef, addFirst, addSecond, GetExpectedJaccardCoefficient(3, 3, 4));

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoef, delFirst, delSecond, GetExpectedJaccardCoefficient(2, 3, 2));

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoef, subFirst, subSecond, GetExpectedJaccardCoefficient(1, 3, 3));

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_jaccardCoef, mixFirst, mixSecond, GetExpectedJaccardCoefficient(4, 6, 5));
		}

		[TestInitialize]
		public void Initializate()
		{
			_jaccardCoef = new JaccardCoefficient();
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