using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace UnikktleMentor.Models
{

    public class Team
    {
        // mst.tTeam.[No]
        public int Id { get; set; }

        // mst.tTeam.[TeamID]
        public string TeamID { get; set; }

        // mst.tTeam.TeamName
        public string TeamExplanation { get; set; }

        // mst.tTeam.AllowPublic
        // ※checkboxの選択状態として使うので、enumにはしない。
        public bool AllowPublic { get; set; } = false;

        // mst.tTeam.AllowApplyToJoinTeam
        // ※checkboxの選択状態として使うので、enumにはしない。
        public bool AllowApplyToJoinTeam { get; set; }

        // mst.tTeam.CreateUserNo
        public long CreateUserNo { get; set; }
    }


}
