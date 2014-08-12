#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	///     Q-gram with the lenght of string 1 is called unigram
	/// </summary>
	public class Unigram : IQgram, IEquatable<Unigram>, IComparable<Unigram>
	{
		#region Constructors

		public Unigram(string value)
		{
			Contract.Requires<NotSupportedException>(value.Length == 1, "Not requested length.");
			Value = value;
		}

		#endregion

		#region IComparable<UniGram> Members

		public int CompareTo(Unigram other)
		{
			return String.Compare(Value, other.Value, StringComparison.Ordinal);
		}

		#endregion

		#region IEquatable<UniGram> Members

		public bool Equals(Unigram other)
		{
			return string.Equals(Value, other.Value);
		}

		#endregion

		#region IQgram Members

		public int Length
		{
			get { return 1; }
		}

		public string Value { get; private set; }

		#endregion

		#region Methods (public)

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Unigram && Equals((Unigram) obj);
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