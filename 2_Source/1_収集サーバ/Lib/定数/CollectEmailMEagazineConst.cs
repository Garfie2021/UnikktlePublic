using System;
using System.Collections.Generic;
using System.Text;

namespace 定数
{
    public static class CollectEmailMEagazineConst
    {
        //public const string host = "pop.xxx.com"; // 接続先IMAPサーバーのホスト名
        //public const int port = 995; // 接続先のポート番号
        //public const string username = "xxx@xxx.com"; // ログインユーザー名
        //public const string password = "xxx"; // ログインパスワード

        public static bool 古いメール削除 = false;

        
        public enum CollectTargetType : byte
        {
            メールマガジン = 1,
            WEB,
        }

        public enum CulcState : byte
        {
            未解析 = 0,
            形態素解析済み = 1,
        }

    }
}
