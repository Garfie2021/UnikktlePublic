using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unikktle.Common;

namespace Unikktle.Models
{
    // [usr].[spUserSetting_Select]ストアド用
    public class UserSetting
    {
        // 実態はNo。プロパティ名は「No」禁止。「Id」じゃないと実行時エラーになる。
        public long Id { get; set; }

        public BackgroundColor BackgroundColor { get; set; }
        public ExternalSearchEngine ExternalSearchEngine { get; set; }
    }

    // [usr].[spUserSetting_SelectProfile]ストアド用
    public class UserSettingProfile
    {
        // 実態はNo。プロパティ名は「No」禁止。「Id」じゃないと実行時エラーになる。
        public long Id { get; set; }

        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int Career { get; set; }
    }


}
