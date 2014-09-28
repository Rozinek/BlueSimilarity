#region

using BlueSimilarity.Containers;
using BlueSimilarity.Indexing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class SemanticBagOfWordsSimilarityTest
	{
		#region Private fields

		private SemanticVocabulary _vocabulary;

		private const double ErrorTolerance = 1e-3;

		#endregion

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityTest()
		{
			const string pattern = "the text";
			const string target = "some text";

			var sim = new SemanticBagOfWordsSimilarity(_vocabulary);

			var semanticSim = sim.GetSimilarity(new StandardTokenizer(pattern), new StandardTokenizer(target));

			semanticSim.Should().BeApproximately(0.79, ErrorTolerance);
		}

		[TestInitialize]
		public void Initializate()
		{
			_vocabulary = new SemanticVocabulary();
			var standardTokenizer = new StandardTokenizer("the the the some some text");
			_vocabulary.AddSource(standardTokenizer);
		}

		#endregion
	}
}