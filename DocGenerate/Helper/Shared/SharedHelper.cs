using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Helper
{
    public class SharedHelper : ISharedHelper
    {
        private readonly ILogHelper _logHelper;
        public SharedHelper(ILogHelper logHelper)
        {
            _logHelper = logHelper;
        }

        /// <summary>
        /// 跳出錯誤視窗
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="messageBoxTitle"></param>
        /// <param name="messagePrefix"></param>
        /// <param name="messageSuffix"></param>
        /// <param name="messageBoxButtons"></param>
        /// <param name="messageBoxIcon"></param>
        /// <returns></returns>
        public DialogResult ShowExceptionMessageBox(Exception ex, string messageBoxTitle = "錯誤", string messagePrefix = "", string messageSuffix = "", MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK, MessageBoxIcon messageBoxIcon = MessageBoxIcon.Error)
        {
            messagePrefix = messagePrefix.Trim();
            messageSuffix = messageSuffix.Trim();
            //格式化訊息
            if (!string.IsNullOrEmpty(messagePrefix))
            {
                if (!messagePrefix.EndsWith('：'))
                {
                    messagePrefix = $"{messagePrefix}：";
                }
                messagePrefix += "\r\n";
            }
            if (!string.IsNullOrEmpty(messageSuffix))
            {
                messageSuffix = $"\r\n{messageSuffix}";
            }
            string message = $"{messagePrefix}{GetFullExceptionString(ex, "錯誤：\r\n")}{messageSuffix}";
            _logHelper.Error(ex);
            return MessageBox.Show(message, messageBoxTitle, messageBoxButtons, messageBoxIcon);

        }

        /// <summary>
        /// 完整錯誤資訊
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="outputMessage"></param>
        /// <returns></returns>
        public string GetFullExceptionString(Exception ex, string outputMessage)
        {
            outputMessage += $"{ex.GetType().FullName}：{ex.Message}\r\n{ex.StackTrace ?? "(沒有堆疊追蹤)"}\r\n";
            if (ex.InnerException is not null)
            {
                outputMessage += "\r\n內部錯誤：\r\n";
                outputMessage += GetFullExceptionString(ex.InnerException, "");
            }
            return outputMessage;
        }
    }
}
