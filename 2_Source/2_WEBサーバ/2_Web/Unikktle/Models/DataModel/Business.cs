using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unikktle.Common;

namespace Unikktle.Models
{
    public class BusinessSelect
    {
        // No。
        public short Id { get; set; }

        public BusinessCategory Category { get; set; }

        // 組織名
        public string OrganizationName { get; set; }
    }

    public class Business
    {
        // No。
        public short Id { get; set; }

        public BusinessCategory Category { get; set; }

        // 組織名
        public string OrganizationName { get; set; }
        // 組織URL
        public string OrganizationURL { get; set; }
        //// ビジネスURL
        //public string BusinessURL { get; set; }
    }
}
