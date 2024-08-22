using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Model.SqlExcelDoc
{
    public abstract class SqlDoc : IDisposable
    {
        protected string _connectionString;
        public SqlDoc(string connectionString)
        {
            _connectionString = connectionString;
        }

        public abstract void Dispose();

        /// <summary>
        /// 一般表格
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<DatabaseSpecifications> GetDatabaseSpecifications();

        /// <summary>
        /// 檢視表
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<DatabaseSpecifications> GetDatabaseViewSpecifications();

        /// <summary>
        /// 預存程序
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<ProcedureSpecifications> GetStoredProcedureSpecifications();

        /// <summary>
        /// 表格清單
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TableSpecifications> GetTableSpecifications();

        /// <summary>
        /// Trigger
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<TriggerSpecifications> GetTriggerSpecifications();
    }
}
