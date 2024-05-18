using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Unikktle.Models
{
    [DataContract]
    public class SearchViewModel
    {
        [DataMember(Name = "l")]
        public IEnumerable<KeywordSearch> KeywordList { get; set; }

        [DataMember(Name = "n")]
        public bool NextAvailable { get; set; }

        [DataMember(Name = "s")]
        public long SearchWordId { get; set; }

        [DataMember(Name = "a")]
        public AdverSelectPrSearch AdverSelectPrSearch { get; set; }

        public ExternalSearch ExternalSearch { get; set; }

        public MindSearchViewModel MindSearchViewModel;
    }
}
