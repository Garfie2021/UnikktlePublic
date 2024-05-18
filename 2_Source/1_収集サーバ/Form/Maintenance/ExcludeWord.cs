using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using 定数;
using Common;
using DB;
using Logging;

namespace Maintenance
{
    public static class ExcludeWord
    {
        public static void Exec(SqlConnection cn)
        {
            // 本当に不採用にして良いか分からない。統計的に下位になるなら不要。その場合、個別に除外しなくても除外される。
            DB.Collect.SP_Keyword.Update_採用(cn, ".", 採用不採用.不採用);
            DB.Collect.SP_Keyword.Update_採用(cn, ". ..", 採用不採用.不採用);
        }

    }
}
