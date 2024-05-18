using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unikktle.Models;

namespace Unikktle.Data
{
    // SQLインジェクション対策済み
    public static class SP_TextWidth
    {
        public static short GetTextWidth(ApplicationDbContext dbContext, 
            string word)
        {
            var outPara = new SqlParameter()
            {
                ParameterName = "@Width",
                SqlDbType = SqlDbType.SmallInt,
                Direction = ParameterDirection.Output
            };

            // 動的SQLによるパフォーマンス低下、SQLインジェクション対策への対処として、
            // スペース区切りの絞り込み検索は、5ワードまでの固定で対応する。
            dbContext.Database
                .ExecuteSqlRaw("EXECUTE clt.spGetTextWidth @Word, @Width OUTPUT",
                new SqlParameter("Word", word),
                outPara);

            return (short)outPara.Value;
        }


    }
}
