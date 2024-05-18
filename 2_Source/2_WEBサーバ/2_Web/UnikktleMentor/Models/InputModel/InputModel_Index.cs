using System;
using System.ComponentModel.DataAnnotations;
using UnikktleMentorEngine;

namespace UnikktleMentor.Models
{
    public class InputModel_Index
    {
        //[Required(ErrorMessage = "The Email field is required.")]
        //[EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        //public string Email { get; set; }

        //[Phone]
        //public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "メールアドレスは必須です")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ニックネームは必須です")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "64")]       // ErrorMessage = "The Gender field is required."
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "65")]       // ErrorMessage = "The BirthDate field is required."
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "66")]       // ErrorMessage = "The Career field is required."
        public int Career { get; set; }

    }

}
