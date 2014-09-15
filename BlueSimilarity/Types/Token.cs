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

		public bool Equals(Token other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Value, other.Value);
		}

		#endregion

		#region Methods (public)

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Token) obj);
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
		///     Nonequality operator on token
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