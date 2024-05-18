using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UnikktleMentor.Common;

namespace UnikktleMentor.Models
{


    // [UnikktleMentorWeb].[hst].[tC02系統値]
    public class C02系統値
    {
        public int Id { get; set; }             // [UnikktleMentorWeb].[hst].[tC02系統値計算].[AnswerId]
        public DateTime AnswerDateStart { get; set; }             // hst.tAnswerHistory.AnswerDateStart
        public byte D { get; set; }
        public byte C { get; set; }
        public byte I { get; set; }
        public byte N { get; set; }
        public byte O { get; set; }
        public byte Co { get; set; }
        public byte Ag { get; set; }
        public byte G { get; set; }
        public byte R { get; set; }
        public byte T { get; set; }
        public byte A { get; set; }
        public byte S { get; set; }
    }

}
