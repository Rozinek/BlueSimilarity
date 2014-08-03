using System.Runtime.InteropServices;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;

namespace BlueSimilarity
{
	public class JaroWinkler : ISimilarity
	{

		public double GetSimilarity(string first, string second)
		{
			return JaroWinklerNative(first, second);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return JaroWinklerNative(first.Value, second.Value);
		}

		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.JaroWinklerEntry, CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		private static extern double JaroWinklerNative([In] string first, [In] string second);
	}
}
