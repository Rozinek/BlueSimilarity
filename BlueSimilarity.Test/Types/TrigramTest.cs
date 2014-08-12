#region

using System;
using BlueSimilarity.Test.Helpers;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Types
{
	[TestClass]
	public class TrigramTest
	{
		#region Methods (public)

		[TestMethod]
		public void TrigramEqualityTest()
		{
			EqualityTest.AssertEquality(new Trigram("abc"), new Trigram("abc"), new Trigram("abx"));
		}

		[ExpectedException(typeof (NotSupportedException))]
		[TestMethod]
		public void TrigramExceptionTest()
		{
			var triGram = new Trigram("abcd");
		}

		[TestMethod]
		public void TrigramInstanceTest()
		{
			var triGram = new Trigram("abc");

			Assert.AreEqual(3, triGram.Length);
			Assert.AreEqual("abc", triGram.Value);
		}

		#endregion
	}
}