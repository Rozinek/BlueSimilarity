#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include <math.h>

#include "Utils.h"
#include "SimHelpers.h"
#include "MathDef.h"
#include "BlueSimilarity_API.h"


// BagOfTokenSimilarity distance
BLUESIMILARITY_API double __stdcall SemanticBagOfTokensSim(const char *patternTokens [], const double patternWeights [], int pLen, const char *targetTokens [], const double targetWeights [], int tLen, TokenSimilarity tokenSim, bool isSymmetric);

double __stdcall SemanticBagOfTokensSim(const char *patternTokens [], const double patternWeights [], int pLen, const char *targetTokens [], const double targetWeights [], int tLen, TokenSimilarity tokenSim, bool isSymmetric)
{
	SimMetric simMetric = GetSimMetric(tokenSim);

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

		double maxOverToken = MinimumScore;
		double weightOverToken = pow(*pWeight,2.0);
		const double *tWeight = targetWeights;
		for (const char ** target = targetTokens;  target != targetTokens + tLen; target++, tWeight++)
		{
			if (target == NULL) continue;

			double currentScore = simMetric(*pattern, *target);
						
			if (currentScore > maxOverToken)
			{
				maxOverToken = currentScore;
				weightOverToken = (*pWeight) * (*tWeight);
			}

			// if score achieves maximum score then breaks the loop and increases the performance
			if (currentScore == MaximumScore)
				break;

		}
		sumOverTokens += weightOverToken * maxOverToken;
		sumWeights += weightOverToken;
	}

	return sumOverTokens / sumWeights;
}

