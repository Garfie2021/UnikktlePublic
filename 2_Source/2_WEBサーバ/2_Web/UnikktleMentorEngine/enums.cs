using System;
using System.Collections.Generic;
using System.Text;

namespace UnikktleMentorEngine
{
    public enum Gender : byte
    {
        Male = 0,  // 男
        Female = 1 // 女
    }

    public enum 回答選択肢 : byte
    {
        _ = 0,
        はい = 1,
        いいえ = 2,
        どちらともいえない = 3,
        訂正回答 = 4,
        未回答 = 5,
    }
}
