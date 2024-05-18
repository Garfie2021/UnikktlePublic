using System.ComponentModel.DataAnnotations;
using UnikktleMentorEngine;

namespace UnikktleMentor.Models
{
    public class InputModel_Answer_Supplement
    {
        // [性別]は必須です。
        [Required(ErrorMessage = "58")]
        public Gender _Gender { get; set; }

        // [キャリア]は必須です。
        [Required(ErrorMessage = "60")]
        public int Career { get; set; }

        public string RecentHappenings { get; set; }
    }

}
