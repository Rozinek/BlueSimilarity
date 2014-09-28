#region

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace BlueSimilarity.Indexing
{

	internal static class BinarySerializer 
	{
		#region Private fields

		private readonly static BinaryFormatter BinaryFormatter;

		#endregion

		#region Constructors

		static BinarySerializer()
		{
			BinaryFormatter = new BinaryFormatter();
		}

		#endregion

		#region ISerializer Members

		internal static byte[] Serialize<T>(T entity)
		{		
			using (var memory = new MemoryStream())
			{
				BinaryFormatter.Serialize(memory, entity);

				return memory.ToArray();
			}
		}

		internal static T Deserialize<T>(byte[] stream)
		{
			using (var memory = new MemoryStream(stream))
			{
				return (T)BinaryFormatter.Deserialize(memory);
			}
		}

		#endregion
	}

	//internal class BinarySerializer
	//{
	//	#region Private fields

	//	private readonly IFormatter _formatter;

	//	#endregion

	//	#region Constructors

	//	internal BinarySerializer()
	//	{
	//		_formatter = new BinaryFormatter();
	//	}

	//	#endregion

	//	#region Methods (internal)

	//	internal T Deserialize<T>(string fileName)
	//	{
	//		using (var fileStream = new FileStream(fileName, FileMode.Open))
	//		{
	//			return (T) _formatter.Deserialize(fileStream);
	//		}
	//	}

	//	internal void Serialize<T>(T instance, string fileName)
	//	{
	//		using (var fileStream = new FileStream(fileName, FileMode.Create))
	//		{
	//			_formatter.Serialize(fileStream, instance);
	//		}
	//	}

	//	#endregion
	//}
}