using System.ComponentModel.DataAnnotations;

namespace UnikktleMentor.Models
{
    public class InputModel_Login
    {
        [Required(ErrorMessage = "61")]     // ErrorMessage = "The UserID field is required."
        public string UserID { get; set; }

        [Required(ErrorMessage = "63")]       // ErrorMessage = "The Password field is required."
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        //public string Latitude { get; set; }
        //public string Longitude { get; set; }
    }

}
