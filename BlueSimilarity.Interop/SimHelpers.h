#ifndef _SIMHELPER_
#define _SIMHELPER_

#include "Utils.h"
#include "Levenshtein.h"
#include "DamerauLevenshtein.h"
#include "JaroNative.h"
#include "JaroWinklerNative.h"
#include "LongestCommonSubsequence.h"
#include "Dice.h"
#include "Jaccard.h"
#include "Overlap.h"

const double MaximumScore = 1.0;
const double MinimumScore = 0.0;

typedef double(__stdcall *SimMetric)(const char*, const char*);


SimMetric GetSimMetric(TokenSimilarity tokenSim);

SimMetric GetSimMetric(TokenSimilarity tokenSim)
{
	SimMetric sim;
	switch (tokenSim)
	{
			case Levenshtein:
				sim = NormLevSim;
			case DamerauLevenshtein:
				sim = NormDamLevSim;
			case Jaro:
				sim = JaroNative;
			case JaroWinkler:
				sim = JaroWinklerNative;
			case DiceCoefficient:
				sim = DiceBigram;
			case JaccardCoefficient:
				sim = JaccardBigram;
			case OverlapCoefficient:
				sim = OverlapBigram;
			case Exact:
			default:
				sim = ExactMatch;
	}

	return sim;
}

#endif