namespace Maintenance
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
            this.btn全更新実行 = new System.Windows.Forms.Button();
            this.chkLocalhost = new System.Windows.Forms.CheckBox();
            this.chk192_168_11_5 = new System.Windows.Forms.CheckBox();
            this.chk192_168_11_31 = new System.Windows.Forms.CheckBox();
            this.chk160_16_75_102 = new System.Windows.Forms.CheckBox();
            this.txtTrunkフォルダパス = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn全更新実行
            // 
            this.btn全更新実行.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.btn全更新実行.Location = new System.Drawing.Point(12, 149);
            this.btn全更新実行.Name = "btn全更新実行";
            this.btn全更新実行.Size = new System.Drawing.Size(417, 33);
            this.btn全更新実行.TabIndex = 0;
            this.btn全更新実行.Text = "全サーバ/DBのストアド/ファンクション/権限　更新実行";
            this.btn全更新実行.UseVisualStyleBackColor = true;
            this.btn全更新実行.Click += new System.EventHandler(this.btn全更新実行_Click);
            // 
            // chkLocalhost
            // 
            this.chkLocalhost.AutoSize = true;
            this.chkLocalhost.Checked = true;
            this.chkLocalhost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLocalhost.ForeColor = System.Drawing.Color.White;
            this.chkLocalhost.Location = new System.Drawing.Point(12, 12);
            this.chkLocalhost.Name = "chkLocalhost";
            this.chkLocalhost.Size = new System.Drawing.Size(70, 16);
            this.chkLocalhost.TabIndex = 1;
            this.chkLocalhost.Text = "localhost";
            this.chkLocalhost.UseVisualStyleBackColor = true;
            // 
            // chk192_168_11_5
            // 
            this.chk192_168_11_5.AutoSize = true;
            this.chk192_168_11_5.ForeColor = System.Drawing.Color.White;
            this.chk192_168_11_5.Location = new System.Drawing.Point(103, 12);
            this.chk192_168_11_5.Name = "chk192_168_11_5";
            this.chk192_168_11_5.Size = new System.Drawing.Size(90, 16);
            this.chk192_168_11_5.TabIndex = 2;
            this.chk192_168_11_5.Text = "192_168_11_5";
            this.chk192_168_11_5.UseVisualStyleBackColor = true;
            // 
            // chk192_168_11_31
            // 
            this.chk192_168_11_31.AutoSize = true;
            this.chk192_168_11_31.Checked = true;
            this.chk192_168_11_31.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk192_168_11_31.ForeColor = System.Drawing.Color.White;
            this.chk192_168_11_31.Location = new System.Drawing.Point(215, 12);
            this.chk192_168_11_31.Name = "chk192_168_11_31";
            this.chk192_168_11_31.Size = new System.Drawing.Size(96, 16);
            this.chk192_168_11_31.TabIndex = 3;
            this.chk192_168_11_31.Text = "192_168_11_31";
            this.chk192_168_11_31.UseVisualStyleBackColor = true;
            // 
            // chk160_16_75_102
            // 
            this.chk160_16_75_102.AutoSize = true;
            this.chk160_16_75_102.ForeColor = System.Drawing.Color.White;
            this.chk160_16_75_102.Location = new System.Drawing.Point(333, 12);
            this.chk160_16_75_102.Name = "chk160_16_75_102";
            this.chk160_16_75_102.Size = new System.Drawing.Size(96, 16);
            this.chk160_16_75_102.TabIndex = 4;
            this.chk160_16_75_102.Text = "160_16_75_102";
            this.chk160_16_75_102.UseVisualStyleBackColor = true;
            // 
            // txtTrunkフォルダパス
            // 
            this.txtTrunkフォルダパス.Location = new System.Drawing.Point(106, 57);
            this.txtTrunkフォルダパス.Name = "txtTrunkフォルダパス";
            this.txtTrunkフォルダパス.Size = new System.Drawing.Size(323, 19);
            this.txtTrunkフォルダパス.TabIndex = 5;
            this.txtTrunkフォルダパス.Text = "D:\\work\\5_Unikktle\\trunk";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Trunkフォルダパス";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(458, 203);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTrunkフォルダパス);
            this.Controls.Add(this.chk160_16_75_102);
            this.Controls.Add(this.chk192_168_11_31);
            this.Controls.Add(this.chk192_168_11_5);
            this.Controls.Add(this.chkLocalhost);
            this.Controls.Add(this.btn全更新実行);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn全更新実行;
        private System.Windows.Forms.CheckBox chkLocalhost;
        private System.Windows.Forms.CheckBox chk192_168_11_5;
        private System.Windows.Forms.CheckBox chk192_168_11_31;
        private System.Windows.Forms.CheckBox chk160_16_75_102;
        private System.Windows.Forms.TextBox txtTrunkフォルダパス;
        private System.Windows.Forms.Label label1;
    }
}

