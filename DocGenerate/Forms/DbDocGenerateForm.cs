using DocGenerate.DatabaseContext;
using DocGenerate.Forms;
using DocGenerate.Helper;
using DocGenerate.Interface.SqlExcelDoc;
using DocGenerate.Model.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
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
        private readonly ISqlExcelDocHelper _sqlExcelDocHelper;
        private List<DatabaseSetting> _settings = new List<DatabaseSetting>();

        public DbDocGenerateForm(
            IServiceProvider serviceProvider,
            DocGenerateDbContext docGenerateDbContext,
            ISharedHelper sharedHelper,
            ISqlExcelDocHelper sqlExcelDocHelper
        )
        {
            _serviceProvider = serviceProvider;
            _docGenerateDbContext = docGenerateDbContext;
            _sharedHelper = sharedHelper;
            _sqlExcelDocHelper = sqlExcelDocHelper;
            InitializeComponent();
        }

        private void InitListView()
        {
            DataGridViewTextBoxColumn uuIdColumn = new DataGridViewTextBoxColumn
            {
                Name = "UUIDColumn",
                HeaderText = "UUID",
                Visible = false // 設置為隱藏
            };
            DataGridView.Columns.Add(uuIdColumn);
            DataGridView.Columns.Add("NameColumn", "名稱");
            DataGridView.Columns.Add("DataBaseTypeColumn", "資料庫類型");
            DataGridView.Columns.Add("ConnectionStringColumn", "連線字串");
            DataGridView.Columns.Add("CreateDate", "建立日期");
            DataGridView.Columns.Add("UpdateDate", "更新日期");
            DataGridView.Columns["NameColumn"].ReadOnly = true;
            DataGridView.Columns["DataBaseTypeColumn"].ReadOnly = true;
            DataGridView.Columns["ConnectionStringColumn"].ReadOnly = true;
            DataGridView.Columns["CreateDate"].ReadOnly = true;
            DataGridView.Columns["UpdateDate"].ReadOnly = true;
            DataGridViewButtonColumn modifyBtnColumn = new DataGridViewButtonColumn
            {
                Name = "ModifyBtn",
                HeaderText = "",
                Text = "修改",
                UseColumnTextForButtonValue = true,
            };
            DataGridView.Columns.Add(modifyBtnColumn);
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
            {
                Name = "DeleteBtn",
                HeaderText = "",
                Text = "刪除",
                UseColumnTextForButtonValue = true,
            };
            DataGridView.Columns.Add(deleteColumn);
            UpdateDataGridView();
            DataGridView.CellContentClick += DataGridView_CellContentClick;
            SetColumnWidths(DataGridView);
        }

        private void SetColumnWidths(DataGridView dataGridView)
        {
            // 設定每一列的寬度百分比
            int totalWidth = dataGridView.ClientSize.Width;
            // 計算每列寬度
            dataGridView.Columns["NameColumn"].Width = (int)(totalWidth * 0.15);
            dataGridView.Columns["DataBaseTypeColumn"].Width = (int)(totalWidth * 0.15);
            dataGridView.Columns["ConnectionStringColumn"].Width = (int)(totalWidth * 0.3);
            dataGridView.Columns["CreateDate"].Width = (int)(totalWidth * 0.15);
            dataGridView.Columns["UpdateDate"].Width = (int)(totalWidth * 0.15);
            dataGridView.Columns["ModifyBtn"].Width = (int)(totalWidth * 0.05);
            dataGridView.Columns["DeleteBtn"].Width = (int)(totalWidth * 0.05);
        }

        private void UpdateDataGridView()
        {
            // 清空所有行
            DataGridView.Rows.Clear();

            // 重新填充 DataGridView
            foreach (var data in _settings)
            {
                var dbType = (DatabaseType)data.DataBaseType;
                var type = "";
                switch (dbType)
                {
                    case DatabaseType.MySQL:
                        type = "MySQL";
                        break;

                    case DatabaseType.MicrosoftSQLServer:
                        type = "Microsoft SQL Server";
                        break;

                    case DatabaseType.PostgreSQL:
                        type = "PostgreSQL";
                        break;
                }
                var createDate = data.CreateDate.ToString("yyyy/MM/dd HH:mm:ss");
                var updateDate = data.UpdateDate.HasValue ? data.UpdateDate.Value.ToString("yyyy/MM/dd HH:mm:ss") : "";
                DataGridView.Rows.Add(data.UUID, data.Name, type, data.ConnectionString, createDate, updateDate);
            }
        }

        private void DataGridView_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = DataGridView.Rows[rowIndex];
            Guid? uuid = (Guid?)row.Cells["UUIDColumn"].Value;
            if (uuid == null)
            {
                return;
            }
            var data = _settings.FirstOrDefault(a => a.UUID == uuid!);
            if (data == null)
            {
                return;
            }
            if (e.ColumnIndex == 6)
            {
                // 修改
                var form = _serviceProvider.GetRequiredService<AddDbSettingForm>();
                form.StartPosition = FormStartPosition.CenterScreen;
                if (data != null)
                {
                    form.DatabaseSetting = data;
                    form.FormClosed += DataGridEditFormClose;
                    form.ShowDialog();
                }
            }
            if (e.ColumnIndex == 7)
            {
                // 刪除
                DialogResult dialogResult = MessageBox.Show("是否確定要移除設定?", "確定", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _docGenerateDbContext.DatabaseSetting.Remove(data!);
                    _docGenerateDbContext.SaveChanges();
                    InitSetting();
                    UpdateDataGridView();
                }
            }
        }

        private void DataGridEditFormClose(object? sender, FormClosedEventArgs e)
        {
            if (sender != null)
            {
                var form = (AddDbSettingForm)sender;
                if (form.IsConfirm)
                {
                    InitSetting();
                    UpdateDataGridView();
                }
            }
        }

        private void InitOptions()
        {
            SettingComboBox.DisplayMember = "Name";
            SettingComboBox.ValueMember = "Value";
            var options = new List<SelectOption<Guid?>>();
            options.Add(new SelectOption<Guid?>("請選擇", null));
            foreach (var setting in _settings)
            {
                options.Add(new SelectOption<Guid?>(setting.Name, setting.UUID));
            }
            SettingComboBox.DataSource = options;
        }

        private void SettingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SettingComboBox.SelectedValue == null)
            {
                EditBtn.Enabled = false;
                GenerateBtn.Enabled = false;
            }
            else
            {
                var data = _settings.FirstOrDefault(a => a.UUID == (Guid)SettingComboBox.SelectedValue!);
                if (data != null)
                {
                    EditBtn.Enabled = true;
                    GenerateBtn.Enabled = true;
                    var dbType = (DatabaseType)data.DataBaseType;
                    var type = "";
                    switch (dbType)
                    {
                        case DatabaseType.MySQL:
                            type = "MySQL";
                            break;

                        case DatabaseType.MicrosoftSQLServer:
                            type = "Microsoft SQL Server";
                            break;

                        case DatabaseType.PostgreSQL:
                            type = "PostgreSQL";
                            break;
                    }
                    DbTypeTextBox.Text = type;
                    DbConnectionTextBox.Text = data.ConnectionString;
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
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

        private void EditFormClose(object? sender, FormClosedEventArgs e)
        {
            if (sender != null)
            {
                var form = (AddDbSettingForm)sender;
                if (form.IsConfirm)
                {
                    var uuid = (Guid)SettingComboBox.SelectedValue!;
                    InitSetting();
                    SettingComboBox.SelectedValue = uuid;
                    UpdateDataGridView();
                }
            }
        }

        private void AddFormClose(object? sender, FormClosedEventArgs e)
        {
            if (sender != null)
            {
                var form = (AddDbSettingForm)sender;
                if (form.IsConfirm)
                {
                    InitSetting();
                    SettingComboBox.SelectedIndex = 0;
                    UpdateDataGridView();
                }
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var form = _serviceProvider.GetRequiredService<AddDbSettingForm>();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.FormClosed += AddFormClose;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }

        private void DbDocGenerateForm_Load(object sender, EventArgs e)
        {
            Text = "資料庫文件產生";
            InitSetting();
            SettingComboBox.SelectedIndex = 0;
            InitListView();
        }

        private void InitSetting()
        {
            _settings = _docGenerateDbContext.DatabaseSetting.ToList();
            InitOptions();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            var uuid = (Guid)SettingComboBox.SelectedValue!;
            var data = _settings.FirstOrDefault(a => a.UUID == (Guid)SettingComboBox.SelectedValue!);
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (data != null)
                {
                    var fileName = data.Name + ".xlsx";
                    var fullPath = Path.Combine(dialog.SelectedPath, fileName);
                    var dbType = (DatabaseType)data.DataBaseType;
                    var type = "";
                    switch (dbType)
                    {
                        case DatabaseType.MySQL:
                            type = "MYSQL";
                            break;

                        case DatabaseType.MicrosoftSQLServer:
                            type = "MSSQL";
                            break;

                        case DatabaseType.PostgreSQL:
                            type = "PostgreSQL";
                            break;
                    }
                    Cursor.Current = Cursors.WaitCursor;
                    var task = Task.Run(() => _sqlExcelDocHelper.CreateDocumentation(data.ConnectionString, fullPath, type));
                    Task.WaitAll(task);
                    _sharedHelper.ShowInfoMsg("文件產生完成", @$"文件已產生到{fullPath}!");
                    Cursor.Current = Cursors.Default;
                }
            }
        }
    }
}
