using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
//using UnikktleMentor.Common;

namespace UnikktleMentor.Models
{



    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel_ChangePassword
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required(ErrorMessage = "72")]       // ErrorMessage = "The Old Password is required."
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required(ErrorMessage = "73")]       // ErrorMessage = "The New Password is required."
        [StringLength(100, ErrorMessage = "51", MinimumLength = 6)]       // ErrorMessage = "The {0} must be at least {2} and at max {1} characters long."
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "52")]       // ErrorMessage = "The new password and confirmation password do not match."
        public string ConfirmPassword { get; set; }
    }

}
