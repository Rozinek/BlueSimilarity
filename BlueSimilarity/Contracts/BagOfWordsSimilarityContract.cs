using BlueSimilarity.Containers;
using BlueSimilarity.Types;

#region

using System;
using System.Diagnostics.Contracts;
using BlueSimilarity.Definitions;

#endregion

// ReSharper disable once CheckNamespace

namespace BlueSimilarity
{
	[ContractClassFor(typeof (IBagOfWordsSimilarity))]
	internal abstract class BagOfWordsSimilarityContract : IBagOfWordsSimilarity
	{
		#region IBagOfTokenSimilarity Members

		public double GetSimilarity(string[] tokensPattern, string[] tokensTarget)
		{
			Contract.Requires<ArgumentNullException>(tokensPattern != null);
			Contract.Requires<ArgumentNullException>(tokensTarget != null);

			return default(double);
		}

		public double GetSimilarity(NormalizedString[] tokensPattern, NormalizedString[] tokensTarget)
		{
			Contract.Requires<ArgumentNullException>(tokensPattern != null);
			Contract.Requires<ArgumentNullException>(tokensTarget != null);

			return default(double);
		}

		public double GetSimilarity(ITokenizer tokensPattern, ITokenizer tokensTarget)
		{
			Contract.Requires<ArgumentNullException>(tokensPattern != null);
			Contract.Requires<ArgumentNullException>(tokensTarget != null);

			return default(double);
		}

		public abstract TokenSimilarity InternalTokenSimilarity { get; }

		public abstract bool IsSymmetric { get; }

		#endregion
	}
}