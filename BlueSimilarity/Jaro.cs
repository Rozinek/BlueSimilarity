using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

namespace BlueSimilarity
{
    /// <summary>
    ///     Jaro distance see description: http://en.wikipedia.org/wiki/Jaro–Winkler_distance
    /// </summary>
    public class Jaro : ISimilarity
    {
        #region ISimilarity Members

        /// <summary>
        ///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
        /// </summary>
        /// <param name="pattern">pattern string</param>
        /// <param name="text">text string</param>
        /// <returns>returns the similarity score between 0 and 1</returns>
        public double GetSimilarity(string pattern, string text)
        {
            int lenStr1 = pattern.Length;
            int lenStr2 = text.Length;

            int halflen = (int)Math.Ceiling(Math.Min(lenStr1, lenStr2) / 2.0);

            char[] common1 = new char[Math.Max(lenStr1, lenStr2) + 1];
            char[] common2 = new char[Math.Max(lenStr1, lenStr2) + 1];


            CommonCharacters(common1, pattern, lenStr1, text, lenStr2, halflen);
            CommonCharacters(common2, text, lenStr2, pattern, lenStr1, halflen);

            int c1Len = common1.Length;
            int c2Len = common2.Length;

            if (c1Len == 0 || c2Len == 0)
            {
                return 0.0;
            }

            // get the count of the transposition from common characters
            int transpositions = GetTranspositionCount(common1, c1Len, common2, c2Len);

            double dist = (c1Len / (double)lenStr1 + c2Len / (double)lenStr2 + (c1Len - transpositions / 2.0) / c1Len) / 3.0;


            return dist;
        }

        /// <summary>
        ///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
        /// </summary>
        /// <param name="first">pattern string</param>
        /// <param name="second">text string</param>
        /// <returns>returns the similarity score between 0 and 1</returns>
        public double GetSimilarity(NormalizedString first, NormalizedString second)
        {
            return GetSimilarity(first.Value, second.Value);
        }

        /// <summary>
        ///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
        /// </summary>
        /// <param name="first">pattern string</param>
        /// <param name="second">text string</param>
        /// <returns>returns the similarity score between 0 and 1</returns>
        public double GetSimilarity(Token first, Token second)
        {
            return GetSimilarity(first.Value, second.Value);
        }

        #endregion

        // internal method
        private void CommonCharacters(char[] returnCommons, string pattern, int lenStr1, string text, int lenStr2, int SlidingWindow)
        {
            int c = 0;
            for (int i = 0; i < lenStr1; i++)
            {
                bool foundIt = false;
                char ch = pattern[i];

                for (int j = 0; j < lenStr2; j++)
                {
                    if ((ch == text[j]) && !foundIt && Math.Abs((int)(i - j)) <= SlidingWindow)
                    {
                        foundIt = true;
                        returnCommons[c++] = ch;
                    }
                }
            }

            //returnCommons[c] = '\0';
        }
        private int GetTranspositionCount(char[] commonChar1, int c1Len, char[] commonChar2, int c2Len)
        {
            int transpositions = 0;
            int minLen = Math.Min(c1Len, c2Len);
            for (int i = 0; i < minLen; i++)
            {
                if (commonChar1[i] != commonChar2[i])
                {
                    transpositions++;
                }
            }

            return transpositions;
        }
    }
}
