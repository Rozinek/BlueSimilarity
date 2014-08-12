#region

using System;
using System.Diagnostics.Contracts;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	[ContractClassFor(typeof (IDistance))]
	internal abstract class DistanceContract : IDistance
	{
		#region IDistance Members

		public int GetDistance(string first, string second)
		{
			Contract.Requires<ArgumentNullException>(first != null, "The argument first can not be a null");
			Contract.Requires<ArgumentNullException>(second != null, "The argument second can not be a null");

			return default(int);
		}

		public int GetDistance(NormalizedString first, NormalizedString second)
		{
			Contract.Requires<ArgumentNullException>(first != null, "The argument first can not be a null");
			Contract.Requires<ArgumentNullException>(second != null, "The argument second can not be a null");

			return default(int);
		}

		public int GetDistance(Token first, Token second)
		{
			Contract.Requires<ArgumentNullException>(first != null, "The argument first can not be a null");
			Contract.Requires<ArgumentNullException>(second != null, "The argument second can not be a null");

			return default(int);
		}

		#endregion
	}
}