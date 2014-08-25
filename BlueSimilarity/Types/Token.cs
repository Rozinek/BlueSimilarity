#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	/// Token set container for unique token and their frequency of the occurence
	/// </summary>
	public class Token : IEquatable<Token>, IComparable<Token>
	{
		#region Constructors

		public Token(string token)
		{
			Contract.Requires<ArgumentNullException>(token != null, "The token must be not null.");
			Contract.Requires<ArgumentNullException>(token.Length > 0, "The token must be not empty.");
			Value = token;
		}

		public Token(NormalizedString token) : this(token.Value)
		{
			Contract.Requires<ArgumentNullException>(token != null, "The token must be not null.");
		}

		#endregion

		#region Properties and Indexers

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

		public static bool operator ==(Token left, Token right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Token left, Token right)
		{
			return !Equals(left, right);
		}

		#endregion
	}
}