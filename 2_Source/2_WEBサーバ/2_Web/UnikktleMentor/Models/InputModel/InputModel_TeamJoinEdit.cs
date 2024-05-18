namespace UnikktleMentor.Models
{
    public class InputModel_TeamJoinEdit
    {
        public string TeamID { get; set; }
        public bool AllowDiagnosisPublicToTheTeamReader { get; set; } = false;
        public bool AllowDiagnosisPublicToTheTeamMember { get; set; } = false;
    }


}
