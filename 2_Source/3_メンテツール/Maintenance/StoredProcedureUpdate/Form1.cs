using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maintenance
{
    public partial class Form1 : Form
    {
        //private List<ConnectionString> ConnectionStringList;

        //public class ConnectionString
        //{
        //    public string key;
        //    public string value;
        //}

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //ConnectionStringList.Add(new ConnectionString()
            //{
            //    key = "localhost/UnikktleCmn",
            //    value = "Data Source=localhost;Initial Catalog=UnikktleCmn;User ID=xxx;Password=xxx"
            //});

        }

        private void btn全更新実行_Click(object sender, EventArgs e)
        {
            try
            {
                ExecAllSpFuncGrant.Exec(
                    chkLocalhost.Checked,
                    chk192_168_11_5.Checked,
                    chk192_168_11_31.Checked,
                    chk160_16_75_102.Checked,
                    txtTrunkフォルダパス.Text);

                MessageBox.Show("完了");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"[Message] \r\n {ex.Message} \r\n\r\n" +
                    $"[ConnectionString] \r\n {ExecAllSpFuncGrant._ConnectionString} \r\n\r\n" +
                    $"[FilePath] \r\n {ExecAllSpFuncGrant._SqlFilePath} \r\n\r\n" +
                    $"[SQL] \r\n {ExecAllSpFuncGrant._Sql}");
            }
        }

    }
}
