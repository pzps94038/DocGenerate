using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocGenerate.Forms
{
    public partial class AddDbSettingForm : Form
    {
        public AddDbSettingForm()
        {
            InitializeComponent();
        }

        private void AddDbSettingForm_Load(object sender, EventArgs e)
        {
            Text = "新增資料庫設定";
        }
    }
}
