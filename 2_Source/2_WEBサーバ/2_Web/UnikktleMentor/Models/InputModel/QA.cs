using System.Collections.Generic;

namespace UnikktleMentor.Models
{

    public class QAModels
    {
        //[DisplayName("設問No")]
        public short QNo { get; set; }

        //[DisplayName("設問文")]
        public string QDesc { get; set; }

        //[DisplayName("回答")]
        public short Ans { get; set; }
    }

}
