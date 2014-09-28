#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#endregion

namespace BlueSimilarity.Containers
{
	/// <summary>
	///     Iterate over the tokens in the file e.g. txt
	/// </summary>
	public class FileTokenizer : ITokenizer
	{
		#region Private fields

		private readonly FileInfo _fileInfo;
		private readonly StreamReader _streamReader;
		private Tokenizer _tokenizerLine;

		#endregion

		#region Constructors

		/// <summary>
		///     Initializes a new instance of the <see cref="FileTokenizer" /> class.
		/// </summary>
		/// <param name="fileInfo">The file.</param>
		public FileTokenizer(FileInfo fileInfo)
		{
			if (fileInfo == null)
				throw new ArgumentNullException("fileInfo");
			if (!fileInfo.Exists)
				throw new FileNotFoundException("fileInfo");

			_fileInfo = fileInfo;
			_streamReader = CreateBufferedStreamReader(_fileInfo);
		}

		#endregion

		#region ITokenizer Members

		public IEnumerator<string> GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		public void Dispose()
		{
			_streamReader.Close();
		}

		public bool MoveNext()
		{
			if (_tokenizerLine == null)
				TryReadLine();

			return _tokenizerLine != null && _tokenizerLine.MoveNext();
		}

		public void Reset()
		{
			Dispose();
			CreateBufferedStreamReader(_fileInfo);
		}

		object IEnumerator.Current
		{
			get { return _tokenizerLine.Current; }
		}

		public string Current
		{
			get { return _tokenizerLine.Current; }
		}

		#endregion

		#region Methods (private)

		private static StreamReader CreateBufferedStreamReader(FileInfo fileInfo)
		{
			var fileStream = File.OpenRead(fileInfo.FullName);
			var bufferedStream = new BufferedStream(fileStream);
				return new StreamReader(bufferedStream);
		}

		private bool TryReadLine()
		{
			var line = _streamReader.ReadLine();
			if (line == null)
			{
				_tokenizerLine = null;
				return false;
			}

			_tokenizerLine = new StandardTokenizer(line);

			return true;
		}

		#endregion
	}
}