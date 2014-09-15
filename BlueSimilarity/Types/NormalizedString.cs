#region

using System;
using System.Globalization;
using System.Text;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	///     Normalized the textual string to invariant comparable form
	///     remove diacritics, special symbols and upper case the text
	/// </summary>
	public class NormalizedString
	{
		#region Private fields

		private readonly string _normalizedValue;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="NormalizedString" /> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public NormalizedString(string value)
		{
			_normalizedValue = ConvertToCanonicalForm(value);
		}

		#endregion

		#region Properties and Indexers

		/// <summary>
		///     Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public string Value
		{
			get { return _normalizedValue; }
		}

		#endregion

		#region Methods (public)

		/// <summary>
		///     Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString()
		{
			return _normalizedValue;
		}

		#endregion

		#region Methods (private)

		private static string ConvertToCanonicalForm(string unicodeString)
		{
			// TODO: test the decomposion find the appropriate one
			var normalizedString = unicodeString.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder(unicodeString.Length);

			foreach (var character in normalizedString)
			{
				var charCategory = CharUnicodeInfo.GetUnicodeCategory(character);
				switch (charCategory)
				{
					case UnicodeCategory.LowercaseLetter:
					case UnicodeCategory.UppercaseLetter:
					case UnicodeCategory.DecimalDigitNumber:
						stringBuilder.Append(Char.ToUpperInvariant(character));
						break;
					case UnicodeCategory.SpaceSeparator:
					case UnicodeCategory.ConnectorPunctuation:
					case UnicodeCategory.DashPunctuation:
						stringBuilder.Append(' ');
						break;
				}
			}

			return stringBuilder.ToString();
		}

		#endregion
	}
}