//using System;
//using BlueSimilarity.Containers;
//using BlueSimilarity.Definitions;
//using BlueSimilarity.Indexing;
//using BlueSimilarity.Types;

//namespace BlueSimilarity
//{
//	public class SoftTFIDF 
//	{
//		private readonly TokenSimilarity _tokenSimilarity;
//		private readonly SemanticVocabulary _semanticVocabulary;

//		public SoftTFIDF(SemanticVocabulary learnedVocabulary, TokenSimilarity tokenSimilarity)
//		{
//			_semanticVocabulary = learnedVocabulary;
//			_tokenSimilarity = tokenSimilarity;
//		}

//		public double GetSimilarity(string[] tokensPattern, string[] tokensTarget)
//		{
//			var patternWeights = _semanticVocabulary.GetSemanticWeight(tokensPattern);
//			var targetWeights = _semanticVocabulary.GetSemanticWeight(tokensTarget);

//			return default(double);
//		}

//		public double GetSimilarity(NormalizedString[] tokensPattern, NormalizedString[] tokensTarget)
//		{
//			throw new NotImplementedException();
//		}

//		public double GetSimilarity(ITokenizer tokensPattern, ITokenizer tokensTarget)
//		{
//			throw new NotImplementedException();
//		}


//		public TokenSimilarity InternalTokenSimilarity { get; private set; }
//	}
//}
