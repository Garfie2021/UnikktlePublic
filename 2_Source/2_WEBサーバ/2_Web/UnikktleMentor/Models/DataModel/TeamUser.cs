using System;
using UnikktleMentor.Common;

namespace UnikktleMentor.Models
{
    public class TeamUser
    {
        public long Id { get; set; }
        public string Nickname { get; set; }
        public string EMail { get; set; }
        public JoinTeamStatus Status { get; set; }
        public int? AnswerId { get; set; }
        public DateTime? AnswerDateStart { get; set; }
    }

}
