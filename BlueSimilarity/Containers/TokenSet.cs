#region

using System;
using System.Collections;
using System.Collections.Generic;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Containers
{
	public class TokenSet : IDictionary<Token, int>
	{

		private readonly Dictionary<Token, int> _tokenDictionary;

		public TokenSet()
		{
			_tokenDictionary = new Dictionary<Token, int>();
		}

		public TokenSet(IDictionary<Token, int> tokenDictionary)
		{
			_tokenDictionary = new Dictionary<Token, int>(tokenDictionary);
		}

		#region IDictionary<QGram,int> Members

		public ICollection<int> Values { get { return _tokenDictionary.Values; } }
		public ICollection<Token> Keys
		{
			get { return _tokenDictionary.Keys; }
		}

		public int this[Token key]
		{
			get { return _tokenDictionary[key]; }
			set { _tokenDictionary[key] = value; }
		}

		public bool TryGetValue(Token key, out int value)
		{
			throw new NotImplementedException();
		}

		public bool Remove(Token key)
		{
			throw new NotImplementedException();
		}

		public void Add(Token key, int value)
		{
			throw new NotImplementedException();
		}

		public bool ContainsKey(Token key)
		{
			throw new NotImplementedException();
		}

		public bool IsReadOnly { get; private set; }
		public int Count { get; private set; }

		public bool Remove(KeyValuePair<Token, int> item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(KeyValuePair<Token, int>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Contains(KeyValuePair<Token, int> item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public void Add(KeyValuePair<Token, int> item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<KeyValuePair<Token, int>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}