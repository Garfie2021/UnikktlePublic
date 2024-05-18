using System;
using System.Collections.Generic;
using System.Text;

namespace 定数
{
    //public static class 列挙型
    //{
    //public const string host = "pop.xxx.com"; // 接続先IMAPサーバーのホスト名
    //public const int port = 995; // 接続先のポート番号
    //public const string username = "xxx@xxx.com"; // ログインユーザー名
    //public const string password = "xxx"; // ログインパスワード

    //public enum TargetCategory : byte
    //{
    //    未選択 = 0,
    //    IT = 1,
    //}

    public enum CollectState : byte
    {
        収集失敗 = 0,
        収集成功 = 1
    }

    // このenumは[mst].[tExecHistory]テーブルと連動する。
    public enum ジョブType : byte
    {
        _1_CollectEmailMEagazine = 1,
        _1_CollectGoogleSearch = 2,
        _1_CollectBingSearch = 3,
        _1_CollectYahooSearch = 4,
        _1_CollectWebPage = 5,
        _2_HtmlParse = 100,
        _2_HtmlParseWebPage = 101,
        _3_ExtractEnglishConcatNoun = 110,
        _3_ExtractEnglishConcatNounWebPage = 111,
        _4_MeCabExec = 120,
        _5_UnikktleCollect_協調フィルタリング = 130,
        FullText = 140,
    }

    public enum 関連キーワードFlag : byte
    {
        関連キーワードじゃない = 0,
        関連キーワード = 1,
    }

    public enum CollectTargetCategory : byte
    {
        未選択 = 0,
        人手入力 = 1,
        メールマガジン = 2,
        Google検索 = 3,
        Bing検索 = 4,
        Yahoo検索 = 5,
        WebPage収集 = 6,
    }

    public enum State : byte
    {
        未解析 = 0,
        解析済み = 1,
    }

    public enum 解析結果 : byte
    {
        失敗 = 0,
        成功 = 1,
    }

    public enum 言語判定結果 : byte
    {
        未判定 = 0,
        日本語 = 1,
        英語 = 2,
    }

    public enum 全角半角 : byte
    {
        未判定 = 0,
        全角含む = 1,
        半角のみ = 2,
    }

    public enum 採用不採用 : byte
    {
        不採用 = 0,
        採用 = 1,
    }

    public enum 名詞区分 : byte
    {
        人手で目検 = 0,
        英語連結名詞 = 1,
        形態素解析名詞 = 2,
        形態素解析連結名詞 = 3,
    }

    public static class 数値
    {
        public static int _10MB = 1024 * 1024 * 10;
    }

}
