namespace TestData
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btnインポート_clt_tCollaborateKeyword = new System.Windows.Forms.Button();
            this.btnエクスポート_clt_tKeyword = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt実行ログ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBCP版 = new System.Windows.Forms.Button();
            this.txtConnectionString_Import = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnインポート_clt_tKeyword = new System.Windows.Forms.Button();
            this.txtConnectionString_Export = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBcpExportArgument_Clt_CollaborateKeyword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBcpImportArgument_Clt_CollaborateKeyword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnインポート_clt_tCollaborateKeyword
            // 
            this.btnインポート_clt_tCollaborateKeyword.ForeColor = System.Drawing.Color.Black;
            this.btnインポート_clt_tCollaborateKeyword.Location = new System.Drawing.Point(200, 73);
            this.btnインポート_clt_tCollaborateKeyword.Name = "btnインポート_clt_tCollaborateKeyword";
            this.btnインポート_clt_tCollaborateKeyword.Size = new System.Drawing.Size(163, 31);
            this.btnインポート_clt_tCollaborateKeyword.TabIndex = 5;
            this.btnインポート_clt_tCollaborateKeyword.Text = "インポート";
            this.btnインポート_clt_tCollaborateKeyword.UseVisualStyleBackColor = true;
            this.btnインポート_clt_tCollaborateKeyword.Click += new System.EventHandler(this.Btnインポート_clt_tCollaborateKeyword_Click);
            // 
            // btnエクスポート_clt_tKeyword
            // 
            this.btnエクスポート_clt_tKeyword.ForeColor = System.Drawing.Color.Black;
            this.btnエクスポート_clt_tKeyword.Location = new System.Drawing.Point(25, 73);
            this.btnエクスポート_clt_tKeyword.Name = "btnエクスポート_clt_tKeyword";
            this.btnエクスポート_clt_tKeyword.Size = new System.Drawing.Size(163, 31);
            this.btnエクスポート_clt_tKeyword.TabIndex = 16;
            this.btnエクスポート_clt_tKeyword.Text = "エクスポート";
            this.btnエクスポート_clt_tKeyword.UseVisualStyleBackColor = true;
            this.btnエクスポート_clt_tKeyword.Click += new System.EventHandler(this.Btnエクスポート_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnインポート_clt_tKeyword);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(15, 244);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(871, 100);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2. clt.tKeyword (BCP)";
            // 
            // txt実行ログ
            // 
            this.txt実行ログ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt実行ログ.BackColor = System.Drawing.Color.Black;
            this.txt実行ログ.ForeColor = System.Drawing.Color.White;
            this.txt実行ログ.Location = new System.Drawing.Point(15, 394);
            this.txt実行ログ.Multiline = true;
            this.txt実行ログ.Name = "txt実行ログ";
            this.txt実行ログ.Size = new System.Drawing.Size(871, 105);
            this.txt実行ログ.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(14, 369);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "実行ログ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtBcpImportArgument_Clt_CollaborateKeyword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtBcpExportArgument_Clt_CollaborateKeyword);
            this.groupBox1.Controls.Add(this.btnエクスポート_clt_tKeyword);
            this.groupBox1.Controls.Add(this.btnインポート_clt_tCollaborateKeyword);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 125);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(872, 113);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. clt.tCollaborateKeyword (Bulk Insert)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "Webの最小限テストができるcollaborate/Keywordテストデータをインポートする。";
            // 
            // btnBCP版
            // 
            this.btnBCP版.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBCP版.Location = new System.Drawing.Point(814, 12);
            this.btnBCP版.Name = "btnBCP版";
            this.btnBCP版.Size = new System.Drawing.Size(75, 23);
            this.btnBCP版.TabIndex = 21;
            this.btnBCP版.Text = "BCP版";
            this.btnBCP版.UseVisualStyleBackColor = true;
            this.btnBCP版.Click += new System.EventHandler(this.BtnBCP版_Click);
            // 
            // txtConnectionString_Import
            // 
            this.txtConnectionString_Import.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString_Import.Location = new System.Drawing.Point(187, 91);
            this.txtConnectionString_Import.Name = "txtConnectionString_Import";
            this.txtConnectionString_Import.Size = new System.Drawing.Size(663, 19);
            this.txtConnectionString_Import.TabIndex = 23;
            this.txtConnectionString_Import.Text = "Data Source=localhost;Initial Catalog=UnikktleWeb;User ID=xxx;Password=xxx";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(13, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "Export先DB ConnectionString";
            // 
            // btnインポート_clt_tKeyword
            // 
            this.btnインポート_clt_tKeyword.ForeColor = System.Drawing.Color.Black;
            this.btnインポート_clt_tKeyword.Location = new System.Drawing.Point(22, 50);
            this.btnインポート_clt_tKeyword.Name = "btnインポート_clt_tKeyword";
            this.btnインポート_clt_tKeyword.Size = new System.Drawing.Size(163, 31);
            this.btnインポート_clt_tKeyword.TabIndex = 17;
            this.btnインポート_clt_tKeyword.Text = "インポート";
            this.btnインポート_clt_tKeyword.UseVisualStyleBackColor = true;
            this.btnインポート_clt_tKeyword.Click += new System.EventHandler(this.Btnインポート_clt_tKeyword_Click);
            // 
            // txtConnectionString_Export
            // 
            this.txtConnectionString_Export.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString_Export.Location = new System.Drawing.Point(187, 63);
            this.txtConnectionString_Export.Name = "txtConnectionString_Export";
            this.txtConnectionString_Export.Size = new System.Drawing.Size(663, 19);
            this.txtConnectionString_Export.TabIndex = 25;
            this.txtConnectionString_Export.Text = "Data Source=160.16.75.102,60002;Initial Catalog=UnikktleWeb;User ID=xxx;Password=xxx";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(13, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(156, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "Import先DB ConnectionString";
            // 
            // txtBcpExportArgument_Clt_CollaborateKeyword
            // 
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Location = new System.Drawing.Point(65, 19);
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Name = "txtBcpExportArgument_Clt_CollaborateKeyword";
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Size = new System.Drawing.Size(786, 19);
            this.txtBcpExportArgument_Clt_CollaborateKeyword.TabIndex = 7;
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Text = resources.GetString("txtBcpExportArgument_Clt_CollaborateKeyword.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "in";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(23, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "out";
            // 
            // txtBcpImportArgument_Clt_CollaborateKeyword
            // 
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Location = new System.Drawing.Point(65, 45);
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Name = "txtBcpImportArgument_Clt_CollaborateKeyword";
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Size = new System.Drawing.Size(786, 19);
            this.txtBcpImportArgument_Clt_CollaborateKeyword.TabIndex = 21;
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Text = "clt.tCollaborateKeyword in D:\\work\\5_Unikktle\\trunk\\2_Source\\3_メンテ\\Maintenance\\Te" +
    "stData\\Data\\clt.tCollaborateKeyword.txt -S localhost -U sa -P xxx -d Unikkt" +
    "leWeb -c -t \\\\t";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "Export先DB ConnectionString";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(180, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "=>";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(213, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(156, 12);
            this.label9.TabIndex = 25;
            this.label9.Text = "Import先DB ConnectionString";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(900, 527);
            this.Controls.Add(this.txtConnectionString_Export);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtConnectionString_Import);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBCP版);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txt実行ログ);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnインポート_clt_tCollaborateKeyword;
        private System.Windows.Forms.Button btnエクスポート_clt_tKeyword;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt実行ログ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBCP版;
        private System.Windows.Forms.Button btnインポート_clt_tKeyword;
        private System.Windows.Forms.TextBox txtConnectionString_Import;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtConnectionString_Export;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBcpExportArgument_Clt_CollaborateKeyword;
        private System.Windows.Forms.TextBox txtBcpImportArgument_Clt_CollaborateKeyword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
    }
}