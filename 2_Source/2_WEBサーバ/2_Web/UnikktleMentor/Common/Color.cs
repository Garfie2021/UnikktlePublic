using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnikktleMentor.Common
{
    public static class WebColor
    {
        // JavaScript用のRGB色リスト
        public static List<string> RGB_JS;

        static WebColor()
        {
            // d3.js、c3.js が内部でループしている色を、そのまま使う。
            RGB_JS = new List<string>()
            {
                //#CAFFBF
                "202, 255, 191",

                //#9BF6FF
                "155, 246, 255",

                //#A0C4FF
                "160, 196, 255",

                //#BDB2FF
                "189, 178, 255",

                //#FFC6FF
                "255, 198, 255",

                //#FFADAD
                "255, 173, 173",

                //#FFD6A5
                "255, 214, 165",

                //#FDFFB6
                "253, 255, 182",

                //#1f77b4
                "31, 119, 180",

                //#ff7f0e
                "255, 127, 14",

                //#2ca02c
                "44, 160, 44",

                //#d62728
                "214, 39, 40",

                //#9467bd
                "148, 103, 189",

                //#8c564b
                "140, 86, 75",

                //#e377c2
                "227, 119, 194",

                //#7f7f7f
                "127, 127, 127",

                //#bcbd22
                "188, 189, 34",

                //#17becf
                "23, 190, 207"
            };

        }





    }
}
