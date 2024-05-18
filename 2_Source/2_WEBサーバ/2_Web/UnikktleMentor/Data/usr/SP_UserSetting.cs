using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Models;
using UnikktleMentor.Common;
using UnikktleMentorEngine;


namespace UnikktleMentor.Data
{
    public static class SP_UserSetting
    {
        public static void Insert(ApplicationDbContext dbContext, 
            long No, string email, string nickname, Gender Gender, DateTime BirthDate, 
            int Career, string IPv4)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_Insert @No, @Email, @Nickname, @Gender, @BirthDate, @Career, @IPv4",
                new SqlParameter("@No", No),
                new SqlParameter("@Email", email),
                new SqlParameter("@Nickname", nickname),
                new SqlParameter("@Gender", Gender),
                new SqlParameter("@BirthDate", BirthDate),
                new SqlParameter("@Career", Career),
                new SqlParameter("@IPv4", IPv4)
                );
        }

        public static void UpdateProfile(ApplicationDbContext dbContext, long No,
            string email, string nickname, Gender Gender, DateTime BirthDate, int Career)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_UpdateProfile @No, @Email, @Nickname, @Gender, @BirthDate, @Career",
                new SqlParameter("@No", No),
                new SqlParameter("@Email", email),
                new SqlParameter("@Nickname", nickname),
                new SqlParameter("@Gender", Gender),
                new SqlParameter("@BirthDate", BirthDate),
                new SqlParameter("@Career", Career)
                );
        }

        public static void UpdateProfile2(ApplicationDbContext dbContext,
            long No, Gender Gender, int Career)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spUserSetting_UpdateProfile2 @No, @Gender, @Career",
                new SqlParameter("@No", No),
                new SqlParameter("@Gender", Gender),
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


        public static List<UserSetting> SelectProfile(ApplicationDbContext dbContext,
            long No)
        {
            return dbContext.UserSettingProfile
                .FromSqlRaw("EXECUTE usr.spUserSetting_SelectProfile @No",
                new SqlParameter("@No", No))
                .ToList();
        }

    }
}
