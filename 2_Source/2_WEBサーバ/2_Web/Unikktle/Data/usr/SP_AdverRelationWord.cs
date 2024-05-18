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
    public static class SP_AdverRelationWord
    {
        public static List<AdverWordRelation> Select(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int AdverNo)
        {
            return dbContext.AdverWordRelation
                .FromSqlRaw("EXECUTE usr.spAdverRelationWord_Select @UserNo, @BusinessNo, @AdverNo",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@AdverNo", AdverNo)
                ).ToList();
        }

        // 競合する単価(CompetingUnitPrices)
        public static List<UnitPrice> SelectCompetingUnitPrices(ApplicationDbContext dbContext,
            long WordNo)
        {
            return dbContext.UnitPrice
                .FromSqlRaw("EXECUTE usr.spAdverRelationWord_SelectCompetingUnitPrices @WordNo",
                new SqlParameter("@WordNo", WordNo)
                ).ToList();
        }

        public static List<AdverSelectPrRelation> SelectPR(ApplicationDbContext dbContext,
            long WordNo, long ClickUserNo, string ClickUserIP, int rowNum)
        {
            return dbContext.AdverSelectPrRelation
                .FromSqlRaw("EXECUTE usr.spAdverRelationWord_SelectPR @WordNo, @ClickUserNo, @ClickUserIP, @RowNum",
                new SqlParameter("@WordNo", WordNo),
                new SqlParameter("@ClickUserNo", ClickUserNo),
                new SqlParameter("@ClickUserIP", ClickUserIP),
                new SqlParameter("@RowNum", rowNum)
                ).ToList();
        }

        public static void InsertUpdate(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int AdverNo, long RelationWordNo, short ClickCost)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spAdverRelationWord_InsertUpdate @UserNo, @BusinessNo, @AdverNo, @RelationWordNo, @ClickCost",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@AdverNo", AdverNo),
                new SqlParameter("@RelationWordNo", RelationWordNo),
                new SqlParameter("@ClickCost", ClickCost));
        }

        public static void Delete(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int AdverNo, long SearchWordNo)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spAdverRelationWord_Delete @UserNo, @BusinessNo, @AdverNo, @SearchWordNo",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@AdverNo", AdverNo),
                new SqlParameter("@SearchWordNo", SearchWordNo));
        }
    }
}
