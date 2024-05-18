using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using UnikktleMentor.Common;
using UnikktleMentor.Models;
using System.Data.Common;

namespace UnikktleMentor.Data
{
    public static class BulkCopy_Answer
    {
        public static void Flush(DbConnection dbConnection, long userNo, int answerId, List<InputModel_AnswerEdit> list)
        {
            var Data = new DataTable("hst.tAnswer");
            Data.Columns.Add(new DataColumn("UserNo", typeof(long)));
            Data.Columns.Add(new DataColumn("AnswerId", typeof(int)));
            Data.Columns.Add(new DataColumn("AnswerNo", typeof(byte)));
            Data.Columns.Add(new DataColumn("Answer", typeof(byte)));

            var answerNo = 0;
            foreach (var item in list)
            {
                Data.Rows.Add(userNo, answerId, answerNo++, item.A0);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A1);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A2);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A3);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A4);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A5);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A6);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A7);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A8);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A9);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A10);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A11);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A12);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A13);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A14);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A15);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A16);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A17);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A18);
                Data.Rows.Add(userNo, answerId, answerNo++, item.A19);
            }

            var sqlConnection = dbConnection as SqlConnection;
            sqlConnection.Open();

            using (var bulkcopy = new SqlBulkCopy(sqlConnection))
            {
                bulkcopy.DestinationTableName = "hst.tAnswerDetail";
                bulkcopy.BulkCopyTimeout = DBConst.CommandTimeout_5m;
                bulkcopy.WriteToServer(Data);
            }

            Data.Clear();
        }
    }
}
