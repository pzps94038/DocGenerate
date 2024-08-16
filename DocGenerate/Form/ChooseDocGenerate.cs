using DocGenerate.Helper;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;



namespace DocGenerate
{
    public partial class ChooseDocGenerateForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISharedHelper _sharedHelper;
        public ChooseDocGenerateForm(
            IServiceProvider serviceProvider,
            ISharedHelper sharedHelper
        )
        {
            InitializeComponent();
            this.Text = "選擇產生文件";
            _serviceProvider = serviceProvider;
            _sharedHelper = sharedHelper;
        }

        /// <summary>
        /// 資料庫文件產生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DbDocGenerateBtnClick(object sender, EventArgs e)
        {
            try
            {
                var form = _serviceProvider.GetRequiredService<DbDocGenerateForm>();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Show();
            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }


        public void APIDocGenerateBtnClick(object sender, System.EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }

        public void FormLoad(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _sharedHelper.ShowExceptionMessageBox(ex);
            }
        }
    }
}
