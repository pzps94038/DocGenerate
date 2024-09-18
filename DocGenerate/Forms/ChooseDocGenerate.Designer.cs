
namespace DocGenerate
{
    partial class ChooseDocGenerateForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DbDocGenerateBtn = new Button();
            APIDocGenerateBtn = new Button();
            SelfSignedCertificateBtn = new Button();
            SuspendLayout();
            // 
            // DbDocGenerateBtn
            // 
            DbDocGenerateBtn.Anchor = AnchorStyles.None;
            DbDocGenerateBtn.AutoSize = true;
            DbDocGenerateBtn.Font = new Font("Microsoft JhengHei UI", 12F);
            DbDocGenerateBtn.Location = new Point(152, 27);
            DbDocGenerateBtn.Name = "DbDocGenerateBtn";
            DbDocGenerateBtn.Size = new Size(259, 55);
            DbDocGenerateBtn.TabIndex = 0;
            DbDocGenerateBtn.Text = "資料庫文件產生";
            DbDocGenerateBtn.UseVisualStyleBackColor = true;
            DbDocGenerateBtn.Click += DbDocGenerateBtnClick;
            // 
            // APIDocGenerateBtn
            // 
            APIDocGenerateBtn.Anchor = AnchorStyles.None;
            APIDocGenerateBtn.Font = new Font("Microsoft JhengHei UI", 12F);
            APIDocGenerateBtn.Location = new Point(152, 100);
            APIDocGenerateBtn.Name = "APIDocGenerateBtn";
            APIDocGenerateBtn.Size = new Size(259, 55);
            APIDocGenerateBtn.TabIndex = 1;
            APIDocGenerateBtn.Text = "API規格文件產生";
            APIDocGenerateBtn.UseVisualStyleBackColor = true;
            APIDocGenerateBtn.Click += APIDocGenerateBtnClick;
            // 
            // SelfSignedCertificateBtn
            // 
            SelfSignedCertificateBtn.Font = new Font("Microsoft JhengHei UI", 12F);
            SelfSignedCertificateBtn.Location = new Point(152, 172);
            SelfSignedCertificateBtn.Name = "SelfSignedCertificateBtn";
            SelfSignedCertificateBtn.Size = new Size(259, 55);
            SelfSignedCertificateBtn.TabIndex = 2;
            SelfSignedCertificateBtn.Text = "自簽憑證產生";
            SelfSignedCertificateBtn.UseVisualStyleBackColor = true;
            SelfSignedCertificateBtn.Click += SelfSignedCertificateBtn_Click;
            // 
            // ChooseDocGenerateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 256);
            Controls.Add(SelfSignedCertificateBtn);
            Controls.Add(APIDocGenerateBtn);
            Controls.Add(DbDocGenerateBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ChooseDocGenerateForm";
            Text = "ChooseDocGenerateForm";
            Load += ChooseDocGenerateForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button DbDocGenerateBtn;
        private Button APIDocGenerateBtn;
        private Button SelfSignedCertificateBtn;
    }
}
