using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unikktle.Common;

namespace Unikktle.Models
{
    public class Feedback
    {
        // No。
        public int Id { get; set; }

        public FeedbackCategory Category { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }
    }
}
