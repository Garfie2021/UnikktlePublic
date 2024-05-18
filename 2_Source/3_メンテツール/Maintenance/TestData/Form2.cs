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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void BtnBCP版_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.ShowDialog();
        }

        private void Btnエクスポート_Click(object sender, EventArgs e)
        {
            BcpProcess.Exec(txtBcpExportArgument_Clt_CollaborateKeyword.Text);

            txt実行ログ.Text = ログ.LogBuffer;

            MessageBox.Show("完了");
        }

        private void Btnインポート_clt_tCollaborateKeyword_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cnWeb = new SqlConnection())
                {
                    cnWeb.ConnectionString = txtConnectionString_Import.Text;
                    cnWeb.Open();

                    SQL_CollaborateKeyword.Truncate(cnWeb);
                }

                BcpProcess.Exec(txtBcpImportArgument_Clt_CollaborateKeyword.Text);

                txt実行ログ.Text = ログ.LogBuffer;

                MessageBox.Show("完了");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btnインポート_clt_tKeyword_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cnWebImport = new SqlConnection())
                using (var cnWebExport = new SqlConnection())
                {
                    cnWebImport.ConnectionString = txtConnectionString_Import.Text;
                    cnWebImport.Open();

                    cnWebExport.ConnectionString = txtConnectionString_Export.Text;
                    cnWebExport.Open();

                    SQL_Keyword.Truncate(cnWebImport);

                    int cnt = 0;
                    long rowCnt = 0;
                    using (var reader = SP_CollaborateKeyword.SelectDistinct_KeywordNo(cnWebImport))
                    {
                        while (reader.Read() == true)
                        {
                            var row = SP_Keyword.SelectNo_FullColumn(cnWebExport, (long)reader[0]);

                            BulkCopy_Keyword.Add(row.No, row.r_w, row.Word, row.FullTextSupple);

                            cnt++;
                            rowCnt++;

                            //if (cnt < 10000)
                            //{
                            //    continue;
                            //}
                            //else
                            //{
                            //    ログ.Add($"SiteMap row count. rowCnt:{rowCnt} cnt:{cnt}");

                            //    cnt = 0;
                            //}
                        }
                    }

                    if (BulkCopy_Keyword.Data.Rows.Count > 0)
                    {
                        BulkCopy_Keyword.Flush(txtConnectionString_Import.Text);
                    }

                    SQL_Keyword.FulltextCatalog_Rebuild(cnWebImport);
                }

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
