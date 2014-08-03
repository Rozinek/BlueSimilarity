using System;
using System.Diagnostics.Contracts;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;

namespace BlueSimilarity.Contracts
{
	[ContractClassFor(typeof(ISimilarity))]
	internal abstract class SimilarityContract : ISimilarity
	{
		public double GetSimilarity(string first, string second)
		{
			Contract.Requires<ArgumentNullException>(first != null, "The argument first can not be a null");
			Contract.Requires<ArgumentNullException>(second != null, "The argument second can not be a null");

			return default(int);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			Contract.Requires<ArgumentNullException>(first != null, "The argument first can not be a null");
			Contract.Requires<ArgumentNullException>(second != null, "The argument second can not be a null");

			return default(int);
		}
	}
}
