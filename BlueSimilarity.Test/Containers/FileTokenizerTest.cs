﻿#region

using System.IO;
using System.Linq;
using BlueSimilarity.Containers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Containers
{
	[TestClass]
	public class FileTokenizerTest
	{
		#region Methods (public)

		private const  string TestFileName = "testFile.txt";

		/// <summary>
		///     Read the file and tokenizer him
		/// </summary>
		[TestMethod]
		public void FileTokenizerReading()
		{
			var fileTokenizer = new FileTokenizer(new FileInfo(TestFileName));
			var listTokens = fileTokenizer.ToList();
			listTokens.Count.Should().Be(102);
		}

		[TestInitialize]
		public void Initializate()
		{
			const string testText = "In computer science, an inverted index (also referred to as postings file or " +
			                        "inverted file) is an index data structure storing a mapping from content, such as words or " +
									"numbers, to its locations in a database file, or in a document or a set of documents.  \n " +


			                        "The purpose of an inverted index is to allow fast full text searches, at a cost of increased " +
			                        "processing when a document is added to the database. The inverted file may be the database " +
			                        "file itself, rather than its index. It is the most popular data structure used in document" +
			                        "retrieval systems,[1] used on a large scale for example in search engines. Several significant " +
			                        "general-purpose mainframe-based database management systems have used inverted list architectures," +
			                        " including ADABAS, DATACOM/DB, and Model 204. There are two main variants of inverted indexes: " +
			                        "A record level inverted index (or inverted file index or just inverted file) contains a list of " +
			                        "references to documents for each word. A word level inverted index (or full inverted index or " +
			                        "inverted list) additionally contains the positions of each word within a document.[2] \n" +


			                        "The latter form offers more functionality (like phrase searches), but needs more processing " +
			                        "power and space to be created.";

			using (var fileStream = File.Create(TestFileName))
			using (var streamWriter = new StreamWriter(fileStream))
			{
				streamWriter.Write(testText);
			}
		}

		#endregion
	}
}