using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PayPalListen.Data
{
    public static class SP_PayPalIPNlog
    {
        public static long Insert(ApplicationDbContext dbContext, 
            DateTime RegisteredDate, string RequestBody)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@No",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE pay.spPayPalIPNlog_Insert @RegisteredDate, @RequestBody, @No OUT",
                new SqlParameter("@RegisteredDate", RegisteredDate),
                new SqlParameter("@RequestBody", RequestBody),
                outPara
                );

            return (long)outPara.Value;
        }
    }
}
