using System;
using BlueSimilarity.Indexing;

namespace BlueSimilarity.Definitions
{
	/// <summary>
	/// The similarity metric supports semantic behaviour
	/// </summary>
	public interface ISemantic
	{
		/// <summary>
		/// Gets the vocabulary.
		/// </summary>
		/// <value>The vocabulary.</value>
		SemanticVocabulary Vocabulary { get; }
	}
}
