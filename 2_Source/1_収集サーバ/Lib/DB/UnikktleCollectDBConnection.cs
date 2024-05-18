//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.IO;
//using System.Text;
//using System.Threading.Tasks;
//using 定数;
//using Logging;
//using AppDirectory;


//namespace DB
//{
//    public static class UnikktleCmnDBConnection
//    {
//        // 初期化
//        public static void Initialize(SqlConnection cn, string connectionString)
//        {
//            ログ.ログ書き出し($"ConnectionString [DB.Cmn] : {connectionString}");

//            cn.ConnectionString = connectionString;
//            cn.Open();

//            // job
//            DB.Cmn.SP_ExecHistory.Initialize(cn);
//        }
//    }

//    public static class UnikktleCollectDBConnection
//    {
//        // 初期化
//        public static void Initialize(SqlConnection cn, string connectionString)
//        {
//            ログ.ログ書き出し($"ConnectionString [DB.Collect] : {connectionString}");

//            cn.ConnectionString = connectionString;
//            cn.Open();

//            // hist
//            DB.Collect.SP_CollectYahoo.Initialize(cn);
//            DB.Collect.SP_CollectBing.Initialize(cn);
//            DB.Collect.SP_CollectGoogle.Initialize(cn);
//            DB.Collect.SP_CollectMail.Initialize(cn);

//            DB.Collect.SP_HtmlParseYahoo.Initialize(cn);
//            DB.Collect.SP_HtmlParseBing.Initialize(cn);
//            DB.Collect.SP_HtmlParseGoogle.Initialize(cn);

//            DB.Collect.SP_ExtractYahoo.Initialize(cn);
//            DB.Collect.SP_ExtractBing.Initialize(cn);
//            DB.Collect.SP_ExtractGoogle.Initialize(cn);
//            DB.Collect.SP_ExtractMail.Initialize(cn);

//            DB.Collect.SP_CollaborateKeyword.Initialize(cn);
            
//            // mst
//            DB.Collect.SP_CollectTarget.Initialize(cn);
//            DB.Collect.SP_Keyword.Initialize(cn);

//            // tmp
//            DB.Collect.SP_TmpCollectTargetKeyword.Initialize(cn);
//            DB.Collect.SP_TmpCollectTargetKeywordMail.Initialize(cn);

//            // wlt
//            DB.Collect.SP_SearchWord.Initialize(cn);            

//            //MeCabDB.Initialize(cn);
//            MorphologicalAnalysisDB.Initialize(cn);

//        }
//    }

//    public static class UnikktleWebDBConnection
//    {
//        // 初期化
//        public static void Initialize(SqlConnection cn, string connectionString)
//        {
//            ログ.ログ書き出し($"ConnectionString [DB.Web] : {connectionString}");

//            cn.ConnectionString = connectionString;
//            cn.Open();

//            // clt
//            DB.Web.SP_Keyword.Initialize(cn);
//            DB.Web.SP_CollaborateKeyword.Initialize(cn);

//            // mst
//            DB.Web.SP_SearchWord.Initialize(cn);
//        }
//    }

//    public static class UnikktleWebCollectWorkDBConnection
//    {
//        // 初期化
//        public static void Initialize(SqlConnection cn, string connectionString)
//        {
//            ログ.ログ書き出し($"ConnectionString [DB.WebCollectWork] : {connectionString}");

//            cn.ConnectionString = connectionString;
//            cn.Open();

//            // mst
//            DB.WebCollectWork.SP_Keyword.Initialize(cn);

//            // hst
//            DB.WebCollectWork.SP_CollaborateKeyword.Initialize(cn);
//            DB.WebCollectWork.SP_CollaborateKeywordCount_WebServer.Initialize(cn);
//            DB.WebCollectWork.SP_CollaborateKeywordCount_CollectServer.Initialize(cn);

//        }
//    }

//    public static class MsdbDBConnection
//    {
//        // 初期化
//        public static void Initialize(SqlConnection cn, string connectionString)
//        {
//            ログ.ログ書き出し($"ConnectionString [DB.Msdb] : {connectionString}");

//            cn.ConnectionString = connectionString;
//            cn.Open();

//            // dbo
//            DB.Msdb.SP_StartJob.Initialize(cn);

//        }
//    }

//}
