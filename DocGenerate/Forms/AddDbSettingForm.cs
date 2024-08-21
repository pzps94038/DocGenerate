using DocGenerate.DatabaseContext;
using DocGenerate.Model.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DocGenerate.Forms
{
    public partial class AddDbSettingForm : Form
    {
        public DatabaseSetting? DatabaseSetting;
        private readonly DocGenerateDbContext _docGenerateDbContext;
        private List<DatabaseSetting> _settings = new List<DatabaseSetting>();
        public AddDbSettingForm(
            DocGenerateDbContext docGenerateDbContext
        )
        {
            InitializeComponent();
            _docGenerateDbContext = docGenerateDbContext;
        }

        private void AddDbSettingForm_Load(object sender, EventArgs e)
        {
            Text = "新增資料庫設定";
            InitOptions();
            _settings = _docGenerateDbContext.DatabaseSetting.ToList();
            if (DatabaseSetting != null)
            {
                InitForm(DatabaseSetting);
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitOptions()
        {
            DbTypeComboBox.DisplayMember = "Name";
            DbTypeComboBox.ValueMember = "Value";
            var options = new List<SelectOption<DatabaseType?>>();
            options.Add(new SelectOption<DatabaseType?>("請選擇資料庫類型", null));
            options.Add(new SelectOption<DatabaseType?>("Microsoft SQL Server", DatabaseType.MicrosoftSQLServer));
            options.Add(new SelectOption<DatabaseType?>("MySQL", DatabaseType.MySQL));
            DbTypeComboBox.DataSource = options;
            DbTypeComboBox.SelectedIndex = 0;
        }

        private void InitForm(DatabaseSetting data)
        {
            var type = (DatabaseType)data.DataBaseType;
            SettingNameTextBox.Text = data.Name;
            DbTypeComboBox.SelectedValue = type;
            DbConnectionStringTextBox.Text = data.ConnectionString;
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if (VaildForm())
            {
                if (DatabaseSetting != null)
                {
                    // 有預設值
                    var data = _docGenerateDbContext.DatabaseSetting.FirstOrDefault(a => a.Name == DatabaseSetting.Name);
                    if (data != null)
                    {
                        data.Name = SettingNameTextBox.Text;
                        data.DataBaseType = (int)DbTypeComboBox.SelectedValue!;
                        data.ConnectionString = DbConnectionStringTextBox.Text;
                        data.UpdateDate = DateTime.Now;
                        _docGenerateDbContext.DatabaseSetting.Update(data);
                        _docGenerateDbContext.SaveChanges();
                        Close();
                    }
                }
                else
                {
                    _docGenerateDbContext.DatabaseSetting.Add(new DatabaseSetting()
                    {
                        UUID = Guid.NewGuid(),
                        Name = SettingNameTextBox.Text,
                        DataBaseType = (int)DbTypeComboBox.SelectedValue!,
                        ConnectionString = DbConnectionStringTextBox.Text,
                        CreateDate = DateTime.Now
                    });
                    _docGenerateDbContext.SaveChanges();
                    Close();
                }
            }
        }

        private void DbTypeComboBox_Validating(object sender, CancelEventArgs e) => DbTypeValidating();

        private bool DbTypeValidating()
        {
            DatabaseType? value = (DatabaseType?)DbTypeComboBox.SelectedValue;
            if (value == null)
            {
                DbTypeErrorProvider.SetError(DbTypeComboBox, "請選擇資料庫類型");
                return false;
            }
            else
            {
                DbTypeErrorProvider.SetError(DbTypeComboBox, "");
                return true;
            }
        }

        private void SettingNameTextBox_Validating(object sender, CancelEventArgs e) => SettingNameValidating();

        private bool SettingNameValidating()
        {
            if (SettingNameTextBox.Text == "")
            {
                SettingNameErrorProvider.SetError(SettingNameTextBox, "請輸入設定名稱");
                return false;
            }
            else
            {

                if (DatabaseSetting != null)
                {
                    // 有帶入參數且跟原本輸入不同
                    if (_settings.Any(a => a.Name != DatabaseSetting.Name && a.Name == SettingNameTextBox.Text))
                    {
                        //有其他重複
                        SettingNameErrorProvider.SetError(SettingNameTextBox, "設定名稱以重複");
                        return false;
                    }
                    else
                    {
                        //無重複
                        return true;
                    }
                }
                else
                {
                    // 無帶入參數
                    if (_settings.Any(a => a.Name == SettingNameTextBox.Text))
                    {
                        //有其他重複
                        SettingNameErrorProvider.SetError(SettingNameTextBox, "設定名稱以重複");
                        return false;
                    }
                    else
                    {
                        //無重複
                        return true;
                    }
                }
            }
        }

        private void DbConnectionStringTextBox_Validating(object sender, CancelEventArgs e) => DbConnectionStringValidating();

        private bool DbConnectionStringValidating()
        {
            if (DbConnectionStringTextBox.Text == "")
            {
                DbConnectionStringErrorProvider.SetError(DbConnectionStringTextBox, "請輸入連線字串");
                return false;
            }
            else
            {
                DbConnectionStringErrorProvider.SetError(DbConnectionStringTextBox, "");
                return true;
            }
        }

        private bool VaildForm()
        {
            var isValid = true;
            if (!SettingNameValidating())
            {
                isValid = false;
            }
            if (!DbTypeValidating())
            {
                isValid = false;
            }
            if (!DbConnectionStringValidating())
            {
                isValid = false;
            }
            return isValid;
        }
    }
}
