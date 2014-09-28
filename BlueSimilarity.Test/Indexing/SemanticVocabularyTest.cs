
using System.IO;
using BlueSimilarity.Containers;
using BlueSimilarity.Indexing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.Test.Indexing
{
	[TestClass]
	public class SemanticVocabularyTest
	{
		private const string VocabularyFileName = "vocabulary.bin";

		[TestInitialize]
		public void Initialize()
		{
			var fileInfo = new FileInfo(VocabularyFileName);
			if(fileInfo.Exists)
				fileInfo.Delete();
		}

		/// <summary>
		/// Serialize/deserialize
		/// </summary>
		[TestMethod]
		public void SemanticVocabularySerialization()
		{
			
			var vocOrigin = new SemanticVocabulary();

			var tokenizer =
				new StandardTokenizer(
					"In computer science, an inverted " +
					"index (also referred to as postings file or inverted file) is an index data structure storing a mapping from content, " +
					"such as words or numbers, to its locations in a database file, or in a document or a set of documents. " +
					"The purpose of an inverted index is to allow fast full text searches, " +
					"at a cost of increased processing when a document is added to the database. " +
					"The inverted file may be the database file itself, rather than its index. " +
					"It is the most popular data structure used in document retrieval systems,[1] " +
					"used on a large scale for example in search engines. " +
					"Several significant general-purpose mainframe-based database management systems have used " +
					"inverted list architectures, including ADABAS, DATACOM/DB, and Model 204.");

			vocOrigin.AddSource(tokenizer);

			vocOrigin.TotalWords.Should().Be(128);
			vocOrigin.UniqueWords.Should().Be(78);

			vocOrigin.SaveToFile(VocabularyFileName);
			var vocDeser = SemanticVocabulary.LoadFromFile(VocabularyFileName);

			// test if the file exist
			var fileInfo = new FileInfo(VocabularyFileName);
			fileInfo.Exists.Should().BeTrue();

			vocOrigin.Equals(vocDeser).Should().BeFalse();
			vocDeser.TotalWords.Should().Be(vocOrigin.TotalWords);
			vocDeser.UniqueWords.Should().Be(vocOrigin.UniqueWords);
		}

		[TestMethod]
		public void SemanticWeightsTest()
		{
			// the 3x, some 2x, text 1x
			var tokenizer = new StandardTokenizer("the the the some some text");
			var vocabulary = new SemanticVocabulary();
			vocabulary.AddSource(tokenizer);

			var wThe  = vocabulary.GetSemanticWeight("THE");
			var wSome = vocabulary.GetSemanticWeight("SOME");
			var wText = vocabulary.GetSemanticWeight("TEXT");

			wThe.Should().BeLessThan(wSome);
			wSome.Should().BeLessThan(wText);
		}
	}
}
