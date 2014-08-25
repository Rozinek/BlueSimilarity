#region

using System;
using System.Collections.Generic;

#endregion

namespace BlueSimilarity.Types
{
	internal static class TypeConversion
	{
		#region Static and contants fields

		private static readonly Dictionary<Type, int> TypeConversionDictionary = new Dictionary<Type, int>
		                                                                         {
			                                                                         {typeof (Unigram), Unigram.UnigramLength},
			                                                                         {typeof (Bigram), Bigram.BigramLength},
			                                                                         {typeof (Trigram), Trigram.TrigramLength},
		                                                                         };

		private static readonly Dictionary<Type, Func<string, IQgram>> DynamicConstructors =
			new Dictionary<Type, Func<string, IQgram>>
			{
				{typeof (Unigram), x => new Unigram(x)},
				{typeof (Bigram), x => new Bigram(x)},
				{typeof (Trigram), x => new Trigram(x)},
			};

		#endregion

		#region Methods (internal)

		internal static T CreateQgram<T>(string token) where T : IQgram
		{
			return (T) DynamicConstructors[typeof (T)].Invoke(token);
		}

		/// <summary>
		///     Get Length of the q-gram
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		internal static int GetQgramLength<T>() where T : IQgram
		{
			return TypeConversionDictionary[typeof (T)];
		}

		#endregion
	}
}