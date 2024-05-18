using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnikktleMentor.Common;
using UnikktleMentorEngine;

namespace UnikktleMentor.Models
{


    // [usr].[spUserSetting_SelectProfile]ストアド用
    public class UserSetting
    {
        // 実態はNo。プロパティ名は「No」禁止。「Id」じゃないと実行時エラーになる。
        public long Id { get; set; }

        public string Email { get; set; }
        public string Nickname { get; set; }

        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Career { get; set; }
        public MentorLiteracy MentorLiteracy { get; set; }
    }

}
