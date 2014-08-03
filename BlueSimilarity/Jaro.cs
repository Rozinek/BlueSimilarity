#region

using System.Runtime.InteropServices;
using BlueSimilarity.Containers;
using BlueSimilarity.Definitions;

#endregion

namespace BlueSimilarity
{
	public class Jaro : ISimilarity
	{

		#region ISimilarity Members

		public double GetSimilarity(string first, string second)
		{
			return JaroNative(first, second);
		}

		public double GetSimilarity(NormalizedString first, NormalizedString second)
		{
			return JaroNative(first.Value, second.Value);
		}

		#endregion

		#region Methods (private)

		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.JaroEntry, CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		private static extern double JaroNative([In] string first, [In] string second);

		#endregion
	}
}