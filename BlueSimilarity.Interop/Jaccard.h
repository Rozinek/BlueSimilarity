#ifndef _JACCARD_
#define _JACCARD_

#include "Utils.h"
#include "MathDef.h"
#include "BlueSimilarity_API.h"

// Jaccard coefficient similarity
BLUESIMILARITY_API double __stdcall  Jaccard(const char *pattern, const char *text, int qGramLenght);
double __stdcall  JaccardBigram(const char *pattern, const char *text);

double __stdcall  Jaccard(const char *pattern, const char *text, int qGramLength)
{
 	std::vector<string> string1_qgrams = GetQgramsVector(pattern, qGramLength);
	std::vector<string> string2_qgrams = GetQgramsVector(text, qGramLength);
	std::vector<string> intersection = Intersection(string1_qgrams, string2_qgrams);

	// calculate jaccard coefficient
	double jaccard = (double) (intersection.size()) / (double) (string1_qgrams.size() + string2_qgrams.size() - intersection.size());

	return jaccard;
}

double __stdcall JaccardBigram(const char *pattern, const char *text)
{
	return Jaccard(pattern, text, 2);
}
#endif