#region

using BlueSimilarity.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Types
{
	[TestClass]
	public class QGramTest
	{
		#region Methods (public)

		[TestMethod]
		public void QGramTesting()
		{
			const string qgramInstance = "val";
			var qgram = new QGram(qgramInstance);

			Assert.AreEqual(qgram.Value, qgramInstance);
		}

		#endregion
	}
}