using System;

namespace BlueSimilarity.UseCase
{
	public class Program
	{
		#region Methods (private)

		private static void Main(string[] args)
		{
			var damer = new DamerauLevenshtein();
			var blb = damer.GetDistance("gfsg", "fdsfd");
			
			var leven = new Levenshtein();
			var dist = leven.GetDistance("gfsg", "fdsfd");
 

			var sim = leven.GetSimilarity("daniel", "daniel b");

			Console.WriteLine("The result is {0}", blb);
			Console.WriteLine("The result is {0}", dist);
			Console.WriteLine("The result is {0}", sim);			
	
			
			
			Console.ReadKey();
		}

		#endregion
	}
}