#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Containers
{
	/// <summary>
	///     Split the free text in tokens
	/// </summary>
	public class Tokenizer : ITokenizer
	{
		#region Static and contants fields

		private static readonly char[] DefaultTokenDelimiters = {' ', '.', ';'};

		#endregion

		#region Private fields

		private readonly IEnumerator<string> _tokensEnumerator;

		#endregion

		#region Constructors

		/// <summary>
		///     Constructs a <see cref="Tokenizer" /> on the specified string, using the default delimiter set
		/// </summary>
		/// <param name="text">The text.</param>
		public Tokenizer(string text)
			: this(text, DefaultTokenDelimiters)
		{
			Contract.Requires<ArgumentNullException>(text != null);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="Tokenizer" /> class.
		/// </summary>
		/// <param name="normalizedText">The normalized text.</param>
		public Tokenizer(NormalizedString normalizedText)
		{
			var tokens = normalizedText.Value.Split(new[] {' '});
			_tokensEnumerator = ((IEnumerable<string>) tokens).GetEnumerator();
			;
		}


		/// <summary>
		///     Initializes a new instance of the <see cref="Tokenizer" /> class.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="delimiters">The delimiters.</param>
		public Tokenizer(string text, char[] delimiters)
		{
			Contract.Requires<ArgumentNullException>(text != null);
			Contract.Requires<ArgumentNullException>(delimiters != null);

			_tokensEnumerator =
				((IEnumerable<string>) text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)).GetEnumerator();
		}

		#endregion

		#region ITokenizer Members

		/// <summary>
		///     Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			_tokensEnumerator.Dispose();
		}

		/// <summary>
		///     Advances the enumerator to the next element of the collection.
		/// </summary>
		/// <returns>
		///     true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the
		///     end of the collection.
		/// </returns>
		public bool MoveNext()
		{
			return _tokensEnumerator.MoveNext();
		}

		/// <summary>
		///     Sets the enumerator to its initial position, which is before the first element in the collection.
		/// </summary>
		public void Reset()
		{
			_tokensEnumerator.Reset();
		}

		/// <summary>
		///     Gets the element in the collection at the current position of the enumerator.
		/// </summary>
		/// <value>The current.</value>
		object IEnumerator.Current
		{
			get { return Current; }
		}

		/// <summary>
		///     Gets the element in the collection at the current position of the enumerator.
		/// </summary>
		/// <value>The current.</value>
		public string Current
		{
			get { return _tokensEnumerator.Current; }
		}

		/// <summary>
		///     Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the
		///     collection.
		/// </returns>
		public IEnumerator<string> GetEnumerator()
		{
			return _tokensEnumerator;
		}

		#endregion
	}
}