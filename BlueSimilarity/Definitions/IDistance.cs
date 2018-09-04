#region

using System.Diagnostics.Contracts;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Definitions
{
	/// <summary>
	///     Edit distance is a way of quantifying how dissimilar two strings (e.g., words) are to one another by counting the
	///     minimum number of operations required to transform one string into the other.
	/// </summary>
	public interface IDistance
	{
		#region Methods (public)

		/// <summary>
		///     Count edit distance
		/// </summary>
		/// <param name="first">first string</param>
		/// <param name="second">second string</param>
		/// <returns>The minimum number of operations required to transform one string into the other.</returns>
		int GetDistance(string first, string second);

		/// <summary>
		///     Count edit distance
		/// </summary>
		/// <param name="first">first <see cref="NormalizedString" /></param>
		/// <param name="second">second <see cref="NormalizedString" /></param>
		/// <returns>The minimum number of operations required to transform one string into the other.</returns>
		int GetDistance(NormalizedString first, NormalizedString second);

		/// <summary>
		///     Count edit distance
		/// </summary>
		/// <param name="first">first <see cref="Token" /></param>
		/// <param name="second">second <see cref="Token" /></param>
		/// <returns>The minimum number of operations required to transform one string into the other.</returns>
		int GetDistance(Token first, Token second);

		#endregion
	}
}