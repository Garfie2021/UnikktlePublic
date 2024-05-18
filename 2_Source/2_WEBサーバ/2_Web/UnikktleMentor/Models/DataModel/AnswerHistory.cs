using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UnikktleMentor.Common;
using UnikktleMentorEngine;


namespace UnikktleMentor.Models
{

    public class AnswerHistory
    {
        public int Id { get; set; }             // [hst].[tAnswerHistory].[AnswerId]

        public DateTime AnswerDate { get; set; }             // [hst].[tAnswerHistory].[AnswerDate]

    }

}
