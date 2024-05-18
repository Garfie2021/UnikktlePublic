namespace UnikktleMentor.Models
{
    public class TeamJoinEdit
    {
        public string TeamID { get; set; }

        public bool AllowDiagnosisPublicToTheTeamReader { get; set; }

        public bool AllowDiagnosisPublicToTheTeamMember { get; set; }
    }
}