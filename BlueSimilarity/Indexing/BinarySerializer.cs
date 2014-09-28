#region

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace BlueSimilarity.Indexing
{
	internal class BinarySerializer
	{
		#region Private fields

		private readonly IFormatter _formatter;

		#endregion

		#region Constructors

		internal BinarySerializer()
		{
			_formatter = new BinaryFormatter();
		}

		#endregion

		#region Methods (internal)

		internal T Deserialize<T>(string fileName)
		{
			using (var fileStream = new FileStream(fileName, FileMode.Open))
			{
				return (T) _formatter.Deserialize(fileStream);
			}
		}

		internal void Serialize<T>(T instance, string fileName)
		{
			using (var fileStream = new FileStream(fileName, FileMode.Create))
			{
				_formatter.Serialize(fileStream, instance);
			}
		}

		#endregion
	}
}