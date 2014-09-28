#region

using System.IO;
using System.IO.Compression;

#endregion

namespace BlueSimilarity.Indexing
{
	internal static class DeflateCompressor
	{
		#region Methods (public)

		internal static byte[] Compress(byte[] entity)
		{
			using (var memoryStreamCompressed = new MemoryStream())
			{
				using (var deflateStream = new DeflateStream(memoryStreamCompressed, CompressionMode.Compress))
				{
					deflateStream.Write(entity, 0, entity.Length);
				}
				return memoryStreamCompressed.ToArray();
			}
		}

		internal static byte[] Decompress(byte[] compressEntity)
		{
			using (var deflateStream = new DeflateStream(new MemoryStream(compressEntity), CompressionMode.Decompress))
			{
				using (var memory = new MemoryStream())
				{
					deflateStream.CopyTo(memory);

					return memory.ToArray();
				}
			}
		}

		#endregion
	}
}