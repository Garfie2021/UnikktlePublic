using System.Collections.Generic;
using System.Runtime.Serialization;
using Unikktle.Models;

namespace Unikktle.Models
{
    [DataContract]
    public class MindSearchViewModel
    {
        [DataMember(Name = "l")]
        public IEnumerable<MindSearch> MindList { get; set; }

        [DataMember(Name = "n")]
        public bool NextAvailable { get; set; }

        [DataMember(Name = "s")]
        public long SearchWordId { get; set; }

        public ExternalSearch ExternalSearch { get; set; }
    }
}
