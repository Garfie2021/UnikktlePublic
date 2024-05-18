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
    public static class SP_UserSetting
    {
        public static void Insert(ApplicationDbContext dbContext, 
            long No, byte Gender, DateTime BirthDate, int Career, string IPv4)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_Insert @No, @Gender, @BirthDate, @Career, @IPv4",
                new SqlParameter("@No", No),
                new SqlParameter("@Gender", Gender),
                new SqlParameter("@BirthDate", BirthDate),
                new SqlParameter("@Career", Career),
                new SqlParameter("@IPv4", IPv4)
                );
        }

        public static void Update(ApplicationDbContext dbContext,
            long No, BackgroundColor color, ExternalSearchEngine engine)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_Update @No, @BackgroundColor, @ExternalSearchEngine",
                new SqlParameter("@No", No),
                new SqlParameter("@BackgroundColor", color),
                new SqlParameter("@ExternalSearchEngine", engine)
                );
        }

        public static void UpdateProfile(ApplicationDbContext dbContext,
            long No, Gender Gender, DateTime BirthDate, int Career)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_UpdateProfile @No, @Gender, @BirthDate, @Career",
                new SqlParameter("@No", No),
                new SqlParameter("@Gender", Gender),
                new SqlParameter("@BirthDate", BirthDate),
                new SqlParameter("@Career", Career)
                );
        }

        public static void UpdateIPv4(ApplicationDbContext dbContext,
            long No, string IPv4)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_UpdateIPv4 @No, @IPv4",
                new SqlParameter("@No", No),
                new SqlParameter("@IPv4", IPv4)
                );
        }

        public static void UpdateOwnedCredit(ApplicationDbContext dbContext,
            long No, int Credit)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_UpdateOwnedCredit @No, @Credit",
                new SqlParameter("@No", No),
                new SqlParameter("@Credit", Credit)
                );
        }

        public static void UserSetting_UpdateGeolocation(ApplicationDbContext dbContext,
            long No, double Latitude, double Longitude)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_UpdateGeolocation @No, @Latitude, @Longitude",
                new SqlParameter("@No", No),
                new SqlParameter("@Latitude", Latitude),
                new SqlParameter("@Longitude", Longitude)
                );
        }

        public static List<UserSettingProfile> SelectProfile(ApplicationDbContext dbContext,
            long No)
        {
            return dbContext.UserSettingProfile
                .FromSqlRaw("EXECUTE usr.spUserSetting_SelectProfile @No",
                new SqlParameter("@No", No))
                .ToList();
        }

        public static List<UserSetting> Select(ApplicationDbContext dbContext, 
            long No)
        {
            return dbContext.UserSetting
                .FromSqlRaw("EXECUTE usr.spUserSetting_Select @No",
                new SqlParameter("@No", No))
                .ToList();
        }

        public static int GetOwnedCredit(ApplicationDbContext dbContext,
            long No)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@OwnedCredit",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_GetOwnedCredit @No, @OwnedCredit OUTPUT",
                new SqlParameter("@No", No),
                outPara);

            return (int)outPara.Value;
        }

    }
}
