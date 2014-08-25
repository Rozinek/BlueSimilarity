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
			return JaroWinklerNative(first.Value, second.Value);
		}

		public double GetSimilarity(string first, string second)
		{
			return JaroWinklerNative(first, second);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return JaroWinklerNative(first.Value, second.Value);
		}

		#endregion

		#region Methods (private)

		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.JaroWinklerEntry,
			CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		private static extern double JaroWinklerNative([In] string first, [In] string second);

		#endregion
	}
}