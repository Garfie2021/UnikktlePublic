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
    public static class SP_CollaborateKeyword
    {
        // SQLインジェクション対策済み
        public static List<Keyword> Select(ApplicationDbContext dbContext,
            long No, long? ExcludeNo, long afterNum, out long allCnt)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@AllCnt",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            if (ExcludeNo == null)
            {
                var result = dbContext.Keyword
                    .FromSqlRaw("EXECUTE clt.spCollaborateKeyword_Select @No, @AfterNum, @AllCnt OUTPUT",
                    new SqlParameter("@No", No),
                    new SqlParameter("@AfterNum", afterNum),
                    outPara)
                    .ToList();

                allCnt = (long)outPara.Value;
                return result;
            }
            else
            {
                var result = dbContext.Keyword
                    .FromSqlRaw("EXECUTE clt.spCollaborateKeyword_Select_ExcludeNo @No, @ExcludeNo, @AfterNum, @AllCnt OUTPUT",
                    new SqlParameter("@No", No),
                    new SqlParameter("@ExcludeNo", (long)ExcludeNo),
                    new SqlParameter("@AfterNum", afterNum),
                    outPara)
                    .ToList();

                allCnt = (long)outPara.Value;
                return result;
            }

        }
    }
}
