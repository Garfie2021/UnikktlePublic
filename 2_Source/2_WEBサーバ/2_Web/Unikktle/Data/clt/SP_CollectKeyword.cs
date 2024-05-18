using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unikktle.Models;

namespace Unikktle.Data
{
    // SQLインジェクション対策済み
    public static class SP_CollectKeyword
    {
        /*
        public static List<Keyword> Keyword_Select(ApplicationDbContext dbContext, 
            string searchWord, long afterNum)
        {
            ////return context.Keyword
            ////    .FromSqlRaw("SELECT TOP (1000) [AAA] as Id, [BBB] as Word FROM [dbo].[Table_1]")
            ////    .ToList();

            ////return context.Keyword
            ////    .FromSqlRaw("EXECUTE clt.spKeyword_Select '{0}'", searchWord)
            ////    .ToList();

            //// 実行結果：0件
            //var a = context.Keyword
            //    .FromSqlRaw("EXECUTE clt.spKeyword_Select '{0}'", "a")
            //    .ToList();

            //// 実行結果：0件
            //var b = context.Keyword
            //    .FromSqlRaw($"EXECUTE clt.spKeyword_Select '{"a"}'")
            //    .ToList();

            //// 実行結果：100件
            //var c = context.Keyword
            //    .FromSqlRaw("EXECUTE clt.spKeyword_Select 'a'")
            //    .ToList();

            //// 実行結果：100件
            //string para = $"EXECUTE clt.spKeyword_Select '{"a"}'";
            //var d = context.Keyword
            //    .FromSqlRaw(para)
            //    .ToList();

            //return new List<Keyword>();

            // 実行結果：100件
            // SQLインジェクション対策済み
            return context.Keyword
                .FromSqlRaw("EXECUTE clt.spKeyword_Select @SearchWord, @AfterNum", 
                new SqlParameter("SearchWord", searchWord),
                new SqlParameter("AfterNum", afterNum)
                ).ToList();
        }
        */

        public static List<KeywordSearch> Select_Contains(ApplicationDbContext dbContext, 
            string searchWord, long afterNum, out long allCnt)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@AllCnt",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            var para = searchWord.Trim().Split(' ');

            // 動的SQLによるパフォーマンス低下、SQLインジェクション対策への対処として、
            // スペース区切りの絞り込み検索は、5ワードまでの固定で対応する。
            var list = dbContext.KeywordSearch
                .FromSqlRaw("EXECUTE clt.spKeyword_Select_Contains @SearchWord1, @SearchWord2, @SearchWord3, @SearchWord4, @SearchWord5, @AfterNum, @AllCnt OUTPUT",
                new SqlParameter("SearchWord1", para[0]),
                new SqlParameter("SearchWord2", para.Length > 1 ? para[1] : ""),
                new SqlParameter("SearchWord3", para.Length > 2 ? para[2] : ""),
                new SqlParameter("SearchWord4", para.Length > 3 ? para[3] : ""),
                new SqlParameter("SearchWord5", para.Length > 4 ? para[4] : ""),
                new SqlParameter("AfterNum", afterNum),
                outPara).ToList();

            allCnt = (long)outPara.Value;
            return list;
        }


        public static List<KeywordSearch> Select_Freetext(ApplicationDbContext dbContext,
            string searchWord, long afterNum, out long allCnt)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@AllCnt",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            // 動的SQLによるパフォーマンス低下、SQLインジェクション対策への対処として、
            // スペース区切りの絞り込み検索は、5ワードまでの固定で対応する。
            var list = dbContext.KeywordSearch
                .FromSqlRaw("EXECUTE clt.spKeyword_Select_Freetext @SearchWord, @AfterNum, @AllCnt OUTPUT",
                new SqlParameter("SearchWord", searchWord),
                new SqlParameter("AfterNum", afterNum),
                outPara).ToList();

            allCnt = (long)outPara.Value;
            return list;
        }


        // SQLインジェクション対策済み
        public static List<Keyword> SelectNo(ApplicationDbContext dbContext, long No)
        {
            return dbContext.Keyword
                .FromSqlRaw("EXECUTE clt.spKeyword_SelectNo @No", 
                new SqlParameter("@No", No))
                .ToList();
        }


        public static long GetCount(ApplicationDbContext dbContext)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Cnt",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE clt.spKeyword_GetCount @Cnt OUTPUT", 
                outPara);

            return (long)outPara.Value;
        }

        public static string GetWord(ApplicationDbContext dbContext, long No)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Word",
                SqlDbType = SqlDbType.NVarChar,
                Size = 100,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE clt.spKeyword_GetWord @No, @Word OUTPUT",
                new SqlParameter("@No", No),
                outPara);

            return (string)outPara.Value;
        }

        public static long GetNo(ApplicationDbContext dbContext, string word)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@No",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE clt.spKeyword_GetWord @Word, @No OUTPUT",
                new SqlParameter("@Word", word),
                outPara);

            return (long)outPara.Value;
        }

    }
}
