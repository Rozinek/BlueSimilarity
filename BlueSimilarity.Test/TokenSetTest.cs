#region

using BlueSimilarity.Containers;
using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class TokenSetTest
	{
		private TokenSet _tokenSet;

		[TestMethod]
		public void Initialization()
		{
			_tokenSet = new TokenSet
			               {
				               {new Token("token"), 1}, 
							   {new Token("token2"), 2}
			               };	
		}


		[TestMethod]
		public void CountTest()
		{
			Assert.AreEqual(2, _tokenSet.Count);
			Assert.AreEqual(2, _tokenSet.Keys.Count);
			Assert.AreEqual(2, _tokenSet.Values.Count);	
		}

		[TestMethod]
		public void ContainsKeyTest()
		{
			Assert.IsTrue(_tokenSet.ContainsKey(new Token("token")));			
		}

		[TestMethod]
		public void IndexerTest()
		{
			Assert.AreEqual(1, _tokenSet[new Token("token")]);
			Assert.AreEqual(2, _tokenSet[new Token("token2")]);
		}
	}
}