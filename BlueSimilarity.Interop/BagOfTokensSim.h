#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include "MathDef.h"
#include "BlueSimilarity_API.h"

/// <summary>
/// Select the internal similarity between tokens (words)
/// </summary>
enum TokenSimilarity
{
	/// <summary>
	/// Levenshtein similarity
	/// </summary>
	Levenshtein,

	/// <summary>
	/// Damerau-Levenshtein similarity
	/// </summary>
	DamerauLevenshtein,

	/// <summary>
	/// Jaro similarity
	/// </summary>
	Jaro,

	/// <summary>
	/// Jaro-Winkler similarity
	/// </summary>
	JaroWinkler,

	/// <summary>
	/// Dice coefficient based on Q-grams
	/// </summary>
	DiceCoefficient,

	/// <summary>
	/// Jaccard coefficient based on Q-grams
	/// </summary>
	JaccardCoefficient,

	/// <summary>
	/// Overlap coefficient based on Q-grams
	/// </summary>
	OverlapCoefficient
};

struct NormalizedString
{
	const char* Value;
};

// BagOfTokenSimilarity distance
BLUESIMILARITY_API double __stdcall BagOfTokensSim(const char *patternTokens [], int m, const char *targetTokens [], int n, TokenSimilarity tokenSim, bool isSymmetric);

// BagOfTokenSimilarity distance
BLUESIMILARITY_API double __stdcall BagOfTokensSimStruct(NormalizedString patternTokens [], int m, NormalizedString targetTokens [], int n, TokenSimilarity tokenSim, bool isSymmetric);


// default Q-gram length = 2
const int DefaultQgramLength = 2;
const double MaximumScore = 1.0;

double __stdcall DiceBigram(const char *pattern, const char *text);
double __stdcall JaccardBigram(const char *pattern, const char *text);
double __stdcall OverlapBigram(const char *pattern, const char *text);

double __stdcall BagOfTokensSim(const char *patternTokens [], int m, const char *targetTokens [], int n, TokenSimilarity tokenSim, bool isSymmetric)
{
	double(__stdcall *refSimilarity)(const char *, const char*);

	// choose the reference for internal metric
	switch (tokenSim)
	{
		case Levenshtein:
			refSimilarity = &NormLevSim;
			break;
		case DamerauLevenshtein:
			refSimilarity = &NormDamLevSim;
			break;	
		case Jaro:
			refSimilarity = &JaroNative;
			break;
		case JaroWinkler:
			refSimilarity = &JaroWinklerNative;
			break;
		case DiceCoefficient:
			refSimilarity = &DiceBigram;
			break;
		case JaccardCoefficient:
			refSimilarity = &JaccardBigram;
			break;
		case OverlapCoefficient:
			refSimilarity = &OverlapBigram;
			break;
		default:
			refSimilarity = &NormLevSim;
	}
	
	// re-calculate similarity symmetric vs. not symmetric
	if (isSymmetric && m > n)
	{
		swap(patternTokens, targetTokens);
		swap(m, n);
	}


	double sumOverTokens = 0;
	for (const char ** pattern = patternTokens; pattern != patternTokens + m; pattern++)
	{
		if (pattern == NULL) continue;

		double maxOverToken = 0;
		for (const char ** target = targetTokens; target != targetTokens + n; target++)
		{
			if (target == NULL) continue;

			double currentScore = refSimilarity(*pattern, *target);
			
			// if score achieves maximum score then breaks the loop and increases the performance
			if (currentScore == MaximumScore)
			{
				maxOverToken = MaximumScore;
				break;
			}

			maxOverToken = MAX(maxOverToken, currentScore);
		}
		sumOverTokens += maxOverToken;
	}

	return sumOverTokens / m;
}

double __stdcall BagOfTokensSimStruct(NormalizedString patternTokens [], int m, NormalizedString targetTokens [], int n, TokenSimilarity tokenSim, bool isSymmetric)
{
	double(__stdcall *refSimilarity)(const char *, const char*);

	// choose the reference for internal metric
	switch (tokenSim)
	{
	case Levenshtein:
		refSimilarity = &NormLevSim;
		break;
	case DamerauLevenshtein:
		refSimilarity = &NormDamLevSim;
		break;
	case Jaro:
		refSimilarity = &JaroNative;
		break;
	case JaroWinkler:
		refSimilarity = &JaroWinklerNative;
		break;
	case DiceCoefficient:
		refSimilarity = &DiceBigram;
		break;
	case JaccardCoefficient:
		refSimilarity = &JaccardBigram;
		break;
	case OverlapCoefficient:
		refSimilarity = &OverlapBigram;
		break;
	default:
		refSimilarity = &NormLevSim;
	}

	// re-calculate similarity symmetric vs. not symmetric
	if (isSymmetric && m > n)
	{
		swap(patternTokens, targetTokens);
		swap(m, n);
	}


	double sumOverTokens = 0;
	for (NormalizedString *pattern = patternTokens; pattern != patternTokens + m; pattern++)
	{
		if (pattern == NULL) continue;

		double maxOverToken = 0;
		for (NormalizedString *target = targetTokens; target != targetTokens + n; target++)
		{
			if (target == NULL) continue;

			double currentScore = refSimilarity((*pattern).Value, (*target).Value);

			// if score achieves maximum score then breaks the loop and increases the performance
			if (currentScore == MaximumScore)
			{
				maxOverToken = MaximumScore;
				break;
			}

			maxOverToken = MAX(maxOverToken, currentScore);
		}
		sumOverTokens += maxOverToken;
	}

	return sumOverTokens / m;
}

double __stdcall DiceBigram(const char *pattern, const char *text)
{
	return Dice(pattern, text, DefaultQgramLength);
}

double __stdcall JaccardBigram(const char *pattern, const char *text)
{
	return Jaccard(pattern, text, DefaultQgramLength);
}

double __stdcall OverlapBigram(const char *pattern, const char *text)
{
	return Overlap(pattern, text, DefaultQgramLength);
}


