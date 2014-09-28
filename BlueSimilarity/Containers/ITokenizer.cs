using System.Collections.Generic;

namespace BlueSimilarity.Containers
{
	/// <summary>
	/// Defines the iterator over the tokens by splitting string
	/// </summary>
	public interface ITokenizer : IEnumerable<string>, IEnumerator<string>
	{

	}
}