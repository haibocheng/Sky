namespace OssDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtBucketName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKeyId = new System.Windows.Forms.TextBox();
            this.txtKeySecret = new System.Windows.Forms.TextBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnChoseFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bucket Name";
            // 
            // txtBucketName
            // 
            this.txtBucketName.Location = new System.Drawing.Point(187, 54);
            this.txtBucketName.Name = "txtBucketName";
            this.txtBucketName.Size = new System.Drawing.Size(186, 21);
            this.txtBucketName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Access Key Secret";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Access Key ID";
            // 
            // txtKeyId
            // 
            this.txtKeyId.Location = new System.Drawing.Point(187, 97);
            this.txtKeyId.Name = "txtKeyId";
            this.txtKeyId.Size = new System.Drawing.Size(186, 21);
            this.txtKeyId.TabIndex = 5;
            // 
            // txtKeySecret
            // 
            this.txtKeySecret.Location = new System.Drawing.Point(187, 136);
            this.txtKeySecret.Name = "txtKeySecret";
            this.txtKeySecret.Size = new System.Drawing.Size(186, 21);
            this.txtKeySecret.TabIndex = 6;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(212, 230);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(187, 182);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(186, 21);
            this.txtFileName.TabIndex = 8;
            // 
            // btnChoseFile
            // 
            this.btnChoseFile.Location = new System.Drawing.Point(393, 180);
            this.btnChoseFile.Name = "btnChoseFile";
            this.btnChoseFile.Size = new System.Drawing.Size(64, 23);
            this.btnChoseFile.TabIndex = 9;
            this.btnChoseFile.Text = "选择文件";
            this.btnChoseFile.UseVisualStyleBackColor = true;
            this.btnChoseFile.Click += new System.EventHandler(this.btnChoseFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 292);
            this.Controls.Add(this.btnChoseFile);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtKeySecret);
            this.Controls.Add(this.txtKeyId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBucketName);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "OssDemo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBucketName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKeyId;
        private System.Windows.Forms.TextBox txtKeySecret;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnChoseFile;
    }
}

