using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unikktle.Common;

namespace Unikktle.Models
{
    public class WordMapViewModel
    {
        public ExternalSearch ExternalSearch { get; set; }

        // 元Keyword Id
        public long Id { get; set; }

        // 元Keyword Text
        public string Word { get; set; }

        // 元Keyword(RelatedKeyword)
        public int R_x { get; set; }// 四角の描画領域（rect）
        public int T_x { get; set; }// テキストの描画領域（text）
        public RelatedKeyword BaseWord { get; set; }

        public SvgWordMap SvgWordMap { get; set; }
    }
}
