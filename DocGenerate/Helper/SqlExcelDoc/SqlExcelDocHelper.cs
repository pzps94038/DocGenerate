﻿using DocGenerate.Constants;
using DocGenerate.Interface.SqlExcelDoc;
using DocGenerate.Model.SqlExcelDoc;
using Microsoft.Data.SqlClient;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Text.RegularExpressions;

namespace DocGenerate.Helper.SqlExcelDoc
{
    internal class SqlExcelDocHelper : ISqlExcelDocHelper
    {
        /// <summary>
        /// 產生資料庫規格文件
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        public void CreateDocumentation(
           string connection,
           string fileName,
           string type
           )
        {
            try
            {
                SqlDoc sqlDoc;
                var upperType = type.ToUpper();
                switch (upperType)
                {
                    case DbType.MicrosoftSQLServer:
                        sqlDoc = new MsSqlDoc(connection);
                        break;

                    case DbType.MySQL:
                        sqlDoc = new MySqlDoc(connection);
                        break;

                    case DbType.PostgreSQL:
                        sqlDoc = new NpgSqlDoc(connection);
                        break;

                    default:
                        throw new Exception("不支援的資料庫類型");
                }
                var isExists = File.Exists(fileName);
                if (isExists)
                {
                    File.Delete(fileName);
                }
                IWorkbook workbook = new XSSFWorkbook();
                var databaseSpecifications = sqlDoc.GetDatabaseSpecifications();
                var databaseViewSpecifications = sqlDoc.GetDatabaseViewSpecifications();
                GenerateDatabaseSpecifications(workbook, databaseSpecifications, databaseViewSpecifications);
                var storedProcedureSpecifications = sqlDoc.GetStoredProcedureSpecifications();
                if (storedProcedureSpecifications.Any())
                {
                    GenerateProcedureSpecifications(workbook, storedProcedureSpecifications);
                }
                var triggerSpecifications = sqlDoc.GetTriggerSpecifications();
                if (triggerSpecifications.Any())
                {
                    GenerateTriggerSpecifications(workbook, triggerSpecifications);
                }
                var tableSpecifications = sqlDoc.GetTableSpecifications();
                GenerateDatabaseSpecifications(workbook, tableSpecifications);

                FileStream sw = File.Create(fileName);
                workbook.Write(sw);
                sw.Close();
                sqlDoc.Dispose();
            }
            catch (SqlException ex)
            {
                throw new Exception("資料庫連接異常", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("產生文件異常", ex);
            }
        }

        /// <summary>
        /// 產生資料庫表格清單
        /// </summary>
        public void GenerateDatabaseSpecifications(IWorkbook workbook, IEnumerable<DatabaseSpecifications> databaseSpecifications, IEnumerable<DatabaseSpecifications> databaseViewSpecifications)
        {
            var sheet = workbook.CreateSheet("表格清單目錄");
            // 表頭
            var headerRow = sheet.CreateRow(0);
            var headerStyle = new CellStyle();
            headerStyle.FontHeightInPoints = 12;
            headerStyle.IsBold = true;
            headerStyle.FontColor = IndexedColors.White.Index;
            headerStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            headerRow.CreateStyleCell(0, headerStyle).SetCellValue("項次");
            headerRow.CreateStyleCell(1, headerStyle).SetCellValue("表格名稱");
            headerRow.CreateStyleCell(2, headerStyle).SetCellValue("描述");

            int i = 1;
            foreach (var item in databaseSpecifications)
            {
                var row = sheet.CreateRow(i);
                i++;
                row.CreateStyleCell(0).SetCellValue(i - 1);
                var hyperlinkStyle = new CellStyle();
                hyperlinkStyle.FontColor = IndexedColors.Blue.Index;
                hyperlinkStyle.Underline = FontUnderlineType.Single;
                var tableNameCell = row.CreateStyleCell(1, hyperlinkStyle);
                var hyperlink = new XSSFHyperlink(HyperlinkType.Document)
                {
                    Address = $"'{ShortenTableName(item.TableName)}'!A1"
                };
                tableNameCell.SetCellValue((item.TableName as string) ?? "");
                tableNameCell.Hyperlink = hyperlink;
                row.CreateStyleCell(2).SetCellValue((item.Description as string) ?? "");
            }

            foreach (var item in databaseViewSpecifications)
            {
                var row = sheet.CreateRow(i);
                i++;
                row.CreateStyleCell(0).SetCellValue(i - 1);
                row.CreateStyleCell(1).SetCellValue((item.TableName as string) ?? "");
                row.CreateStyleCell(2).SetCellValue((item.Description as string) ?? "");
            }
            CellRangeAddress filterRange = new CellRangeAddress(0, i, 0, 2);

            // 在工作表上設置自動篩選的範圍
            sheet.SetAutoFilter(filterRange);
            sheet.AutoSheetSize(3);
        }

        /// <summary>
        /// 產生資料庫表格細項
        /// </summary>
        public void GenerateDatabaseSpecifications(IWorkbook workbook, IEnumerable<TableSpecifications> tableSpecifications)
        {
            var headerStyle = new CellStyle();
            headerStyle.FontHeightInPoints = 12;
            headerStyle.IsBold = true;
            headerStyle.FontColor = IndexedColors.White.Index;
            headerStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            var group = tableSpecifications.GroupBy(a => a.TableName);
            foreach (var keyPair in group)
            {
                var name = ShortenTableName(keyPair.Key);
                var sheet = workbook.CreateSheet(name);
                var titleRow = sheet.CreateRow(0);
                var titleCellStyle = new CellStyle();
                titleCellStyle.IsBold = true;
                titleRow.CreateStyleCell(0, titleCellStyle).SetCellValue("表格名稱");
                titleRow.CreateStyleCell(1, titleCellStyle).SetCellValue(keyPair.Key);
                var headerRow = sheet.CreateRow(1);
                var headerCellIdx = 0;
                headerRow.CreateStyleCell(headerCellIdx, headerStyle).SetCellValue("項次");
                headerCellIdx++;
                headerRow.CreateStyleCell(headerCellIdx, headerStyle).SetCellValue("欄位名稱");
                headerCellIdx++;
                headerRow.CreateStyleCell(headerCellIdx, headerStyle).SetCellValue("型態");
                headerCellIdx++;
                headerRow.CreateStyleCell(headerCellIdx, headerStyle).SetCellValue("長度");
                headerCellIdx++;
                headerRow.CreateStyleCell(headerCellIdx, headerStyle).SetCellValue("NOT NULL");
                headerCellIdx++;
                headerRow.CreateStyleCell(headerCellIdx, headerStyle).SetCellValue("FOREIGN KEY");
                headerCellIdx++;
                headerRow.CreateStyleCell(headerCellIdx, headerStyle).SetCellValue("描述");
                int i = 2;
                var lastColIdx = 0;
                foreach (var item in keyPair.ToList())
                {
                    var row = sheet.CreateRow(i);
                    i++;
                    var isPrimaryKey = item.IsPrimaryKey == "Y";
                    var cellStyle = new CellStyle();
                    var referencedTableNameCellStyle = new CellStyle();
                    if (isPrimaryKey)
                    {
                        cellStyle.FillForegroundColor = IndexedColors.Yellow.Index;
                        referencedTableNameCellStyle.FillForegroundColor = IndexedColors.Yellow.Index;
                    }
                    var cellIdx = 0;
                    row.CreateStyleCell(cellIdx, cellStyle).SetCellValue(i - 2);
                    cellIdx++;
                    row.CreateStyleCell(cellIdx, cellStyle).SetCellValue((item.ColumnName as string) ?? "");
                    cellIdx++;
                    row.CreateStyleCell(cellIdx, cellStyle).SetCellValue((item.DataType as string).ToUpper() ?? "");
                    cellIdx++;
                    var len = ((item.Length as string) ?? "") == "-1" ? "MAX" : ((item.Length as string) ?? "");
                    row.CreateStyleCell(cellIdx, cellStyle).SetCellValue(len);
                    cellIdx++;
                    row.CreateStyleCell(cellIdx, cellStyle).SetCellValue((item.NotNull as string) ?? "");
                    cellIdx++;
                    row.CreateStyleCell(cellIdx, cellStyle).SetCellValue((item.IsForeignKey as string) ?? "");
                    cellIdx++;
                    row.CreateStyleCell(cellIdx, cellStyle).SetCellValue((item.Description as string) ?? "");
                    if (cellIdx > lastColIdx)
                    {
                        lastColIdx = cellIdx;
                    }
                }
                CellRangeAddress filterRange = new CellRangeAddress(1, i, 0, lastColIdx);

                // 在工作表上設置自動篩選的範圍
                sheet.SetAutoFilter(filterRange);
                sheet.AutoSheetSize(lastColIdx);
            }
        }

        /// <summary>
        /// 產生預存程序細項
        /// </summary>
        public void GenerateProcedureSpecifications(IWorkbook workbook, IEnumerable<ProcedureSpecifications> storedProcedureSpecifications)
        {
            var headerStyle = new CellStyle();
            headerStyle.FontHeightInPoints = 12;
            headerStyle.IsBold = true;
            headerStyle.FontColor = IndexedColors.White.Index;
            headerStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            var sheet = workbook.CreateSheet("預存程序清單目錄");
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateStyleCell(0, headerStyle).SetCellValue("項次");
            headerRow.CreateStyleCell(1, headerStyle).SetCellValue("預存程序名稱");
            headerRow.CreateStyleCell(2, headerStyle).SetCellValue("描述");
            int i = 1;
            foreach (var item in storedProcedureSpecifications)
            {
                var row = sheet.CreateRow(i);
                i++;
                row.CreateStyleCell(0).SetCellValue(i - 1);
                row.CreateStyleCell(1).SetCellValue((item.ProcedureName as string) ?? "");
                row.CreateStyleCell(2).SetCellValue((item.Description as string) ?? "");
            }
            CellRangeAddress filterRange = new CellRangeAddress(0, i, 0, 2);
            // 在工作表上設置自動篩選的範圍
            sheet.SetAutoFilter(filterRange);
            sheet.AutoSheetSize(2);
        }

        /// <summary>
        /// 產生預存程序細項
        /// </summary>
        public void GenerateTriggerSpecifications(IWorkbook workbook, IEnumerable<TriggerSpecifications> triggerSpecifications)
        {
            var sheet = workbook.CreateSheet("Trigger清單目錄");
            var headerRow = sheet.CreateRow(0);
            var headerStyle = new CellStyle();
            headerStyle.FontHeightInPoints = 12;
            headerStyle.IsBold = true;
            headerStyle.FontColor = IndexedColors.White.Index;
            headerStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            headerRow.CreateStyleCell(0, headerStyle).SetCellValue("項次");
            headerRow.CreateStyleCell(1, headerStyle).SetCellValue("觸發表格名稱");
            headerRow.CreateStyleCell(2, headerStyle).SetCellValue("Trigger名稱");
            headerRow.CreateStyleCell(3, headerStyle).SetCellValue("TypeDesc");
            int i = 1;
            foreach (var item in triggerSpecifications)
            {
                var row = sheet.CreateRow(i);
                i++;
                row.CreateStyleCell(0).SetCellValue(i - 1);
                row.CreateStyleCell(1).SetCellValue((item.TableName as string) ?? "");
                row.CreateStyleCell(2).SetCellValue((item.TriggerName as string) ?? "");
                row.CreateStyleCell(3).SetCellValue((item.TypeDesc as string) ?? "");
            }
            CellRangeAddress filterRange = new CellRangeAddress(0, i, 0, 3);
            // 在工作表上設置自動篩選的範圍
            sheet.SetAutoFilter(filterRange);
            sheet.AutoSheetSize(3);
        }

        public static string ShortenTableName(string tableName)
        {
            // 直接處理表格名稱，無前綴
            string shortenedName = tableName;

            // 使用首字母縮寫來縮短名稱
            if (shortenedName.Length > 31)
            {
                shortenedName = GetAcronym(shortenedName);
            }

            // 確保名稱不超過31字符，必要時截斷
            if (shortenedName.Length > 31)
            {
                shortenedName = shortenedName.Substring(0, 31);
            }

            return shortenedName;
        }

        private static string GetAcronym(string name)
        {
            // 取得每個單詞的首字母並組合
            string[] words = Regex.Split(name, @"(?<!^)(?=[A-Z])");
            string acronym = "";
            foreach (var word in words)
            {
                acronym += word[0];
            }
            return acronym;
        }
    }
}