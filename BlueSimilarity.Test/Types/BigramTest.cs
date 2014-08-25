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
		public void BigramEqualityTest()
		{
			HelpersTest.AssertEquality(new Bigram("ab"), new Bigram("ab"), new Bigram("ac"));
		}

		[ExpectedException(typeof (NotSupportedException))]
		[TestMethod]
		public void BigramExceptionTest()
		{
			var unigram = new Bigram("a");
		}

		[TestMethod]
		public void BigramInstanceTest()
		{
			var unigram = new Bigram("ab");

			Assert.AreEqual(2, unigram.Length);
			Assert.AreEqual("ab", unigram.Value);
		}

		[TestMethod]
		public void ToStringTest()
		{
			var bigram = new Bigram("ab");
			Assert.AreEqual("ab", bigram.ToString());
		}

		[TestMethod]
		public void CompareToTest()
		{
			var instance = new Bigram("xy");
			var higherInstance = new Bigram("ab");
			var equalInstance = new Bigram("xy");
			HelpersTest.AssertCompareTo(instance, higherInstance, equalInstance);
		}

		#endregion
	}
}