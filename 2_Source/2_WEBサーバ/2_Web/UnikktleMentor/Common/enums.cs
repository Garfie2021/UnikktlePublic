using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnikktleMentor.Common
{

    public enum 編集モード : byte
    {
        未選択 = 0,
        新規作成 = 1,
        編集 = 2,
    }

    public enum JoinTeamStatus : byte
    {
        参加申請中 = 1,
        参加済み = 2,
        チームオーナー = 3,
    }

    public enum FeedbackCategory : byte
    {
        問題バグ = 1,
        意見感想 = 2,
        その他 = 3,
    }

    // [mst].[tAttribute]テーブル[Class]列の内訳
    public enum AttributeClass : int
    {
        CareerCategory = 1,
    }


    public enum MentorLiteracy : byte
    {
        一般ユーザ = 0,
        開発者 = 5,
    }

}
