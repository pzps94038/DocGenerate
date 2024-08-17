using DocGenerate.Forms;
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
        public DbDocGenerateForm(
            IServiceProvider serviceProvider
        )
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            Text = "資料庫文件產生";
        }

        private void InitListView()
        {
            ListView.View = View.Details;
            // 暫停更新
            ListView.BeginUpdate();
            ListView.ColumnClick += new ColumnClickEventHandler(ColumnClick!);
            ListView.Columns.Add("日期");
            ListView.Columns.Add("名稱");
            ListView.Columns.Add("連線字串");
            ListView.Columns.Add("產生路徑");
            foreach (ColumnHeader column in ListView.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            for (int i = 0; i < 100; i++)
            {
                ListViewItem item = new("John" + i.ToString());
                item.SubItems.Add("S" + i.ToString());
                ListView.Items.Add(item);
            }
            ListView.EndUpdate();
        }

        private void InitOptions()
        {
            SettingComboBox.DisplayMember = "Name";   // 显示的文本
            SettingComboBox.ValueMember = "Value";    // 对应的值
            var options = new List<SelectOption>();
            options.Add(new SelectOption("請選擇", null));
            SettingComboBox.DataSource = options;
            SettingComboBox.SelectedIndex = 0;
        }

        private void ColumnClick(object sender, ColumnClickEventArgs args)
        {

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
                EditBtn.Enabled = true;
                GenerateBtn.Enabled = true;
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

        }

        private void DbType_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<AddDbSettingForm>();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DbDocGenerateForm_Load(object sender, EventArgs e)
        {
            InitListView();
            InitOptions();
        }
    }
}
