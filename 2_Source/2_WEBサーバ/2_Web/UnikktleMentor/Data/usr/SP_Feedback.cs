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
    public static class SP_Feedback
    {
        public static List<Feedback> SelectUserNo(ApplicationDbContext dbContext,
            long UserNo)
        {
            return dbContext.Feedback
                .FromSqlRaw("EXECUTE usr.spFeedback_SelectUserNo @UserNo",
                new SqlParameter("@UserNo", UserNo))
                .ToList();
        }

        public static void Insert(ApplicationDbContext dbContext,
            long UserNo, FeedbackCategory Category, string Subject, string Text)
        {
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE usr.spFeedback_Insert @UserNo, @Category, @Subject, @Text",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@Category", Category),
                new SqlParameter("@Subject", Subject),
                new SqlParameter("@Text", Text)
                );
        }


    }
}
