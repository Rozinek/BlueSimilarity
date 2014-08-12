#region

using System;
using BlueSimilarity.Test.Helpers;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Types
{
	[TestClass]
	public class BigramTest
	{
		#region Methods (public)

		[TestMethod]
		public void UnigramEqualityTest()
		{
			EqualityTest.AssertEquality(new Bigram("ab"), new Bigram("ab"), new Bigram("ac"));
		}

		[ExpectedException(typeof (NotSupportedException))]
		[TestMethod]
		public void UnigramExceptionTest()
		{
			var unigram = new Bigram("a");
		}

		[TestMethod]
		public void UnigramInstanceTest()
		{
			var unigram = new Bigram("ab");

			Assert.AreEqual(2, unigram.Length);
			Assert.AreEqual("ab", unigram.Value);
		}

		#endregion
	}
}