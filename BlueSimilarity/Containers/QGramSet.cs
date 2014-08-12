#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Containers
{
	/// <summary>
	///     Creates Q-gram stream from ordered
	///     sequence (contextual) tokens
	/// </summary>
	public class QGramSet<T> : IQGramSet<T>, ISetOperations<QGramSet<T>> where T : IQgram
	{
		#region Static and contants fields

		/// <summary>
		///     Default blank space symbol
		/// </summary>
		private const string BlankSpace = " ";

		#endregion

		#region Private fields

		private readonly Func<string, IQgram> _currentQgramConstructor;


		private readonly Dictionary<Type, Func<string, IQgram>> _dynamicConstructors =
			new Dictionary<Type, Func<string, IQgram>>
			{
				{typeof (Unigram), x => new Unigram(x)},
				{typeof (Bigram), x => new Bigram(x)},
				{typeof (Trigram), x => new Trigram(x)},
			};

		/// <summary>
		///     intenally storage for all q-grams
		/// </summary>
		private readonly Dictionary<string, int> _qGramsDictionary;

		private readonly Dictionary<Type, Func<int>> _typeConversion = new Dictionary<Type, Func<int>>
		                                                               {
			                                                               {typeof (Unigram), () => 1},
			                                                               {typeof (Bigram), () => 2},
			                                                               {typeof (Trigram), () => 3},
		                                                               };

		#endregion

		#region Constructors

		/// <summary>
		/// </summary>
		/// <param name="text"></param>
		public QGramSet(string text) : this()
		{
			Contract.Requires<ArgumentNullException>(text != null, "text");
			_qGramsDictionary = QGramStreaming(text, QGramLength);
		}

		public QGramSet()
		{
			_qGramsDictionary = new Dictionary<string, int>();
			var typeQGram = typeof (T);

			_currentQgramConstructor = _dynamicConstructors[typeQGram];
			QGramLength = _typeConversion[typeQGram].Invoke();
		}

		/// <summary>
		/// </summary>
		/// <param name="normalizedString"></param>
		public QGramSet(NormalizedString normalizedString)
			: this(normalizedString.Value)
		{
			Contract.Requires<ArgumentNullException>(normalizedString != null, "normalizedString");
		}

		public QGramSet(Token token)
			: this(token.Value)
		{
			Contract.Requires<ArgumentNullException>(token != null, "token");
		}

		/// <summary>
		/// </summary>
		/// <param name="qGramDictionary"></param>
		public QGramSet(IQGramSet<T> qGramDictionary)
		{
			_qGramsDictionary = qGramDictionary.ToDictionary();
			QGramLength = qGramDictionary.QGramLength;
		}

		#endregion

		#region IQGramSet<T> Members

		public int this[T key]
		{
			get { return _qGramsDictionary[key.Value]; }
			set
			{
				//Contract.Requires<ArgumentOutOfRangeException>(value > 0, "Frequency must be positive integer!");
				_qGramsDictionary[key.Value] = value;
			}
		}

		public Dictionary<string, int> ToDictionary()
		{
			return _qGramsDictionary;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
		{
			foreach (var pair in _qGramsDictionary)
			{
				yield return new KeyValuePair<T, int>((T) _currentQgramConstructor.Invoke(pair.Key), pair.Value);
			}
		}

		public void Add(KeyValuePair<T, int> item)
		{
			_qGramsDictionary.Add(item.Key.Value, item.Value);
		}

		void ICollection<KeyValuePair<T, int>>.CopyTo(KeyValuePair<T, int>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<T, int>>) this).CopyTo(array, arrayIndex);
		}

		public void Clear()
		{
			_qGramsDictionary.Clear();
		}

		bool ICollection<KeyValuePair<T, int>>.Contains(KeyValuePair<T, int> item)
		{
			return ((ICollection<KeyValuePair<T, int>>) this).Contains(item);
		}

		bool ICollection<KeyValuePair<T, int>>.Remove(KeyValuePair<T, int> item)
		{
			return ((ICollection<KeyValuePair<T, int>>) this).Remove(item);
		}

		bool ICollection<KeyValuePair<T, int>>.IsReadOnly
		{
			get { return false; }
		}

		public bool ContainsKey(T key)
		{
			return _qGramsDictionary.ContainsKey(key.Value);
		}

		public void Add(T key, int value)
		{
			_qGramsDictionary.Add(key.Value, value);
		}

		public bool Remove(T key)
		{
			return _qGramsDictionary.Remove(key.Value);
		}

		public ICollection<T> Keys
		{
			get
			{
				return _qGramsDictionary
					.Select(x => (T) _currentQgramConstructor.Invoke(x.Key)).ToArray();
			}
		}

		public ICollection<int> Values
		{
			get { return _qGramsDictionary.Values.ToArray(); }
		}

		public int QGramLength { get; private set; }


		public int Count
		{
			get { return _qGramsDictionary.Count; }
		}

		public bool TryGetValue(T key, out int value)
		{
			return _qGramsDictionary.TryGetValue(key.Value, out value);
		}

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

		#region ISetOperations<QGramSet<T>> Members

		public QGramSet<T> Union(QGramSet<T> set)
		{
			if (QGramLength != set.QGramLength)
				throw new InvalidOperationException("The q-gram length must be equal.");

			var newQgramSet = new QGramSet<T>(this);

			foreach (var pair in set)
			{
				int frequency;
				newQgramSet.TryGetValue(pair.Key, out frequency);
				newQgramSet[pair.Key] = Math.Max(pair.Value, frequency);
			}

			return newQgramSet;
		}

		public QGramSet<T> Intersect(QGramSet<T> set)
		{
			var newQgramSet = new QGramSet<T>();
			foreach (var pair in set)
			{
				int frequency;
				if (TryGetValue(pair.Key, out frequency))
				{
					newQgramSet.Add(pair.Key, Math.Min(pair.Value, frequency));
				}
			}

			return newQgramSet;
		}

		#endregion
	}
}