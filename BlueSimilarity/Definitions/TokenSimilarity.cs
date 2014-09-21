namespace BlueSimilarity.Definitions
{
	/// <summary>
	/// Select the internal similarity between tokens (words)
	/// </summary>
	public enum TokenSimilarity
	{
		/// <summary>
		/// Levenshtein similarity
		/// </summary>
		Levenshtein,

		/// <summary>
		/// Damerau-Levenshtein similarity
		/// </summary>
		DamerauLevenshtein,

		/// <summary>
		/// Jaro similarity
		/// </summary>
		Jaro,
		
		/// <summary>
		/// Jaro-Winkler similarity
		/// </summary>
		JaroWinkler,

		/// <summary>
		/// Dice coefficient based on Q-grams
		/// </summary>
		DiceCoefficient,

		/// <summary>
		/// Jaccard coefficient based on Q-grams
		/// </summary>
		JaccardCoefficient,

		/// <summary>
		/// Overlap coefficient based on Q-grams
		/// </summary>
		OverlapCoefficient
	}
}