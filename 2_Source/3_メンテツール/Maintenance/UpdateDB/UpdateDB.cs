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
using 定数;
using Common;


namespace UpdateDB
{
    public partial class UpdateDB : Form
    {
        public UpdateDB()
        {
            InitializeComponent();
        }

        private void btn実行_UpdateDomainNo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var cnCollect = new SqlConnection())
                {
                    cnCollect.ConnectionString = txtConnectionString.Text;
                    cnCollect.Open();

                    var urlRowList = DB.Collect.SP_Url.Select_DomainNoNull(cnCollect);

                    foreach (var row in urlRowList)
                    {
                        //var domainNo = DB.Collect.SP_Domain.GetWithInsert(cnCollect, (new Uri(row.URL)).Authority);
                        var domainNo = UrlParse.GetDomainNo(cnCollect, row.URL);

                        DB.Collect.SP_Url.Update_DomainNo(cnCollect, row.No, domainNo);
                    }
                }

                MessageBox.Show("完了");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
