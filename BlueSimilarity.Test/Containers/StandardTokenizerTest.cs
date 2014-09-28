using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueSimilarity.Containers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.Test.Containers
{	
	/// <summary>
	/// 
	/// </summary>
	[TestClass]	
	public class StandardTokenizerTest 
	{
		/// <summary>
		/// 
		/// </summary>
		[TestMethod]
		public void StandardTokenizerCreate()
		{
			var tokenizer = new StandardTokenizer("some .,& text");
			var listWords = tokenizer.ToArray();

			listWords[0].Should().Be("SOME");
			listWords[1].Should().Be("TEXT");
		}			
	}
}
