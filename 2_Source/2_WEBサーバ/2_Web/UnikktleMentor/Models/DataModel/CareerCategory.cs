using System.Collections.Generic;

namespace UnikktleMentor.Models
{
    public class CareerCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Career> CareerList { get; set; }
    }
}
