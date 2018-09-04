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
    ///     Jaro-Winkler method	see description: http://en.wikipedia.org/wiki/Jaro–Winkler_distance
    /// </summary>
    public class JaroWinkler : ISimilarity
    {
        #region ISimilarity Members

        private const double PREFIXSCALE = 0.1;
        private const int MINPREFIXLENGTH = 4;

        private static Jaro jaroSim;

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

        static JaroWinkler()
        {
            jaroSim = new Jaro();
        }

        /// <summary>
        ///     Get the normalized similarity score from 0 to 1 where 1 is total similarity
        /// </summary>
        /// <param name="pattern">pattern string</param>
        /// <param name="text">text string</param>
        /// <returns>returns the similarity score between 0 and 1</returns>
        public double GetSimilarity(string pattern, string text)
        {
            int lenPattern = pattern.Length;
            int lenText = text.Length;

            // compute Jaro Distance
            double jarodist = jaroSim.GetSimilarity(pattern, text);

            // compute prefix length
            int prefixLength = PrefixLength(pattern, lenPattern, text, lenText);

            var distJaroWinkler = jarodist + prefixLength * PREFIXSCALE * (1.0 - jarodist);

            return distJaroWinkler;
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

        private static int PrefixLength(string pattern, int lenPattern, string text, int lenText)
        {
            int n = Utils.Min3(MINPREFIXLENGTH, lenPattern, lenText);

            for (int i = 0; i < n; i++)
            {
                if (pattern[i] != text[i])
                {
                    return i;
                }
            }
            return n;
        }


        #endregion
    }
}
