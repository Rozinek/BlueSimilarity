#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include <math.h>
#include "MathDef.h"
#include "BlueSimilarity_API.h"

// Jaro distance
BLUESIMILARITY_API double __stdcall  Jaro(const char *pattern, const char *text);

// internal method
void CommonCharacters(char* returnCommons, const char * pattern, size_t lenStr1, const char * text, size_t lenStr2, int SlidingWindow);
int GetTranspositionCount(char *commonChar1, size_t c1Len, char *commonChar2, size_t c2Len);

/****************************************************************************************************/
/*Jaro distance																						*/
/****************************************************************************************************/
double __stdcall Jaro(const char *pattern, const char *text)
{
	size_t lenStr1 = strlen(pattern);
	size_t lenStr2 = strlen(text);

	size_t c1Len, c2Len;
	int halflen = (int) ceil(MIN(lenStr1, lenStr2) / 2.0);

	char *common1 = new char[(MAX(lenStr1, lenStr2) + 1) * sizeof(char)];
	char *common2 = new char[(MAX(lenStr1, lenStr2) + 1) * sizeof(char)];


	CommonCharacters(common1, pattern, lenStr1, text, lenStr2, halflen);
	CommonCharacters(common2, text, lenStr2, pattern, lenStr1, halflen);

	c1Len = strlen(common1);
	c2Len = strlen(common2);

	if (c1Len == 0 || c2Len == 0) {
		return 0.0;
	}

	// get the count of the transposition from common characters
	int transpositions = GetTranspositionCount(common1, c1Len, common2, c2Len);

	double dist = ((double) c1Len / (double) lenStr1
		+ (double) c2Len / (double) lenStr2
		+ ((double) c1Len - (double) transpositions / 2.0) / (double) c1Len) / 3.0;


	delete(common1);
	delete(common2);

	return dist;
}


void CommonCharacters(char* returnCommons, const char * pattern, size_t lenStr1, const char * text, size_t lenStr2, int SlidingWindow)
{
	int c = 0;
	for (size_t i = 0; i < lenStr1; i++)
	{
		bool foundIt = false;
		const char *ch = &pattern[i];

		for (size_t j = 0; j < lenStr2; j++)
		{
			if ((*ch == text[j]) && !foundIt && abs((signed) (i - j)) <= SlidingWindow) 
			{
				foundIt = true;
				returnCommons[c++] = *ch;
			}
		}
	}

	returnCommons[c] = '\0';
}

int GetTranspositionCount(char *commonChar1, size_t c1Len, char *commonChar2, size_t c2Len)
{
	int transpositions = 0;
	size_t minLen = MIN(c1Len, c2Len);
	for (size_t i = 0; i < minLen; i++)
	{
		if (commonChar1[i] != commonChar2[i])
		{
			transpositions++;
		}
	}

	return transpositions;
}
