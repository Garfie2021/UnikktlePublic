using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 定数;
using Logging;
using AppDirectory;
using Common;

namespace UnitTest_x64.Lib.Common
{
    [TestClass]
    public class UrlParseTest
    {
        // 解析できない、存在しないURLが収集されることがあるらしい。
        [TestMethod]
        public void TestMethod1()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = "Data Source=localhost;Initial Catalog=UnikktleCmn;User ID=xxx;Password=xxx";
                cn.Open();

                UrlParse.GetDomainNo(cn, "http://Official%20agency%20profile");
            }
        }
    }
}
