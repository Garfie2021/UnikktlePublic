using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Models;
using UnikktleMentor.Common;

namespace UnikktleMentor.Data
{
    public static class SP_JoinTeam
    {
        public static JoinTeamStatus? SelectStatus(ApplicationDbContext dbContext,
            long userNo, int teamNo)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Status",
                SqlDbType = SqlDbType.TinyInt,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spJoinTeam_SelectStatus @UserNo, @TeamNo, @Status OUTPUT",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@TeamNo", teamNo),
                outPara);

            if (outPara.Value == DBNull.Value)
            {
                return null;
            }

            return (JoinTeamStatus)outPara.Value;
        }

        public static int SelectCnt(ApplicationDbContext dbContext,
            long userNo, int teamNo)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Cnt",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spJoinTeam_SelectCnt @UserNo, @TeamNo, @Cnt OUTPUT",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@TeamNo", teamNo),
                outPara);

            return (int)outPara.Value;
        }

        public static List<Team_TeamID> Select(ApplicationDbContext dbContext,
            long userNo)
        {
            return dbContext.Team_TeamID
                .FromSqlRaw("EXECUTE usr.spJoinTeam_Select @UserNo",
                new SqlParameter("@UserNo", userNo))
                .ToList();
        }

        public static List<TeamUser> SelectUser(ApplicationDbContext dbContext,
            long teamNo)
        {
            return dbContext.TeamUser
                .FromSqlRaw("EXECUTE usr.spJoinTeam_SelectUser @TeamNo",
                new SqlParameter("@TeamNo", teamNo))
                .ToList();
        }

        public static void Insert(ApplicationDbContext dbContext,
            long userNo, int teamNo)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spJoinTeam_Insert @UserNo, @TeamNo, @Status",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@TeamNo", teamNo),
                new SqlParameter("@Status", JoinTeamStatus.チームオーナー));
        }

        public static void Insert_TeamID(ApplicationDbContext dbContext,
            long userNo, string teamID)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spJoinTeam_Insert_TeamID @UserNo, @TeamID",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@TeamID", teamID));
        }

        public static void Update_Status(ApplicationDbContext dbContext,
            long userNo, int teamNo, JoinTeamStatus status)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spJoinTeam_Update_Status @UserNo, @TeamNo, @Status",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@TeamNo", teamNo),
                new SqlParameter("@Status", status));
        }

        public static void Delete(ApplicationDbContext dbContext,
             int teamNo)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spJoinTeam_Delete @TeamNo",
                new SqlParameter("@TeamNo", teamNo));
        }

        public static void DeleteMember(ApplicationDbContext dbContext,
             int teamNo, long userNo)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spJoinTeam_DeleteMember @TeamNo, @UserNo",
                new SqlParameter("@TeamNo", teamNo),
                new SqlParameter("@UserNo", userNo));
        }
    }
}
