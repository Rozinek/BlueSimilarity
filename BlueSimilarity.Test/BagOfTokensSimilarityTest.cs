﻿#region

using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class BagOfTokensSimilarityTest
	{
		private const double ErrorTolerance = 1E-03;

		#region Methods (public)

		[TestMethod]
		public void GetSimilarityNormalizedStringNotSymmetric()
		{
			var simNormString = new BagOfWordsSimilarity();
			var patternTokens = new[] { new NormalizedString("miss"), new NormalizedString("anna"), new NormalizedString("kurnikovova") };
			var targetTokens =  new[] {  new NormalizedString("kurnikovova"), new NormalizedString("anna"), };

			var simResult = simNormString.GetSimilarity(patternTokens, targetTokens);

			simResult.Should().BeApproximately(0.696, ErrorTolerance);
		}

		[TestMethod]
        public void GetSimilarityTokenizerNotSymmetric()
		{
			const string pattern = "miss anna kurnikovova";
			const string target = "kurnikovova anna";

			var simNormString = new BagOfWordsSimilarity();
			var patternTokenizer = new Tokenizer(pattern);
			var targetTokenizer = new Tokenizer(target);
			var simResult = simNormString.GetSimilarity(patternTokenizer, targetTokenizer);

			simResult.Should().BeApproximately(0.696, ErrorTolerance);
		}

		[TestMethod]
		public void GetSimilaritySymmetric()
		{
			// not symmetric instance of BagOfTokensSimilarity
			var bagOfTokensNotSymmetric = new BagOfWordsSimilarity(TokenSimilarity.Levenshtein, false);
			bagOfTokensNotSymmetric.IsSymmetric.Should().Be(false);

			// symmetric instance of BagOfTokensSimilarity
			var bagOfTokensSymmetric = new BagOfWordsSimilarity(TokenSimilarity.Levenshtein, true);
			bagOfTokensSymmetric.IsSymmetric.Should().Be(true);

			var patternTokens = new[] {"miss",  "anna", "kurnikovova" };
			var targetTokens = new[] { "kurnikovova", "anna" };

			var scoreSymmetricRes = bagOfTokensNotSymmetric.GetSimilarity(patternTokens, targetTokens);
            scoreSymmetricRes.Should().BeApproximately(0.696, ErrorTolerance);

			var scoreNotSymmetricRes =  bagOfTokensSymmetric.GetSimilarity(patternTokens, targetTokens);
			scoreNotSymmetricRes.Should().Be(1.0);
		}

		[TestMethod]
		public void DefaultConstructor()
		{
			var simDefault = new BagOfWordsSimilarity();
			simDefault.InternalTokenSimilarity.Should().Be(TokenSimilarity.Levenshtein);
			simDefault.IsSymmetric.Should().Be(false);
		}

		[TestMethod]
		public void GetSimilarityStringsDefault()
		{
			var bagOfTokensSim = new BagOfWordsSimilarity();

			var patternTokens = new[] {"anna", "kurnikovova"};
			var targetTokens = new[] {"kurnikovova", "anna"};
			var scoreRes = bagOfTokensSim.GetSimilarity(patternTokens, targetTokens);
			scoreRes.Should().Be(1.0);

			var patternTokens2 = new[] { "jaromir", "jagr" };
			var targetTokens2 = new[] { "jaromir", "jager" };
			var scoreRes2 = bagOfTokensSim.GetSimilarity(patternTokens2, targetTokens2);

			scoreRes2.Should().Be(0.9);
		}

		[TestMethod]
		public void GetSimilarityStringsLevenshtein()
		{
			var bagOfTokensSim = new BagOfWordsSimilarity(TokenSimilarity.Levenshtein);
			bagOfTokensSim.InternalTokenSimilarity.Should().Be(TokenSimilarity.Levenshtein);

			var patternTokens = new[] { "anna", "kurnikovova" };
			var targetTokens = new[] { "kurnikovova", "anna" };
			var scoreRes = bagOfTokensSim.GetSimilarity(patternTokens, targetTokens);
			scoreRes.Should().Be(1.0);

			var patternTokens2 = new[] { "jaromir", "jagr" };
			var targetTokens2 = new[] { "jaromir", "jager" };
			var scoreRes2 = bagOfTokensSim.GetSimilarity(patternTokens2, targetTokens2);

			scoreRes2.Should().BeApproximately(0.9, ErrorTolerance);
		}

		[TestMethod]
		public void GetSimilarityStringsDamerauLevenshtein()
		{
			var bagOfTokensSim = new BagOfWordsSimilarity(TokenSimilarity.DamerauLevenshtein);
			bagOfTokensSim.InternalTokenSimilarity.Should().Be(TokenSimilarity.DamerauLevenshtein);

			var patternTokens = new[] { "anna", "kurnikovova" };
			var targetTokens = new[] { "kurnikovova", "anna" };
			var scoreRes = bagOfTokensSim.GetSimilarity(patternTokens, targetTokens);
			scoreRes.Should().Be(1.0);

			var patternTokens2 = new[] { "jaromir", "jager" };
			var targetTokens2 = new[] { "jaromir", "jagre" };
			var scoreRes2 = bagOfTokensSim.GetSimilarity(patternTokens2, targetTokens2);

			scoreRes2.Should().BeApproximately(0.9, ErrorTolerance);
		}

		[TestMethod]
		public void GetSimilarityStringsJaro()
		{
			var bagOfTokensSim = new BagOfWordsSimilarity(TokenSimilarity.Jaro);
			bagOfTokensSim.InternalTokenSimilarity.Should().Be(TokenSimilarity.Jaro);

			var patternTokens = new[] { "anna", "kurnikovova" };
			var targetTokens = new[] { "kurnikovova", "anna" };
			var scoreRes = bagOfTokensSim.GetSimilarity(patternTokens, targetTokens);
			scoreRes.Should().Be(1.0);

			var patternTokens2 = new[] { "jaromir", "jager" };
			var targetTokens2 = new[] { "jaromir", "jagre" };
			var scoreRes2 = bagOfTokensSim.GetSimilarity(patternTokens2, targetTokens2);

			scoreRes2.Should().BeApproximately(0.966, ErrorTolerance);
		}

		[TestMethod]
		public void GetSimilarityStringsJaroWinkler()
		{
			var bagOfTokensSim = new BagOfWordsSimilarity(TokenSimilarity.JaroWinkler);
			bagOfTokensSim.InternalTokenSimilarity.Should().Be(TokenSimilarity.JaroWinkler);

			var patternTokens = new[] { "anna", "kurnikovova" };
			var targetTokens = new[] { "kurnikovova", "anna" };
			var scoreRes = bagOfTokensSim.GetSimilarity(patternTokens, targetTokens);
			scoreRes.Should().Be(1.0);

			var patternTokens2 = new[] { "jaromir", "jager" };
			var targetTokens2 = new[] { "jaromir", "jagre" };
			var scoreRes2 = bagOfTokensSim.GetSimilarity(patternTokens2, targetTokens2);

			scoreRes2.Should().BeApproximately(0.976, ErrorTolerance);
		}

		[TestMethod]
		public void GetSimilarityStringsDiceCoefficient()
		{
			var bagOfTokensSim = new BagOfWordsSimilarity(TokenSimilarity.DiceCoefficient);
			bagOfTokensSim.InternalTokenSimilarity.Should().Be(TokenSimilarity.DiceCoefficient);

			var patternTokens = new[] { "anna", "kurnikovova" };
			var targetTokens = new[] { "kurnikovova", "anna" };
			var scoreRes = bagOfTokensSim.GetSimilarity(patternTokens, targetTokens);
			scoreRes.Should().Be(1.0);

			var patternTokens2 = new[] { "jaromir", "jager" };
			var targetTokens2 = new[] { "jaromir", "jagre" };
			var scoreRes2 = bagOfTokensSim.GetSimilarity(patternTokens2, targetTokens2);

			scoreRes2.Should().BeApproximately(0.750, ErrorTolerance);

		}

		[TestMethod]
		public void GetSimilarityStringsJaccardCoefficient()
		{
			var bagOfTokensSim = new BagOfWordsSimilarity(TokenSimilarity.JaccardCoefficient);
			bagOfTokensSim.InternalTokenSimilarity.Should().Be(TokenSimilarity.JaccardCoefficient);

			var patternTokens = new[] { "anna", "kurnikovova" };
			var targetTokens = new[] { "kurnikovova", "anna" };
			var scoreRes = bagOfTokensSim.GetSimilarity(patternTokens, targetTokens);
			scoreRes.Should().Be(1.0);

			var patternTokens2 = new[] { "jaromir", "jager" };
			var targetTokens2 = new[] { "jaromir", "jagre" };
			var scoreRes2 = bagOfTokensSim.GetSimilarity(patternTokens2, targetTokens2);

			scoreRes2.Should().BeApproximately(0.667, ErrorTolerance);
		}


		[TestMethod]
		public void GetSimilarityStringsOverlapCoefficient()
		{
			var bagOfTokensSim = new BagOfWordsSimilarity(TokenSimilarity.OverlapCoefficient);
			bagOfTokensSim.InternalTokenSimilarity.Should().Be(TokenSimilarity.OverlapCoefficient);

			var patternTokens = new[] { "anna", "kurnikovova" };
			var targetTokens = new[] { "kurnikovova", "anna" };
			var scoreRes = bagOfTokensSim.GetSimilarity(patternTokens, targetTokens);
			scoreRes.Should().Be(1.0);

			var patternTokens2 = new[] { "jaromir", "jager" };
			var targetTokens2 = new[] { "jaromir", "jagre" };
			var scoreRes2 = bagOfTokensSim.GetSimilarity(patternTokens2, targetTokens2);

			scoreRes2.Should().BeApproximately(0.750, ErrorTolerance);
		}

		#endregion
	}
}