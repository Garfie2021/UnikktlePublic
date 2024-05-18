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
    public static class SP_AdverSearchWord
    {
        public static List<AdverWordSearch> Select(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int AdverNo)
        {
            return dbContext.AdverWordSearch
                .FromSqlRaw("EXECUTE usr.spAdverSearchWord_Select @UserNo, @BusinessNo, @AdverNo",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@AdverNo", AdverNo)
                ).ToList();
        }

        // 競合する単価(CompetingUnitPrices)
        public static List<UnitPrice> SelectCompetingUnitPrices(ApplicationDbContext dbContext,
            string Word)
        {
            return dbContext.UnitPrice
                .FromSqlRaw("EXECUTE usr.spAdverSearchWord_SelectCompetingUnitPrices @Word",
                new SqlParameter("@Word", Word)
                ).ToList();
        }

        public static List<AdverSelectPrSearch> SelectPR(ApplicationDbContext dbContext,
            string Word, long ClickUserNo, string ClickUserIP, int rowNum)
        {
            return dbContext.AdverSelectPrSearch
                .FromSqlRaw("EXECUTE usr.spAdverSearchWord_SelectPR @Word, @ClickUserNo, @ClickUserIP, @RowNum",
                new SqlParameter("@Word", Word),
                new SqlParameter("@ClickUserNo", ClickUserNo),
                new SqlParameter("@ClickUserIP", ClickUserIP),
                new SqlParameter("@RowNum", rowNum)
                ).ToList();
        }

        public static void InsertUpdate(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int AdverNo, long SearchWordNo, short ClickCost)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spAdverSearchWord_InsertUpdate @UserNo, @BusinessNo, @AdverNo, @SearchWordNo, @ClickCost",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@AdverNo", AdverNo),
                new SqlParameter("@SearchWordNo", SearchWordNo),
                new SqlParameter("@ClickCost", ClickCost));
        }

        public static void Delete(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int AdverNo, long SearchWordNo)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spAdverSearchWord_Delete @UserNo, @BusinessNo, @AdverNo, @SearchWordNo",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@AdverNo", AdverNo),
                new SqlParameter("@SearchWordNo", SearchWordNo));
        }
    }
}
