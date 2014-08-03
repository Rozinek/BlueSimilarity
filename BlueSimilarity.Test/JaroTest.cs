#region

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class JaroTest
	{
		#region Static and contants fields

		private static Jaro _jaro;

		#endregion

		#region Methods (public)

		/// <summary>
		///     <seealso cref="http://en.wikipedia.org/wiki/Jaro%E2%80%93Winkler_distance" />
		/// </summary>
		[TestMethod]
		public void GetSimilarityTest()
		{
			// addition edit distance test
			const string addFirst = "abcd";
			const string addSecond = "abcdx";
			SimilarityHelpers.SimilarityInterfaceTest(_jaro, addFirst, addSecond, 0.933);

			// deletation edit distance test
			const string delFirst = "abcd";
			const string delSecond = "abc";
			SimilarityHelpers.SimilarityInterfaceTest(_jaro, delFirst, delSecond, 0.917);

			// substitution edit distance test
			const string subFirst = "abcd";
			const string subSecond = "axcd";
			SimilarityHelpers.SimilarityInterfaceTest(_jaro, subFirst, subSecond, 0.833);

			// substitution and deletation together
			const string mixFirst = "abcdxyz";
			const string mixSecond = "zbcdxy";
			SimilarityHelpers.SimilarityInterfaceTest(_jaro, mixFirst, mixSecond, 0.849);
		}

		[TestInitialize]
		public void Initialize()
		{
			_jaro = new Jaro();
		}

		[TestMethod]
		public void RealCasesTest()
		{
			// test from wikipedia
			const string wikiFirst = "martha";
			const string wikiSecond = "marhta";
			SimilarityHelpers.SimilarityInterfaceTest(_jaro, wikiFirst, wikiSecond, 0.944);

			//// test from wikipedia
			//const string wiki2First = "dwayne";
			//const string wiki2Second = "duane";
			//SimilarityHelpers.SimilarityInterfaceTest(_jaro, wiki2First, wiki2Second, 0.933);
		}

		#endregion
	}
}