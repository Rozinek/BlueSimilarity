namespace BlueSimilarity.Types
{
	/// <summary>
	///     Q-gram properties
	/// </summary>
	public interface IQgram
	{
		#region Properties and Indexers

		/// <summary>
		///     Gets the length of q-gram
		/// </summary>
		/// <value>The length.</value>
		int Length { get; }

		/// <summary>
		///     Gets the value of q-gram
		/// </summary>
		/// <value>The value.</value>
		string Value { get; }

		#endregion
	}
}