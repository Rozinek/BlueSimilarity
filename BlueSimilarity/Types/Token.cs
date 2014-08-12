#region

using System;

#endregion

namespace BlueSimilarity.Types
{
	public class Token : IEquatable<Token>, IComparable<Token>
	{
		#region Constructors

		public Token(string token)
		{
			Value = token;
		}

		public Token(NormalizedString token) : this(token.Value)
		{
		}

		#endregion

		#region Properties and Indexers

		public string Value { get; private set; }

		#endregion

		#region IComparable<Token> Members

		public int CompareTo(Token other)
		{
			return String.Compare(Value, other.Value, StringComparison.Ordinal);
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
			return (Value != null ? Value.GetHashCode() : 0);
		}

		public override string ToString()
		{
			return Value;
		}

		#endregion

		#region Operators (${Access})

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