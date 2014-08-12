#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	///     Q-gram with the lenght of string 2 is called bigram
	/// </summary>
	public class Trigram : IQgram, IEquatable<Trigram>, IComparable<Trigram>
	{
		#region Static and contants fields

		private const int TrigramLength = 3;

		#endregion

		#region Constructors

		public Trigram(string value)
		{
			Contract.Requires<NotSupportedException>(value.Length == TrigramLength, "Not requested length.");
			Value = value;
		}

		#endregion

		#region IComparable<TriGram> Members

		public int CompareTo(Trigram other)
		{
			return String.Compare(Value, other.Value, StringComparison.Ordinal);
		}

		#endregion

		#region IEquatable<TriGram> Members

		public bool Equals(Trigram other)
		{
			return string.Equals(Value, other.Value);
		}

		#endregion

		#region IQgram Members

		public int Length
		{
			get { return TrigramLength; }
		}

		public string Value { get; private set; }

		#endregion

		#region Methods (public)

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Trigram && Equals((Trigram) obj);
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