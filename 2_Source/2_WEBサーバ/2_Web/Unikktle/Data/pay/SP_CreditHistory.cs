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
    public static class SP_CreditHistory
    {
        public static List<Credit> SelectUserNo(ApplicationDbContext dbContext,
            long UserNo)
        {
            return dbContext.Credit
                .FromSqlRaw("EXECUTE pay.spCreditHistory_SelectUserNo @UserNo",
                new SqlParameter("@UserNo", UserNo))
                .ToList();
        }

        public static int GetCnt(ApplicationDbContext dbContext,
            long UserNo, string txn_id)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Cnt",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE pay.spCreditHistory_GetCnt @UserNo, @txn_id, @Cnt OUTPUT",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@txn_id", txn_id),
                outPara);

            return (int)outPara.Value;
        }
    }
}
