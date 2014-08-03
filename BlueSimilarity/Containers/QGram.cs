#region

using System;

#endregion

namespace BlueSimilarity.Containers
{
	/// <summary>
	///     Define the atomic q-gram structure
	///     contained from q-gram value,
	///     and its occurence
	/// </summary>
	public class QGram : IEquatable<QGram>, IComparable<QGram>
	{
		#region Constructors

		public QGram(string value) 
		{
			Value = value;
		}

		#endregion

		#region Properties and Indexers

		/// <summary>
		///     Q-gram length (bi-gram, tri-gram etc.)
		/// </summary>
		public int Length
		{
			get { return Value.Length; }
		}

		/// <summary>
		///     Value of certain q-gram
		/// </summary>
		public string Value { get; private set; }

		#endregion

		#region IComparable<QGram> Members

		public int CompareTo(QGram other)
		{
			return String.Compare(Value, other.Value, StringComparison.Ordinal);
		}

		#endregion

		#region IEquatable<QGram> Members

		public bool Equals(QGram other)
		{
			return string.Equals(Value, other.Value);
		}

		#endregion

		#region Methods (public)

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is QGram && Equals((QGram) obj);
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