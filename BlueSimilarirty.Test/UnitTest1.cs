using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarirty.Test
{
	[TestClass]
	public class BlueSimilarityInteropEntryPointsTest
	{

		[TestMethod]
		public void LevenstheinDistanceTest()
		{
			var result = BlueSimilarity.BlueSimilarity.LevenshteinDistance("text", "test2");
		}
	}
}
