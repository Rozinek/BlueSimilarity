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

BLUESIMILARITY_API double __stdcall NormLCSubsequenceSim(const char *pattern, const char *text);
BLUESIMILARITY_API size_t __stdcall LCSubsequence(const char *pattern, const char *text);

// longest common subsequence
BLUESIMILARITY_API char* __stdcall GetLCSubsequence(const char *pattern, const char *text);

// INTERNAL FUNCTIONS
int LCSubsequenceInternal(const char *pattern, size_t m, const char *text, size_t n);

/****************************************************************************************************/
/*Longest Common Subsequence Normalized Similarity													*/
/****************************************************************************************************/
double __stdcall NormLCSubsequenceSim(const char *pattern, const char *text)
{
	size_t m = strlen(pattern);
	size_t n = strlen(text);

	if (m == 0 || n == 0)
		return 0;

	return 1.0 - static_cast<double>(LCSubsequenceInternal(pattern, m, text, n) / MAX(m, n));

}
/****************************************************************************************************/
/*Longest Common Subsequence																		*/
/****************************************************************************************************/
size_t __stdcall LCSubsequence(const char *pattern, const char *text)
{
	size_t m = strlen(pattern);
	size_t n = strlen(text);

	if (m == 0 || n == 0)
		return 0;

	return LCSubsequenceInternal(pattern, m, text, n);
}


// INTERNAL FUNCTIONS
int LCSubsequenceInternal(const char *pattern, size_t m, const char *text, size_t n)
{
	matrix<int> costs(m + 1, n + 1);

	for (unsigned int i = 0; i <= m; m++)
	{
		for (unsigned int j = 0; j <= n; j++)
		{
			if (i == 0 || j == 0)
				costs(i, j) = 0;
			else if (pattern[i - 1] == text[j - 1])
				costs(i, j) = costs(i - 1, j - 1) + 1;
			else
				costs(i, j) = MAX(costs(i - 1, j), costs(i, j - 1));
		}
	}

	return costs(m, n);
}
