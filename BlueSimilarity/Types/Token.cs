#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	///     Token represents atomic string for measurement similarity
	/// </summary>
	public class Token : IEquatable<Token>, IComparable<Token>
	{
		#region Constructors

		/// <summary>
		///     Create token from the string
		/// </summary>
		/// <param name="text">the text</param>
		public Token(string text)
		{
			Contract.Requires<ArgumentNullException>(text != null, "The token must be not null.");
			Contract.Requires<ArgumentNullException>(text.Length > 0, "The token must be not empty.");
			Value = text;
		}

		/// <summary>
		///     Create token from normalized string
		/// </summary>
		/// <param name="normalizedString">normalized string</param>
		public Token(NormalizedString normalizedString)
			: this(normalizedString.Value)
		{
			Contract.Requires<ArgumentNullException>(normalizedString != null, "The token must be not null.");
		}

		#endregion

		#region Properties and Indexers

		/// <summary>
		///     Value of the token
		/// </summary>
		public string Value { get; private set; }

		#endregion

		#region IComparable<Token> Members

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
		public int CompareTo(Token other)
		{
			var val = String.Compare(Value, other.Value, StringComparison.Ordinal);

			if (val > 0)
				return 1;
			if (val < 0)
				return -1;
			return 0;
		}

		#endregion

		#region IEquatable<Token> Members

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
		public bool Equals(Token other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Value, other.Value);
		}

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
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Token) obj);
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

		#region Operators

		/// <summary>
		///     Equality operator on token
		/// </summary>
		/// <param name="left">Left token</param>
		/// <param name="right">Right token</param>
		/// <returns>true when equals the tokens otherwise false</returns>
		public static bool operator ==(Token left, Token right)
		{
			return Equals(left, right);
		}

		/// <summary>
		///     Non equality operator on token
		/// </summary>
		/// <param name="left">Left token</param>
		/// <param name="right">Right token</param>
		/// <returns>false when equals the tokens otherwise true</returns>
		public static bool operator !=(Token left, Token right)
		{
			return !Equals(left, right);
		}

		#endregion
	}
}