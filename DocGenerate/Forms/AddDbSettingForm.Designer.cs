namespace DocGenerate.Forms
{
    partial class AddDbSettingForm
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
            DbTypeComboBox = new ComboBox();
            SettingNameTextBox = new TextBox();
            DbConnectionStringTextBox = new TextBox();
            DbTypeLabel = new Label();
            SettingNameLabel = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            DbConnectionStringLabel = new Label();
            ConfirmBtn = new Button();
            CancelBtn = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            SettingNameErrorProvider = new ErrorProvider(components);
            DbTypeErrorProvider = new ErrorProvider(components);
            DbConnectionStringErrorProvider = new ErrorProvider(components);
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SettingNameErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DbTypeErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DbConnectionStringErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // DbTypeComboBox
            // 
            DbTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DbTypeComboBox.FormattingEnabled = true;
            DbTypeComboBox.Location = new Point(3, 89);
            DbTypeComboBox.Name = "DbTypeComboBox";
            DbTypeComboBox.Size = new Size(404, 23);
            DbTypeComboBox.TabIndex = 0;
            DbTypeComboBox.Validating += DbTypeComboBox_Validating;
            // 
            // SettingNameTextBox
            // 
            SettingNameTextBox.Location = new Point(3, 28);
            SettingNameTextBox.Name = "SettingNameTextBox";
            SettingNameTextBox.Size = new Size(404, 23);
            SettingNameTextBox.TabIndex = 1;
            SettingNameTextBox.Validating += SettingNameTextBox_Validating;
            // 
            // DbConnectionStringTextBox
            // 
            DbConnectionStringTextBox.Location = new Point(15, 156);
            DbConnectionStringTextBox.Multiline = true;
            DbConnectionStringTextBox.Name = "DbConnectionStringTextBox";
            DbConnectionStringTextBox.ScrollBars = ScrollBars.Vertical;
            DbConnectionStringTextBox.Size = new Size(404, 101);
            DbConnectionStringTextBox.TabIndex = 2;
            DbConnectionStringTextBox.Validating += DbConnectionStringTextBox_Validating;
            // 
            // DbTypeLabel
            // 
            DbTypeLabel.AutoSize = true;
            DbTypeLabel.Font = new Font("Microsoft JhengHei UI", 12F);
            DbTypeLabel.Location = new Point(3, 56);
            DbTypeLabel.Name = "DbTypeLabel";
            DbTypeLabel.Size = new Size(89, 20);
            DbTypeLabel.TabIndex = 3;
            DbTypeLabel.Text = "資料庫類型";
            // 
            // SettingNameLabel
            // 
            SettingNameLabel.AutoSize = true;
            SettingNameLabel.Font = new Font("Microsoft JhengHei UI", 12F);
            SettingNameLabel.Location = new Point(3, 0);
            SettingNameLabel.Name = "SettingNameLabel";
            SettingNameLabel.Size = new Size(73, 20);
            SettingNameLabel.TabIndex = 4;
            SettingNameLabel.Text = "設定名稱";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(SettingNameTextBox, 0, 1);
            tableLayoutPanel1.Controls.Add(DbTypeComboBox, 0, 3);
            tableLayoutPanel1.Controls.Add(SettingNameLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(DbTypeLabel, 0, 2);
            tableLayoutPanel1.Location = new Point(12, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel1.Size = new Size(429, 125);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // DbConnectionStringLabel
            // 
            DbConnectionStringLabel.AutoSize = true;
            DbConnectionStringLabel.Font = new Font("Microsoft JhengHei UI", 12F);
            DbConnectionStringLabel.Location = new Point(12, 133);
            DbConnectionStringLabel.Name = "DbConnectionStringLabel";
            DbConnectionStringLabel.Size = new Size(73, 20);
            DbConnectionStringLabel.TabIndex = 5;
            DbConnectionStringLabel.Text = "連線字串";
            // 
            // ConfirmBtn
            // 
            ConfirmBtn.Font = new Font("Microsoft JhengHei UI", 16F);
            ConfirmBtn.Location = new Point(205, 3);
            ConfirmBtn.Name = "ConfirmBtn";
            ConfirmBtn.Size = new Size(196, 94);
            ConfirmBtn.TabIndex = 7;
            ConfirmBtn.Text = "確定";
            ConfirmBtn.UseVisualStyleBackColor = true;
            ConfirmBtn.Click += ConfirmBtn_Click;
            // 
            // CancelBtn
            // 
            CancelBtn.Font = new Font("Microsoft JhengHei UI", 16F);
            CancelBtn.Location = new Point(3, 3);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(196, 94);
            CancelBtn.TabIndex = 8;
            CancelBtn.Text = "取消";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(CancelBtn, 0, 0);
            tableLayoutPanel2.Controls.Add(ConfirmBtn, 1, 0);
            tableLayoutPanel2.Location = new Point(15, 263);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(404, 100);
            tableLayoutPanel2.TabIndex = 9;
            // 
            // SettingNameErrorProvider
            // 
            SettingNameErrorProvider.ContainerControl = this;
            // 
            // DbTypeErrorProvider
            // 
            DbTypeErrorProvider.ContainerControl = this;
            // 
            // DbConnectionStringErrorProvider
            // 
            DbConnectionStringErrorProvider.ContainerControl = this;
            // 
            // AddDbSettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(442, 364);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(DbConnectionStringTextBox);
            Controls.Add(DbConnectionStringLabel);
            Name = "AddDbSettingForm";
            Text = "AddDbSettingForm";
            Load += AddDbSettingForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SettingNameErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)DbTypeErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)DbConnectionStringErrorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox DbTypeComboBox;
        private TextBox SettingNameTextBox;
        private TextBox DbConnectionStringTextBox;
        private Label DbTypeLabel;
        private Label SettingNameLabel;
        private TableLayoutPanel tableLayoutPanel1;
        private Label DbConnectionStringLabel;
        private Button ConfirmBtn;
        private Button CancelBtn;
        private TableLayoutPanel tableLayoutPanel2;
        private ErrorProvider SettingNameErrorProvider;
        private ErrorProvider DbTypeErrorProvider;
        private ErrorProvider DbConnectionStringErrorProvider;
    }
}