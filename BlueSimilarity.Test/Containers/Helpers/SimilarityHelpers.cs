#region

using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	public static class SimilarityHelpers
	{
		#region Static and contants fields

		private const double ErrorTollerance = 1E-03;

		#endregion

		#region Methods (public)

		public static void DistanceInterfaceTest(IDistance distance, string first, string second, int expected)
		{
			var resultString = distance.GetDistance(first, second);
			Assert.AreEqual(expected, resultString);

			var resultNorm = distance.GetDistance(new NormalizedString(first), new NormalizedString(second));
			Assert.AreEqual(expected, resultNorm);

			var resultToken = distance.GetDistance(new Token(first), new Token(second));
			Assert.AreEqual(expected, resultToken);
		}

		public static void SimilarityInterfaceTest(ISimilarity distance, string first, string second, double expected)
		{
			var resultString = distance.GetSimilarity(first, second);
			Assert.AreEqual(expected, resultString, ErrorTollerance);

			var resultNorm = distance.GetSimilarity(new NormalizedString(first), new NormalizedString(second));
			Assert.AreEqual(expected, resultNorm, ErrorTollerance);

			var resultToken = distance.GetSimilarity(new Token(first), new Token(second));
			Assert.AreEqual(expected, resultToken, ErrorTollerance);
		}

		#endregion
	}
}