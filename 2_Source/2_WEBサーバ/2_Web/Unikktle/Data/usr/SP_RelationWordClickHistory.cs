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
    public static class SP_RelationWordClickHistory
    {
        public static void Insert(ApplicationDbContext dbContext,
            long WordNo, long ClickUserNo, string ClickUserIP)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE [usr].[spRelationWordClickHistory_Insert] @WordNo, @ClickUserNo, @ClickUserIP",
                new SqlParameter("@WordNo", WordNo),
                new SqlParameter("@ClickUserNo", ClickUserNo),
                new SqlParameter("@ClickUserIP", ClickUserIP));
        }
    }
}
