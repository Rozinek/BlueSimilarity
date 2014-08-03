#region

using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

#endregion

namespace BlueSimilarity.Containers
{
	/// <summary>
	///     Creates Q-gram stream from ordered
	///     sequence (contextual) tokens
	/// </summary>
	public class QGramSet : IQGramSet, ISetOperations<QGramSet>
	{
		#region Static and contants fields

		/// <summary>
		///     Default blank space symbol
		/// </summary>
		private const string BlankSpace = " ";

		private const int DefaultQgramLength = 2;

		#endregion

		public QGramSet Union(QGramSet set)
		{
			if(QGramLength != set.QGramLength)
				throw new InvalidOperationException("The q-gram length must be equal.");

			var newQgramSet = new QGramSet(this);

			foreach (var pair in set)
			{
				int frequency;
				newQgramSet.TryGetValue(pair.Key, out frequency);
				newQgramSet[pair.Key] = Math.Max(pair.Value, frequency);
			}

			return newQgramSet;
		}

		public QGramSet Intersect(QGramSet set)
		{
			if (QGramLength != set.QGramLength)
				throw new InvalidOperationException("The q-gram length must be equal.");

			var newQgramSet = new QGramSet(this);

			foreach (var pair in set)
			{
				int frequency;
				if (newQgramSet.TryGetValue(pair.Key, out frequency))
				{
					newQgramSet.Remove(pair.Key);
				}
				else
				{
					newQgramSet[pair.Key] = Math.Min(pair.Value, frequency);
				}
			}

			return newQgramSet;
		}

		#region Private fields


		/// <summary>
		///     intenally storage for all q-grams
		/// </summary>
		private readonly Dictionary<string, int> _qGramsDictionary;

		#endregion

		#region Constructors

		/// <summary>
		/// </summary>
		/// <param name="text"></param>
		/// <param name="qgramLength"></param>
		public QGramSet(string text, int qgramLength = DefaultQgramLength)
		{
			Contract.Requires<ArgumentNullException>(text != null, "text");
			Contract.Requires<ArgumentOutOfRangeException>(qgramLength > 0, "qgramLength");

			QGramLength = qgramLength;
			_qGramsDictionary = QGramStreaming(text, qgramLength);
		}

		/// <summary>
		/// </summary>
		/// <param name="normalizedString"></param>
		/// <param name="qgramLength"></param>
		public QGramSet(NormalizedString normalizedString, int qgramLength = DefaultQgramLength)
			: this(normalizedString.Value, qgramLength)
		{
			Contract.Requires<ArgumentNullException>(normalizedString != null, "normalizedString");
		}

		public QGramSet(Token token, int qgramLength = DefaultQgramLength) : this(token.Value, qgramLength)
		{
			Contract.Requires<ArgumentNullException>(token != null, "token");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="qGramDictionary"></param>
		public QGramSet(IQGramSet qGramDictionary)
		{
			_qGramsDictionary = qGramDictionary.ToDictionary();
			QGramLength = qGramDictionary.QGramLength;
		}


		public Dictionary<string, int> ToDictionary()
		{
			return _qGramsDictionary;
		}

		#endregion

		#region IQGramSet Members


		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<KeyValuePair<QGram, int>> GetEnumerator()
		{
			foreach (var pair in _qGramsDictionary)
			{
				yield return new KeyValuePair<QGram, int>(new QGram(pair.Key), pair.Value);
			}
		}

		public void Add(KeyValuePair<QGram, int> item)
		{
			_qGramsDictionary.Add(item.Key.Value, item.Value);
		}

		void ICollection<KeyValuePair<QGram, int>>.CopyTo(KeyValuePair<QGram, int>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<QGram, int>>)this).CopyTo(array, arrayIndex);
		}

		public void Clear()
		{
			_qGramsDictionary.Clear();
		}

		bool  ICollection<KeyValuePair<QGram, int>>.Contains(KeyValuePair<QGram, int> item)
		{
			return ((ICollection<KeyValuePair<QGram, int>>)this).Contains(item);
		}

		bool ICollection<KeyValuePair<QGram, int>>.Remove(KeyValuePair<QGram, int> item)
		{
			return ((ICollection<KeyValuePair<QGram, int>>)this).Remove(item);
		}

		bool ICollection<KeyValuePair<QGram, int>>.IsReadOnly { get { return false; } }

		public bool ContainsKey(QGram key)
		{
			return _qGramsDictionary.ContainsKey(key.Value);
		}

		public void Add(QGram key, int value)
		{
			//Contract.Requires<ArgumentException>(key.Length != QGramLength,"Q-gram length is different!");
			_qGramsDictionary.Add(key.Value, value); 
		}

		public bool Remove(QGram key)
		{
			return _qGramsDictionary.Remove(key.Value);
		}

		public bool TryGetValue(QGram key, out int value)
		{
			return _qGramsDictionary.TryGetValue(key.Value, out value);
		}

		public ICollection<QGram> Keys 
		{ 
			get{ return _qGramsDictionary.Select(x => new QGram(x.Key)).ToArray(); }
		}

		public ICollection<int> Values
		{
			get { return _qGramsDictionary.Values.ToArray(); }
		}

		public int this[QGram key]
		{
			get
			{
				return _qGramsDictionary[key.Value];
			}
			set
			{
				//Contract.Requires<ArgumentOutOfRangeException>(value > 0, "Frequency must be positive integer!");
				_qGramsDictionary[key.Value] = value;
			}
		}

		public int QGramLength { get; private set; }


		public int Count
		{
			get { return _qGramsDictionary.Count; }
		}

		#endregion

		#region Methods (public)

		
		#endregion

		#region Methods

		private static string NormalizedToken(string token, int qgramLength)
		{
			/**** Exception for Shorter String **********************/
			if (token.Length < qgramLength && token.Length > 0)
			{
				var diff = qgramLength - token.Length;
				for (var e = 0; e < diff; e++)
				{
					token = token + BlankSpace;
				}
			}

			/**** AddDocumentTerms Padding ***************************************/
			token = String.Concat(BlankSpace, token);

			return token;
		}

		/// <summary>
		///     Creates the stream of q-grams from ordered
		///     sequence of tokens
		/// </summary>
		/// <param name="token"> ordered sequence of tokens </param>
		/// <param name="qgramLength">q-gram length</param>
		/// <returns> </returns>
		private static Dictionary<string, int> QGramStreaming(string token, int qgramLength)
		{
			var ngramDict = new Dictionary<string, int>();

			// copy current string
			var wordBuild = token;

			if (qgramLength != 1)
				wordBuild = NormalizedToken(token, qgramLength);


			/**** Q-gram splitting **********************************/
			for (var i = 0; i < wordBuild.Length - (qgramLength - 1); i++)
			{
				var ngitem = wordBuild.Substring(i, qgramLength);

				int occurence;
				if (ngramDict.TryGetValue(ngitem, out occurence))
				{
					ngramDict[ngitem] = occurence + 1;
				}
				else
				{
					ngramDict.Add(ngitem, 1);
				}
			}

			return ngramDict;
		}

		#endregion
	}
}