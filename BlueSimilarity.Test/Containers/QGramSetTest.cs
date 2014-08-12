#region

using BlueSimilarity.Containers;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Preprocessing
{
	[TestClass]
	public class QGramSetTest
	{
		#region Methods (public)

		[TestMethod]
		public void CtorQGramSet()
		{
			// create set of unigrams
			var unigramSet = new QGramSet<Unigram>("abcd");

			Assert.AreEqual(1, unigramSet.QGramLength);
			Assert.AreEqual(4, unigramSet.Count);

			// create set of bigrams
			var bigramSet = new QGramSet<Bigram>("abcd");

			Assert.AreEqual(2, bigramSet.QGramLength);
			Assert.AreEqual(4, bigramSet.Count);


			// create set of trigrams
			var trigramSet = new QGramSet<Trigram>("abcd");

			Assert.AreEqual(3, trigramSet.QGramLength);
			Assert.AreEqual(3, trigramSet.Count);
		}

		[TestMethod]
		public void IntersectQgramSets()
		{
			var firstSet = new QGramSet<Unigram>("abcd");
			var secondSet = new QGramSet<Unigram>("bxdy");

			var intersectSet = firstSet.Intersect(secondSet);
			Assert.AreEqual(2, intersectSet.Count);
			Assert.AreEqual(1, intersectSet.QGramLength);
		}

		[TestMethod]
		public void UnionQgramSets()
		{
			var firstSet = new QGramSet<Unigram>("abcd");
			var secondSet = new QGramSet<Unigram>("bxdy");

			var unionSet = firstSet.Union(secondSet);
			Assert.AreEqual(6, unionSet.Count);
			Assert.AreEqual(1, unionSet.QGramLength);
		}

		#endregion
	}
}