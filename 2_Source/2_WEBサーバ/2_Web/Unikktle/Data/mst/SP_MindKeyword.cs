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
    public static class SP_MindKeyword
    {
        public static long GetInsert(ApplicationDbContext dbContext, string Word)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@No",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spMindKeyword_GetInsert @Word, @No OUTPUT",
                new SqlParameter("Word", Word),
                outPara);

            return (long)outPara.Value;
        }

    }
}
