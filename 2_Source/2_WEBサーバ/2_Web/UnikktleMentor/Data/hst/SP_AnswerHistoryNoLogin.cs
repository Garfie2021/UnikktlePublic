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
    public static class SP_AnswerHistoryNoLogin
    {
        public static List<AnswerHistoryNoLogin> Select(ApplicationDbContext dbContext,
            string SessionId)
        {
            return dbContext.AnswerHistoryNoLogin
                .FromSqlRaw("EXECUTE hst.spAnswerHistoryNoLogin_Select @SessionId",
                new SqlParameter("@SessionId", SessionId))
                .ToList();
        }

        public static void InsertUpdate(ApplicationDbContext dbContext,
            string SessionId, DateTime answerNewStart, DateTime answerNewEnd)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE hst.spAnswerHistoryNoLogin_InsertUpdate @SessionId, @AnswerNewStart, @AnswerNewEnd",
                new SqlParameter("@SessionId", SessionId),
                new SqlParameter("@AnswerNewStart", answerNewStart),
                new SqlParameter("@AnswerNewEnd", answerNewEnd));
        }

    }

}
