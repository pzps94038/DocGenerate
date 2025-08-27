using DocGenerate.Interface.APIDoc;
using System.Text;

namespace DocGenerate.Helper.APIDoc
{
    public class WebViewHelper : IWebViewHelper
    {
        private readonly ISharedHelper _sharedHelper;

        public WebViewHelper(ISharedHelper sharedHelper)
        {
            _sharedHelper = sharedHelper;
        }

        /// <summary>
        /// 顯示文字資訊
        /// </summary>
        /// <param name="messageBoxTitle"></param>
        /// <param name="messageBoxContent"></param>
        /// <returns></returns>
        public DialogResult ShowInfoMsg(string messageBoxTitle, string messageBoxContent)
        {
            return _sharedHelper.ShowInfoMsg(messageBoxTitle, messageBoxContent);
        }

        /// <summary>
        /// 選擇對應檔案
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filter"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public string ChooseFile(string fileName, string filter, string title)
        {
            var dialog = new OpenFileDialog()
            {
                FileName = fileName,
                Filter = filter,
                Title = title
            };
            dialog.ShowDialog();
            return dialog.FileName;
        }

        /// <summary>
        /// 讀取檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath, Encoding.UTF8);
        }

        /// <summary>
        /// 下載範例檔案
        /// </summary>
        /// <returns></returns>
        public void DownloadExampleFile()
        {
            var basePath = AppContext.BaseDirectory;
            var relativePath = "Asset\\APIDoc";
            var fileName = "example.json";
            var fullPath = Path.Combine(basePath, relativePath, fileName);

            using (var dialog = new SaveFileDialog())
            {
                dialog.FileName = fileName;
                dialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                dialog.Title = "下載範例檔案";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(fullPath, dialog.FileName, true);
                        _sharedHelper.ShowInfoMsg("下載完成", $"範例檔案已儲存至：{dialog.FileName}");
                    }
                    catch (Exception ex)
                    {
                        _sharedHelper.ShowExceptionMessageBox(ex, "下載失敗");
                    }
                }
            }
        }
    }
}