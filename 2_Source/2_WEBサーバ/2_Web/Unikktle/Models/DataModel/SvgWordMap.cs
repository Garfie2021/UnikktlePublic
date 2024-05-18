using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unikktle.Models
{
    public class SvgWordMap
    {
        public Svg Svg { get; set; }

        // 元Keywordと先Keywordを連結
        public Line Line { get; set; }

        // 大枠
        public Rect RectBorder { get; set; }

        // 広告
        public AdverSelectPrRelation AdverSelectPrRelation { get; set; }

        // 先Keywordリスト(RelatedKeywordList)
        public int R_x { get; set; }// 四角の描画領域（rect）
        public int T_x { get; set; }// テキストの描画領域（text）

        // 関連ワードリスト
        public List<RelatedKeyword> WordList { get; set; }

        public long SearchWordId { get; set; }

        // Nextボタンのy座標
        public bool NextAvailable { get; set; }
        public int Next_r_y { get; set; }
        public int Next_t_y { get; set; }

        // 広告テキストのy座標。1行目。
        public int A_t_y_1 { get; set; }
        // 広告テキストのy座標。2行目。
        public int A_t_y_2 { get; set; }
    }
}
