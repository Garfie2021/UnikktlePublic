using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unikktle.Models;
using Unikktle.Common;

namespace Unikktle.Data
{
    public static class SP_AdverSearchClickHistory
    {
        public static void Insert(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int AdverNo, long WordNo, 
            long ClickUserNo, string ClickUserIP,
            out string email)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.NVarChar,
                Size = 256,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE [usr].[spAdverSearchClickHistory_Insert] @UserNo, @BusinessNo, @AdverNo, @WordNo, @ClickUserNo, @ClickUserIP, @Email OUTPUT",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@AdverNo", AdverNo),
                new SqlParameter("@WordNo", WordNo),
                new SqlParameter("@ClickUserNo", ClickUserNo),
                new SqlParameter("@ClickUserIP", ClickUserIP),
                outPara);

            if (outPara.Value == DBNull.Value)
            {
                email = null;
            }
            else
            {
                email = (string)outPara.Value;
            }
        }
    }
}
