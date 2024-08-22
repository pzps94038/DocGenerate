using DocGenerate.Model.SqlExcelDoc;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Interface.SqlExcelDoc
{
    public interface ISqlExcelDocHelper
    {
        public void CreateDocumentation(string connection, string fileName, string type);
        public void GenerateDatabaseSpecifications(IWorkbook workbook, IEnumerable<DatabaseSpecifications> databaseSpecifications, IEnumerable<DatabaseSpecifications> databaseViewSpecifications);
        public void GenerateDatabaseSpecifications(IWorkbook workbook, IEnumerable<TableSpecifications> tableSpecifications);
        public void GenerateProcedureSpecifications(IWorkbook workbook, IEnumerable<ProcedureSpecifications> storedProcedureSpecifications);
        public void GenerateTriggerSpecifications(IWorkbook workbook, IEnumerable<TriggerSpecifications> triggerSpecifications);
    }
}
