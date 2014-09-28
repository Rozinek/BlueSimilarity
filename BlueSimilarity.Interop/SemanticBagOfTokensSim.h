#include <stdio.h>
#include <malloc.h>
#include <string.h>

#include "Utils.h"
#include "MathDef.h"
#include "BlueSimilarity_API.h"


// BagOfTokenSimilarity distance
BLUESIMILARITY_API double __stdcall SemanticBagOfTokensSim(const char *patternTokens [], const double patternWeights [], int pLen, const char *targetTokens [], const double targetWeights [], int tLen, TokenSimilarity tokenSim, bool isSymmetric);

double __stdcall SemanticBagOfTokensSim(const char *patternTokens [], const double patternWeights [], int pLen, const char *targetTokens [], const double targetWeights [], int tLen, TokenSimilarity tokenSim, bool isSymmetric)
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
	if (isSymmetric && pLen > tLen)
	{
		swap(patternTokens, targetTokens);
		swap(patternWeights, targetWeights);
		swap(pLen, tLen);
	}

	double sumOverTokens = 0;
	double sumWeights = 0;
	const double *pWeight = patternWeights;
	for (const char ** pattern = patternTokens; pattern != patternTokens + pLen; pattern++, pWeight++)
	{
		if (pattern == NULL) continue;

		double maxOverToken = 0;
		double weightOverToken = 0;
		const double *tWeight = targetWeights;
		for (const char ** target = targetTokens;  target != targetTokens + tLen; target++, tWeight++)
		{
			if (target == NULL) continue;

			double currentWeight = (*pWeight) * (*tWeight);
			double currentScore = currentWeight * refSimilarity(*pattern, *target);
			
			if (currentScore > maxOverToken)
			{
				maxOverToken = currentScore;
				weightOverToken = currentWeight;
			}

			// if score achieves maximum score then breaks the loop and increases the performance
			if (currentScore == MaximumScore)
				break;

		}
		sumOverTokens += maxOverToken;
		sumWeights += weightOverToken;
	}

	return sumOverTokens / sumWeights;
}

