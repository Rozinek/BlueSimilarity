#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include <math.h>

// custom linked headers
#include "MathDef.h"
#include "BlueSimilarity_API.h"

// Jaro-Winkler distance
BLUESIMILARITY_API double __stdcall  JaroWinklerNative(const char *pattern, const char *text);

// declare constants
const double PREFIXSCALE = 0.1;
const size_t MINPREFIXLENGTH = 4;

// internal function
size_t PrefixLength(const char * pattern, size_t lenPattern, const char * text, size_t lenText);

///****************************************************************************************************/
///*Jaro-Winkler distance																			    */
///****************************************************************************************************/
double __stdcall JaroWinklerNative(const char *pattern, const char *text)
{
	size_t lenPattern = strlen(pattern);
	size_t lenText = strlen(text);

	double distJaroWinkler;

	// compute Jaro Distance
	double jarodist = JaroNative(pattern, text);
						
	// compute prefix length
	size_t prefixLength = PrefixLength(pattern, lenPattern, text, lenText);

	distJaroWinkler = jarodist + (double) prefixLength * PREFIXSCALE * (1.0 - jarodist);

	return distJaroWinkler;
}

size_t PrefixLength(const char * pattern, size_t lenPattern, const char * text, size_t lenText)
{
	size_t n = MIN3(MINPREFIXLENGTH, lenPattern, lenText);
	for (size_t i = 0; i < n; i++)
	{
		if (pattern[i] != text[i]) 
		{
			return i;
		}
	}
	return n;
}

