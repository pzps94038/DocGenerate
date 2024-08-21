using DocGenerate.DatabaseContext;
using DocGenerate.Forms;
using DocGenerate.Helper;
using DocGenerate.Model.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocGenerate
{
    public partial class DbDocGenerateForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DocGenerateDbContext _docGenerateDbContext;
        private readonly ISharedHelper _sharedHelper;
        private List<DatabaseSetting> _settings = new List<DatabaseSetting>();
        public DbDocGenerateForm(
            IServiceProvider serviceProvider,
            DocGenerateDbContext docGenerateDbContext,
            ISharedHelper sharedHelper
        )
        {
            _serviceProvider = serviceProvider;
            _docGenerateDbContext = docGenerateDbContext;
            _sharedHelper = sharedHelper;
            InitializeComponent();
        }

        private void InitListView()
        {
            try
            {
                DataGridView.Columns.Add("DateColumn", "日期");
                DataGridView.Columns.Add("NameColumn", "名稱");
                DataGridView.Columns.Add("ConnectionStringColumn", "連線字串");
                DataGridView.Columns.Add("PathColumn", "產生路徑");
                for (int i = 0; i < 100; i++)
                {
                    DataGridView.Rows.Add("John" + i.ToString(), "S" + i.ToString(), "ConnectionString" + i.ToString(), "Path" + i.ToString());
                }
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }

        private void InitOptions()
        {
            try
            {
                SettingComboBox.DisplayMember = "Name";
                SettingComboBox.ValueMember = "Value";
                var options = new List<SelectOption<Guid?>>();
                options.Add(new SelectOption<Guid?> ("請選擇", null));
                foreach (var setting in _settings)
                {
                    options.Add(new SelectOption<Guid?>(setting.Name, setting.UUID));
                }
                SettingComboBox.DataSource = options;
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }

        private void SettingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SettingComboBox.SelectedValue == null)
                {
                    EditBtn.Enabled = false;
                    GenerateBtn.Enabled = false;
                }
                else
                {
                    EditBtn.Enabled = true;
                    GenerateBtn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var form = _serviceProvider.GetRequiredService<AddDbSettingForm>();
                form.StartPosition = FormStartPosition.CenterScreen;
                var data = _settings.FirstOrDefault(a => a.UUID == (Guid)SettingComboBox.SelectedValue!);
                if (data != null)
                {
                    form.DatabaseSetting = data;
                    form.FormClosed += EditFormClose;
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }

        private void EditFormClose(object? sender, FormClosedEventArgs e)
        {
            try
            {
                var uuid = (Guid)SettingComboBox.SelectedValue!;
                InitSetting();
                SettingComboBox.SelectedValue = uuid;
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }


        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var form = _serviceProvider.GetRequiredService<AddDbSettingForm>();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.FormClosed += EditFormClose;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }

        private void DbDocGenerateForm_Load(object sender, EventArgs e)
        {
            try
            {
                Text = "資料庫文件產生";
                InitSetting();
                SettingComboBox.SelectedIndex = 0;
                InitListView();
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }

        private void InitSetting()
        {
            try
            {
                _settings = _docGenerateDbContext.DatabaseSetting.ToList();
                InitOptions();
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }
    }
}
