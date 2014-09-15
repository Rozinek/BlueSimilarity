#region

using System.Collections.Generic;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity.Containers
{
	/// <summary>
	///     Defines operations and properties, which are needed
	///     for working Q-grams set
	/// </summary>
	public interface IQGramSet<T> : IDictionary<T, int> where T : IQgram
	{
		#region Properties and Indexers

		/// <summary>
		///     Q-gram order
		/// </summary>
		int QGramLength { get; }

		#endregion

		//Dictionary<string, int> ToDictionary();
	}
}