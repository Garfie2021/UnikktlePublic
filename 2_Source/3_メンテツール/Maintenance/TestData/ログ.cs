using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestData
{
    public static class ログ
    {
        public static string LogBuffer;

        public static void Add(string log)
        {
            LogBuffer += log + "\r\n";
        }

        public static void Output(TextBox textBox, string log)
        {
            textBox.Text += log + "\r\n";
        }
    }
}
