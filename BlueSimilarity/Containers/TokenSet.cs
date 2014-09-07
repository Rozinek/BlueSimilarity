#region

using System.Collections;
using System.Collections.Generic;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Containers
{
	/// <summary>
	/// 
	/// </summary>
	public class TokenSet : IDictionary<Token, int>
	{
		private readonly Dictionary<Token, int> _tokenDictionary;


		/// <summary>
		/// Create empty token set
		/// </summary>
		public TokenSet()
		{
			_tokenDictionary = new Dictionary<Token, int>();
		}

		/// <summary>
		/// Create token set from <see cref="IDictionary"/>
		/// </summary>
		/// <param name="tokenDictionary">dictionary of tokens and their occurences</param>
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
			return _tokenDictionary.TryGetValue(key, out value);
		}

		public bool Remove(Token key)
		{
			return _tokenDictionary.Remove(key);
		}


		/// <summary>
		/// Add token with its occurence
		/// </summary>
		/// <param name="key">the token</param>
		/// <param name="value">the occurence</param>
		public void Add(Token key, int value)
		{
			_tokenDictionary.Add(key, value);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(Token key)
		{
			return _tokenDictionary.ContainsKey(key);
		}

		public bool IsReadOnly { get { return false; } }

		public int Count { get { return _tokenDictionary.Count; } }

		bool ICollection<KeyValuePair<Token, int>>.Remove(KeyValuePair<Token, int> item)
		{
			return ((ICollection<KeyValuePair<Token, int>>)_tokenDictionary).Remove(item);
		}

		void ICollection<KeyValuePair<Token, int>>.CopyTo(KeyValuePair<Token, int>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<Token, int>>)_tokenDictionary).CopyTo(array, arrayIndex);
		}

		bool ICollection<KeyValuePair<Token, int>>.Contains(KeyValuePair<Token, int> item)
		{
			return ((ICollection<KeyValuePair<Token, int>>)_tokenDictionary).Contains(item);
		}

		public void Clear()
		{
			_tokenDictionary.Clear();
		}

		void ICollection<KeyValuePair<Token, int>>.Add(KeyValuePair<Token, int> item)
		{
			((ICollection<KeyValuePair<Token, int>>)_tokenDictionary).Add(item);
		}

		IEnumerator<KeyValuePair<Token, int>> IEnumerable<KeyValuePair<Token, int>>.GetEnumerator()
		{
			return _tokenDictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _tokenDictionary.GetEnumerator();
		}

		#endregion
	}
}