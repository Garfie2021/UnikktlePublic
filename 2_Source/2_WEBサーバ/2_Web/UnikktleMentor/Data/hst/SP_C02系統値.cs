using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using UnikktleMentor.Models;
using UnikktleMentorEngine;

namespace UnikktleMentor.Data
{
    public static class SP_C02系統値
    {
        public static void Insert(ApplicationDbContext dbContext,
            long userNo, int answerId, C02系統値計算_結果 c02結果)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE hst.spC02系統値_Insert @UserNo, @AnswerId, @D, @C, @I, @N, @O, @Co, @Ag, @G, @R, @T, @A, @S",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@AnswerId", answerId),
                new SqlParameter("@D", c02結果.標準点D_抑うつ性),
                new SqlParameter("@C", c02結果.標準点C_気分の変化),
                new SqlParameter("@I", c02結果.標準点I_劣等感),
                new SqlParameter("@N", c02結果.標準点N_神経質),
                new SqlParameter("@O", c02結果.標準点O_主観性),
                new SqlParameter("@Co", c02結果.標準点Co_協調性),
                new SqlParameter("@Ag", c02結果.標準点Ag_攻撃性),
                new SqlParameter("@G", c02結果.標準点G_活動性),
                new SqlParameter("@R", c02結果.標準点R_のん気),
                new SqlParameter("@T", c02結果.標準点T_思考性),
                new SqlParameter("@A", c02結果.標準点A_支配性),
                new SqlParameter("@S", c02結果.標準点S_社会性));
        }

        public static List<C02系統値> Select(ApplicationDbContext dbContext,
            long UserNo, int AnswerId)
        {
            return dbContext.C02系統値Select
                .FromSqlRaw("EXECUTE hst.spC02系統値_Select @UserNo, @AnswerId",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@AnswerId", AnswerId))
                .ToList();
        }

        public static List<C02系統値> Select_AllDate(ApplicationDbContext dbContext,
            long UserNo)
        {
            return dbContext.C02系統値Select
                .FromSqlRaw("EXECUTE hst.spC02系統値_Select_AllDate @UserNo",
                new SqlParameter("@UserNo", UserNo))
                .ToList();
        }

        public static List<C02系統値_TeamUser> SelectTeamUserAll(ApplicationDbContext dbContext,
            int teamNo)
        {
            return dbContext.C02系統値_TeamUser_Select
                .FromSqlRaw("EXECUTE hst.spC02系統値_SelectTeamUserAll @TeamNo",
                new SqlParameter("@TeamNo", teamNo))
                .ToList();
        }
    }

}
