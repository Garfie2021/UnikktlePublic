using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnikktleMentor.Common
{
    public static class DBConst
    {
        public const int CommandTimeout_6h = 60 * 60 * 6;    // 21600秒=6時間
        public const int CommandTimeout_1h = 60 * 60 * 1;    // =1時間
        public const int CommandTimeout_5m = 60 * 5;         // 5分
    }


    // セッション変数名
    // 下記で使う。
    // 　HttpContext.Session.SetInt32(
    // 　HttpContext.Session.SetString(
    // 　HttpContext.Session.Set(
    // 　HttpContext.Session.GetInt32(
    // 　HttpContext.Session.GetString(
    // 　HttpContext.Session.Get<
    public static class SessionKey
    {

        public const string AnswerList = "al";
        public const string AnswerNewStart = "ans";
        public const string AnswerNewEnd = "ane";
        public const string AnswerSupplement = "as";
        public const string C01結果 = "c01";
        public const string C02結果 = "c02";
        public const string C04結果 = "c04";
        public const string C07結果 = "c07";
        public const string C09結果 = "c09";
        public const string C10結果 = "c10";
        public const string CareerAllList_JA = "calj";
        public const string CareerAllList_EN = "cale";
        public const string CareerAndCategoryList_JA = "caclj";
        public const string CareerAndCategoryList_EN = "cacle";

        public const string CurrentPage = "cp";

        public const string LastDiagnosisDate = "ldd";

        public const string Question_AnswerList = "qal";
        public const string QuestionListKey_JA = "qlj";
        public const string QuestionListKey_EN = "qle";

        public const string StatusMessage = "sm";

        // TeamJoinEdit.cshtml.cs
        public const string TeamEdit_SessionModel = "tesm";
        // 参加するチームの条件を設定
        public const string TeamJoinEdit = "tjet";
        // 編集中のチームのID
        public const string EditTeamNo = "etn";


        public const string UserNo = "un";
    }
}
