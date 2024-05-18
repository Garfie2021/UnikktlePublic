using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unikktle.Models
{
    public class CareerCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Career> CareerList { get; set; }
    }

    public class Career
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
