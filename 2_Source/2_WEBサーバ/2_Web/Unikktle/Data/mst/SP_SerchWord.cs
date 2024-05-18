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
    public static class SP_SearchWord
    {
        public static long SearchWord_GetNoWithInsert(ApplicationDbContext dbContext,
            string Word)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@No",
                SqlDbType = SqlDbType.BigInt,
                Direction = ParameterDirection.Output
            };

            var result = dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spSearchWord_GetNoWithInsert @Word, @No OUTPUT",
                new SqlParameter("@Word", Word),
                outPara);

            return (long)outPara.Value;
        }

    }
}
