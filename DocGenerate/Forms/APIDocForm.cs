using DocGenerate.Interface.APIDoc;
using Microsoft.Web.WebView2.Core;
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
    public partial class APIDocForm : Form
    {
        private readonly IWebViewHelper _iWebViewHelper;
        public APIDocForm(
            IWebViewHelper iWebViewHelper
        )
        {
            _iWebViewHelper = iWebViewHelper;
            InitializeComponent();
        }

        private void APIDocForm_Load(object sender, EventArgs e)
        {
            this.Text = "API規格文件產生";
            string basePath = AppContext.BaseDirectory;
            string relativePath = "Asset\\APIDoc";
            var fileName = "index.html";
            var fullPath = Path.Combine(basePath, relativePath, fileName);
            WebView.Source = new Uri(fullPath);
            InitializeAsync();
        }

        async void InitializeAsync()
        {

            // 要先執行非同步的 EnsureCoreWebView2Async 才會觸發控件的 CoreWebView2 的初始化

            await WebView.EnsureCoreWebView2Async(null);

            // 向網頁提供可呼叫的類別 WebViewClass，第一個參數是名稱，第二個參數是類別實體

            WebView.CoreWebView2.AddHostObjectToScript("webViewClass", _iWebViewHelper);

        }
    }
}
