#include <stdio.h>
#include <malloc.h>
#include <string.h>

#include "Utils.h"
#include "MathDef.h"
#include "BlueSimilarity_API.h"

// TF-IDF
BLUESIMILARITY_API double __stdcall TFIDFNative(const char* patternToken [], double patternWeights [], int pLen, const char* targetToken[], double targetWeights [], int tLen, TokenSimilarity tokenSim);

// internal method
void UnitVectorizing(double weights [], int wLen);

/****************************************************************************************************/
/*	TF-IDF similarity																*/
/****************************************************************************************************/
BLUESIMILARITY_API double __stdcall TFIDFNative(const char* patternToken [], double patternWeights [], int pLen, const char* targetToken[], double targetWeights [], int tLen, TokenSimilarity tokenSim)
{
	UnitVectorizing(patternWeights, pLen);
	UnitVectorizing(targetWeights, tLen);

	set<const char*> setTokens;
	for (const char **tT = targetToken; tT < targetToken + tLen; tT++)
	{		
		setTokens.insert(*tT);
	}
	
	double score = 0;
	double *w = patternWeights;
	for (const char **pToken = patternToken; pToken < patternToken + pLen; pToken++)
	{
		if (setTokens.find(*pToken) != setTokens.end())
			score += *w;
		w++;
	}
	
	return score;
}


