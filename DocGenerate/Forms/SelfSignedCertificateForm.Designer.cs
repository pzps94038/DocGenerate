﻿namespace DocGenerate.Forms
{
    partial class SelfSignedCertificateForm
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
            components = new System.ComponentModel.Container();
            CountryTextBox = new TextBox();
            CountryLabel = new Label();
            ProvinceLabel = new Label();
            ProvinceTextBox = new TextBox();
            CityTextBox = new TextBox();
            CityLabel = new Label();
            OrganizationNameTextBox = new TextBox();
            OrganizationNameLabel = new Label();
            OrganizationalUnitTextBox = new TextBox();
            OrganizationalUnitLabel = new Label();
            EmailTextBox = new TextBox();
            EmailLabel = new Label();
            CommonNameTextBox = new TextBox();
            CommonNameLabel = new Label();
            GenerateBtn = new Button();
            CountryErrorProvider = new ErrorProvider(components);
            ProvinceErrorProvider = new ErrorProvider(components);
            CityErrorProvider = new ErrorProvider(components);
            OrganizationNameErrorProvider = new ErrorProvider(components);
            OrganizationalUnitErrorProvider = new ErrorProvider(components);
            EmailErrorProvider = new ErrorProvider(components);
            CommonNameErrorProvider = new ErrorProvider(components);
            label1 = new Label();
            DNSTextBox = new TextBox();
            DNSLabel = new Label();
            IPTextBox = new TextBox();
            IPLabel = new Label();
            DNSErrorProvider = new ErrorProvider(components);
            IPErrorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)CountryErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ProvinceErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CityErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OrganizationNameErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OrganizationalUnitErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EmailErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CommonNameErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DNSErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IPErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // CountryTextBox
            // 
            CountryTextBox.Location = new Point(22, 27);
            CountryTextBox.Name = "CountryTextBox";
            CountryTextBox.Size = new Size(763, 23);
            CountryTextBox.TabIndex = 0;
            CountryTextBox.Text = "TW";
            CountryTextBox.Validating += CountryTextBox_Validating;
            // 
            // CountryLabel
            // 
            CountryLabel.AutoSize = true;
            CountryLabel.Location = new Point(22, 9);
            CountryLabel.Name = "CountryLabel";
            CountryLabel.Size = new Size(55, 15);
            CountryLabel.TabIndex = 1;
            CountryLabel.Text = "國家代碼";
            // 
            // ProvinceLabel
            // 
            ProvinceLabel.AutoSize = true;
            ProvinceLabel.Location = new Point(22, 53);
            ProvinceLabel.Name = "ProvinceLabel";
            ProvinceLabel.Size = new Size(60, 15);
            ProvinceLabel.TabIndex = 2;
            ProvinceLabel.Text = "所屬省/州";
            // 
            // ProvinceTextBox
            // 
            ProvinceTextBox.Location = new Point(22, 71);
            ProvinceTextBox.Name = "ProvinceTextBox";
            ProvinceTextBox.Size = new Size(763, 23);
            ProvinceTextBox.TabIndex = 3;
            ProvinceTextBox.Text = "Taiwan";
            ProvinceTextBox.Validating += ProvinceTextBox_Validating;
            // 
            // CityTextBox
            // 
            CityTextBox.Location = new Point(22, 115);
            CityTextBox.Name = "CityTextBox";
            CityTextBox.Size = new Size(763, 23);
            CityTextBox.TabIndex = 5;
            CityTextBox.Text = "Taipei";
            CityTextBox.Validating += CityTextBox_Validating;
            // 
            // CityLabel
            // 
            CityLabel.AutoSize = true;
            CityLabel.Location = new Point(22, 97);
            CityLabel.Name = "CityLabel";
            CityLabel.Size = new Size(55, 15);
            CityLabel.TabIndex = 4;
            CityLabel.Text = "所在城市";
            // 
            // OrganizationNameTextBox
            // 
            OrganizationNameTextBox.Location = new Point(22, 159);
            OrganizationNameTextBox.Name = "OrganizationNameTextBox";
            OrganizationNameTextBox.Size = new Size(763, 23);
            OrganizationNameTextBox.TabIndex = 7;
            OrganizationNameTextBox.Text = "組織名稱";
            OrganizationNameTextBox.Validating += OrganizationNameTextBox_Validating;
            // 
            // OrganizationNameLabel
            // 
            OrganizationNameLabel.AutoSize = true;
            OrganizationNameLabel.Location = new Point(22, 141);
            OrganizationNameLabel.Name = "OrganizationNameLabel";
            OrganizationNameLabel.Size = new Size(55, 15);
            OrganizationNameLabel.TabIndex = 6;
            OrganizationNameLabel.Text = "組織名稱";
            // 
            // OrganizationalUnitTextBox
            // 
            OrganizationalUnitTextBox.Location = new Point(22, 204);
            OrganizationalUnitTextBox.Name = "OrganizationalUnitTextBox";
            OrganizationalUnitTextBox.Size = new Size(763, 23);
            OrganizationalUnitTextBox.TabIndex = 9;
            OrganizationalUnitTextBox.Text = "IT Department";
            OrganizationalUnitTextBox.Validating += OrganizationalUnitTextBox_Validating;
            // 
            // OrganizationalUnitLabel
            // 
            OrganizationalUnitLabel.AutoSize = true;
            OrganizationalUnitLabel.Location = new Point(22, 186);
            OrganizationalUnitLabel.Name = "OrganizationalUnitLabel";
            OrganizationalUnitLabel.Size = new Size(55, 15);
            OrganizationalUnitLabel.TabIndex = 8;
            OrganizationalUnitLabel.Text = "組織單位";
            // 
            // EmailTextBox
            // 
            EmailTextBox.Location = new Point(22, 247);
            EmailTextBox.Name = "EmailTextBox";
            EmailTextBox.Size = new Size(763, 23);
            EmailTextBox.TabIndex = 11;
            EmailTextBox.Text = "admin@example.com";
            EmailTextBox.Validating += EmailTextBox_Validating;
            // 
            // EmailLabel
            // 
            EmailLabel.AutoSize = true;
            EmailLabel.Location = new Point(22, 230);
            EmailLabel.Name = "EmailLabel";
            EmailLabel.Size = new Size(187, 15);
            EmailLabel.TabIndex = 10;
            EmailLabel.Text = "電子郵件地址，用於聯繫相關人員";
            // 
            // CommonNameTextBox
            // 
            CommonNameTextBox.Location = new Point(22, 291);
            CommonNameTextBox.Name = "CommonNameTextBox";
            CommonNameTextBox.Size = new Size(763, 23);
            CommonNameTextBox.TabIndex = 13;
            CommonNameTextBox.Text = "通用名稱";
            CommonNameTextBox.Validating += CommonNameTextBox_Validating;
            // 
            // CommonNameLabel
            // 
            CommonNameLabel.AutoSize = true;
            CommonNameLabel.Location = new Point(22, 273);
            CommonNameLabel.Name = "CommonNameLabel";
            CommonNameLabel.Size = new Size(200, 15);
            CommonNameLabel.TabIndex = 12;
            CommonNameLabel.Text = "通用名稱（Common Name，CN）";
            // 
            // GenerateBtn
            // 
            GenerateBtn.Font = new Font("Microsoft JhengHei UI", 16F);
            GenerateBtn.Location = new Point(22, 542);
            GenerateBtn.Name = "GenerateBtn";
            GenerateBtn.Size = new Size(763, 51);
            GenerateBtn.TabIndex = 14;
            GenerateBtn.Text = "產生";
            GenerateBtn.UseVisualStyleBackColor = true;
            GenerateBtn.Click += GenerateBtn_Click;
            // 
            // CountryErrorProvider
            // 
            CountryErrorProvider.ContainerControl = this;
            // 
            // ProvinceErrorProvider
            // 
            ProvinceErrorProvider.ContainerControl = this;
            // 
            // CityErrorProvider
            // 
            CityErrorProvider.ContainerControl = this;
            // 
            // OrganizationNameErrorProvider
            // 
            OrganizationNameErrorProvider.ContainerControl = this;
            // 
            // OrganizationalUnitErrorProvider
            // 
            OrganizationalUnitErrorProvider.ContainerControl = this;
            // 
            // EmailErrorProvider
            // 
            EmailErrorProvider.ContainerControl = this;
            // 
            // CommonNameErrorProvider
            // 
            CommonNameErrorProvider.ContainerControl = this;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(432, 343);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 17;
            // 
            // DNSTextBox
            // 
            DNSTextBox.Location = new Point(22, 335);
            DNSTextBox.Multiline = true;
            DNSTextBox.Name = "DNSTextBox";
            DNSTextBox.ScrollBars = ScrollBars.Vertical;
            DNSTextBox.Size = new Size(763, 90);
            DNSTextBox.TabIndex = 20;
            DNSTextBox.Validating += DNSTextBox_Validating;
            // 
            // DNSLabel
            // 
            DNSLabel.AutoSize = true;
            DNSLabel.Location = new Point(22, 317);
            DNSLabel.Name = "DNSLabel";
            DNSLabel.Size = new Size(67, 15);
            DNSLabel.TabIndex = 19;
            DNSLabel.Text = "允許的域名";
            // 
            // IPTextBox
            // 
            IPTextBox.Location = new Point(22, 446);
            IPTextBox.Multiline = true;
            IPTextBox.Name = "IPTextBox";
            IPTextBox.ScrollBars = ScrollBars.Vertical;
            IPTextBox.Size = new Size(763, 90);
            IPTextBox.TabIndex = 22;
            IPTextBox.Validating += IPTextBox_Validating;
            // 
            // IPLabel
            // 
            IPLabel.AutoSize = true;
            IPLabel.Location = new Point(22, 428);
            IPLabel.Name = "IPLabel";
            IPLabel.Size = new Size(77, 15);
            IPLabel.TabIndex = 21;
            IPLabel.Text = "允許的IP地址";
            // 
            // DNSErrorProvider
            // 
            DNSErrorProvider.ContainerControl = this;
            // 
            // IPErrorProvider
            // 
            IPErrorProvider.ContainerControl = this;
            // 
            // SelfSignedCertificateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(809, 607);
            Controls.Add(IPTextBox);
            Controls.Add(IPLabel);
            Controls.Add(DNSTextBox);
            Controls.Add(DNSLabel);
            Controls.Add(label1);
            Controls.Add(GenerateBtn);
            Controls.Add(CommonNameTextBox);
            Controls.Add(CommonNameLabel);
            Controls.Add(EmailTextBox);
            Controls.Add(EmailLabel);
            Controls.Add(OrganizationalUnitTextBox);
            Controls.Add(OrganizationalUnitLabel);
            Controls.Add(OrganizationNameTextBox);
            Controls.Add(OrganizationNameLabel);
            Controls.Add(CityTextBox);
            Controls.Add(CityLabel);
            Controls.Add(ProvinceTextBox);
            Controls.Add(ProvinceLabel);
            Controls.Add(CountryLabel);
            Controls.Add(CountryTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "SelfSignedCertificateForm";
            Text = "SelfSignedCertificateForm";
            Load += SelfSignedCertificateForm_Load;
            ((System.ComponentModel.ISupportInitialize)CountryErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)ProvinceErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)CityErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)OrganizationNameErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)OrganizationalUnitErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)EmailErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)CommonNameErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)DNSErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)IPErrorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox CountryTextBox;
        private Label CountryLabel;
        private Label ProvinceLabel;
        private TextBox ProvinceTextBox;
        private TextBox CityTextBox;
        private Label CityLabel;
        private TextBox OrganizationNameTextBox;
        private Label OrganizationNameLabel;
        private TextBox OrganizationalUnitTextBox;
        private Label OrganizationalUnitLabel;
        private TextBox EmailTextBox;
        private Label EmailLabel;
        private TextBox CommonNameTextBox;
        private Label CommonNameLabel;
        private Button GenerateBtn;
        private ErrorProvider CountryErrorProvider;
        private ErrorProvider ProvinceErrorProvider;
        private ErrorProvider CityErrorProvider;
        private ErrorProvider OrganizationNameErrorProvider;
        private ErrorProvider OrganizationalUnitErrorProvider;
        private ErrorProvider EmailErrorProvider;
        private ErrorProvider CommonNameErrorProvider;
        private Label label1;
        private TextBox DNSTextBox;
        private Label DNSLabel;
        private TextBox IPTextBox;
        private Label IPLabel;
        private ErrorProvider DNSErrorProvider;
        private ErrorProvider IPErrorProvider;
    }
}