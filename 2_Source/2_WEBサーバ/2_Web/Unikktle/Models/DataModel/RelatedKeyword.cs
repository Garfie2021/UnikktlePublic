using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Unikktle.Models
{
    public class RelatedKeyword
    {
        // Keyword Id
        public long Id { get; set; }

        // Keyword Text
        public string Word { get; set; }

        // 四角の描画領域（rect）
        public int R_y { get; set; }    // Rect y座標
        public int R_w { get; set; }    // Rect 幅

        // テキストの描画領域（text）
        public int T_y { get; set; }
    }

    [DataContract]
    public class RelatedKeywordJS
    {
        // Keyword Id
        [DataMember(Name = "i")]
        public long Id { get; set; }

        // Keyword Text
        [DataMember(Name = "w")]
        public string Word { get; set; }

        // 四角の描画領域（rect）
        //[DataMember(Name = "r")]
        public int R_w { get; set; }    // Rect 幅
    }
}
