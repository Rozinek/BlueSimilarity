using System;
using BlueSimilarity.Containers;
using BlueSimilarity.Types;

namespace BlueSimilarity.UseCase
{
	public class Program
	{
		#region Methods (private)

		private static void Main(string[] args)
		{
			/****************************************************/
			/*				BagOfTokensSimilarity				*/
			/****************************************************/
			
			// the recommend method for complex similarity on more words
			var bagOfTokens = new BagOfWordsSimilarity();
			const string pattern	= "John Smith";
			const string targetText = "Mr. John Smith, Jr.";

			// using normalized string and tokenizer returns score 1.0
			var resultingSim = bagOfTokens.GetSimilarity(new Tokenizer(new NormalizedString(pattern)), 
														 new Tokenizer(new NormalizedString(targetText)));


			/****************************************************/
			/*				Levenshtein							*/
			/****************************************************/		
			const string nameCorrect = "martha";
			const string nameError =   "marhta";		

			// Levenshtein distance (implements interface IDistance)
			// & similarity (implements interface ISimilarity)
			var lev = new Levenshtein();

			// returns edit distance 2
			var distLev = lev.GetDistance(nameCorrect, nameError);

			// normalized string removes special symbols, diacritics and make case insensitivity
			// returns score 0.67
			var simLev = lev.GetSimilarity(new NormalizedString(nameCorrect), new NormalizedString(nameError));

			/****************************************************/
			/*				Damerau-Levenshtein							*/
			/****************************************************/
			// DamerauLevenshtein implements IDistance and ISimilarity
			var damLev = new DamerauLevenshtein();

			// returns edit distance 1
			var distDamLev = damLev.GetDistance(nameCorrect, nameError);

			// returns score 0.83
			var simDamLev = damLev.GetSimilarity(nameCorrect, nameError);

			/****************************************************/
			/*				Jaro, Jaro-Winler					*/
			/****************************************************/

			// Jaro, Jaro-Winkler implements only ISimilarity 
			var nameFirst = new Token("dwayne");
			var nameSecond = new Token("duane");
			var jaro = new Jaro();
			var jaroWinkler = new JaroWinkler();
			jaroWinkler.GetSimilarity(nameFirst, nameSecond);


			/****************************************************/
			/*				Q-grams coefficient					*/
			/****************************************************/
			// q-grams similarity coefficient - Dice, Jaccard, Overlap
			// with different q-grams type
			var diceUnigrams = new DiceCoefficient<Bigram>();
			var jaccardBigrams = new JaccardCoefficient<Bigram>();
			var overlapTrigrams = new OverlapCoefficient<Bigram>();

			// returns score 0.5
			var jaccardSim = jaccardBigrams.GetSimilarity(pattern, targetText);

			// returns score 0.67
			var diceSim = diceUnigrams.GetSimilarity(pattern, targetText);
	
			// returns score 1.0
			var overlapSim = overlapTrigrams.GetSimilarity(pattern, targetText);

			Console.ReadKey();
		}

		#endregion
	}
}