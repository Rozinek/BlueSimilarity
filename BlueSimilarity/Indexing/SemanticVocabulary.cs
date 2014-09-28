#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using BlueSimilarity.Containers;

#endregion

namespace BlueSimilarity.Indexing
{
	/// <summary>
	///     Learn the semantic vocabulary for semantic similarity metric
	///     from some sources which can represents
	/// </summary>
	[Serializable]
	public class SemanticVocabulary
	{
		private const int DefaultVocabularySize = 1024;


		/// <summary>
		///     Learned vocabulary of unique words
		/// </summary>
		private readonly Dictionary<string, int> _learnedVocabulary;


		/// <summary>
		///     Initializes a new instance of the <see cref="SemanticVocabulary" /> class.
		/// </summary>
		public SemanticVocabulary()
		{
			_learnedVocabulary = new Dictionary<string, int>(DefaultVocabularySize);
		}

		/// <summary>
		///     Gets the total words in vocabulary.
		/// </summary>
		/// <value>The total words.</value>
		public int TotalWords { get; private set; }

		/// <summary>
		///     Gets the unique words.
		/// </summary>
		/// <value>The unique words.</value>
		public int UniqueWords
		{
			get { return _learnedVocabulary.Count; }
		}

		/// <summary>
		///     Gets the semantic weight of the word
		/// </summary>
		/// <param name="word">The word.</param>
		/// <returns>System.Double.</returns>
		internal double GetSemanticWeight(string word)
		{
			if (string.IsNullOrWhiteSpace(word))
				throw new ArgumentException("word");

			int words;
			if (!_learnedVocabulary.TryGetValue(word, out words))
				words = 1;

			return ComputeSemanticWeight(words, TotalWords);
		}

		internal double[] GetSemanticWeight(string[] words)
		{
			var semanticArray = new double[words.Length];
			for (var i = 0; i < semanticArray.Length; i++)
			{
				semanticArray[i] = GetSemanticWeight(words[i]);
			}

			return semanticArray;
		}

		private static double ComputeSemanticWeight(int words, int totalWords)
		{
			var probabilityOccurrence = (double) words/totalWords;
			return -Math.Log(probabilityOccurrence);
		}

		/// <summary>
		///     Adds the source for learning vocabulary
		/// </summary>
		/// <param name="tokenizer">The standard tokenizer.</param>
		public void AddSource(ITokenizer tokenizer)
		{
			foreach (var token in tokenizer)
			{
				int wordCount;
				if (_learnedVocabulary.TryGetValue(token, out wordCount))
				{
					_learnedVocabulary[token] = wordCount + 1;
				}
				else
				{
					_learnedVocabulary.Add(token, 1);
				}

				TotalWords++;
			}
		}

		public void SaveToFile(string fileName)
		{
			var serializedStream = BinarySerializer.Serialize(this);
			var compressedStream = DeflateCompressor.Compress(serializedStream);

			using (var fileStream = new FileStream(fileName, FileMode.Create))
			{
				fileStream.Write(compressedStream, 0, compressedStream.Length);
			}
		}

		public static SemanticVocabulary LoadFromFile(string fileName)
		{
			using (var fileStream = new FileStream(fileName, FileMode.Open))
			{
				var byteStream = new byte[fileStream.Length];
				fileStream.Read(byteStream, 0, byteStream.Length);

				var decompressStream = DeflateCompressor.Decompress(byteStream);
				return BinarySerializer.Deserialize<SemanticVocabulary>(decompressStream);
			}
		}

#if DEBUG

		[OnSerializing]
		private void OnSerializingMethod(StreamingContext context)
		{
			Console.WriteLine("OnSerializing " + GetType().Name);
		}

		[OnSerialized]
		private void OnSerializedMethod(StreamingContext context)
		{
			Console.WriteLine("OnSerialized " + GetType().Name);
		}

		[OnDeserializing]
		private void OnDeserializingMethod(StreamingContext context)
		{
			Console.WriteLine("OnDeserializing " + GetType().Name);
		}

		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			Console.WriteLine("OnSerialize " + GetType().Name);
		}
#endif
	}
}