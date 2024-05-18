namespace UpdateDB
{
    partial class UpdateDB
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
            this.btn実行_UpdateDomainNo = new System.Windows.Forms.Button();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn実行_UpdateDomainNo
            // 
            this.btn実行_UpdateDomainNo.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.btn実行_UpdateDomainNo.ForeColor = System.Drawing.Color.Black;
            this.btn実行_UpdateDomainNo.Location = new System.Drawing.Point(35, 77);
            this.btn実行_UpdateDomainNo.Name = "btn実行_UpdateDomainNo";
            this.btn実行_UpdateDomainNo.Size = new System.Drawing.Size(387, 28);
            this.btn実行_UpdateDomainNo.TabIndex = 0;
            this.btn実行_UpdateDomainNo.Text = "[mst].[tUrl]テーブルの[DomainNo]カラムをUpdateする。";
            this.btn実行_UpdateDomainNo.UseVisualStyleBackColor = true;
            this.btn実行_UpdateDomainNo.Click += new System.EventHandler(this.btn実行_UpdateDomainNo_Click);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Location = new System.Drawing.Point(106, 15);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(496, 19);
            this.txtConnectionString.TabIndex = 16;
            this.txtConnectionString.Text = "Data Source=localhost;Initial Catalog=UnikktleCollect;User ID=xxx;Password=xxx" +
    "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(8, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "ConnectionString";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConnectionString);
            this.groupBox1.Controls.Add(this.btn実行_UpdateDomainNo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 150);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // UpdateDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(909, 208);
            this.Controls.Add(this.groupBox1);
            this.Name = "UpdateDB";
            this.Text = "UpdateDB";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn実行_UpdateDomainNo;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

