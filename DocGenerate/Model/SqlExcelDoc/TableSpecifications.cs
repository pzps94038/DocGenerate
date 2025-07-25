﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Model.SqlExcelDoc
{
    public class TableSpecifications
    {
        /// <summary>
        /// 表格名稱
        /// </summary>
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;

        /// <summary>
        /// 資料格式
        /// </summary>
        public string DataType { get; set; } = string.Empty;

        /// <summary>
        /// 是否不為NULL Y or ""
        /// </summary>
        public string NotNull { get; set; } = string.Empty;

        /// <summary>
        /// 長度限制
        /// </summary>
        public string? Length { get; set; }

        /// <summary>
        /// 是否為主鍵 Y/N
        /// </summary>
        public string IsPrimaryKey { get; set; } = string.Empty;

        /// <summary>
        /// 是否為外鍵 Y/N
        /// </summary>
        public string IsForeignKey { get; set; } = string.Empty;

        /// <summary>
        /// 說明
        /// </summary>
        public string? Description { get; set; }
    }
}