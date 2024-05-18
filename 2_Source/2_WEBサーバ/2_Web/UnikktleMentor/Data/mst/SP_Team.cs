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
    public static class SP_Team
    {
        public static List<Team> Select(ApplicationDbContext dbContext, int teamNo)
        {
            return dbContext.TeamSelect
                .FromSqlRaw("EXECUTE mst.spTeam_Select @TeamNo",
                new SqlParameter("@TeamNo", teamNo)
                ).ToList();
        }

        public static List<Team> Select_TeamID(ApplicationDbContext dbContext, string teamID)
        {
            return dbContext.TeamSelect
                .FromSqlRaw("EXECUTE mst.spTeam_Select_TeamID @TeamID",
                new SqlParameter("@TeamID", teamID)
                ).ToList();
        }
    
        public static List<Team> Select_CreatedTeam(ApplicationDbContext dbContext, long createUserNo)
        {
            return dbContext.TeamSelect
                .FromSqlRaw("EXECUTE mst.spTeam_Select_CreatedTeam @CreateUserNo",
                new SqlParameter("@CreateUserNo", createUserNo)
                ).ToList();
        }

        public static bool ChkTeamOwner(ApplicationDbContext dbContext, 
            long UserNo, int teamNo)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Cnt",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spTeam_ChkTeamOwner @UserNo, @TeamNo, @Cnt OUTPUT",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@TeamNo", teamNo),
                outPara);

            var cnt = (int)outPara.Value;

            if (cnt > 0)
            {
                // 一致するTeamが有る。
                return true;
            }
            else
            {
                // 一致するTeamが無い。
                return false;
            }
        }

        public static bool SelectAllowApplyToJoinTeam(ApplicationDbContext dbContext,
            string teamID)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@AllowApplyToJoinTeam",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spTeam_SelectAllowApplyToJoinTeam @TeamID, @AllowApplyToJoinTeam OUTPUT",
                new SqlParameter("@TeamID", teamID),
                outPara);

            return (bool)outPara.Value;
        }

        public static string Select_CreateUserMailAddress(ApplicationDbContext dbContext,
            string teamID)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Size = 450,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spTeam_Select_CreateUserMailAddress @TeamID, @Email OUTPUT",
                new SqlParameter("@TeamID", teamID),
                outPara);

            return (string)outPara.Value;
        }

        public static int SelectCount(ApplicationDbContext dbContext, string teamID, out int? teamNo)
        {
            var outParaCnt = new SqlParameter()
            {
                ParameterName = "@Cnt",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            var outParaTeamNo = new SqlParameter()
            {
                ParameterName = "@teamNo",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spTeam_SelectCount @TeamID, @Cnt OUTPUT, @TeamNo OUTPUT",
                new SqlParameter("@TeamID", teamID),
                outParaCnt, outParaTeamNo);

            var cnt = (int)outParaCnt.Value;

            if (cnt > 0)
            {
                teamNo = (int)outParaTeamNo.Value;
            }
            else
            {
                teamNo = null;
            }

            return (int)outParaCnt.Value;
        }

        public static int SelectCount_CreateUserNo(ApplicationDbContext dbContext, long userNo)
        {
            var outParaCnt = new SqlParameter()
            {
                ParameterName = "@Cnt",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spTeam_SelectCount_CreateUserNo @CreateUserNo, @Cnt OUTPUT",
                new SqlParameter("@CreateUserNo", userNo),
                outParaCnt);

            return (int)outParaCnt.Value;
        }

        // Todo：実装
        public static bool IsExistence(ApplicationDbContext dbContext, InputModel_TeamEdit input)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Cnt",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            if (input.TeamNo == null)
            {
                // 新規登録の場合は、全体で重複チェックする。
                dbContext.Database
                    .ExecuteSqlRaw("EXECUTE mst.spTeam_IsExistence @TeamID, @Cnt OUTPUT",
                    new SqlParameter("@TeamID", input.TeamID),
                    outPara);
            }
            else
            {
                // 新規登録の場合は、全体で重複チェックする。
                dbContext.Database
                    .ExecuteSqlRaw("EXECUTE mst.spTeam_IsExistence_ExcludedNo @TeamID, @TeamNo, @Cnt OUTPUT",
                    new SqlParameter("@TeamID", input.TeamID),
                    new SqlParameter("@TeamNo", input.TeamNo),
                    outPara);
            }

            var cnt = (int)outPara.Value;

            if (cnt > 0)
            {
                // 既に使われてる
                return true;
            }
            else
            {
                // 使われてない
                return false;
            }
        }

        public static int Insert(ApplicationDbContext dbContext,
             long createUserNo, InputModel_TeamEdit input)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@TeamNo",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spTeam_Insert @TeamID, @TeamExplanation, @AllowPublic, @AllowApplyToJoinTeam, @CreateUserNo, @TeamNo OUTPUT ",
                new SqlParameter("@TeamID", input.TeamID),
                new SqlParameter("@TeamExplanation", ControlCommon.NullToEmptyString(input.TeamExplanation)),
                new SqlParameter("@AllowPublic", input.AllowPublic),
                new SqlParameter("@AllowApplyToJoinTeam", input.AllowApplyToJoinTeam),
                new SqlParameter("@CreateUserNo", createUserNo),
                outPara);

            return (int)outPara.Value;
        }

        public static void Update(ApplicationDbContext dbContext,
             long updateUserNo, int teamNo, InputModel_TeamEdit input)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spTeam_Update @TeamNo, @TeamID, @TeamExplanation, @AllowPublic, @AllowApplyToJoinTeam, @UpdateUserNo",
                new SqlParameter("@TeamNo", teamNo),
                new SqlParameter("@TeamID", input.TeamID),
                new SqlParameter("@TeamExplanation", ControlCommon.NullToEmptyString(input.TeamExplanation)),
                new SqlParameter("@AllowPublic", input.AllowPublic),
                new SqlParameter("@AllowApplyToJoinTeam", input.AllowApplyToJoinTeam),
                new SqlParameter("@UpdateUserNo", updateUserNo)
                );
        }

        public static void Delete(ApplicationDbContext dbContext,
             int teamNo)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE mst.spTeam_Delete @TeamNo",
                new SqlParameter("@TeamNo", teamNo));
        }
    }
}
