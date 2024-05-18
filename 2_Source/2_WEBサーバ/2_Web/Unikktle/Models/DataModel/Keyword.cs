using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Unikktle.Models
{
    // [clt].[spKeyword_Select_Contains]ストアド用
    [DataContract]
    public class KeywordSearch
    {
        // プロパティ名は「No」禁止。「Id」じゃないと実行時エラーになる。
        [DataMember(Name = "i")]
        public long Id { get; set; }

        [DataMember(Name = "w")]
        public string Word { get; set; }
    }

    // [clt].[spKeyword_Select]ストアド用
    // [clt].[spCollaborateKeyword_Select]ストアド用
    public class Keyword
    {
        // プロパティ名は「No」禁止。「Id」じゃないと実行時エラーになる。
        public long Id { get; set; }

        // Rectの幅
        public short r_w { get; set; }

        public string Word { get; set; }
    }
}
