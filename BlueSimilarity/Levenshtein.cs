#region

using System;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	///     Levenshtein algorithm providing similarity
	///     measurement <see cref="ISimilarity" /> and distance measurement <see cref="IDistance" />
	///     see description: http://en.wikipedia.org/wiki/Levenshtein_distance
	/// </summary>
	public class Levenshtein : IDistance, ISimilarity
	{
		#region IDistance Members

		/// <summary>
		///     <see cref="GetDistance(string, string)" />
		/// </summary>
		/// <param name="first">the pattern token</param>
		/// <param name="second">the text token</param>
		/// <returns>returns the number of edit distance</returns>
		public int GetDistance(Token first, Token second)
		{
			return GetDistance(first.Value, second.Value);
		}

		/// <summary>
		///     <see cref="GetDistance(string, string)" />
		/// </summary>
		/// <param name="first">the pattern normalized string</param>
		/// <param name="second">the text normalized string</param>
		/// <returns>returns the number of edit distance</returns>
		public int GetDistance(NormalizedString first, NormalizedString second)
		{
			return GetDistance(first.Value, second.Value);
		}

		/// <summary>
		///     Levenshtein distance returns the number of edit operations
		///     (addition, deletation, substitution) which are needed for transformation
		///     from one string to another
		///     <example>
		///         return 1 for deletation character: ABC => AC
		///         return 1 for substitution character ABC => AXC
		///         return 1 for addition character ABC => ABCD
		///     </example>
		/// </summary>
		/// <param name="first">the pattern string</param>
		/// <param name="second">the text string</param>
		/// <returns>returns the number of edit distance</returns>
		public int GetDistance(string first, string second)
		{

		    int m = first.Length;
		    int n = second.Length;

		    if (m == 0 || n == 0)
		        return 0;

		    int len = (m + 1) * (n + 1);
		               
		    // allocate 1D array
		    int[] d = new int[len];

		    // initializing the first row of the matrix (1D array offset)
		    for (int i = 1, dp = n + 1; i < m + 1; ++i, dp += n + 1)
		    {
		        d[dp] = i;
		    }

		    // initializing the first column of the matrix (1D array offset)
		    for (int j = 1, dp = 1; j < n + 1; ++j, ++dp)
		    {
		        d[dp] = j;
		    }

            for (int i = 1, f = 0, dp = n + 2; i < m + 1; ++i, f += 1, ++dp)
            {
                char p1 = first[f];

		        for (int j = 1, s = 0; j < n + 1; ++j, s += 1, ++dp)
		        {
		            char p2 = second[s];

		            if (p1 == p2)
		            {
		                d[dp] = d[dp - n - 2];
		            }
		            else
		            {
		                d[dp] = Utils.Min3(d[dp - 1] + 1, d[dp - n - 1] + 1, d[dp - n - 2] + 1);
		            }
		        }
		    }

		    int dist = d[len - 1];

		    return dist;
        }

		#endregion

		#region ISimilarity Members

		/// <summary>
		///     Normalized similarity from 0 to 1 where the 1 is total similarity
		/// </summary>
		/// <param name="first">the pattern token</param>
		/// <param name="second">the text token</param>
		/// <returns>returns the number of edit distance</returns>
		public double GetSimilarity(Token first, Token second)
		{
		    return GetSimilarity(first.Value, second.Value);
		}

		/// <summary>
		///     Normalized similarity from 0 to 1 where the 1 is total similarity
		/// </summary>
		/// <param name="pattern">the pattern token</param>
		/// <param name="text">the text token</param>
		/// <returns>returns the number of edit distance</returns>
		public double GetSimilarity(string pattern, string text)
		{
		    int distance = GetDistance(pattern, text);

		    double result = 1.0 - distance / (double)Math.Max(pattern.Length, text.Length);

		    return result;
        }

		/// <summary>
		///     Normalized similarity from 0 to 1 where the 1 is total similarity
		/// </summary>
		/// <param name="first">the pattern normalized string</param>
		/// <param name="second">the text normalized string</param>
		/// <returns>returns the number of edit distance</returns>
		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
		    return GetSimilarity(first.Value, second.Value);
        }

		#endregion
	}
}