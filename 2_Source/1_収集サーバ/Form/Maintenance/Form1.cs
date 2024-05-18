using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 定数;
using DB;
using Logging;
using AppDirectory;
using Common;


namespace Maintenance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn除外ワード登録_Click(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                cn.Open();

                ExcludeWord.Exec(cn);
            }

            MessageBox.Show("完了");
        }

        private void btnWEB用にSQLiteへExport_Click(object sender, EventArgs e)
        {

        }
    }
}
