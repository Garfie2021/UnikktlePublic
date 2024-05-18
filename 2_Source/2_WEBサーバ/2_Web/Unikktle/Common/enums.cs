using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unikktle.Common
{

    public enum ItemType : byte
    {
        Item = 0,
        Sentence = 1,
    }

    public enum RelationType : byte
    {
        無関係 = 0,  // LeftはRightに関係する（ブランク）
        依存 = 1,  // LeftはRightに依存する（>）
        接続 = 2,  // LeftはRightに接続する（-）
    }

    public enum PolyLineNo : byte
    {
        A_始点から中間点 = 0,  // 始点から中間点
        B_中間点から中間点 = 1,  // 中間点から中間点
        C_中間点から終点 = 2,  // 中間点から終点
    }

    public enum LineType : byte
    {
        SuperParentが違う = 0,
        SuperParentが同じ = 1,
    }

    public enum MindEditMode : byte
    {
        Unselect = 0,
        New = 1,
        Edit = 2,
        Duplication = 3,
        Delete = 4,
    }

    public enum MindType : byte
    {
        // HTTP POSTのデータは対象外だった
        Rect = 0,
        Line = 1,
        Text = 2,
        Link = 3,
    }

    public enum PayPalIpnStatus : byte
    {
        // HTTP POSTのデータは対象外だった
        Excluded = 0,
        UserNotFound = 1,
        Completed = 2,
    }

    public enum PayPalBuyStatus : byte
    {
        NotCompleted = 0,
        IPN_Fail = 1,
        Completed = 2,
    }

    public enum ScreenTransitionMode_AdverCompetingUnitPricesSelect : byte
    {
        不明 = 0,
        競合単価画面遷移 = 1,  // 競合単価画面から戻って来た
    }

    public enum ScreenTransitionMode : byte
    {
        不明 = 0,
        新規登録 = 1,
        編集開始 = 2,
        編集中 = 3,
    }

    public enum AdverPageType : byte
    {
        Search = 0,
        Relation = 1,
    }

    public enum DelFlg : byte
    {
        削除しない = 0,
        削除する = 1,
    }

    
    public enum Valid : byte
    {
        無効 = 0, // Invalid
        有効 = 1, // Valid
    }

    public enum AdverCategory : byte
    {
        無料 = 1,
        有料 = 2,
    }

    public enum BusinessCategory : byte
    {
        法人 = 1,      // NPOを含む
        個人 = 2,      // NGOを含む
        その他 = 3,    // NGOを含む
    }

    public enum FeedbackCategory : byte
    {
        問題バグ = 1,
        意見感想 = 2,
        その他 = 3,
    }

    // [UnikktleWeb].[mst].[tAttribute]テーブル[Class]列の内訳
    public enum AttributeClass : int
    {
        CareerCategory = 1,
    }

    public enum BackgroundColor : byte
    {
        White = 1,
        Black = 2,
    }

    public enum Gender : byte
    {
        White = 1,
        Black = 2,
    }

    public enum ExternalSearchEngine : byte
    {
        Google = 1,
        Bing = 2,
        Yahoo = 3
    }

    public enum 全角半角 : byte
    {
        未判定 = 0,
        全角含む = 1,
        半角のみ = 2,
    }

}
