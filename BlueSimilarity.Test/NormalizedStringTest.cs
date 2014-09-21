#region

using BlueSimilarity.Containers;
using BlueSimilarity.Types;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test
{
	[TestClass]
	public class NormalizedStringTest
	{
		#region Methods (public)

		[TestMethod]
		public void ConstructorTest()
		{
			var normalizedString = new NormalizedString("abcd");
			Assert.AreEqual("ABCD", normalizedString.Value);
			Assert.AreEqual("ABCD", normalizedString.ToString());
		}

		[TestMethod]
		public void EmptySpaceTest()
		{
			const string twoTokens = "e - Levenshtein";
			var normTwoTokens = new NormalizedString(twoTokens);
			normTwoTokens.Value.Split(' ').Length.Should().Be(2);
		}

		[TestMethod]
		public void RemoveCzechDiactriticsTest()
		{
			// czech diacritics
			const string stringWithCzechDiactritics = "ěščřžýáíóéúůďťň";
			const string expectedNormCzechDiacritics = "ESCRZYAIOEUUDTN";
			var normalizedCzechString = new NormalizedString(stringWithCzechDiactritics);

			Assert.AreEqual(expectedNormCzechDiacritics, normalizedCzechString.Value);
			Assert.AreEqual(expectedNormCzechDiacritics, normalizedCzechString.ToString());
		}

		[Ignore]
		[TestMethod]
		public void RemoveGermanDiactriticsTest()
		{
			// germany diacritics
			const string stringWithGermanDiacritics = "ßüabcdöä";
			var expectedNormGermanDiactritics = ("ß").ToUpperInvariant() + "UABCDOA";

			var normalizedGermanyString = new NormalizedString(stringWithGermanDiacritics);
			Assert.AreEqual(expectedNormGermanDiactritics, normalizedGermanyString.Value);
			Assert.AreEqual(expectedNormGermanDiactritics, normalizedGermanyString);
		}

		[TestMethod]
		public void SpecialSymbolRemovingTest()
		{
			const string stringWithSpecialSymbols = @"?><:|!*[]=)(abcd)&^%$#@!~/\";
			var normalizedString = new NormalizedString(stringWithSpecialSymbols);

			Assert.AreEqual("ABCD", normalizedString.Value);
			Assert.AreEqual("ABCD", normalizedString.ToString());
		}

		#endregion
	}
}