using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.SpeedTest
{
	

	class Program
	{	
		static void Main(string[] args)
		{
			const string pattern = "cool";
			const string text = "col";
			const int distanceExpected = 1;

			var lev = new Levenshtein();
			
			// Levensthein distance test
			var distanceResult = lev.GetDistance(pattern, text);
			Assert.AreEqual(distanceExpected,distanceResult);

			// Normalized Levensthein similarity test
			var normSimResult = lev.GetSimilarity(pattern, text);
			const double normSimExpected = 0.75;
			Assert.AreEqual(normSimResult, normSimExpected, 0.001);
			

		}
	}
}
