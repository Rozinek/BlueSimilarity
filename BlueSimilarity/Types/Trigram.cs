#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	///     Q-gram with the length of string 2 is called bigram
	/// </summary>
	public class Trigram : IQgram, IEquatable<Trigram>, IComparable<Trigram>
	{
		#region Static and contants fields

		/// <summary>
		///     The trigram length
		/// </summary>
		public const int TrigramLength = 3;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="Trigram" /> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public Trigram(string value)
		{
			Contract.Requires<ArgumentNullException>(value != null, "Value must not be null.");
			// ReSharper disable once PossibleNullReferenceException
			Contract.Requires<NotSupportedException>(value.Length == TrigramLength, "Not requested length.");
			Value = value;
		}

		#endregion

		#region IComparable<Trigram> Members

		/// <summary>
		///     Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///     A value that indicates the relative order of the objects being compared. The return value has the following
		///     meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This
		///     object is equal to <paramref name="other" />. Greater than zero This object is greater than
		///     <paramref name="other" />.
		/// </returns>
		public int CompareTo(Trigram other)
		{
			var val = String.Compare(Value, other.Value, StringComparison.Ordinal);

			if (val > 0)
				return 1;
			if (val < 0)
				return -1;
			return 0;
		}

		#endregion

		#region IEquatable<Trigram> Members

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
		public bool Equals(Trigram other)
		{
			return string.Equals(Value, other.Value);
		}

		#endregion

		#region IQgram Members

		/// <summary>
		///     Gets the length of q-gram
		/// </summary>
		/// <value>The length.</value>
		public int Length
		{
			get { return TrigramLength; }
		}

		/// <summary>
		///     Gets the value of q-gram
		/// </summary>
		/// <value>The value.</value>
		public string Value { get; private set; }

		#endregion

		#region Methods (public)

		/// <summary>
		///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Trigram && Equals((Trigram) obj);
		}

		/// <summary>
		///     Returns a hash code for this instance.
		/// </summary>
		/// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		/// <summary>
		///     Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString()
		{
			return Value;
		}

		#endregion
	}
}