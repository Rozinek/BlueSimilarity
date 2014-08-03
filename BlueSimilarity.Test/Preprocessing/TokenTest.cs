using System;
using BlueSimilarity.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.Test.Preprocessing
{
	[TestClass]
	public class TokenTest
	{
		[TestMethod]
		public void CtorToken()
		{
			const string tokenTest = "token";
			var normalizedToken = new NormalizedString(tokenTest);
			var token = new Token(tokenTest);
			var tokenFromNormalizedString = new Token(normalizedToken);
			
			Assert.AreEqual(token.Value, tokenTest);
			Assert.AreEqual(tokenFromNormalizedString.Value, normalizedToken.Value);
		}

		[TestMethod]
		public void EqualityTest()
		{
			var tokenInstance = new Token("instance");
			var tokenEqualInstance = new Token("instance");
			var tokenNotEqualInstance = new Token("notInstance");

			EqualityTest(tokenInstance, tokenEqualInstance, tokenNotEqualInstance);
		}

		private static void EqualityTest<T>(T instance, T equalInstance, T notEqualInstance) where T : IEquatable<T>
		{
			

			Assert.IsTrue(instance.Equals(equalInstance));
			Assert.IsFalse(instance.Equals(notEqualInstance));

			Assert.IsTrue(instance.Equals((object)equalInstance));
			Assert.IsFalse(instance.Equals((object)notEqualInstance));


			Assert.IsFalse(instance.Equals(null));
			Assert.IsTrue(instance.Equals(instance));
			Assert.IsTrue(instance.Equals((object)instance));
			
			// check for another type
			Assert.IsFalse(instance.Equals(true));

		    // check for hash code
		    Assert.AreEqual(instance.GetHashCode(), equalInstance.GetHashCode());
		} 
	}
}
