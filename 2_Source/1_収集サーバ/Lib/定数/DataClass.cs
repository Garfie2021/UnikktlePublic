using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace 定数
{
    // アプリ次回実行時に引き継ぐ設定データ。
    // ファイル無かった場合、このクラスのデフォルト値でファイルを作成するので、各メンバ変数にはデフォルト値が必須。
    // 
    // データファイルのパス
    // 　AppSetting.AppDataFilePath
    // 　appsettings.json > AppDataFilePath
    [DataContract]
    public class AppData
    {
        [DataMember]
        public string BcpExportLastDate_MstKeyword { get; set; } = "2001/1/1 00:00:00";
    }

    public class HtmlParseResult
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        //public State State;
        public 解析結果 解析結果;
    }

    public class HtmlParseResultWebPage
    {
        public long DomainNo;
        public long UrlNo;
        public 解析結果 解析結果;
    }
    

    public class HtmlParseFail
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public string Status;
        public string Html;
    }

    public class HtmlParseFailWebPage
    {
        public long DomainNo;
        public long UrlNo;
        public string Status;
        public string Html;
    }


    public class 抽出文
    {
        public byte SearchResultNo;
        public 関連キーワードFlag 関連キーワード;
        public string 文;
    }

    // [hst].[tCollectTargetKeyword_Bing]テーブルに対応する行クラス
    public class CollectTargetKeyword_Bing
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte SearchResultNo;
        public long KeywordNo;
        public DateTime 登録日時;
        public DateTime 更新日時;
    }

    // [hst].[tCollectTargetKeyword_Google]テーブルに対応する行クラス
    public class CollectTargetKeyword_Google
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte SearchResultNo;
        public long KeywordNo;
        public DateTime 登録日時;
        public DateTime 更新日時;
    }

    // [hst].[tCollectTargetKeyword_Yahoo]テーブルに対応する行クラス
    public class CollectTargetKeyword_Yahoo
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte SearchResultNo;
        public long KeywordNo;
        public DateTime 登録日時;
        public DateTime 更新日時;
    }

    // [hst].[tCollectTargetKeyword_Mail]テーブルに対応する行クラス
    public class CollectTargetKeyword_Mail
    {
        public long CollectTargetMailNo;
        public DateTime SearchDate;
        public long KeywordNo;
        public DateTime 登録日時;
        public DateTime 更新日時;
    }

    // [mst].[tCollectTarget]テーブルに対応する行クラス
    public class CollectTargetRow
    {
        public long No;
        public string 名称;
        public string From_MailAddress;
    }

    // [hst].[tCollectMail]テーブルに対応する行クラス
    public class CollectMailRow
    {
        public long CollectTargetNo;    // [hst].[tCollectCollectHistory].[CollectTargetNo] ※使わない
        public DateTime SendDate;
        public DateTime 登録日時;
        public byte State;
        public string FromDisplayName = "";
        public string CurrentMessageID;
        public string CurrentSubject = "";
        public string CurrentBody = "";
        public string FromMailAddress;
    }

    // hst.tCollectGoogle テーブルに対応する行クラス
    public class CollectGoogleRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte State;
        public string 検索結果Html;
    }

    // hst.tCollectBing テーブルに対応する行クラス
    public class CollectBingRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte State;
        public string 検索結果Html;
    }

    // hst.tCollectYahoo テーブルに対応する行クラス
    public class CollectYahooRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte State;
        public string 検索結果Html;
    }

    // hst.t2HtmlParseBing テーブルに対応する行クラス
    public class HtmlParseBingRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte State;
        public string HtmlTag除外後1段階目;
        public string HtmlTag除外後2段階目;
    }

    // hst.t2HtmlParseGoogle テーブルに対応する行クラス
    public class HtmlParseGoogleRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte State;
        public string HtmlTag除外後1段階目;
        public string HtmlTag除外後2段階目;
    }

    // hst.t2HtmlParseYahoo テーブルに対応する行クラス
    public class HtmlParseYahooRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte State;
        public string HtmlTag除外後1段階目;
        public string HtmlTag除外後2段階目;
    }

    // hst.t2HtmlParseWebPage テーブルに対応する行クラス
    public class HtmlParseWebPageRow
    {
        public long DomainNo;
        public long UrlNo;
        public byte State;
        public string HtmlTag除外後1段階目;
        public string HtmlTag除外後2段階目;
    }


    // hst.tExtractGoogle テーブルに対応する行クラス
    public class ExtractGoogleRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte 関連キーワード;
        public byte SearchResultNo;
        public byte EnglishConcatNoun_State;
        public byte MeCabState;
        public 言語判定結果 言語判定;
        public DateTime 登録日時;
        public DateTime 更新日時;
        public string 検索結果Html;
        public string 検索結果Tag除外後;
        public string 不要文字列除外後 = "";
        public string 記号区切り数値 = "";
        public string 記号区切り数値除外後 = "";
        public string 英語連結名詞 = "";
        public string 英語連結名詞除外後 = "";
        public string MeCab名詞 = "";
    }

    // hst.tExtractBing テーブルに対応する行クラス
    public class ExtractBingRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte 関連キーワード;
        public byte SearchResultNo;
        public byte EnglishConcatNoun_State;
        public byte MeCabState;
        public 言語判定結果 言語判定;
        public DateTime 登録日時;
        public DateTime 更新日時;
        public string 検索結果Html;
        public string 検索結果Tag除外後;
        public string 不要文字列除外後 = "";
        public string 記号区切り数値 = "";
        public string 記号区切り数値除外後 = "";
        public string 英語連結名詞 = "";
        public string 英語連結名詞除外後 = "";
        public string MeCab名詞 = "";
    }

    // hst.tExtractYahoo テーブルに対応する行クラス
    public class ExtractYahooRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte 関連キーワード;
        public byte SearchResultNo;
        public byte EnglishConcatNoun_State;
        public byte MeCabState;
        public 言語判定結果 言語判定;
        public DateTime 登録日時;
        public DateTime 更新日時;
        public string 検索結果Html;
        public string 検索結果Tag除外後;
        public string 不要文字列除外後 = "";
        public string 記号区切り数値 = "";
        public string 記号区切り数値除外後 = "";
        public string 英語連結名詞 = "";
        public string 英語連結名詞除外後 = "";
        public string MeCab名詞 = "";
    }

    // hst.tExtractWebPage テーブルに対応する行クラス
    public class ExtractWebPageRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte 関連キーワード;
        public byte SearchResultNo;
        public byte EnglishConcatNoun_State;
        public byte MeCabState;
        public 言語判定結果 言語判定;
        public DateTime 登録日時;
        public DateTime 更新日時;
        public string 検索結果Html;
        public string 検索結果Tag除外後;
        public string 不要文字列除外後 = "";
        public string 記号区切り数値 = "";
        public string 記号区切り数値除外後 = "";
        public string 英語連結名詞 = "";
        public string 英語連結名詞除外後 = "";
        public string MeCab名詞 = "";
    }


    // [hst].[tCollectMail]テーブルに対応する行クラス
    public class ExtractMailRow
    {
        public long CollectTargetNo;    // [hst].[tCollectCollectHistory].[CollectTargetNo] ※使わない
        public DateTime SendDate;
        public DateTime 登録日時;
        public DateTime 更新日時;
        public byte MeCabState;
        public 言語判定結果 言語判定;
        public string 不要文字列除外後 = "";
        public string 記号区切り数値 = "";
        public string 記号区切り数値除外後 = "";
        public string 英語連結名詞 = "";
        public string 英語連結名詞除外後 = "";
        public string MeCab名詞 = "";
    }

    // hst.tCollectTargetKeyword_Mailテーブルに対応する行クラス
    public class CollectTargetKeyword_MailRow
    {
        public long CollectTargetMailNo;
        public DateTime SendDate;
        public long KeywordNo;
    }

    // hst.tCollectTargetKeyword_Googleテーブルに対応する行クラス
    public class CollectTargetKeyword_GoogleRow
    {
        public long SearchKeywordNo;
        public DateTime SearchDate;
        public byte SearchResultNo;
        public long KeywordNo;
    }


    // [mst].[tKeyword]テーブルに対応する行クラス
    public class KeywordRow
    {
        public long No;
        public CollectTargetCategory CollectTargetCategory;
        public long CollectNo;
        public long? SearchResultNo;
        public 関連キーワードFlag 関連キーワード;
        public DateTime? SendDate;
        public byte 採用;           // 0：不採用	1：採用
        public byte 採用判定済み;   // 0：未判定	1：判定済み
        public 名詞区分 名詞区分;       // 0：英語連結名詞	1：形態素解析名詞
        public DateTime 登録日時;
	    public DateTime 更新日時;
        public DateTime Google検索日時;
        public DateTime Yahoo検索日時;
        public DateTime Bing検索日時;
        public string Word;
        public string 解析元データ;
    }

    public class UrlRow
    {
        public long DomainNo;
        public long UrlNo;
        public string URL;
        public DateTime CollectDate;
        public CollectState CollectState;
    }

    public class MorphologicalAnalysisInfo
    {
        public long No;                      // [hst].[tCollectCollectHistory].[CollectTargetNo]
        public DateTime メール送信日時;
        public string From_MailAddress;
        public string From_DisplayName;
        public string CurrentMessageID;
        //public string CurrentSubject;
        public string MorphologicalAnalysis;
        //public string PreviousMessageID;
        //public string PreviousSubject;
        //public string PreviousBody;
    }
}
