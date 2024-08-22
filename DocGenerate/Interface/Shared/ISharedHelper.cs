using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Helper
{
    public interface ISharedHelper
    {
        public DialogResult ShowExceptionMessageBox(Exception ex, string messageBoxTitle = "錯誤", string messagePrefix = "", string messageSuffix = "", MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK, MessageBoxIcon messageBoxIcon = MessageBoxIcon.Error);
        public DialogResult ShowInfoMsg(string messageBoxTitle, string messageBoxContent);
    }
}
