using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btnエクスポート_Click(object sender, EventArgs e)
        {
            BcpProcess.Exec(txtBcpExportArgument_Clt_Keyword.Text);
            BcpProcess.Exec(txtBcpExportArgument_Clt_CollaborateKeyword.Text);

            txt実行ログ.Text = ログ.LogBuffer;

            MessageBox.Show("完了");
        }

        private void Btnインポート_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cnWeb = new SqlConnection())
                {
                    cnWeb.ConnectionString = txtConnectionString_Import.Text;
                    cnWeb.Open();

                    SQL_Keyword.Truncate(cnWeb);
                    SQL_CollaborateKeyword.Truncate(cnWeb);
                }

                BcpProcess.Exec(txtBcpImportArgument_Clt_Keyword.Text);
                BcpProcess.Exec(txtBcpImportArgument_Clt_CollaborateKeyword.Text);

                txt実行ログ.Text = ログ.LogBuffer;

                MessageBox.Show("完了");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
