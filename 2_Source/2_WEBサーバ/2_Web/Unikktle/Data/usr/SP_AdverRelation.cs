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
    public static class SP_AdverRelation
    {
        public static int Get無料Count_自分を除く(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int AdverNo)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Count",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spAdverRelation_Get無料Count_自分を除く @UserNo, @BusinessNo, @AdverNo, @Count OUTPUT",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@AdverNo", AdverNo),
                outPara);

            return (int)outPara.Value;
        }

        public static int Get無料Count(ApplicationDbContext dbContext,
            long UserNo)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Count",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spAdverRelation_Get無料Count @UserNo, @Count OUTPUT",
                new SqlParameter("@UserNo", UserNo),
                outPara);

            return (int)outPara.Value;
        }

        public static List<AdverRelationSelect> Select(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo)
        {
            return dbContext.AdverRelationSelect
                .FromSqlRaw("EXECUTE usr.spAdverRelation_Select @UserNo, @BusinessNo",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo))
                .ToList();
        }

        public static Adver SelectOne(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, long No)
        {
            return dbContext.Adver
                .FromSqlRaw("EXECUTE usr.spAdverRelation_SelectOne @UserNo, @BusinessNo, @No",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@No", No))
                .ToList()[0];
        }

        public static int Insert(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo,
            Valid Valid, AdverCategory Category, string AdverName, 
            string AdverTitle1, string AdverTitle2, string AdverURL, 
			int AdvertisingBudget)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@No",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spAdverRelation_Insert @UserNo, @BusinessNo, @Valid, @Category, @AdverName, @AdverTitle1, @AdverTitle2, @AdverURL, @AdvertisingBudget, @No OUTPUT",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@Valid", Valid),
                new SqlParameter("@Category", Category),
                new SqlParameter("@AdverName", AdverName),
                new SqlParameter("@AdverTitle1", AdverTitle1),
                new SqlParameter("@AdverTitle2", AdverTitle2),
                new SqlParameter("@AdverURL", AdverURL),
                new SqlParameter("@AdvertisingBudget", AdvertisingBudget),
                outPara);

            return (int)outPara.Value;
        }

        public static void Update(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int No,
            Valid Valid, AdverCategory Category, string AdverName, 
            string AdverTitle1, string AdverTitle2, string AdverURL, 
			int AdvertisingBudget)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spAdverRelation_Update @UserNo, @BusinessNo, @No, @Valid, @Category, @AdverName, @AdverTitle1, @AdverTitle2, @AdverURL, @AdvertisingBudget",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@No", No),
                new SqlParameter("@Valid", Valid),
                new SqlParameter("@Category", Category),
                new SqlParameter("@AdverName", AdverName),
                new SqlParameter("@AdverTitle1", AdverTitle1),
                new SqlParameter("@AdverTitle2", AdverTitle2),
                new SqlParameter("@AdverURL", AdverURL),
                new SqlParameter("@AdvertisingBudget", AdvertisingBudget)
                );
        }

        public static void Delete(ApplicationDbContext dbContext,
            long UserNo, short BusinessNo, int No)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spAdverRelation_Delete @UserNo, @BusinessNo, @No",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@BusinessNo", BusinessNo),
                new SqlParameter("@No", No)
                );
        }

    }
}
