#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include <math.h>

// custom linked headers
#include "MathDef.h"
#include "BlueSimilarity_API.h"

// Jaro-Winkler distance
BLUESIMILARITY_API double __stdcall  JaroWinkler(const char *pattern, const char *text);

// declare constants
const double PREFIXSCALE = 0.1;
const int MINPREFIXLENGTH = 4;

// internal function
int PrefixLength(const char * pattern, unsigned long lenPattern, const char * text, unsigned long lenText);

///****************************************************************************************************/
///*Jaro-Winkler distance																			    */
///****************************************************************************************************/
double __stdcall JaroWinkler(const char *pattern, const char *text)
{
	unsigned short lenPattern = strlen(pattern);
	unsigned short lenText = strlen(text);

	double distJaroWinkler;

	// compute Jaro Distance
	double jarodist = Jaro(pattern, text);

	// compute prefix length
	int prefixLength = PrefixLength(pattern, lenPattern, text, lenText);

	distJaroWinkler = jarodist + (double) prefixLength * PREFIXSCALE * (1.0 - jarodist);

	return distJaroWinkler;
}

int PrefixLength(const char * pattern, unsigned long lenPattern, const char * text, unsigned long lenText) 
{
	int n = MIN(MINPREFIXLENGTH, MIN(lenPattern, lenText));
	for (int i = 0; i < n; i++) 
	{
		if (pattern[i] != text[i]) 
		{
			return i;
		}
	}
	return n;
}

