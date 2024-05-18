using System.ComponentModel.DataAnnotations;
using UnikktleMentor.Common;

namespace UnikktleMentor.Models
{
    public class InputModel_Feedback
    {
        [Required(ErrorMessage = "67")]       // ErrorMessage = "The Feedback Category field is required."
        public FeedbackCategory FeedbackCategory { get; set; }

        [Required(ErrorMessage = "68")]       // ErrorMessage = "The Subject field is required."
        public string Subject { get; set; }

        [Required(ErrorMessage = "69")]       // ErrorMessage = "The Text is required."
        public string Text { get; set; }
    }

}
