#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	///     Q-gram with the length of string 1 is called unigram
	/// </summary>
	public class Unigram : IQgram, IEquatable<Unigram>, IComparable<Unigram>
	{
		#region Static and contants fields

		public const int UnigramLength = 1;

		#endregion

		#region Constructors

		public Unigram(string value)
		{
			Contract.Requires<ArgumentException>(value != null, "Value must not be null or empty");
// ReSharper disable once PossibleNullReferenceException
			Contract.Requires<NotSupportedException>(value.Length == UnigramLength, "Not requested length.");
			Value = value;
		}

		#endregion

		#region IComparable<Unigram> Members

		public int CompareTo(Unigram other)
		{
			var val = String.Compare(Value, other.Value, StringComparison.Ordinal);

			if (val > 0)
				return 1;
			if (val < 0)
				return -1;
			return 0;
		}

		#endregion

		#region IEquatable<Unigram> Members

		public bool Equals(Unigram other)
		{
			return string.Equals(Value, other.Value);
		}

		#endregion

		#region IQgram Members

		public int Length
		{
			get { return UnigramLength; }
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
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value;
		}

		#endregion
	}
}