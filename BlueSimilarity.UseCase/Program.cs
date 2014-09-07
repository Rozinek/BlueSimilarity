using System;

namespace BlueSimilarity.UseCase
{
	public class Program
	{
		#region Methods (private)

		private static void Main(string[] args)
		{
			var leven = new Levenshtein();

			var sim = leven.GetSimilarity("daniel", "daniel b");

			Console.WriteLine("The result is {0}", sim);

			Console.ReadKey();
		}

		#endregion
	}
}