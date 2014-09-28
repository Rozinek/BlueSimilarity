using BlueSimilarity.Types;

namespace BlueSimilarity.Containers
{
	/// <summary>
	/// Standard tokenizer
	/// </summary>
	public class StandardTokenizer : Tokenizer
	{
		/// <summary>
		/// Constructs a <see cref="Tokenizer" /> on the specified string, using the default delimiter set
		/// </summary>
		/// <param name="text">The text.</param>
		public StandardTokenizer(string text) : base(new NormalizedString(text))
		{
			
		}
	}
}
