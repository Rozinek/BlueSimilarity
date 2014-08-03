#ifndef _TOOLS_H_
#define _TOOLS_H_

#include <iostream>
#include <algorithm>
#include <set>
#include <vector>
#include <iterator>

using namespace std;

std::set<string>  GetQgrams(const char *token, int legnthQgram);
std::vector<string>  GetQgramsVector(const char *token, int legnthQgram);
std::vector<string> Intersection(std::vector<string> &v1, std::vector<string> &v2);

std::set<string>  GetQgrams(const char *token, int legnthQgram)
{
	std::set<string> token_qgrams;
	string tokenStr = string(token);

	// extract character bigrams from string1
	for (unsigned int i = 0; i < (tokenStr.length() - 1); i++)
	{
		token_qgrams.insert(tokenStr.substr(i, legnthQgram));
	}

	return token_qgrams;
}

std::vector<string>  GetQgramsVector(const char *token, int legnthQgram)
{
	std::vector<string> token_qgrams;
	string tokenStr = string(token);

	// extract character bigrams from string1
	for (unsigned int i = 0; i < (tokenStr.length() - 1); i++)
	{
		token_qgrams.push_back(tokenStr.substr(i, legnthQgram));
	}

	return token_qgrams;
}

std::vector<string> Intersection(std::vector<string> &v1, std::vector<string> &v2)
{
	std::vector<string> v3;
	sort(v1.begin(), v1.end());
	sort(v2.begin(), v2.end());

	set_intersection(v1.begin(), v1.end(), v2.begin(), v2.end(), back_inserter(v3));

	return v3;
}

#endif