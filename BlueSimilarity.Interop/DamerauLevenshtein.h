#include <stdio.h>
#include <malloc.h>
#include <string.h>

// BOOST 
#include <boost/numeric/ublas/matrix.hpp>
#include <boost/numeric/ublas/io.hpp>

// CUSTOM
#include "MathDef.h"
#include "BlueSimilarity_API.h"

using namespace boost::numeric::ublas;

// Damerau-Levenshtein distance
BLUESIMILARITY_API int __stdcall DamLevDist(const char *pattern, const char *text);
BLUESIMILARITY_API double __stdcall NormDamLevSim(const char *pattern, const char *text);

// INTERNAL FUNCTIONS
int DamerauLevenshteinDistanceInternal(const char *pattern, size_t m, const char *text, size_t n);

/****************************************************************************************************/
/*	Damerau-Levenshtein distance																			*/
/****************************************************************************************************/
int __stdcall  DamLevDist(const char *pattern, const char *text)
{
	size_t m = strlen(pattern);
	size_t n = strlen(text);
	return DamerauLevenshteinDistanceInternal(pattern, m, text, n);
}

/****************************************************************************************************/
/*	Damerau-Levenshtein distance																			*/
/****************************************************************************************************/
double __stdcall NormDamLevSim(const char *pattern, const char *text)
{
	size_t m = strlen(pattern);
	size_t n = strlen(text);

	int distance = DamerauLevenshteinDistanceInternal(pattern, m, text, n);

	double result = 1.0 - (double) distance / ((double) MAX(m, n));

	return result;
}

/****************************************************************************************************/
/*Damerau-Levenshtein distance																		*/
/****************************************************************************************************/
int  DamerauLevenshteinDistanceInternal(const char *pattern, size_t m, const char *text, size_t n)
{
	if (m == 0 || n == 0)
		return 0;

	// define variables
	int cost;
	unsigned int i, j;

	// creating a matrix of m+1 rows and n+1 columns
	matrix<int> costs(m + 1, n + 1);

	// initializing the first column of the matrix
	for (i = 0; i <= m; ++i) 
	{
		costs(i, 0) = i;
	}

	// initializing the first row of the matrix
	for (j = 0; j <= n; ++j)
	{
		costs(0, j) = j;
	}


	// starting the main process for computing 
	// the distance between the two strings "pattern" and "text"
	for (i = 1; i <= m; ++i) {
		for (j = 1; j <= n; ++j) {

			if (pattern[i - 1] == text[j - 1]) {
				cost = 0;
			}
			else {
				cost = 1;
			}

			// computes the current value of the "edit distance" and place
			// the result into the current matrix cell
			costs(i, j) = MIN3(costs(i - 1, j) + 1, costs(i, j - 1) + 1, costs(i - 1, j - 1) + cost);


			if ((i > 1) && (j > 1) && (pattern[i - 1] == text[j - 2]) && (pattern[i - 2] == text[j - 1]))
			{
				costs(i, j) = MIN(costs(i, j), costs(i - 2, j - 2) + cost);
			}
		}
	}
	
	return costs(m, n);
}