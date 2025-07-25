﻿using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Model.SqlExcelDoc
{
    public class MsSqlDoc : SqlDoc
    {
        private SqlConnection _connection;

        public MsSqlDoc(string connectionString) : base(connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public override IEnumerable<DatabaseSpecifications> GetDatabaseSpecifications()
        {
            // 一般表格
            var sql = @"SELECT
							O.name as TableName,
							O.type_desc		AS [Type],
							P.value			AS [Description]
						FROM
							sys.objects AS O
							LEFT JOIN sys.schemas AS S on S.schema_id = O.schema_id
							LEFT JOIN sys.extended_properties AS P ON P.major_id = O.object_id AND P.minor_id = 0 and P.name = 'MS_Description'
						WHERE
							is_ms_shipped = 0
							AND parent_object_id = 0
							AND type_desc = 'USER_TABLE'
							ORDER BY TableName";
            var result = _connection.Query<DatabaseSpecifications>(sql);
            return result;
        }

        public override IEnumerable<DatabaseSpecifications> GetDatabaseViewSpecifications()
        {
            // 檢視表
            var sql = @"SELECT
							O.name AS TableName,
							O.type_desc		AS [Type],
							P.value			AS [Description]
						FROM
							sys.objects AS O
						LEFT JOIN sys.schemas AS S on S.schema_id = O.schema_id
						LEFT JOIN sys.extended_properties AS P ON P.major_id = O.object_id AND P.minor_id = 0 and P.name = 'MS_Description'
						WHERE
							is_ms_shipped = 0
							AND parent_object_id = 0
							AND type_desc = 'VIEW'
							ORDER BY TableName";
            var result = _connection.Query<DatabaseSpecifications>(sql);
            return result;
        }

        public override IEnumerable<ProcedureSpecifications> GetStoredProcedureSpecifications()
        {
            // 預存
            var sql = @"SELECT
							p.name as ProcedureName,
							ep.value AS Description
						FROM
							sys.procedures p
						LEFT JOIN
							sys.extended_properties ep ON p.object_id = ep.major_id
							AND ep.minor_id = 0
							AND ep.name = 'MS_Description'
						WHERE
							p.is_ms_shipped = 0 AND ep.major_id IS NOT NULL
						ORDER BY
							ProcedureName;";
            var result = _connection.Query<ProcedureSpecifications>(sql);
            return result;
        }

        public override IEnumerable<TableSpecifications> GetTableSpecifications()
        {
            var sql = @"SELECT
							t.TABLE_NAME AS TableName,
							c.COLUMN_NAME AS ColumnName,
							c.DATA_TYPE AS DataType,
							CASE
								WHEN c.IS_NULLABLE = 'YES' THEN 'N'
								WHEN c.IS_NULLABLE = 'NO' THEN 'Y'
								ELSE 'N'
							END AS NotNull,
							c.CHARACTER_MAXIMUM_LENGTH AS Length,
							ISNULL(ep.value, '') AS Description,
							MAX(CASE WHEN k.CONSTRAINT_TYPE = 'PRIMARY KEY' THEN 'Y' ELSE 'N' END) AS IsPrimaryKey,
							MAX(CASE WHEN k.CONSTRAINT_TYPE = 'FOREIGN KEY' THEN 'Y' ELSE 'N' END) AS IsForeignKey
						FROM
							INFORMATION_SCHEMA.TABLES t
						INNER JOIN
							INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_SCHEMA = c.TABLE_SCHEMA AND t.TABLE_NAME = c.TABLE_NAME
						LEFT JOIN
							sys.extended_properties ep ON ep.major_id = OBJECT_ID(t.TABLE_SCHEMA + '.' + t.TABLE_NAME) AND c.COLUMN_NAME = COL_NAME(ep.major_id, ep.minor_id) AND ep.name = 'MS_Description'
						LEFT JOIN
							INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu ON c.TABLE_SCHEMA = ccu.TABLE_SCHEMA AND c.TABLE_NAME = ccu.TABLE_NAME AND c.COLUMN_NAME = ccu.COLUMN_NAME
						LEFT JOIN
							INFORMATION_SCHEMA.TABLE_CONSTRAINTS k ON ccu.CONSTRAINT_SCHEMA = k.CONSTRAINT_SCHEMA AND ccu.CONSTRAINT_NAME = k.CONSTRAINT_NAME
						WHERE
							t.TABLE_TYPE = 'BASE TABLE'
						GROUP BY
							t.TABLE_SCHEMA, t.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE, c.IS_NULLABLE, c.CHARACTER_MAXIMUM_LENGTH, ep.value
						ORDER BY
							TableName, IsPrimaryKey DESC, IsForeignKey DESC, ColumnName
			";
            var result = _connection.Query<TableSpecifications>(sql);
            return result;
        }

        public override IEnumerable<TriggerSpecifications> GetTriggerSpecifications()
        {
            var sql = @"SELECT
							t.name AS TableName,
							tr.name AS TriggerName,
							tr.type_desc AS TypeDesc
							FROM
								sys.triggers tr
							INNER JOIN
								sys.tables t ON tr.parent_id = t.object_id
							INNER JOIN
								sys.schemas s ON t.schema_id = s.schema_id
							ORDER BY
								TableName,
								TriggerName;
						";
            var result = _connection.Query<TriggerSpecifications>(sql);
            return result;
        }

        public override void Dispose()
        {
            _connection.Dispose();
        }
    }
}