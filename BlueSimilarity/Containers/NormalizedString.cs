using System;
using System.Globalization;
using System.Text;

namespace BlueSimilarity.Containers
{
	/// <summary>
	/// Normalized the textual string to invariant comparable form 
	/// remove diacritics and special symbols ?!@#$%^&*()_+ etc. and
	/// upper case the text
	/// </summary>
	/// 
	public class NormalizedString
	{
		#region Private fields

		private readonly string _normalizedValue;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="value">the string for normalization</param>
		public NormalizedString(string value)
		{
			_normalizedValue = ConvertToCanonicalForm(value);
		}

		#endregion

		#region Properties and Indexers

		public string Value
		{
			get { return _normalizedValue; }
		}

		#endregion

		#region Methods (public)

		public override string ToString()
		{
			return _normalizedValue;
		}

		#endregion

		#region Methods (private)

		private static string ConvertToCanonicalForm(string unicodeString)
		{
			// TODO: test the decompositon find the appropriate one
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