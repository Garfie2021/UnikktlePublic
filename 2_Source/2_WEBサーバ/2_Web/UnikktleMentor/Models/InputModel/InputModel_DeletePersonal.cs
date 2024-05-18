using System.ComponentModel.DataAnnotations;

namespace UnikktleMentor.Models
{
    public class InputModel_DeletePersonal
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required(ErrorMessage = "71")]       // ErrorMessage = "The Password is required."
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
