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
    public static class SP_Attribute
    {
        public static List<Models.DataModel.Attribute> SelectClass_JA(ApplicationDbContext dbContext,
            AttributeClass Class)
        {
            return dbContext.Attribute
                .FromSqlRaw("EXECUTE mst.spAttribute_SelectClass_JA @Class", new SqlParameter("@Class", Class))
                .ToList();
        }

        public static List<Models.DataModel.Attribute> SelectClass_EN(ApplicationDbContext dbContext,
            AttributeClass Class)
        {
            return dbContext.Attribute
                .FromSqlRaw("EXECUTE mst.spAttribute_SelectClass_EN @Class", new SqlParameter("@Class", Class))
                .ToList();
        }
    }
}
