using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PayPalListen.Models;


namespace PayPalListen.Data
{
    public class SP_AspNetUsers
    {
        public static long? GetNo_FromEmail(ApplicationDbContext dbContext,
            string email)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@No",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE UnikktleWeb.dbo.spAspNetUsers_GetNo_FromEmail @Email, @No OUTPUT",
                new SqlParameter("@Email", email),
                outPara);

            if (outPara.Value == DBNull.Value)
            {
                return null;
            }

            return (long)outPara.Value;
        }
    }
}
