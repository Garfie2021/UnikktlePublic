using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unikktle.Common;
using Unikktle.Models;

namespace Unikktle.Data
{
    public static class SP_Attribute
    {
        public static List<Models.DataModel.Attribute> SelectClass(ApplicationDbContext dbContext,
            AttributeClass Class)
        {
            return dbContext.Attribute
                .FromSqlRaw("EXECUTE mst.spAttribute_SelectClass @Class", new SqlParameter("@Class", Class))
                .ToList();
        }
    }
}
