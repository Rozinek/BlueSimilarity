#region

using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;
using BlueSimilarity.Indexing;
using BlueSimilarity.Types;
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
		public void GetSimilarityDefaultTest()
		{
			const string pattern = "the text";
			const string target = "some text";

			var sim = new SemanticBagOfWordsSimilarity(_vocabulary);

			var semanticSim = sim.GetSimilarity(new StandardTokenizer(pattern), new StandardTokenizer(target));
			
			

			semanticSim.Should().BeApproximately(0.79, ErrorTolerance);
			
			
		}

		[TestMethod]
		public void GetSimilarityInternalMetricTest()
		{
			var sim = new SemanticBagOfWordsSimilarity(_vocabulary, TokenSimilarity.Levenshtein);

			var semanticSimString = sim.GetSimilarity(new[] { "THE", "TEXT" }, new[] { "SOME", "TEXT" });

			semanticSimString.Should().BeApproximately(0.79, ErrorTolerance);
		}

		[TestMethod]
		public void GetSimilarityInternalMetricAndSymmetricTest()
		{
			var sim = new SemanticBagOfWordsSimilarity(_vocabulary, TokenSimilarity.Levenshtein, true);

			var semanticNormalizedString = sim.GetSimilarity(new[] { new NormalizedString("the"), new NormalizedString("text") },
				new[] { new NormalizedString("some"), new NormalizedString("text") });

			semanticNormalizedString.Should().BeApproximately(0.79, ErrorTolerance);
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