using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace UnikktleCommon
{
    public static class ExceptionSt
    {
        public static void ExceptionCommon(Exception ex)
        {
#if DEBUG
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
#endif
            Console.WriteLine("Exception!!!");
            Console.WriteLine(ex.Message);
        }

    }
}
