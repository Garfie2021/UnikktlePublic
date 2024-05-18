using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using 定数;
using Logging;
using AppDirectory;
using Common;
using DB;


namespace MeCabExec
{
    public class Program
    {
        private static int cntList_Mail = 0;
        private static int cntList_Google = 0;
        private static int cntList_Bing = 0;
        private static int cntList_Yahoo = 0;

        static void Main(string[] args)
        {
            var now = DateTime.Now;

            try
            {
                App.Initialize(args[0], typeof(Program).Namespace);

                // MeCab初期化
                var ptrMeCab = MeCabConst.mecab_new2("");

                using (var cnCmn = new SqlConnection())
                using (var cn = new SqlConnection())
                {
                    cnCmn.ConnectionString = AppSetting.ConnectionString_UnikktleCmn;
                    cnCmn.Open();

                    cn.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                    cn.Open();

                    DB.Collect.SP_TmpCollectTargetKeyword.ReCreateTemptable(cn);

                    MeCabParseGoogle.Exec(cn, ptrMeCab, now, out int cntList_Google, out long CntMeCabState0_Google, out long Cnt関連キーワード以外_Google, out long Cnt日本語_Google, out long Cnt英語_Google);
                    MeCabParseBing.Exec(cn, ptrMeCab, now, out int cntList_Bing, out long CntMeCabState0_Bing, out long Cnt関連キーワード以外_Bing, out long Cnt日本語_Bing, out long Cnt英語_Bing);
                    MeCabParseYahoo.Exec(cn, ptrMeCab, now, out int cntList_Yahoo, out long CntMeCabState0_Yahoo, out long Cnt関連キーワード以外_Yahoo, out long Cnt日本語_Yahoo, out long Cnt英語_Yahoo);
                    MeCabParseMail.Exec(cn, ptrMeCab, now, out int cntList_Mail);

                    if (cntList_Google < 1 || cntList_Bing < 1 || cntList_Mail < 1)
                    {
                        var log = 
                            "Google: " + cntList_Google + "\r\n" +
                            "Bing: " + cntList_Bing + "\r\n" +
                            "Yahoo: " + cntList_Yahoo + "\r\n" +
                            "Mail: " + cntList_Mail + "\r\n\r\n\r\n" +
                            "CntMeCabState0_Google: " + CntMeCabState0_Google + "\r\n" +
                            "Cnt関連キーワード以外_Google: " + Cnt関連キーワード以外_Google + "\r\n" +
                            "Cnt日本語_Google: " + Cnt日本語_Google + "\r\n" +
                            "Cnt英語_Google: " + Cnt英語_Google + "\r\n\r\n" +
                            "CntMeCabState0_Bing: " + CntMeCabState0_Bing + "\r\n" +
                            "Cnt関連キーワード以外_Bing: " + Cnt関連キーワード以外_Bing + "\r\n" +
                            "Cnt日本語_Bing: " + Cnt日本語_Bing + "\r\n" +
                            "Cnt英語_Bing: " + Cnt英語_Bing + "\r\n\r\n" +
                            "CntMeCabState0_Yahoo: " + CntMeCabState0_Yahoo + "\r\n" +
                            "Cnt関連キーワード以外_Yahoo: " + Cnt関連キーワード以外_Yahoo + "\r\n" +
                            "Cnt日本語_Yahoo: " + Cnt日本語_Yahoo + "\r\n" +
                            "Cnt英語_Yahoo: " + Cnt英語_Yahoo + "\r\n\r\n";

                        ログ.ログ書き出し(log);

                        if ((Cnt日本語_Google < 1 && Cnt英語_Google < 1) ||
                            (Cnt日本語_Bing   < 1 && Cnt英語_Bing   < 1) ||
                            (Cnt日本語_Yahoo  < 1 && Cnt英語_Yahoo  < 1))
                        {
                            // Yahoo以外が0件になることはまれ。
                            // 英語データも0件の場合は障害が発生してる。
                            Mail.SendMailAndLogErr("0件発生 MeCabExec", log);
                        }
                    }

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._4_MeCabExec, now, DateTime.Now);
                }

                // MeCab開放
                MeCabConst.mecab_destroy(ptrMeCab);
            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 MeCabExec", ex);
                }
                catch (Exception ex2)
                {
                    File.WriteAllText(@"c:\MeCabExec.log", ex.Message);
                    File.WriteAllText(@"c:\MeCabExec.log", ex.StackTrace);

                    File.WriteAllText(@"c:\MeCabExec.log", ex2.Message);
                    File.WriteAllText(@"c:\MeCabExec.log", ex2.StackTrace);
                }
            }
            finally
            {
                ログ.ログ書き出し($"MeCabExec End. cntList_Mail:{cntList_Mail} cntList_Google:{cntList_Google} cntList_Bing:{cntList_Bing} cntList_Yahoo:{cntList_Yahoo}");
            }
        }

    }
}
