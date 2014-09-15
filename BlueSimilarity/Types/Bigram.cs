#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	///     Q-gram with the length of string 2 is called bigram
	/// </summary>
	public class Bigram : IQgram, IEquatable<Bigram>, IComparable<Bigram>
	{
		#region Static and contants fields

		/// <summary>
		/// The bigram length
		/// </summary>
		public const int BigramLength = 2;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Bigram"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Bigram(string value)
		{
			Contract.Requires<ArgumentNullException>(value != null, "Value must not be null.");
// ReSharper disable once PossibleNullReferenceException
			Contract.Requires<NotSupportedException>(value.Length == BigramLength, "Not requested length.");
			Value = value;
		}

		#endregion

		#region IComparable<Bigram> Members

		public int CompareTo(Bigram other)
		{
			var val = String.Compare(Value, other.Value, StringComparison.Ordinal);

			if (val > 0)
				return 1;
			if (val < 0)
				return -1;
			return 0;
		}

		#endregion

		#region IEquatable<Bigram> Members

		public bool Equals(Bigram other)
		{
			return string.Equals(Value, other.Value);
		}

		#endregion

		#region IQgram Members

		public int Length
		{
			get { return BigramLength; }
		}

		public string Value { get; private set; }

		#endregion

		#region Methods (public)

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Bigram && Equals((Bigram) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value;
		}

		#endregion
	}
}