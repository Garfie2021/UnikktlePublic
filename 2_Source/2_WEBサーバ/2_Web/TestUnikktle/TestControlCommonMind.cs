using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikktleCommon;


namespace TestUnikktle
{
    [TestClass]
    public class TestControlCommonMind
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var _dbContext = UnitTestCommon.GetDbContext();

                //var Input = new InputModel_MindEdit
                //{
                //    Title = "Asp.Net Core Webシステム",
                //    Explanation = "2019年に構築した、Asp.Net Core ベースWebシステムの構成。",
                //    Item = "1|Webサイト||||\n" +
                //           "2|Webサーバー||||",
                //    ItemRelation = "2|>|1|"
                //};

                //ClassConvertMind.ClassConvert(
                //    _dbContext, Input.Item, Input.ItemRelation,
                //    out List<MindRow_Text> textList,
                //    out List<MindRow_Link> linkList,
                //    out List<MindRow_Rect> rectList,
                //    out List<MindRow_Line> lineList,
                //    out string error);

                //var a = textList.Count();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                var _dbContext = UnitTestCommon.GetDbContext();

                var Input = new InputModel_MindEdit
                {
                    Title = "Asp.Net Core Webシステム",
                    Explanation = "2019年に構築した、Asp.Net Core ベースWebシステムの構成。",
                    //Item = "1|Webサイト||||\n" +
                    //       "2|Webサーバー||||\n" +
                    //       "3|CentOS 7.7||||",
                    ItemRelation = "2|>|1|\n" +
                                   "3|>|2|"
                };

                //ClassConvertMind.ClassConvert(
                //    _dbContext, Input.Item, Input.ItemRelation,
                //    out List<MindRow_Text> textList,
                //    out List<MindRow_Link> linkList,
                //    out List<MindRow_Rect> rectList,
                //    out List<MindRow_Line> lineList,
                //    out string error);

                //var a = textList.Count();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            try
            {
                var _dbContext = UnitTestCommon.GetDbContext();

                var Input = new InputModel_MindEdit
                {
                    Title = "Asp.Net Core Webシステム",
                    Explanation = "2019年に構築した、Asp.Net Core ベースWebシステムの構成。",
                    //Item = "1|Webサイト||||\n" +
                    //       "2|Webサーバー||||\n" +
                    //       "3|CentOS 7.7||||\n" +
                    //       "4|.NET Core 2.2.7||||\n" +
                    //       "5|Nginx||||",
                    ItemRelation = "2|>|1|\n" +
                                   "3|>|2|\n" +
                                   "4|>|3|\n" +
                                   "5|>|3|"
                };

                //ClassConvertMind.ClassConvert(
                //    _dbContext, Input.Item, Input.ItemRelation,
                //    out List<MindRow_Text> textList,
                //    out List<MindRow_Link> linkList,
                //    out List<MindRow_Rect> rectList,
                //    out List<MindRow_Line> lineList,
                //    out string error);

                //var a = textList.Count();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }

        [TestMethod]
        public void TestMethod4()
        {
            try
            {
                var _dbContext = UnitTestCommon.GetDbContext();

                var Input = new InputModel_MindEdit
                {
                    Title = "Asp.Net Core Webシステム",
                    Explanation = "2019年に構築した、Asp.Net Core ベースWebシステムの構成。",
                    //Item = "1|Webサイト||||\n" +
                    //       "2|Webサーバー||||\n" +
                    //       "3|CentOS 7.7||||\n" +
                    //       "4|.NET Core 2.2.7||||\n" +
                    //       "5|Nginx||||\n" +
                    //       "100|クライアント||||\n" +
                    //       "101|Webブラウザ||||",
                    ItemRelation = "2|>|1|\n" +
                                   "3|>|2|\n" +
                                   "4|>|3|\n" +
                                   "5|>|3|\n" +
                                   "101|>|100|\n" +
                                   "101|-|5|"
                };

                //ClassConvertMind.ClassConvert(
                //    _dbContext, Input.Item, Input.ItemRelation,
                //    out List<MindRow_Text> textList,
                //    out List<MindRow_Link> linkList,
                //    out List<MindRow_Rect> rectList,
                //    out List<MindRow_Line> lineList,
                //    out string error);

                //var a = textList.Count();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            try
            {
                var _dbContext = UnitTestCommon.GetDbContext();

                var Input = new InputModel_MindEdit
                {
                    Title = "Asp.Net Core Webシステム",
                    Explanation = "2019年に構築した、Asp.Net Core ベースWebシステムの構成。",
                    //Item = "1|サーバーホスティングサービス||||\n" +
                    //        "2|Webサーバー||||\n" +
                    //        "3|CentOS 7.7||||\n" +
                    //        "4|.NET Core 2.2.7||||\n" +
                    //        "5|Nginx||||\n" +
                    //        "6|SSH||||\n" +
                    //        "100|クライアント||||\n" +
                    //        "101|Webブラウザ||||\n" +
                    //        "200|イントラ||||\n" +
                    //        "201|Windows||||\n" +
                    //        "202|PuTTY||||",
                    ItemRelation = "2|>|1|\n" +
                                    "3|>|2|\n" +
                                    "4|>|3|\n" +
                                    "5|>|3|\n" +
                                    "6|>|3|\n" +
                                    "101|>|100|\n" +
                                    "201|>|200|\n" +
                                    "202|>|201|\n" +
                                    "203|>|201|\n" +
                                    "101|-|5|\n" +
                                    "6|-|202|"
                };

                //ClassConvertMind.ClassConvert(
                //    _dbContext, Input.Item, Input.ItemRelation,
                //    out List<MindRow_Text> textList,
                //    out List<MindRow_Link> linkList,
                //    out List<MindRow_Rect> rectList,
                //    out List<MindRow_Line> lineList,
                //    out string error);

                //var a = textList.Count();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }
    }
}
