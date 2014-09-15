#region

using System.Collections.Generic;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Containers
{
	/// <summary>
	/// </summary>
	public class TokenSet : Dictionary<Token, int>
	{
		#region Constructors

		/// <summary>
		/// </summary>
		public TokenSet()
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="capacity"></param>
		public TokenSet(int capacity)
			: base(capacity)
		{
		}

		public TokenSet(IDictionary<Token, int> dictionary) : base(dictionary)
		{
		}

		#endregion
	}
}