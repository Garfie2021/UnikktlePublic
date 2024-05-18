//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using DB;


//namespace DB
//{
//    public static class MeCabDB
//    {
//        public static void DB登録(SqlConnection cn, long strDF_ID, DateTime strTF_ID, List<string> goiList)
//        {
//            try
//            {
//                foreach (var goi in goiList)
//                {
//                    var goiNo = MorphologicalAnalysisDB.GetKeywordNo(goi);

//                    BulkCopy_CollectHistory.Add(strDF_ID, strTF_ID, goiNo);
//                }

//                BulkCopy_CollectHistory.Flush();
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }
//    }
//}
