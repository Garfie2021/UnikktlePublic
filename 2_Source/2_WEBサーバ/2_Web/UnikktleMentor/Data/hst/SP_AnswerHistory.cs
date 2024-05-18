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
    public static class SP_AnswerHistory
    {
        public static List<AnswerHistory> Select(ApplicationDbContext dbContext,
            long UserNo)
        {
            return dbContext.AnswerHistory
                .FromSqlRaw("EXECUTE hst.spAnswerHistory_Select @UserNo",
                new SqlParameter("@UserNo", UserNo))
                .ToList();
        }

        public static List<AnswerHistory> Select_Desc(ApplicationDbContext dbContext,
            long UserNo)
        {
            return dbContext.AnswerHistory
                .FromSqlRaw("EXECUTE hst.spAnswerHistory_Select_Desc @UserNo",
                new SqlParameter("@UserNo", UserNo))
                .ToList();
        }

        public static List<AnswerHistory> Select_Desc_Top1(ApplicationDbContext dbContext,
            long UserNo)
        {
            return dbContext.AnswerHistory
                .FromSqlRaw("EXECUTE hst.spAnswerHistory_Select_Desc_Top1 @UserNo",
                new SqlParameter("@UserNo", UserNo))
                .ToList();
        }

        public static int Insert(ApplicationDbContext dbContext,
            long UserNo, DateTime answerNewStart, DateTime answerNewEnd)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@AnswerId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            dbContext.Database
                .ExecuteSqlRaw("EXECUTE hst.spAnswerHistory_Insert @UserNo, @AnswerNewStart, @AnswerNewEnd, @AnswerId OUTPUT",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@AnswerNewStart", answerNewStart),
                new SqlParameter("@AnswerNewEnd", answerNewEnd),
                outPara);

            return (int)outPara.Value;

        }

    }

}
