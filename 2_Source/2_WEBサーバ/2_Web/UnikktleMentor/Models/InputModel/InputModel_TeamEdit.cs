using System.ComponentModel.DataAnnotations;


namespace UnikktleMentor.Models
{
    //public class InputModel_TeamJoinEdit
    //{
    //    public int? TeamNo { get; set; }
    //    public string TeamID { get; set; }
    //    public string TeamExplanation { get; set; }
    //    public bool AllowPublic { get; set; } = false;
    //    public bool AllowApplyToJoinTeam { get; set; } = false;
    //}

    public class InputModel_TeamEdit
    {
        public int? TeamNo { get; set; }

        // チームIDは必須です。
        [Required(ErrorMessage = "46")]
        // チームIDが100文字を超えています
        [StringLength(100, ErrorMessage = "47")]
        public string TeamID { get; set; }

        // チーム説明が200文字を超えています
        [StringLength(200, ErrorMessage = "48")]
        public string TeamExplanation { get; set; }

        public bool AllowPublic { get; set; } = false;

        public bool AllowApplyToJoinTeam { get; set; }
    }


}
