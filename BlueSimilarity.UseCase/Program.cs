using System;
using BlueSimilarity.Types;

namespace BlueSimilarity.UseCase
{
	public class Program
	{
		#region Methods (private)

		private static void Main(string[] args)
		{
			const string nameCorrect = "martha";
			const string nameError = "marhta@";		

			// Levenshtein distance (implements interface IDistance)
			// & similarity (implements interface ISimilarity)
			var lev = new Levenshtein();
			var distLev = lev.GetDistance(nameCorrect, nameError);
			// normalized string removes special symbols, diacritics and make case insensitivity
			var simLev = lev.GetSimilarity(new NormalizedString(nameCorrect), new NormalizedString(nameError));

			// Another similarity metric implements IDistance and ISimilarity
			var damLev = new DamerauLevenshtein();

			// Jaro, Jaro-Winkler implements only ISimilarity 
			var nameFirst = new Token("dwayne");
			var nameSecond = new Token("duane");
			var jaro = new Jaro();
			var jaroWinkler = new JaroWinkler();
			jaroWinkler.GetSimilarity(nameFirst, nameSecond);

			// q-grams similarity coefficient - Dice, Jaccard, Overlap
			// with different q-grams type
			var diceUnigrams = new DiceCoefficient<Unigram>();
			var jaccardBigrams = new JaccardCoefficient<Bigram>();
			var overlapTrigrams = new OverlapCoefficient<Trigram>();




			Console.ReadKey();
		}

		#endregion
	}
}