using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppDirectory;

namespace Maintenance
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppSetting.ConnectionString_UnikktleCollect = "Data Source=xxx;Initial Catalog=UnikktleCollect;User ID=xxx;Password=xxx";

            Application.Run(new Form1());
        }
    }
}
