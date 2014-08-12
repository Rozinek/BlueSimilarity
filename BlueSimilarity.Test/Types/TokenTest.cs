#region

using BlueSimilarity.Test.Helpers;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Types
{
	[TestClass]
	public class TokenTest
	{
		#region Methods (public)

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
		public void TokenEqualityTest()
		{
			var tokenInstance = new Token("instance");
			var tokenEqualInstance = new Token("instance");
			var tokenNotEqualInstance = new Token("notInstance");
			EqualityTest.AssertEquality(tokenInstance, tokenEqualInstance, tokenNotEqualInstance);
		}

		#endregion
	}
}