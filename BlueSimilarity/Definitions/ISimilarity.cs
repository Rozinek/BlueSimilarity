using System.Diagnostics.Contracts;
using BlueSimilarity.Containers;
using BlueSimilarity.Contracts;

namespace BlueSimilarity.Definitions
{
	[ContractClass(typeof(SimilarityContract))]
	public interface ISimilarity
	{
		double GetSimilarity(string first, string second);

		double GetSimilarity(NormalizedString first, NormalizedString second);

		double GetSimilarity(Token first, Token second);
	}
}
