#include <stdio.h>
#include <malloc.h>
#include <string.h>

#include "MathDef.h"
#include "SimHelpers.h"
#include "BlueSimilarity_API.h"


// BagOfTokenSimilarity distance
BLUESIMILARITY_API double __stdcall BagOfTokensSim(const char *patternTokens [], int m, const char *targetTokens [], int n, TokenSimilarity tokenSim, bool isSymmetric);

// BagOfTokenSimilarity distance
BLUESIMILARITY_API double __stdcall BagOfTokensSimStruct(NormalizedString patternTokens [], int m, NormalizedString targetTokens [], int n, TokenSimilarity tokenSim, bool isSymmetric);


double __stdcall BagOfTokensSim(const char *patternTokens [], int m, const char *targetTokens [], int n, TokenSimilarity tokenSim, bool isSymmetric)
{	
	// TODO: check arguments and create exception

	SimMetric refSimilarity = GetSimMetric(tokenSim);
	
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
	// TODO: check arguments and create exception

	SimMetric refSimilarity = GetSimMetric(tokenSim);

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



