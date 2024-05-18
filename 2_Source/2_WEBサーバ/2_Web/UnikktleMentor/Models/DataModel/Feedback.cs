using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnikktleMentor.Common;

namespace UnikktleMentor.Models
{
    public class Feedback
    {
        // No。
        public int Id { get; set; }

        public FeedbackCategory Category { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public DateTime SendDate { get; set; }
        
    }
}
