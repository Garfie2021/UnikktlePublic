using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unikktle.Common;
using Unikktle.Models;


namespace Unikktle.Data
{
    public static class SP_Mind
    {
        public static List<MindSearch> Select_Contains(ApplicationDbContext dbContext,
            string searchWord, long afterNum, long createUserNo, out long allCnt)
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
            var list = dbContext.MindSearch
                .FromSqlRaw("EXECUTE mst.spMind_Select_Contains @SearchWord1, @SearchWord2, @SearchWord3, @SearchWord4, @SearchWord5, @AfterNum, @CreateUserNo, @AllCnt OUTPUT",
                new SqlParameter("SearchWord1", para[0]),
                new SqlParameter("SearchWord2", para.Length > 1 ? para[1] : ""),
                new SqlParameter("SearchWord3", para.Length > 2 ? para[2] : ""),
                new SqlParameter("SearchWord4", para.Length > 3 ? para[3] : ""),
                new SqlParameter("SearchWord5", para.Length > 4 ? para[4] : ""),
                new SqlParameter("AfterNum", afterNum),
                new SqlParameter("CreateUserNo", createUserNo),
                outPara).ToList();

            allCnt = (long)outPara.Value;
            return list;
        }

        public static List<MindSearch> Select_Freetext(ApplicationDbContext dbContext,
            string searchWord, long afterNum, long createUserNo, out long allCnt)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@AllCnt",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            // 動的SQLによるパフォーマンス低下、SQLインジェクション対策への対処として、
            // スペース区切りの絞り込み検索は、5ワードまでの固定で対応する。
            var list = dbContext.MindSearch
                .FromSqlRaw("EXECUTE mst.spMind_Select_Freetext @SearchWord, @AfterNum, @CreateUserNo, @AllCnt OUTPUT",
                new SqlParameter("SearchWord", searchWord),
                new SqlParameter("AfterNum", afterNum),
                new SqlParameter("CreateUserNo", createUserNo),
                outPara).ToList();

            allCnt = (long)outPara.Value;
            return list;
        }

        public static List<MindSearch> Select_Author(ApplicationDbContext dbContext,
            long userNo)
        {
            // 動的SQLによるパフォーマンス低下、SQLインジェクション対策への対処として、
            // スペース区切りの絞り込み検索は、5ワードまでの固定で対応する。
            return dbContext.MindSearch
                .FromSqlRaw("EXECUTE mst.spMind_Select_Author @UserNo",
                new SqlParameter("UserNo", userNo)
                ).ToList();
        }


        public static List<Mind> SelectNo(ApplicationDbContext dbContext, long i)
        {
            return dbContext.Mind
                .FromSqlRaw("EXECUTE mst.spMind_SelectNo @No",
                new SqlParameter("@No", i)
                ).ToList();
        }

        public static List<MindJson> SelectNo_JsonViewModel(ApplicationDbContext dbContext, long no)
        {
            return dbContext.MindJson
                .FromSqlRaw("EXECUTE mst.spMind_SelectNo_JsonViewModel @No",
                new SqlParameter("@No", no)
                ).ToList();
        }

        public static void Get_UserNo(ApplicationDbContext dbContext, long no,
            out long UserNo, out bool AllowOtherEdit)
        {
            var outUserNo = new SqlParameter()
            {
                ParameterName = "@UserNo",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            var outAllowOtherEdit = new SqlParameter()
            {
                ParameterName = "@AllowOtherEdit",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };
            
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spMind_Get_UserNo_AllowOtherEdit @No, @UserNo OUTPUT, @AllowOtherEdit OUTPUT",
                new SqlParameter("@No", no),
                outUserNo,
                outAllowOtherEdit);

            UserNo = (long)outUserNo.Value;
            AllowOtherEdit = (bool)outAllowOtherEdit.Value;
        }

        public static long Insert(ApplicationDbContext dbContext,
            long userNo, string Title, string Explanation, string Item_SpaceSeparator, string ItemRelation, bool PublishOnlyToMe, bool AllowOtherEdit, string JsonViewModel)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@MindNo",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spMind_Insert @UserNo, @Title, @Explanation, @Item_SpaceSeparator, @ItemRelation, @PublishOnlyToMe, @AllowOtherEdit, @JsonViewModel, @MindNo OUTPUT",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@Title", Title),
                new SqlParameter("@Explanation", Explanation),
                new SqlParameter("@Item_SpaceSeparator", Item_SpaceSeparator),
                new SqlParameter("@ItemRelation", ItemRelation),
                new SqlParameter("@PublishOnlyToMe", PublishOnlyToMe),
                new SqlParameter("@AllowOtherEdit", AllowOtherEdit),
                new SqlParameter("@JsonViewModel", JsonViewModel),
                outPara);
            
            return (long)outPara.Value;
        }

        public static void Update(ApplicationDbContext dbContext,
            long No, string Title, string Explanation, string Item_SpaceSeparator, string ItemRelation, bool PublishOnlyToMe, bool AllowOtherEdit, string JsonViewModel)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spMind_Update @No, @Title, @Explanation, @Item_SpaceSeparator, @ItemRelation, @PublishOnlyToMe, @AllowOtherEdit, @JsonViewModel",
                new SqlParameter("@No", No),
                new SqlParameter("@Title", Title),
                new SqlParameter("@Explanation", Explanation),
                new SqlParameter("@Item_SpaceSeparator", Item_SpaceSeparator),
                new SqlParameter("@ItemRelation", ItemRelation),
                new SqlParameter("@PublishOnlyToMe", PublishOnlyToMe),
                new SqlParameter("@AllowOtherEdit", AllowOtherEdit),
                new SqlParameter("@JsonViewModel", JsonViewModel)
                );
        }

        public static void Delete(ApplicationDbContext dbContext,
            long No)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spMind_Delete @No",
                new SqlParameter("@No", No)
                );
        }

    }
}
