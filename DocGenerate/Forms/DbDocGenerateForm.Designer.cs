﻿namespace DocGenerate
{
    partial class DbDocGenerateForm
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
            AddBtn = new Button();
            SettingComboBox = new ComboBox();
            EditBtn = new Button();
            GenerateBtn = new Button();
            DbConnectionTextBox = new TextBox();
            SettingNameLabel = new Label();
            DbConnectionLabel = new Label();
            DbTypeTextBox = new TextBox();
            DbType = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            DataGridView = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridView).BeginInit();
            SuspendLayout();
            // 
            // AddBtn
            // 
            AddBtn.Font = new Font("Microsoft JhengHei UI", 12F);
            AddBtn.Location = new Point(3, 3);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(89, 33);
            AddBtn.TabIndex = 0;
            AddBtn.Text = "新增";
            AddBtn.UseVisualStyleBackColor = true;
            AddBtn.Click += AddBtn_Click;
            // 
            // SettingComboBox
            // 
            SettingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SettingComboBox.FormattingEnabled = true;
            SettingComboBox.Location = new Point(110, 123);
            SettingComboBox.Name = "SettingComboBox";
            SettingComboBox.Size = new Size(703, 23);
            SettingComboBox.TabIndex = 1;
            SettingComboBox.SelectedIndexChanged += SettingComboBox_SelectedIndexChanged;
            // 
            // EditBtn
            // 
            EditBtn.BackgroundImage = Properties.Resources.edit;
            EditBtn.BackgroundImageLayout = ImageLayout.Zoom;
            EditBtn.Enabled = false;
            EditBtn.Location = new Point(819, 123);
            EditBtn.Name = "EditBtn";
            EditBtn.Size = new Size(30, 25);
            EditBtn.TabIndex = 2;
            EditBtn.UseVisualStyleBackColor = true;
            EditBtn.Click += EditBtn_Click;
            // 
            // GenerateBtn
            // 
            GenerateBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            GenerateBtn.Enabled = false;
            GenerateBtn.Font = new Font("Microsoft JhengHei UI", 16F);
            GenerateBtn.Location = new Point(870, 12);
            GenerateBtn.Name = "GenerateBtn";
            GenerateBtn.Size = new Size(175, 163);
            GenerateBtn.TabIndex = 4;
            GenerateBtn.Text = "產生";
            GenerateBtn.UseVisualStyleBackColor = true;
            GenerateBtn.Click += GenerateBtn_Click;
            // 
            // DbConnectionTextBox
            // 
            DbConnectionTextBox.Enabled = false;
            DbConnectionTextBox.Location = new Point(110, 87);
            DbConnectionTextBox.Name = "DbConnectionTextBox";
            DbConnectionTextBox.Size = new Size(703, 23);
            DbConnectionTextBox.TabIndex = 5;
            // 
            // SettingNameLabel
            // 
            SettingNameLabel.AutoSize = true;
            SettingNameLabel.Font = new Font("Microsoft JhengHei UI", 12F);
            SettingNameLabel.Location = new Point(3, 120);
            SettingNameLabel.Name = "SettingNameLabel";
            SettingNameLabel.Size = new Size(73, 20);
            SettingNameLabel.TabIndex = 6;
            SettingNameLabel.Text = "設定名稱";
            // 
            // DbConnectionLabel
            // 
            DbConnectionLabel.AutoSize = true;
            DbConnectionLabel.Font = new Font("Microsoft JhengHei UI", 12F);
            DbConnectionLabel.Location = new Point(3, 84);
            DbConnectionLabel.Name = "DbConnectionLabel";
            DbConnectionLabel.Size = new Size(73, 20);
            DbConnectionLabel.TabIndex = 7;
            DbConnectionLabel.Text = "連線字串";
            // 
            // DbTypeTextBox
            // 
            DbTypeTextBox.Enabled = false;
            DbTypeTextBox.Location = new Point(110, 49);
            DbTypeTextBox.Name = "DbTypeTextBox";
            DbTypeTextBox.Size = new Size(703, 23);
            DbTypeTextBox.TabIndex = 8;
            // 
            // DbType
            // 
            DbType.AutoSize = true;
            DbType.Font = new Font("Microsoft JhengHei UI", 12F);
            DbType.Location = new Point(3, 46);
            DbType.Name = "DbType";
            DbType.Size = new Size(89, 20);
            DbType.TabIndex = 9;
            DbType.Text = "資料庫類型";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 107F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95.1677856F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.832215F));
            tableLayoutPanel1.Controls.Add(AddBtn, 0, 0);
            tableLayoutPanel1.Controls.Add(DbConnectionTextBox, 1, 2);
            tableLayoutPanel1.Controls.Add(DbTypeTextBox, 1, 1);
            tableLayoutPanel1.Controls.Add(DbType, 0, 1);
            tableLayoutPanel1.Controls.Add(EditBtn, 2, 3);
            tableLayoutPanel1.Controls.Add(SettingComboBox, 1, 3);
            tableLayoutPanel1.Controls.Add(SettingNameLabel, 0, 3);
            tableLayoutPanel1.Controls.Add(DbConnectionLabel, 0, 2);
            tableLayoutPanel1.Location = new Point(12, 9);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(852, 166);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // DataGridView
            // 
            DataGridView.AllowUserToAddRows = false;
            DataGridView.AllowUserToOrderColumns = true;
            DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridView.Dock = DockStyle.Bottom;
            DataGridView.Location = new Point(0, 181);
            DataGridView.Name = "DataGridView";
            DataGridView.Size = new Size(1045, 347);
            DataGridView.TabIndex = 11;
            // 
            // DbDocGenerateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 528);
            Controls.Add(DataGridView);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(GenerateBtn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "DbDocGenerateForm";
            Text = "DbDocGenerateForm";
            Load += DbDocGenerateForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button AddBtn;
        private ComboBox SettingComboBox;
        private Button EditBtn;
        private Button GenerateBtn;
        private TextBox DbConnectionTextBox;
        private Label SettingNameLabel;
        private Label DbConnectionLabel;
        private TextBox DbTypeTextBox;
        private Label DbType;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView DataGridView;
    }
}