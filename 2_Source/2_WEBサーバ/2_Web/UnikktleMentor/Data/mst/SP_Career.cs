using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Common;
using UnikktleMentor.Models;

namespace UnikktleMentor.Data
{
    public static class SP_Career
    {
        public static List<Career> Select_JA(ApplicationDbContext dbContext,
            int CareerCategoryNo)
        {
            return dbContext.Career
                .FromSqlRaw("EXECUTE mst.spCareer_Select_JA @CareerCategoryNo",
                new SqlParameter("@CareerCategoryNo", CareerCategoryNo)
                ).ToList();
        }

        public static List<Career> Select_EN(ApplicationDbContext dbContext,
            int CareerCategoryNo)
        {
            return dbContext.Career
                .FromSqlRaw("EXECUTE mst.spCareer_Select @CareerCategoryNo",
                new SqlParameter("@CareerCategoryNo", CareerCategoryNo)
                ).ToList();
        }

        public static List<Career> SelectAll_JA(ApplicationDbContext dbContext)
        {
            return dbContext.Career.FromSqlRaw("EXECUTE mst.spCareer_SelectAll_JA").ToList();
        }

        public static List<Career> SelectAll_EN(ApplicationDbContext dbContext)
        {
            return dbContext.Career.FromSqlRaw("EXECUTE mst.spCareer_SelectAll").ToList();
        }
    }
}
