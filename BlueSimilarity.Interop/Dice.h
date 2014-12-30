#ifndef _DICE_
#define _DICE_

#include "Utils.h"
#include "MathDef.h"
#include "BlueSimilarity_API.h"

// Dice coefficient similarity
BLUESIMILARITY_API double __stdcall  Dice(const char *pattern, const char *text, int qGramLenght);
double __stdcall  DiceBigram(const char *pattern, const char *text);

double __stdcall  Dice(const char *pattern, const char *text, int qGramLength)
{
	std::vector<string> string1_qgrams = GetQgramsVector(pattern, qGramLength);
	std::vector<string> string2_qgrams = GetQgramsVector(text, qGramLength);
	std::vector<string> intersection = Intersection(string1_qgrams, string2_qgrams);

	// calculate dice coefficient
	double dice = (double) (intersection.size() * 2) / (double) (string1_qgrams.size() + string2_qgrams.size());

	return dice;
}

double __stdcall DiceBigram(const char *pattern, const char *text)
{
	return Dice(pattern, text, 2);
}

#endif