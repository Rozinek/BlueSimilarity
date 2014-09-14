#region

using System.Runtime.InteropServices;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;
using BlueSimilarity.Types;

#endregion

namespace BlueSimilarity
{
	/// <summary>
	/// Jaro-Winkler method <see cref="http://en.wikipedia.org/wiki/Jaro–Winkler_distance"/>
	/// </summary>
	public class JaroWinkler : ISimilarity
	{
		#region ISimilarity Members

		public double GetSimilarity(Token first, Token second)
		{
			return NativeEntryPoint.JaroWinklerNative(first.Value, second.Value);
		}

		public double GetSimilarity(string first, string second)
		{
			return NativeEntryPoint.JaroWinklerNative(first, second);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return NativeEntryPoint.JaroWinklerNative(first.Value, second.Value);
		}

		#endregion

		#region Methods (private)

		

		#endregion
	}
}