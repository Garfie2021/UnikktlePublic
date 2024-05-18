using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnikktleMentor.Common;

namespace UnikktleMentor.Models
{
    public class TeamEdit_SessionModel
    {
        public 編集モード EditMode { get; set; }
        public int TeamNo { get; set; }

        public string TeamID { get; set; }
        public string TeamExplanation { get; set; }
        public bool AllowPublic { get; set; } = false;
        public bool AllowApplyToJoinTeam { get; set; }
    }
}
