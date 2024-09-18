using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Interface.APIDoc
{
    public interface IWebViewHelper
    {
        public DialogResult ShowInfoMsg(string messageBoxTitle, string messageBoxContent);
        public string ChooseFile(string fileName, string filter, string title);
        public string ReadFile(string filePath);
    }
}
