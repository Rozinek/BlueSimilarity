#region

using System.Collections.Generic;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Containers
{
	/// <summary>
	/// Set of tokens with their occurrences
	/// </summary>
	public class TokenSet : Dictionary<Token, int>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TokenSet"/> class.
		/// </summary>
		public TokenSet()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TokenSet"/> class.
		/// </summary>
		/// <param name="capacity">The capacity.</param>
		public TokenSet(int capacity)
			: base(capacity)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TokenSet"/> class.
		/// </summary>
		/// <param name="dictionary">The dictionary.</param>
		public TokenSet(IDictionary<Token, int> dictionary) : base(dictionary)
		{
		}

		#endregion
	}
}