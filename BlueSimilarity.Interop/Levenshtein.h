#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include "MathDef.h"
#include "BlueSimilarity_API.h"

// Levenshtein distance
BLUESIMILARITY_API int __stdcall LevDist(const char *pattern, const char *text);
BLUESIMILARITY_API double __stdcall NormLevSim(const char *pattern, const char *text);

// INTERNAL FUNCTIONS
int LevenshteinDistanceInternal(const char *pattern, unsigned int m, const char *text, unsigned int n);


/****************************************************************************************************/
/*	Levenshtein distance																			*/
/****************************************************************************************************/
int __stdcall LevDist(const char *pattern, const char *text)
{
	unsigned int m = strlen(pattern);
	unsigned int n = strlen(text);
	return LevenshteinDistanceInternal(pattern, m, text, n);
}


/****************************************************************************************************/
/*	Levenshtein normalized similarity																			*/
/****************************************************************************************************/
double __stdcall NormLevSim(const char *pattern, const char *text)
{
	unsigned int m = strlen(pattern);
	unsigned int n = strlen(text);

	int distance = LevenshteinDistanceInternal(pattern, m, text, n);

	double result = 1.0 - (double) distance / ((double) MAX(m, n));

	return result;
}

//
// Levenshtein distance for computing edit distance of two strings (sequences)
//
int LevenshteinDistanceInternal(const char *pattern, unsigned int m, const char *text, unsigned int n)
{
	if (m == 0 || n == 0)
		return 0;

	unsigned int i, j;
	int len = (m + 1) * (n + 1);
	char *p1, *p2;
	unsigned int *d, *dp, dist;

	// allocate 1D array
	d = (unsigned int*) malloc(len * sizeof(unsigned int));

	*d = 0;

	// initializing the first row of the matrix (1D array offset)
	for (i = 1, dp = d + n + 1; i < m + 1; ++i, dp += n + 1)
	{
		*dp = i;
	}


	// initializing the first column of the matrix (1D array offset)
	for (j = 1, dp = d + 1; j < n + 1; ++j, ++dp)
	{
		*dp = j;
	}

	for (i = 1, p1 = (char*) pattern, dp = d + n + 2; i < m + 1; ++i, p1 += 1, ++dp)
	{
		for (j = 1, p2 = (char*) text; j < n + 1; ++j, p2 += 1, ++dp)
		{
			if (*p1 == *p2)
			{
				*dp = *(dp - n - 2);
			}
			else
			{
				*dp = MIN3(*(dp - 1) + 1, *(dp - n - 1) + 1, *(dp - n - 2) + 1);
			}
		}
	}

	dist = *(dp - 2);

	dp = NULL;
	free(d);
	return  dist;
}



