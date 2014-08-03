using System.Collections.Generic;

namespace BlueSimilarity.Containers
{
	/// <summary>
	///     Defines operations and properties, which are needed
	///     for working Q-grams set
	/// </summary>
	public interface IQGramSet : IDictionary<QGram, int>
	{
		#region Properties and Indexers

		/// <summary>
		/// Q-gram order
		/// </summary>
		int QGramLength { get; }

		Dictionary<string, int> ToDictionary();

		#endregion
	}
}