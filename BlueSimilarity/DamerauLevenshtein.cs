using System;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

namespace BlueSimilarity
{
    /// <summary>
    ///     Damerau - Levenshtein algorithm providing similarity measurement <see cref="ISimilarity" />
    ///     and distance measurement <see cref="IDistance" /> see description:
    ///     http://en.wikipedia.org/wiki/Damerau–Levenshtein_distance
    /// </summary>
    public class DamerauLevenshtein : IDistance, ISimilarity
    {
        #region IDistance Members

        /// <summary>
        ///     Offers the same behavior as <see cref="Levenshtein.GetDistance(string, string)" />
        ///     and extends for transposition of two character that will have only 1 distance
        /// </summary>
        /// <example>
        ///     return 1 for transposition two character ABC => ACB
        /// </example>
        /// <param name="pattern">the pattern string</param>
        /// <param name="text">the text string</param>
        /// <returns>return the number of edit distance</returns>
        public int GetDistance(string pattern, string text)
        {
            int m = pattern.Length;
            int n = text.Length;

            if (m == 0 || n == 0)
                return 0;

            // define variables
            int cost;
            ushort i, j;

            // creating a matrix of m+1 rows and n+1 columns
            int[,] costs = new int[m +1,n +1];

            // initializing the pattern column of the matrix
            for (i = 0; i <= m; ++i)
            {
                costs[i, 0] = i;
            }

            // initializing the pattern row of the matrix
            for (j = 0; j <= n; ++j)
            {
                costs[0, j] = j;
            }

            // starting the main process for computing 
            // the distance between the two strings "pattern" and "text"
            for (i = 1; i <= m; ++i)
            {
                for (j = 1; j <= n; ++j)
                {

                    if (pattern[i - 1] == text[j - 1])
                    {
                        cost = 0;
                    }
                    else
                    {
                        cost = 1;
                    }

                    // computes the current value of the "edit distance" and place
                    // the result into the current matrix cell
                    costs[i, j] = Utils.Min3(costs[i - 1, j] + 1, costs[i, j - 1] + 1, costs[i - 1, j - 1] + cost);


                    if ((i > 1) && (j > 1) && (pattern[i - 1] == text[j - 2]) && (pattern[i - 2] == text[j - 1]))
                    {
                        costs[i, j] = Math.Min(costs[i, j], costs[i - 2, j - 2] + cost);
                    }
                }
            }

            return costs[m, n];
        }

        /// <summary>
        ///     Offers the same behavior as <see cref="Levenshtein.GetDistance(NormalizedString, NormalizedString)" />
        ///     and extends for transposition of two character that will have only 1 distance
        /// </summary>
        /// <example>
        ///     return 1 for transposition two character ABC => ACB
        /// </example>
        /// <param name="first">the pattern normalized string</param>
        /// <param name="second">the text normalized string</param>
        /// <returns>return the number of edit distance</returns>
        public int GetDistance(NormalizedString first, NormalizedString second)
        {
            return GetDistance(first.Value, second.Value);
        }


        /// <summary>
        ///     Offers the same behavior as <see cref="Levenshtein.GetDistance(Token, Token)" />
        ///     and extends for transposition of two character that will have only 1 distance
        /// </summary>
        /// <example>
        ///     return 1 for transposition two character ABC => ACB
        /// </example>
        /// <param name="first">the pattern normalized string</param>
        /// <param name="second">the text normalized string</param>
        /// <returns>return the number of edit distance</returns>
        public int GetDistance(Token first, Token second)
        {
            return GetDistance(first.Value, second.Value);
        }

        #endregion

        #region ISimilarity Members

        /// <summary>
        ///     Get the similarity score
        /// </summary>
        /// <param name="first">pattern string</param>
        /// <param name="second">text string</param>
        /// <returns>returns the similarity score between 0 and 1</returns>
        public double GetSimilarity(Token first, Token second)
        {
            return GetSimilarity(first.Value, second.Value);
        }


        /// <summary>
        ///     Get the similarity score
        /// </summary>
        /// <param name="pattern">pattern string</param>
        /// <param name="text">text string</param>
        /// <returns>returns the similarity score between 0 and 1</returns>
        public double GetSimilarity(string pattern, string text)
        {
            int distance = GetDistance(pattern, text);

            double result = 1.0 - distance / (double)Math.Max(pattern.Length, text.Length);

            return result;
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

        #endregion
    }
}
