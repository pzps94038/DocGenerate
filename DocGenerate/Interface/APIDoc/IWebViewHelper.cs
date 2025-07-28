using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Interface.APIDoc
{
    public interface IWebViewHelper
    {
        /// <summary>
        /// 提示訊息
        /// </summary>
        /// <param name="messageBoxTitle"></param>
        /// <param name="messageBoxContent"></param>
        /// <returns></returns>
        public DialogResult ShowInfoMsg(string messageBoxTitle, string messageBoxContent);

        /// <summary>
        /// 選取檔案
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filter"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public string ChooseFile(string fileName, string filter, string title);

        /// <summary>
        /// 讀取檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string ReadFile(string filePath);

        /// <summary>
        /// 下載範例檔案
        /// </summary>
        /// <returns></returns>
        public void DownloadExampleFile();
    }
}