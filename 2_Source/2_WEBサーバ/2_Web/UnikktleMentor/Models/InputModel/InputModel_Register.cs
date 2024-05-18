using System;
using System.ComponentModel.DataAnnotations;
using UnikktleMentorEngine;

namespace UnikktleMentor.Models
{
    public class InputModel_Register
    {
        //[Required]
        //public string Name { get; set; }

        // ログインIDは必須です。
        [Required(ErrorMessage = "61")]
        public string LoginID { get; set; }

        // 電子メールは必須です。
        [Required(ErrorMessage = "54")]
        // 電子メールは有効な電子メールアドレスではありません。
        [EmailAddress(ErrorMessage = "55")]
        public string Email { get; set; }

        // パスワードは必須です。
        [Required(ErrorMessage = "56")]
        // {0}は、少なくとも{2}文字で、最大{1}文字の長さである必要があります。
        [StringLength(100, ErrorMessage = "51", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // パスワードの確認]は必須です。
        [Required(ErrorMessage = "57")]
        // {0}は、少なくとも{2}文字で、最大{1}文字の長さである必要があります。
        [StringLength(100, ErrorMessage = "51", MinimumLength = 6)] // ErrorMessage = "The {0} must be at least {2} and at max {1} characters long."
        // パスワードが一致しません。
        [Compare("Password", ErrorMessage = "50")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        // ニックネームは必須です
        [Required(ErrorMessage = "49")]
        public string Nickname { get; set; }

        // 性別は必須です。
        [Required(ErrorMessage = "58")]
        public Gender Gender { get; set; }

        // 誕生日は必須です。s
        [Required(ErrorMessage = "59")]
        public DateTime? BirthDate { get; set; }

        // 職業は必須です。
        [Required(ErrorMessage = "60")]
        public int Career { get; set; }
    }

}
