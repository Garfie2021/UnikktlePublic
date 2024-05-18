using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unikktle.Common
{
    public static class アイテム間の関係
    {
        public const byte 列0_左アイテム名 = 0;
        public const byte 列1_関係 = 1;
        public const byte 列2_右アイテム名 = 2;
        public const byte 列3_関係説明 = 3;
    }

    // 定数
    public static class Consts
    {
        public const int y間隔 = 40; // Rect/Textの間隔
        public const int ParentRect_yStart = 40;
        public const int ParentText_yStart = 60;
        public const int PrText_yStart = ParentRect_yStart + 30;
        public const int ChildRect_yStart = ParentRect_yStart + 30; // 20は予約
        public const int ChildText_yStart = ParentText_yStart + 30; // 20は予約
        public const int Line_y = ParentRect_yStart + 15;

        public const int OnePageNum = 30;     // 1ページに表示する件数

        public const int StartColumnNoNumber = 0;  // 列Noのデフォルト値
        public const int StartRowNoNumber = 0;     // 行Noのデフォルト値
    }

    // セッション変数名
    // HttpContext.Session.SetInt32(
    // HttpContext.Session.SetString(
    // HttpContext.Session.Set(
    // HttpContext.Session.GetInt32(
    // HttpContext.Session.GetString(
    // HttpContext.Session.Get<
    public static class SessionKey
    {
        public const string MindNo = "mn";
        public const string MindEditMode = "mem";
        public const string MindEditInput = "mei";
        public const string MindViewModel = "mvm";
        public const string MindEditPreview = "mep";

        public const string StatusMessage = "sm";
        
        public const string UserNo = "un";

        public const string BackgroundColor = "bc";
        public const string ExternalSearchEngine = "ese";
        public const string ClickPRList = "cpl";
        public const string ClickWordList = "cwl";

        public const string UserSearchString = "uss";
        public const string UserRelationString = "urs";

        public const string HeaderTitile = "ht";

        public const string ScreenTransitionMode = "stm";

        public const string Referer = "rf";
        
        // Set Adver.cshtml.cs
        // Get Adver.cshtml.cs
        // Get AdverRelationEdit.cshtml.cs
        // Get AdverSearchEdit.cshtml.cs
        public const string BusinessId = "bi";

        public const string AdverRelationWordEdit = "arwe";
        public const string ViewModel_AdverEdit = "vm_ae";
        //public const string AdverRelationWordSelect_rid = "arws_ri";
        public const string BusinessEditModel_No = "bem_n";
        //public const string AdverCompetingUnitPrice = "acp";
        public const string AdverSearchWordEdit = "asw";

        //public const string AdverCompetingUnitPricesSelect_Word = "acupsw";

        public const string AdverPageType = "apt";

        public const string ScreenTransitionMode_AdverCompetingUnitPricesSelect = "stmacups";
        

        //public const string InputModel_AdverWordEdit = "imawe";


    }
}
