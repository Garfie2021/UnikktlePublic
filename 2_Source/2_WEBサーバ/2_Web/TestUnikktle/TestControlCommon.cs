using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unikktle.Common;
using UnikktleCommon;

namespace TestUnikktle
{
    [TestClass]
    public class TestControlCommon
    {
        [TestMethod]
        public void TestMethod2()
        {
            try
            {
                //var Unikktile = "a";

                //var result = ControlCommonMind.UnikktileConvertToClass(Unikktile, out string error);

            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                //var Unikktile =
                //    "Rect|1|10|20|100|50\n" +
                //    "Line|2|20|50|50|100\n" +
                //    "Text|3|30|80|テキスト|\n" +
                //    "Link|4|40|120|リンク|";

                //var result = ControlCommonMind.UnikktileConvertToClass(Unikktile, out string error);

            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }
    }
}
