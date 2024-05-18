namespace Tool
{
    partial class Top
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
            this.btnHttpPost = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHTMLデコード = new System.Windows.Forms.Button();
            this.btnHTMLデコード_IPN即時通知 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHttpPost
            // 
            this.btnHttpPost.ForeColor = System.Drawing.Color.Black;
            this.btnHttpPost.Location = new System.Drawing.Point(9, 48);
            this.btnHttpPost.Name = "btnHttpPost";
            this.btnHttpPost.Size = new System.Drawing.Size(110, 23);
            this.btnHttpPost.TabIndex = 0;
            this.btnHttpPost.Text = "HTTP POST";
            this.btnHttpPost.UseVisualStyleBackColor = true;
            this.btnHttpPost.Click += new System.EventHandler(this.BtnHttpPost_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnHttpPost);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 92);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "テスト";
            // 
            // btnHTMLデコード
            // 
            this.btnHTMLデコード.ForeColor = System.Drawing.Color.Black;
            this.btnHTMLデコード.Location = new System.Drawing.Point(21, 142);
            this.btnHTMLデコード.Name = "btnHTMLデコード";
            this.btnHTMLデコード.Size = new System.Drawing.Size(110, 23);
            this.btnHTMLデコード.TabIndex = 2;
            this.btnHTMLデコード.Text = "HTML デコード";
            this.btnHTMLデコード.UseVisualStyleBackColor = true;
            this.btnHTMLデコード.Click += new System.EventHandler(this.btnHTMLデコード_Click);
            // 
            // btnHTMLデコード_IPN即時通知
            // 
            this.btnHTMLデコード_IPN即時通知.ForeColor = System.Drawing.Color.Black;
            this.btnHTMLデコード_IPN即時通知.Location = new System.Drawing.Point(153, 142);
            this.btnHTMLデコード_IPN即時通知.Name = "btnHTMLデコード_IPN即時通知";
            this.btnHTMLデコード_IPN即時通知.Size = new System.Drawing.Size(209, 23);
            this.btnHTMLデコード_IPN即時通知.TabIndex = 3;
            this.btnHTMLデコード_IPN即時通知.Text = "HTML デコード（ IPN即時通知）";
            this.btnHTMLデコード_IPN即時通知.UseVisualStyleBackColor = true;
            this.btnHTMLデコード_IPN即時通知.Click += new System.EventHandler(this.btnHTMLデコード_IPN即時通知_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "http://xxx/PayPalIPN/Receive";
            // 
            // Top
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(506, 314);
            this.Controls.Add(this.btnHTMLデコード_IPN即時通知);
            this.Controls.Add(this.btnHTMLデコード);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Top";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHttpPost;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHTMLデコード;
        private System.Windows.Forms.Button btnHTMLデコード_IPN即時通知;
        private System.Windows.Forms.Label label2;
    }
}

