#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BlueSimilarity.Test.Helpers
{
	/// <summary>
	///     Test equality of the object
	/// </summary>
	public static class HelpersTest
	{
		#region Methods (public)


		public static void AssertCompareTo<T>(T instance, T higherInstance, T equalInstance)
			where T : IComparable<T>
		{

			Assert.AreEqual(1, instance.CompareTo(higherInstance));
			Assert.AreEqual(0, instance.CompareTo(equalInstance));
			Assert.AreEqual(-1, higherInstance.CompareTo(instance)); 

		} 

		public static void AssertEquality<T>(T instance, T equalInstance, T notEqualInstance) where T : IEquatable<T>
		{
			Assert.IsTrue(instance.Equals(equalInstance));
			Assert.IsFalse(instance.Equals(notEqualInstance));

			Assert.IsTrue(instance.Equals((object) equalInstance));
			Assert.IsFalse(instance.Equals((object) notEqualInstance));


			Assert.IsFalse(instance.Equals(null));
			Assert.IsTrue(instance.Equals(instance));
			Assert.IsTrue(instance.Equals((object) instance));

			// check for another type
			Assert.IsFalse(instance.Equals(true));

			// check for hash code
			Assert.AreEqual(instance.GetHashCode(), equalInstance.GetHashCode());
		}

		#endregion
	}
}