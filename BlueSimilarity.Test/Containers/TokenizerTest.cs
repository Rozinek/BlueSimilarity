using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueSimilarity.Containers;
using BlueSimilarity.Types;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.Test.Containers
{
	[TestClass]
	public class TokenizerTest
	{
		[TestMethod]
		public void TokenizerIterationNormalizedString()
		{
			const string freeText = "BlueSimilarity is a similarity metric library optimized for speed " +
			                        "and simple usage (edit distance - Levenshtein, Damerau-Levenshtein, Jaro, " +
			                        "Jaro-Winkler, Jaccard, Dice, Overlap and other metrics)";

			var tokenizer = new Tokenizer(new NormalizedString(freeText));
					
			int tokenCount = tokenizer.Count();
			tokenCount.Should().Be(26);

			// tokenizer not contain empty token
			tokenizer.Reset();
			tokenizer.ToArray().Any(x => x == string.Empty).Should().BeFalse();

			tokenizer.MoveNext().Should().BeFalse();
			tokenizer.Reset();
			tokenizer.MoveNext().Should().BeTrue();

			tokenizer.Dispose();
			tokenizer.MoveNext().Should().BeTrue();
			tokenizer.Current.Should().Be("IS");
		}


		[TestMethod]
		public void TokenizerIteration()
		{
			const string freeText = "BlueSimilarity is a";

			var tokenizer = new Tokenizer(freeText);
			tokenizer.Count().Should().Be(3);

			tokenizer.Reset();
			tokenizer.MoveNext();
			(((IEnumerable) tokenizer).GetEnumerator()).Current.Should().Be("BlueSimilarity");

			((IEnumerable) tokenizer).GetEnumerator().Should().NotBeNull();
		}
	}
}
