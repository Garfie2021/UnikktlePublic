using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using UnikktleMentorEngine;
using UnikktleMentor.Models;
using UnikktleMentor.Common;

namespace UnikktleMentor.Data
{
    public static class SP_AnswerDetailSupplement
    {
        public static void Insert(ApplicationDbContext dbContext,
            long userNo, int AnswerId, InputModel_Answer_Supplement answerSupplement)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE hst.spAnswerDetailSupplement_Insert @UserNo, @AnswerId, @Gender, @Career, @RecentHappenings",
                new SqlParameter("@UserNo", userNo),
                new SqlParameter("@AnswerId", AnswerId),
                new SqlParameter("@Gender", (byte)answerSupplement._Gender),
                new SqlParameter("@Career", answerSupplement.Career),
                new SqlParameter("@RecentHappenings", ControlCommon.NullToEmptyString(answerSupplement.RecentHappenings)));
        }

        public static List<AnswerDetailSupplement> Select(ApplicationDbContext dbContext,
           long UserNo, int AnswerId)
        {
            return dbContext.AnswerDetailSupplementSelect
                .FromSqlRaw("EXECUTE hst.spAnswerDetailSupplement_Select @UserNo, @AnswerId",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@AnswerId", AnswerId))
                .ToList();
        }
    }
}
