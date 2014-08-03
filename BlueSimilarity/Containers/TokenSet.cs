using System;
using System.Collections;
using System.Collections.Generic;

namespace BlueSimilarity.Containers
{
	public class TokenSet : IDictionary<QGram, int>
	{
		
		
		public ICollection<int> Values { get; private set; }
		public ICollection<QGram> Keys { get; private set; }

		public int this[QGram key]
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public bool TryGetValue(QGram key, out int value)
		{
			throw new NotImplementedException();
		}

		public bool Remove(QGram key)
		{
			throw new NotImplementedException();
		}

		public void Add(QGram key, int value)
		{
			throw new NotImplementedException();
		}

		public bool ContainsKey(QGram key)
		{
			throw new NotImplementedException();
		}

		public bool IsReadOnly { get; private set; }
		public int Count { get; private set; }
		public bool Remove(KeyValuePair<QGram, int> item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(KeyValuePair<QGram, int>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Contains(KeyValuePair<QGram, int> item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public void Add(KeyValuePair<QGram, int> item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<KeyValuePair<QGram, int>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
