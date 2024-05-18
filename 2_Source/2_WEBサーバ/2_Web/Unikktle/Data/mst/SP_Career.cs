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
    public static class SP_Career
    {
        public static List<Career> Select(ApplicationDbContext dbContext,
            int CareerCategoryNo)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "ja")
            {
                return dbContext.Career
                .FromSqlRaw("EXECUTE mst.spCareer_Select_JA @CareerCategoryNo",
                new SqlParameter("@CareerCategoryNo", CareerCategoryNo)
                ).ToList();
            }
            else
            {
                return dbContext.Career
                    .FromSqlRaw("EXECUTE mst.spCareer_Select @CareerCategoryNo",
                    new SqlParameter("@CareerCategoryNo", CareerCategoryNo)
                    ).ToList();
            }
        }
    }
}
