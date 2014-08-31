using System.Runtime.InteropServices;

namespace BlueSimilarity.Definitions
{
	/// <summary>
	/// Native methods from <see cref="NativeEntryPoint"/>
	/// </summary>
	internal static class NativeMethods
	{
		/// <summary>
		/// Overlap coefficient native method
		/// </summary>
		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.OverlapCoefficientEntry,
			CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		internal static extern double Overlap([In] string first, [In] string second, int qgramLength);

		/// <summary>
		/// Jaccard coefficeint native method
		/// </summary>
		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.JaccardCoefficientEntry,
			CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		internal static extern double Jaccard([In] string first, [In] string second, int qgramLength);

		/// <summary>
		/// Dice coefficient native method
		/// </summary>
		[DllImport(NativeEntryPoint.BlueSimilarityInteropName, EntryPoint = NativeEntryPoint.DiceCoefficientEntry,
			CallingConvention = NativeEntryPoint.InteropCallingConvention)]
		internal static extern double Dice([In] string first, [In] string second, int qgramLength);
	}
}