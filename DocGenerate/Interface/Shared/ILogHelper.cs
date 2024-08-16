using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Helper
{
    public interface ILogHelper
    {
        public void Info(string msg);
        public void Error(Exception ex);
    }
}
