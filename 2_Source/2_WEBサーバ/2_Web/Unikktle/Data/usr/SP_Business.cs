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
    public static class SP_Business
    {
        public static List<BusinessSelect> Select(ApplicationDbContext dbContext,
            long UserNo)
        {
            return dbContext.BusinessSelect
                .FromSqlRaw("EXECUTE usr.spBusiness_Select @UserNo",
                new SqlParameter("@UserNo", UserNo))
                .ToList();
        }

        public static Business SelectOne(ApplicationDbContext dbContext,
            long UserNo, short No)
        {
            return dbContext.Business
                .FromSqlRaw("EXECUTE usr.spBusiness_SelectOne @UserNo,  @No",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@No", No))
                .ToList()[0];
        }

        public static void Insert(ApplicationDbContext dbContext,
            long UserNo, BusinessCategory Category, string OrganizationName, 
            string OrganizationURL)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spBusiness_Insert @UserNo, @Category, @OrganizationName, @OrganizationURL",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@Category", Category),
                new SqlParameter("@OrganizationName", OrganizationName),
                new SqlParameter("@OrganizationURL", OrganizationURL)
                );
        }

        public static void Update(ApplicationDbContext dbContext,
            long UserNo, short No, BusinessCategory Category, string OrganizationName,
            string OrganizationURL)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spBusiness_Update @UserNo, @No, @Category, @OrganizationName, @OrganizationURL",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@No", No),
                new SqlParameter("@Category", Category),
                new SqlParameter("@OrganizationName", OrganizationName),
                new SqlParameter("@OrganizationURL", OrganizationURL)
                );
        }

        public static void Delete(ApplicationDbContext dbContext,
            long UserNo, short No)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spBusiness_Delete @UserNo, @No",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@No", No)
                );
        }

    }
}
