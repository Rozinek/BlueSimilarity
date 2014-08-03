using System.Diagnostics.Contracts;
using BlueSimilarity.Containers;

namespace BlueSimilarity.Definitions
{
	[ContractClass(typeof(DistanceContract))]
	public interface IDistance
	{
		int GetDistance(string first, string second);

		int GetDistance(NormalizedString first, NormalizedString second);
	}
}