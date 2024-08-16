using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocGenerate.Helper
{
    public class LogHelper : ILogHelper
    {
        private readonly ILogger<LogHelper> _logger;
        public LogHelper(ILogger<LogHelper> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 記錄錯誤
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            _logger.LogError(ex, $"程式發生錯誤：{ex.Message}");
        }

        /// <summary>
        /// 紀錄資訊
        /// </summary>
        /// <param name="msg"></param>
        public void Info(string msg)
        {
            _logger.LogInformation(msg);
        }
    }
}
