#include "Utils.h"
#include "MathDef.h"
#include "BlueSimilarity_API.h"

// Jaccard coefficient similarity
BLUESIMILARITY_API double __stdcall  Overlap(const char *pattern, const char *text, int qGramLenght);

double __stdcall  Overlap(const char *pattern, const char *text, int qGramLength)
{
	std::vector<string> string1_qgrams = GetQgramsVector(pattern, qGramLength);
	std::vector<string> string2_qgrams = GetQgramsVector(text, qGramLength);
	std::vector<string> intersection = Intersection(string1_qgrams, string2_qgrams);

	// calculate jaccard coefficient
	double overlap = (double) (intersection.size()) / (double) (MIN(string1_qgrams.size(), string2_qgrams.size()));

	return overlap;
}