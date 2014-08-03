using BlueSimilarity.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlueSimilarity.Test.Preprocessing
{
	[TestClass]
	public class QGramSetTest
	{
		[TestMethod]
		public void CtorQGramSet()
		{
		    
			var qGramSet = new QGramSet("abcd");

			

			//var qgramsList = qGramSet.ToList();


			//Assert.AreEqual(4, qGramSet.Count);
		}

	}
}
