using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using UnikktleMentor.Common;
using UnikktleMentor.Models;

namespace UnikktleMentor.Data
{
    public static class SP_AspNetUsers
    {
        public static long GetNo(ApplicationDbContext dbContext, 
            string Id)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@No",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE dbo.spAspNetUsers_GetNo @Id, @No OUTPUT",
                new SqlParameter("@Id", Id),
                outPara);

            return (long)outPara.Value;
        }

        public static long GetNo_FromEmail(ApplicationDbContext dbContext,
            string email)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@No",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE dbo.spAspNetUsers_GetNo_FromEmail @Email, @No OUTPUT",
                new SqlParameter("@Email", email),
                outPara);

            return (long)outPara.Value;
        }

        public static List<AspNetUser> Select_FromUserName(ApplicationDbContext dbContext,
            string UserName)
        {
            return dbContext.AspNetUser
                .FromSqlRaw("EXECUTE dbo.spAspNetUsers_Select_FromUserName @UserName", new SqlParameter("@UserName", UserName))
                .ToList();
        }
    }
}
