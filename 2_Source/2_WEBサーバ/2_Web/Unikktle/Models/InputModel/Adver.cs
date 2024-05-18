using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Unikktle.Common;

namespace Unikktle.Models
{
    public class InputModel_Register
    {
        //[Required]
        //public string Name { get; set; }

        [Required(ErrorMessage = "54")]     // ErrorMessage = "The Email field is required."
        [EmailAddress(ErrorMessage = "55")] // ErrorMessage = "The Email field is not a valid e-mail address."
        public string Email { get; set; }

        [Required(ErrorMessage = "56")]                             // ErrorMessage = "The Password field is required."
        [StringLength(100, ErrorMessage = "51", MinimumLength = 6)] // ErrorMessage = "The {0} must be at least {2} and at max {1} characters long."
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "57")]                             // ErrorMessage = "The Confirm Password field is required."
        [StringLength(100, ErrorMessage = "51", MinimumLength = 6)] // ErrorMessage = "The {0} must be at least {2} and at max {1} characters long."
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "50")]                  // ErrorMessage = "The password and confirmation password do not match."
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "58")]       // ErrorMessage = "The Gender field is required."
        public byte Gender { get; set; }

        [Required(ErrorMessage = "59")]       // ErrorMessage = "The BirthDate field is required."
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "60")]       // ErrorMessage = "The Career field is required."
        public int Career { get; set; }
    }

    public class InputModel_Login
    {
        [Required(ErrorMessage = "61")]     // ErrorMessage = "The UserID field is required."
        [EmailAddress(ErrorMessage = "62")] // ErrorMessage = "The Email field is not a valid e-mail address."
        public string UserID { get; set; }

        [Required(ErrorMessage = "63")]       // ErrorMessage = "The Password field is required."
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        //public string Latitude { get; set; }
        //public string Longitude { get; set; }
    }

    public class InputModel_UserSetting
    {
        public BackgroundColor BackgroundColor { get; set; }

        public ExternalSearchEngine ExternalSearchEngine { get; set; }

        //[Phone]
        //public string PhoneNumber { get; set; }
    }

    public class InputModel_Index
    {
        //[Required(ErrorMessage = "The Email field is required.")]
        //[EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        //public string Email { get; set; }

        //[Phone]
        //public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "64")]       // ErrorMessage = "The Gender field is required."
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "65")]       // ErrorMessage = "The BirthDate field is required."
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "66")]       // ErrorMessage = "The Career field is required."
        public int Career { get; set; }
    }

    public class InputModel_Feedback
    {
        [Required(ErrorMessage = "67")]       // ErrorMessage = "The Feedback Category field is required."
        public FeedbackCategory FeedbackCategory { get; set; }

        [Required(ErrorMessage = "68")]       // ErrorMessage = "The Subject field is required."
        public string Subject { get; set; }

        [Required(ErrorMessage = "69")]       // ErrorMessage = "The Text is required."
        public string Text { get; set; }
    }


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

    public class InputModel_Business
    {
        [Required(ErrorMessage = "70")]       // ErrorMessage = "The Category is required."
        public BusinessCategory Category { get; set; }

        [Required(ErrorMessage = "74")]       // ErrorMessage = "The Organization Name is required."
        [StringLength(50)]       // ErrorMessage = 
        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "75")]       // ErrorMessage = "The Organization URL is required."
        [Url]       // ErrorMessage = 
        [StringLength(200)]       // ErrorMessage = 
        public string OrganizationURL { get; set; }
    }

    public class InputModel_AdverRelation
    {
        [Required(ErrorMessage = "76")]       // ErrorMessage = "The Valid is required."
        public Valid Valid { get; set; }

        [Required(ErrorMessage = "77")]       // ErrorMessage = "The Category is required."
        public AdverCategory Category { get; set; }

        [Required(ErrorMessage = "78")]       // ErrorMessage = "The Adver Name is required."
        [StringLength(50)]
        public string AdverName { get; set; }

        [Required(ErrorMessage = "79")]       // ErrorMessage = "The Adver Title1 is required."
        [StringLength(50)]
        public string AdverTitle1 { get; set; }

        [Required(ErrorMessage = "80")]       // ErrorMessage = "The Adver Title2 is required."
        [StringLength(50)]
        public string AdverTitle2 { get; set; }

        [Required(ErrorMessage = "81")]       // ErrorMessage = "The Adver URL is required."
        [Url]       // ErrorMessage = 
        [StringLength(200)]       // ErrorMessage = 
        public string AdverURL { get; set; }

        // 広告予算
        [Required(ErrorMessage = "82")]       // ErrorMessage = "The Advertising Budget is required."
        [Range(1, 999999999)]       // ErrorMessage = 
        public int AdvertisingBudget { get; set; }
    }

    public class InputModel_AdverSearch
    {
        [Required(ErrorMessage = "83")]       // ErrorMessage = "The Valid is required."
        public Valid Valid { get; set; }

        [Required(ErrorMessage = "84")]       // ErrorMessage = "The Category is required."
        public AdverCategory Category { get; set; }

        [Required(ErrorMessage = "85")]       // ErrorMessage = "The Adver Name is required."
        [StringLength(50)]       // ErrorMessage = 
        public string AdverName { get; set; }

        [Required(ErrorMessage = "86")]       // ErrorMessage = "The Adver Title1 is required."
        [StringLength(100)]       // ErrorMessage = 
        public string AdverTitle1 { get; set; }

        [Required(ErrorMessage = "87")]       // ErrorMessage = "The Adver Title2 is required."
        [StringLength(100)]       // ErrorMessage = 
        public string AdverTitle2 { get; set; }

        [Required(ErrorMessage = "88")]       // ErrorMessage = "The Adver URL is required."
        [Url]       // ErrorMessage = 
        [StringLength(200)]       // ErrorMessage = 
        public string AdverURL { get; set; }

        // 広告予算
        [Required(ErrorMessage = "89")]       // ErrorMessage = "The Advertising Budget is required."
        [Range(1, 999999999)]       // ErrorMessage = 
        public int AdvertisingBudget { get; set; }
    }

    public class InputModel_AdverWordEdit
    {
        [Required(ErrorMessage = "90")]       // ErrorMessage = "The Word is required."
        public string Word { get; set; }

        [Required(ErrorMessage = "91")]       // ErrorMessage = "The ClickCost is required."
        [Range(1, 9999)]       // ErrorMessage = 
        public short ClickCost { get; set; } = 10;
    }

    //public class InputModel_Adver
    //{
    //    [Required]
    //    public Valid Valid { get; set; }

    //    [Required]
    //    public AdverCategory Category { get; set; }

    //    [Required]
    //    public string AdverName { get; set; }

    //    [Required]
    //    public string AdverTitle1 { get; set; }

    //    [Required]
    //    public string AdverTitle2 { get; set; }

    //    [Required]
    //    [Url]
    //    public string AdverURL { get; set; }

    //    [Required]
    //    public int AdvertisingBudget { get; set; }
    //}

    //public class AdverCompetingUnitPrice
    //{
    //    public AdverPageType AdverPageType { get; set; }

    //    public List<UnitPrice> UnitPriceList { get; set; }
    //}

}
