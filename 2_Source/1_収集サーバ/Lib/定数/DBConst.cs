using System;
using System.Collections.Generic;
using System.Text;

namespace 定数
{
    public static class DBConst
    {
        public const int CommandTimeout_6h = 60 * 60 * 6;    // 21600秒=6時間
        public const int CommandTimeout_1h = 60 * 60 * 1;    // =1時間
    }

    public static class MorphologicalAnalysis_DBConst
    {
        public static DF_F[] strctDF_F;
        public static TF_F[] strctTF_F;

        // Tbl203_KeywordFrequencyDF INSERT/UPDATE用
        public struct DF_F
        {
            int intDF_ID;
            int intKeywordID;
            int intFrequency;
        }

        // Tbl204_KeywordFrequencyTF INSERT/UPDATE用
        public struct TF_F
        {
            int intTF_ID;
            int intDF_ID;
            int intKeywordID;
            int intFrequency;
        }
    }
}
