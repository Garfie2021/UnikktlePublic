//using System;
//using System.Collections.Generic;
//using System.Data;
//
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using UnikktleMentor.Models;
//using UnikktleMentorEngine;

//namespace UnikktleMentor.Data
//{
//    public static class SP_C01粗点
//    {
//        public static void Insert(ApplicationDbContext dbContext,
//            long userNo, int answerId, C01粗点計算_結果 c01結果)
//        {
//            dbContext.Database
//                .ExecuteSqlRaw("EXECUTE hst.spC01粗点_Insert @UserNo, @AnswerId, @S, @A, @T, @R, @G, @Ag, @Co, @O, @N, @I, @C, @D",
//                new SqlParameter("@UserNo", userNo),
//                new SqlParameter("@AnswerId", answerId),
//                new SqlParameter("@S", c01結果.@粗点S),
//                new SqlParameter("@A", c01結果.@粗点A),
//                new SqlParameter("@T", c01結果.@粗点T),
//                new SqlParameter("@R", c01結果.@粗点R),
//                new SqlParameter("@G", c01結果.@粗点G),
//                new SqlParameter("@Ag", c01結果.@粗点Ag),
//                new SqlParameter("@Co", c01結果.@粗点Co),
//                new SqlParameter("@O", c01結果.@粗点O),
//                new SqlParameter("@N", c01結果.@粗点N),
//                new SqlParameter("@I", c01結果.@粗点I),
//                new SqlParameter("@C", c01結果.@粗点C),
//                new SqlParameter("@D", c01結果.@粗点D));
//        }

//    }

//}
