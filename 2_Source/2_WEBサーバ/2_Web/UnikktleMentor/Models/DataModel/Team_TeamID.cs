using UnikktleMentor.Common;


namespace UnikktleMentor.Models
{
    public class Team_TeamID
    {
        // mst.tTeam.[No]
        public int Id { get; set; }

        // mst.tTeam.[TeamID]
        public string TeamID { get; set; }

        // usr.tJoinTeam.[Status]
        public JoinTeamStatus Status { get; set; }
    }
}
