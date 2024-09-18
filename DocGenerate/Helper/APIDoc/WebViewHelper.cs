using DocGenerate.Interface.APIDoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
