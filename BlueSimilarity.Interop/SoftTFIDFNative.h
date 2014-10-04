#include <stdio.h>
#include <malloc.h>
#include <string.h>

#include "Utils.h"
#include "SimHelpers.h"
#include "MathDef.h"
#include "BlueSimilarity_API.h"
#include "SimHelpers.h"

const TokenSimilarity DefaultTokenSimilarity = TokenSimilarity::Jaro;
const double DefaultThreshold = 0.9;

BLUESIMILARITY_API double __stdcall SoftTFIDFNative(const char* patternTokens [], double patternWeights [], int pLen, const char* targetTokens[], double targetWeights [], int tLen, TokenSimilarity tokenSim);

BLUESIMILARITY_API double __stdcall SoftTFIDFNative(const char* patternTokens [], double patternWeights [], int pLen, const char* targetTokens[], double targetWeights [], int tLen, TokenSimilarity tokenSim)
{
	// get the similarity metric
	//SimMetric simMetric = GetSimMetric(DefaultTokenSimilarity);
	SimMetric simMetric = GetSimMetric(tokenSim);

	// unit vectorizing
	UnitVectorizing(patternWeights, pLen);
	UnitVectorizing(targetWeights, tLen);

	double finalScore = 0;
	const double *pWeight = patternWeights;
	for (const char ** pattern = patternTokens; pattern != patternTokens + pLen; pattern++, pWeight++)
	{
		if (pattern == NULL) continue;

		double maxOverToken = MinimumScore;
		double weightOverToken = 0;
		const double *tWeight = targetWeights;
		for (const char ** target = targetTokens; target != targetTokens + tLen; target++, tWeight++)
		{
			if (target == NULL) continue;

			double currentScore = simMetric(*pattern, *target);

			if (currentScore > DefaultThreshold && currentScore > maxOverToken)
			{
				maxOverToken = currentScore;
				weightOverToken = (*pWeight) * (*tWeight);
			}

			// if score achieves maximum score then breaks the loop and increases the performance
			if (currentScore == MaximumScore)
				break;
		}
		finalScore += weightOverToken * maxOverToken;
	}

	return finalScore;
}
