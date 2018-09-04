using BlueSimilarity.Definitions;

namespace BlueSimilarity
{

    public delegate double SimMetric(string pattern, string target);


    public static class SimHelpers
    {

        public const double MaximumScore = 1.0;
        public const double MinimumScore = 0.0;



        public static SimMetric GetSimMetric(TokenSimilarity tokenSim)
        {
            switch (tokenSim)
            {
                case TokenSimilarity.Levenshtein:
                    //sim = NormLevSim;
                    break;
                case TokenSimilarity.DamerauLevenshtein:
                    //sim = NormDamLevSim;
                    break;
                case TokenSimilarity.Jaro:
                    //sim = JaroNative;
                    break;
                case TokenSimilarity.JaroWinkler:
                    //sim = JaroWinklerNative;
                    break;
                case TokenSimilarity.DiceCoefficient:
                    //sim = DiceBigram;
                    break;
                case TokenSimilarity.JaccardCoefficient:
                    //sim = JaccardBigram;
                    break;
                case TokenSimilarity.OverlapCoefficient:
                    //sim = OverlapBigram;
                    break;
                case TokenSimilarity.Exact:
                default:
                    //sim = ExactMatch;
                    break;
            }

            return null; //sim;
        }


    }
}
