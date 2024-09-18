using DocGenerate.Forms;
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
            this.Text = "��ܲ��ͤ��";
            _serviceProvider = serviceProvider;
            _sharedHelper = sharedHelper;
        }

        private void ChooseDocGenerateForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ��Ʈw��󲣥�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DbDocGenerateBtnClick(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<DbDocGenerateForm>();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }


        public void APIDocGenerateBtnClick(object sender, System.EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<APIDocForm>();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        private void SelfSignedCertificateBtn_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<SelfSignedCertificateForm>();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }
    }
}
