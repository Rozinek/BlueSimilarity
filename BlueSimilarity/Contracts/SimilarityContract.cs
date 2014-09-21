#region

using System;
using System.Diagnostics.Contracts;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

// ReSharper disable once CheckNamespace

namespace BlueSimilarity
{
	[ContractClassFor(typeof (ISimilarity))]
	internal abstract class SimilarityContract : ISimilarity
	{
		#region ISimilarity Members

		public double GetSimilarity(string first, string second)
		{
			Contract.Requires<ArgumentNullException>(first != null, "The argument first can not be a null");
			Contract.Requires<ArgumentNullException>(second != null, "The argument second can not be a null");

			return default(double);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			//Contract.Requires<ArgumentNullException>(first != null, "The argument first can not be a null");
			//Contract.Requires<ArgumentNullException>(second != null, "The argument second can not be a null");

			return default(double);
		}

		public double GetSimilarity(Token first, Token second)
		{
			Contract.Requires<ArgumentNullException>(first != null, "The argument first can not be a null");
			Contract.Requires<ArgumentNullException>(second != null, "The argument second can not be a null");

			return default(double);
		}

		#endregion
	}
}