using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSimilarity
{
    internal static class Utils
    {
        private const double Epsilon = 1E-15;

        internal static int Max3(int x, int y, int z)
        {
            // Or inline it as x < y ? (y < z ? z : y) : (x < z ? z : x);
            // Time it before micro-optimizing though!
            return Math.Max(x, Math.Max(y, z));
        }

        internal static int Min3(int x, int y, int z)
        {
            return Math.Min(x, Math.Min(y, z));
        }

        internal static HashSet<string> GetQgrams(string token, int legnthQgram)
        {
            HashSet<string> token_qgrams = new HashSet<string>();
            string tokenStr = token;

            // extract character bigrams from string1
            for (int i = 0; i < (tokenStr.Length - 1); i++)
            {
                token_qgrams.Add(tokenStr.Substring(i, legnthQgram));
            }

            return token_qgrams;
        }

        internal static double ExactMatch(string pattern, string target)
        {
            return pattern == target ? 1.0 : 0.0;
        }

        internal static List<string> GetQgramsVector(string token, int legnthQgram)
        {
            List<string> tokenQgrams = new List<string>();
            string tokenStr = token;

            // extract character bigrams from string1
            for (int i = 0; i < tokenStr.Length - (legnthQgram - 1); i++)
            {
                tokenQgrams.Add(tokenStr.Substring(i, legnthQgram));
            }

            return tokenQgrams;
        }

        /// <summary>
        /// Units the vectorizing. http://en.wikipedia.org/wiki/Unit_vector
        /// </summary>
        /// <param name="weights">The weights.</param>
        internal static void UnitVectorizing(double[] weights)
        {
            double normalizer = weights.Sum(w => w * w);

            normalizer = Math.Sqrt(normalizer);

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = weights[i] / normalizer;
            }
        }

        internal static bool Equals(double x, double y)
        {
            return Math.Abs(x - y) <= Epsilon;
        }

        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
