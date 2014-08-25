#region

using System.Linq;
using BlueSimilarity.Containers;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Containers
{
	[TestClass]
	public class QGramSetTest
	{
		#region Methods (public)

		[TestMethod]
		public void CtorQGramSet()
		{
			// create set of unigrams
			var unigramSet = new QGramSet<Unigram>(new Token("abcd"));
			Assert.AreEqual(1, unigramSet.QGramLength);
			Assert.AreEqual(4, unigramSet.Count);

			var enumerator = unigramSet.GetEnumerator();
			var loopCount = 0;
			while (enumerator.MoveNext())
			{
				loopCount++;
			}

			Assert.AreEqual(4, loopCount);

			var dict = unigramSet.ToDictionary(x => x.Key, y => y.Value);
			Assert.AreEqual(4, dict.Count);

			//TODO: make ToList() functional
			//var enumContainer  = (IEnumerable<KeyValuePair<Unigram, int>>)unigramSet;
			//var listUnigram = new List<KeyValuePair<Unigram, int>>(enumContainer);

			//var expectedUnigram = new List<KeyValuePair<Unigram, int>>()
			//					  {
			//						  new KeyValuePair<Unigram, int>(new Unigram("a"), 1),
			//						  new KeyValuePair<Unigram, int>(new Unigram("b"), 1),
			//						  new KeyValuePair<Unigram, int>(new Unigram("c"), 1),
			//						  new KeyValuePair<Unigram, int>(new Unigram("d"), 1)
			//					  };

			//CollectionAssert.AreEqual(listUnigram, expectedUnigram);

			// create set of bigrams
			var bigramSet = new QGramSet<Bigram>(new Token("abcd"));

			Assert.AreEqual(2, bigramSet.QGramLength);
			Assert.AreEqual(4, bigramSet.Count);

			// create set of trigrams
			var trigramSet = new QGramSet<Trigram>(new Token("abcd"));

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
		    //new QGramSet<IQgram>();
			var firstSet = new QGramSet<Unigram>("abcd");
			var secondSet = new QGramSet<Unigram>("bxdy");

			var unionSet = firstSet.Union(secondSet);
			Assert.AreEqual(6, unionSet.Count);
			Assert.AreEqual(1, unionSet.QGramLength);
		}

		[TestMethod]
		public void ToStringTest()
		{
			var unigramSet = new QGramSet<Unigram>(new Token("abcd"));
			var stringQgrams = unigramSet.ToString();
			Assert.AreEqual("[a;1],[b;1],[c;1],[d;1]", stringQgrams);
		}

		#endregion
	}
}