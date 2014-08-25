#region

using System;
using BlueSimilarity.Test.Helpers;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Types
{
	[TestClass]
	public class UnigramTest
	{
		#region Methods (public)

		[TestMethod]
		public void UnigramEqualityTest()
		{
			HelpersTest.AssertEquality(new Unigram("a"), new Unigram("a"), new Unigram("b"));
		}

		[ExpectedException(typeof (NotSupportedException))]
		[TestMethod]
		public void UnigramExceptionTest()
		{
			var unigram = new Unigram("ab");
		}

		[TestMethod]
		public void UnigramInstanceTest()
		{
			var unigram = new Unigram("a");

			Assert.AreEqual(1, unigram.Length);
			Assert.AreEqual("a", unigram.Value);
		}

		[TestMethod]
		public void ToStringTest()
		{
			var unigram = new Unigram("a");
			Assert.AreEqual("a", unigram.ToString());
		}

		[TestMethod]
		public void CompareToTest()
		{
			var instance = new Unigram("x");
			var higherInstance = new Unigram("a");
			var equalInstance = new Unigram("x");
			HelpersTest.AssertCompareTo(instance, higherInstance, equalInstance);
		}


		#endregion
	}
}