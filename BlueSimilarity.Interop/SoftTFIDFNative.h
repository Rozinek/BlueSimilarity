#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include "Utils.h"
#include "MathDef.h"
#include "BlueSimilarity_API.h"


BLUESIMILARITY_API double __stdcall SoftTFIDFNative(const char* patternToken [], double patternWeights [], int pLength, const char* targetToken, double targetWeights [], int tLen, TokenSimilarity tokenSim);


BLUESIMILARITY_API double __stdcall SoftTFIDFNative(const char* patternToken [], double patternWeights [], int pLength, const char* targetToken, double targetWeights [], int tLen, TokenSimilarity tokenSim)
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


}
