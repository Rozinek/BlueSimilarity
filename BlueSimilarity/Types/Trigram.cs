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

		public const int TrigramLength = 3;

		#endregion

		#region Constructors

		public Trigram(string value)
		{
			Contract.Requires<ArgumentNullException>(value != null, "Value must not be null.");
			// ReSharper disable once PossibleNullReferenceException
			Contract.Requires<NotSupportedException>(value.Length == TrigramLength, "Not requested length.");
			Value = value;
		}

		#endregion

		#region IComparable<Trigram> Members

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
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value;
		}

		#endregion
	}
}