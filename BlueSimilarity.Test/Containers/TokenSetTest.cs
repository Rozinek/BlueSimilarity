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
		private TokenSet _tokenSet;

		[TestInitialize]
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
		public void AddTest()
		{
			var tokenSet = new TokenSet {{new Token("abcd"), 1}};
			Assert.AreEqual(1, tokenSet.Count);   
		}

		[TestMethod]
		public void RemoveTest()
		{
			_tokenSet.Remove(new Token("token"));
			Assert.AreEqual(1, _tokenSet.Count);
		}

		[TestMethod]
		public void CollectionContains()
		{
			var pairContain = new KeyValuePair<Token, int>(new Token("token"), 1);
			var pairNotContain = new KeyValuePair<Token, int>(new Token("nothing"), 1);

			var containsToken = ((ICollection<KeyValuePair<Token, int>>)_tokenSet).Contains(pairContain);
			var notContainsToken = ((ICollection<KeyValuePair<Token, int>>)_tokenSet).Contains(pairNotContain);

			Assert.IsTrue(containsToken);
			Assert.IsFalse(notContainsToken);
		}

		[TestMethod]
		public void CollectionAdd()
		{
			var pair = new KeyValuePair<Token, int>(new Token("token3"), 1);
			((ICollection<KeyValuePair<Token, int>>)_tokenSet).Add(pair);
		}

		[TestMethod]
		public void CollectionRemove()
		{
			var pair = new KeyValuePair<Token, int>(new Token("token"), 1);
			((ICollection<KeyValuePair<Token, int>>)_tokenSet).Remove(pair);
		}

		[TestMethod]
		public void IsReadOnlyTest()
		{
			Assert.IsFalse(_tokenSet.IsReadOnly);
		}

		[TestMethod]
		public void Enumerator()
		{
			int setCount = 0;	
			foreach (var pair in _tokenSet)
			{
				setCount++;
			}
			Assert.AreEqual(2, setCount);
		}


		[TestMethod]
		public void ContainsKeyTest()
		{
			Assert.IsTrue(_tokenSet.ContainsKey(new Token("token")));			
		}

		[TestMethod]
		public void IndexerTest()
		{
			var token = new Token("token");
			Assert.AreEqual(1, _tokenSet[new Token("token")]);
			Assert.AreEqual(2, _tokenSet[new Token("token2")]);

			_tokenSet[token] = 3;
			Assert.AreEqual(3, _tokenSet[token]);

		}

		[TestMethod]
		public void ClearTest()
		{
			_tokenSet.Clear();
			Assert.AreEqual(0,_tokenSet.Count);
		}

		
		[TestMethod]
		public void CopyToTest()
		{
			var listCopy = new List<KeyValuePair<Token, int>>()
			               {
				               new KeyValuePair<Token, int>(new Token("token3"), 1),
				               new KeyValuePair<Token, int>(new Token("token4"), 2)
			               };

			((ICollection<KeyValuePair<Token, int>>) _tokenSet).CopyTo(listCopy.ToArray(), 0);

			Assert.AreEqual(4, _tokenSet.Count);
		}
	}
}