using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;

namespace ExtractEnglishConcatNoun
{
    // MeCabでは半角スペースが除外されてしまう。
    // 「Google API Client Library for .NET」など半角スペースで連結した英語名詞を、
    // MeCabを使わずに切り出す。
    //
    // ※切り出した文字列を除外してMeCabにかけるまではやらない。
    //　 除外してしまうと、英語連結名詞に含まれている「API」などの単語との関係性が弱まってしまうので良くない。

    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var now = DateTime.Now;

                App.Initialize(args[0], typeof(Program).Namespace);

                using (var cnCmn = new SqlConnection())
                using (var cn = new SqlConnection())
                {
                    cnCmn.ConnectionString = AppSetting.ConnectionString_UnikktleCmn;
                    cnCmn.Open();

                    cn.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                    cn.Open();

                    DB.Collect.SP_TmpCollectTargetKeyword.ReCreateTemptable(cn);

                    抽出Bing.Exec(cn, now);
                    抽出Google.Exec(cn, now);
                    抽出Yahoo.Exec(cn, now);
                    抽出Mail.Exec(cn, now);
                    抽出WebPage.Exec(cn, now);

                    DB.Cmn.SP_ExecHistory.ExecHistory_Insert(cnCmn, ジョブType._3_ExtractEnglishConcatNoun, now, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Mail.SendMailAndLogErr("Exception発生 ExtractEnglishConcatNoun", ex);
                }
                catch (Exception ex2)
                {
                    File.WriteAllText(@"d:\ExtractEnglishConcatNoun.log", ex.Message);
                    File.WriteAllText(@"d:\ExtractEnglishConcatNoun.log", ex.StackTrace);

                    File.WriteAllText(@"d:\ExtractEnglishConcatNoun.log", ex2.Message);
                    File.WriteAllText(@"d:\ExtractEnglishConcatNoun.log", ex2.StackTrace);
                }
            }
            finally
            {
                ログ.ログ書き出し("End");
            }
        }

    }
}

