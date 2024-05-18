using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unikktle.Common;

namespace Unikktle.Models
{
    // ※AdverWordSearch と AdverWordRelation を１つにするのは禁止。EF Core の結果が上書きされる。
    public class AdverWordSearch
    {
        // RowNum
        public int? Id { get; set; } = null;

        // usr.tAdverSearchWord.[SearchWordNo]。
        public long? WordId { get; set; } = null;

        public string Word { get; set; }

        public short ClickCost { get; set; } = 10;

        // 0:削除しない　1:削除する
        public DelFlg DelFlg { get; set; }
    }

    // ※AdverWordSearch と AdverWordRelation を１つにするのは禁止。EF Core の結果が上書きされる。
    public class AdverWordRelation
    {
        // RowNum（行ID）
        public int? Id { get; set; } = null;

        // usr.tAdverSearchWord.[SearchWordNo]。
        public long? WordId { get; set; } = null;

        public string Word { get; set; }

        public short ClickCost { get; set; } = 10;

        // 0:削除しない　1:削除する
        public DelFlg DelFlg { get; set; }
    }

}
