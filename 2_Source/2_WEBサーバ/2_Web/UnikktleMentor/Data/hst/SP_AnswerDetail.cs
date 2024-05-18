using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Models;
using UnikktleMentorEngine;

namespace UnikktleMentor.Data
{
    public static class SP_AnswerDetail
    {
        public static List<AnswerDetail> Select(ApplicationDbContext dbContext,
            long UserNo, int AnswerId)
        {
            return dbContext.AnswerDetailSelect
                .FromSqlRaw("EXECUTE hst.spAnswerDetail_Select @UserNo, @AnswerId",
                new SqlParameter("@UserNo", UserNo),
                new SqlParameter("@AnswerId", AnswerId))
                .ToList();
        }
    }

}
