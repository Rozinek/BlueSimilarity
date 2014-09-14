#region

using System.Collections.Generic;
using BlueSimilarity.Containers;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Containers
{
	[TestClass]
	public class TokenSetTest
	{
		[TestMethod]
		public void ConstructorTest()
		{
			var dictionaryTokens = new Dictionary<Token, int>
			                       {
				                       {new Token("token"), 1},
				                       {new Token("token2"), 2}
			                       };

			var tokenSetFromDict = new TokenSet(dictionaryTokens);
			Assert.AreEqual(2, tokenSetFromDict.Count);

			var tokenSetEmpty = new TokenSet();
			Assert.AreEqual(0, tokenSetEmpty.Count);

			var tokenSetEmptyCapacity = new TokenSet(20);
			Assert.AreEqual(0, tokenSetEmptyCapacity.Count); 

		}

	}
}