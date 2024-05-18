using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Unikktle.Models
{
    [DataContract]
    public class SvgWordMapJS
    {
        //[DataMember(Name = "w")]
        public long WordId { get; set; }

        // 広告
        //[DataMember(Name = "a")]
        public AdverSelectPrRelation AdverSelectPrRelation { get; set; }

        // 関連ワードリスト
        //[DataMember(Name = "l")]
        public List<RelatedKeywordJS> WordList { get; set; }

        //[DataMember(Name = "n")]
        public bool NextAvailable { get; set; }
    }
}
