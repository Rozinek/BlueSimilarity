#region

using System.Diagnostics.Contracts;
using BlueSimilarity.Containers;
using BlueSimilarity.Contracts;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Definitions
{
	[ContractClass(typeof (SimilarityContract))]
	public interface ISimilarity
	{
		#region Methods (public)

		double GetSimilarity(string first, string second);

		double GetSimilarity(NormalizedString first, NormalizedString second);

		double GetSimilarity(Token first, Token second);

		#endregion
	}
}