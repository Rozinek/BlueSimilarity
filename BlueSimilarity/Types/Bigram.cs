#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	///     Q-gram with the lenght of string 2 is called bigram
	/// </summary>
	public class Bigram : IQgram, IEquatable<Bigram>, IComparable<Bigram>
	{
		#region Static and contants fields

		private const int BigramLength = 2;

		#endregion

		#region Constructors

		public Bigram(string value)
		{
			Contract.Requires<NotSupportedException>(value.Length == BigramLength, "Not requested length.");
			Value = value;
		}

		#endregion

		#region IComparable<BiGram> Members

		public int CompareTo(Bigram other)
		{
			return String.Compare(Value, other.Value, StringComparison.Ordinal);
		}

		#endregion

		#region IEquatable<BiGram> Members

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
			return (Value != null ? Value.GetHashCode() : 0);
		}

		public override string ToString()
		{
			return Value;
		}

		#endregion
	}
}