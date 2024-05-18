using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Unikktle.Common;

namespace Unikktle.Models
{
    public class ClickPR
    {
        public long uid;
        public short bid;
        public int aid;
        public long wid;
    }

    // 
    [DataContract]
    public class AdverSelectPrSearch
    {
        [DataMember(Name = "i")]
        public int Id { get; set; }

        [DataMember(Name = "u")]
        public long UserNo { get; set; }

        [DataMember(Name = "b")]
        public short BusinessNo { get; set; }

        [DataMember(Name = "a")]
        public int AdverNo { get; set; }

        [DataMember(Name = "t1")]
        public string AdverTitle1 { get; set; }

        [DataMember(Name = "t2")]
        public string AdverTitle2 { get; set; }

        [DataMember(Name = "au")]
        public string AdverURL { get; set; }
    }

    [DataContract]
    public class AdverSelectPrRelation
    {
        [DataMember(Name = "i")]
        public int Id { get; set; }

        [DataMember(Name = "u")]
        public long UserNo { get; set; }

        [DataMember(Name = "b")]
        public short BusinessNo { get; set; }

        [DataMember(Name = "a")]
        public int AdverNo { get; set; }

        [DataMember(Name = "t1")]
        public string AdverTitle1 { get; set; }

        [DataMember(Name = "t2")]
        public string AdverTitle2 { get; set; }

        //[DataMember(Name = "aw")]
        public short AdverTitle_r_w { get; set; }

        [DataMember(Name = "au")]
        public string AdverURL { get; set; }

    }


    public class ViewModel_AdverEdit
    {
        public short BusinessId { get; set; }
        public Adver Adver { get; set; }
        public List<AdverWordSearch> SearchWordList { get; set; }
        public List<AdverWordRelation> RelationWordList { get; set; }
    }


    // 単価全般で使う
    public class UnitPrice
    {
        // No。
        public byte Id { get; set; }

        // 単価
        public short Price { get; set; }
    }

    // ※AdverSearchSelect と AdverRelationSelect を１つにするのは禁止。EF Core の結果が上書きされる。
    public class AdverSearchSelect
    {
        // No。
        public int Id { get; set; }

        public Valid Valid { get; set; }

        public AdverCategory Category { get; set; }

        // 組織名
        public string AdverName { get; set; }
        // 広告予算
        public int AdvertisingBudget { get; set; }
        // 広告有効期限
        public DateTime ExpirationDate { get; set; }
    }

    // ※AdverSearchSelect と AdverRelationSelect を１つにするのは禁止。EF Core の結果が上書きされる。
    public class AdverRelationSelect
    {
        // No。
        public int Id { get; set; }

        public Valid Valid { get; set; }

        public AdverCategory Category { get; set; }

        // 組織名
        public string AdverName { get; set; }
        // 広告予算
        public int AdvertisingBudget { get; set; }
        // 広告有効期限
        public DateTime ExpirationDate { get; set; }
        
    }

    public class Adver
    {
        // No。
        public int? Id { get; set; } = null;

        
        public Valid Valid { get; set; } = Valid.有効;

        public AdverCategory Category { get; set; } = AdverCategory.有料;

        // 組織名
        public string AdverName { get; set; }

        public string AdverTitle1 { get; set; }
        public string AdverTitle2 { get; set; }

        // 組織URL
        public string AdverURL { get; set; }
        
        // 広告予算
        public int AdvertisingBudget { get; set; } = 10000;
        
        //// ビジネスURL
        //public string AdverURL { get; set; }
    }
}
