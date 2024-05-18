namespace TestData
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.btnインポート = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBcpImportArgument_Clt_Keyword = new System.Windows.Forms.TextBox();
            this.txtConnectionString_Import = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBcpImportArgument_Clt_CollaborateKeyword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt実行ログ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnエクスポート = new System.Windows.Forms.Button();
            this.txtBcpExportArgument_Clt_Keyword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBcpExportArgument_Clt_CollaborateKeyword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Webの最小限テストができるcollaborate/Keywordテストデータをインポートする。";
            // 
            // btnインポート
            // 
            this.btnインポート.ForeColor = System.Drawing.Color.Black;
            this.btnインポート.Location = new System.Drawing.Point(13, 107);
            this.btnインポート.Name = "btnインポート";
            this.btnインポート.Size = new System.Drawing.Size(163, 31);
            this.btnインポート.TabIndex = 5;
            this.btnインポート.Text = "インポート";
            this.btnインポート.UseVisualStyleBackColor = true;
            this.btnインポート.Click += new System.EventHandler(this.Btnインポート_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtBcpImportArgument_Clt_Keyword);
            this.groupBox1.Controls.Add(this.txtConnectionString_Import);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnインポート);
            this.groupBox1.Controls.Add(this.txtBcpImportArgument_Clt_CollaborateKeyword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(14, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(957, 153);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "テストデータ";
            // 
            // txtBcpImportArgument_Clt_Keyword
            // 
            this.txtBcpImportArgument_Clt_Keyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBcpImportArgument_Clt_Keyword.Location = new System.Drawing.Point(186, 44);
            this.txtBcpImportArgument_Clt_Keyword.Name = "txtBcpImportArgument_Clt_Keyword";
            this.txtBcpImportArgument_Clt_Keyword.Size = new System.Drawing.Size(748, 19);
            this.txtBcpImportArgument_Clt_Keyword.TabIndex = 8;
            this.txtBcpImportArgument_Clt_Keyword.Text = "clt.tKeyword in D:\\work\\5_Unikktle\\trunk\\2_Source\\3_メンテ\\Maintenance\\TestData\\Data" +
    "\\clt.tKeyword.txt -S localhost -U sa -P xxx -d UnikktleWeb -c -t \\\\t";
            // 
            // txtConnectionString_Import
            // 
            this.txtConnectionString_Import.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString_Import.Location = new System.Drawing.Point(186, 18);
            this.txtConnectionString_Import.Name = "txtConnectionString_Import";
            this.txtConnectionString_Import.Size = new System.Drawing.Size(748, 19);
            this.txtConnectionString_Import.TabIndex = 14;
            this.txtConnectionString_Import.Text = "Data Source=localhost;Initial Catalog=UnikktleWeb;User ID=xxx;Password=xxx";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "Import先DB ConnectionString";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "clt.tKeyword";
            // 
            // txtBcpImportArgument_Clt_CollaborateKeyword
            // 
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Location = new System.Drawing.Point(186, 72);
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Name = "txtBcpImportArgument_Clt_CollaborateKeyword";
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Size = new System.Drawing.Size(748, 19);
            this.txtBcpImportArgument_Clt_CollaborateKeyword.TabIndex = 6;
            this.txtBcpImportArgument_Clt_CollaborateKeyword.Text = "clt.tCollaborateKeyword in D:\\work\\5_Unikktle\\trunk\\2_Source\\3_メンテ\\Maintenance\\Te" +
    "stData\\Data\\clt.tCollaborateKeyword.txt -S localhost -U sa -P xxx -d Unikkt" +
    "leWeb -c -t \\\\t";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "clt.tCollaborateKeyword";
            // 
            // txt実行ログ
            // 
            this.txt実行ログ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt実行ログ.BackColor = System.Drawing.Color.Black;
            this.txt実行ログ.ForeColor = System.Drawing.Color.White;
            this.txt実行ログ.Location = new System.Drawing.Point(14, 385);
            this.txt実行ログ.Multiline = true;
            this.txt実行ログ.Name = "txt実行ログ";
            this.txt実行ログ.Size = new System.Drawing.Size(956, 137);
            this.txt実行ログ.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 356);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "実行ログ";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnエクスポート);
            this.groupBox2.Controls.Add(this.txtBcpExportArgument_Clt_Keyword);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtBcpExportArgument_Clt_CollaborateKeyword);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(14, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(957, 124);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "テストデータ";
            // 
            // btnエクスポート
            // 
            this.btnエクスポート.ForeColor = System.Drawing.Color.Black;
            this.btnエクスポート.Location = new System.Drawing.Point(14, 76);
            this.btnエクスポート.Name = "btnエクスポート";
            this.btnエクスポート.Size = new System.Drawing.Size(163, 31);
            this.btnエクスポート.TabIndex = 16;
            this.btnエクスポート.Text = "エクスポート";
            this.btnエクスポート.UseVisualStyleBackColor = true;
            this.btnエクスポート.Click += new System.EventHandler(this.Btnエクスポート_Click);
            // 
            // txtBcpExportArgument_Clt_Keyword
            // 
            this.txtBcpExportArgument_Clt_Keyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBcpExportArgument_Clt_Keyword.Location = new System.Drawing.Point(148, 23);
            this.txtBcpExportArgument_Clt_Keyword.Name = "txtBcpExportArgument_Clt_Keyword";
            this.txtBcpExportArgument_Clt_Keyword.Size = new System.Drawing.Size(786, 19);
            this.txtBcpExportArgument_Clt_Keyword.TabIndex = 8;
            this.txtBcpExportArgument_Clt_Keyword.Text = resources.GetString("txtBcpExportArgument_Clt_Keyword.Text");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(12, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "clt.tKeyword";
            // 
            // txtBcpExportArgument_Clt_CollaborateKeyword
            // 
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Location = new System.Drawing.Point(148, 51);
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Name = "txtBcpExportArgument_Clt_CollaborateKeyword";
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Size = new System.Drawing.Size(786, 19);
            this.txtBcpExportArgument_Clt_CollaborateKeyword.TabIndex = 6;
            this.txtBcpExportArgument_Clt_CollaborateKeyword.Text = resources.GetString("txtBcpExportArgument_Clt_CollaborateKeyword.Text");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(12, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "clt.tCollaborateKeyword";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(982, 534);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt実行ログ);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnインポート;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBcpImportArgument_Clt_Keyword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBcpImportArgument_Clt_CollaborateKeyword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt実行ログ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConnectionString_Import;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBcpExportArgument_Clt_Keyword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBcpExportArgument_Clt_CollaborateKeyword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnエクスポート;
    }
}

