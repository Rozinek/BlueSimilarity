#region

using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

#endregion

namespace BlueSimilarity.Types
{
	/// <summary>
	///     Normalized the textual string to invariant comparable form
	///     remove diacritics, special symbols and upper case the text
	/// </summary>

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct NormalizedString
	{
		#region Private fields

		[MarshalAsAttribute(UnmanagedType.LPStr)]
		private readonly string _normalizedValue;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="NormalizedString" /> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public NormalizedString(string value)
		{
			Contract.Requires<ArgumentNullException>(value!= null);
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


		/// <summary>
		/// Append lower case, upper case and numbers
		/// </summary>
		/// <param name="unicodeString"></param>
		/// <returns></returns>
		private static string ConvertToCanonicalForm(string unicodeString)
		{
			// TODO: test the decomposion find the appropriate one
			var normalizedString = unicodeString.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder(unicodeString.Length);

			bool isLastCharEmpty = false;
			foreach (var character in normalizedString)
			{
				var charCategory = CharUnicodeInfo.GetUnicodeCategory(character);
				switch (charCategory)
				{
					
					case UnicodeCategory.UppercaseLetter:
					case UnicodeCategory.DecimalDigitNumber:
						stringBuilder.Append(character);
						isLastCharEmpty = false;
						break;
					case UnicodeCategory.LowercaseLetter:
						stringBuilder.Append(Char.ToUpperInvariant(character));
						isLastCharEmpty = false;
						break;
					case UnicodeCategory.NonSpacingMark:
						   break;
					default:
						if (isLastCharEmpty) 
						break;
						stringBuilder.Append(' ');
						isLastCharEmpty = true;
						break;
				}
			}

			return stringBuilder.ToString().Trim();
		}

		#endregion
	}
}