﻿using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerate.Model.SqlExcelDoc
{
    public class NpgSqlDoc : SqlDoc
    {
        private NpgsqlConnection _connection;

        public NpgSqlDoc(string connectionString) : base(connectionString)
        {
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
        }

        public override IEnumerable<DatabaseSpecifications> GetDatabaseSpecifications()
        {
            // 一般表格
            var sql = @"SELECT
							c.relname AS ""TableName"",
							c.relkind AS ""Type"",
							d.description AS ""Description""
						FROM
							pg_class c
							LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
							LEFT JOIN pg_description d ON d.objoid = c.oid AND d.objsubid = 0
						WHERE
							c.relkind = 'r'  -- 'r' 表示普通表 (relation)
							AND n.nspname NOT IN ('pg_catalog', 'information_schema')  -- 排除系統表
						ORDER BY
							c.relname;";
            var result = _connection.Query<DatabaseSpecifications>(sql);
            return result;
        }

        public override IEnumerable<DatabaseSpecifications> GetDatabaseViewSpecifications()
        {
            // 檢視表
            var sql = @"SELECT
							c.relname AS ""TableName"",
							c.relkind AS ""Type"",
							d.description AS ""Description""
						FROM
							pg_class c
							LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
							LEFT JOIN pg_description d ON d.objoid = c.oid AND d.objsubid = 0
						WHERE
							c.relkind = 'v'  -- 'v' 表示檢視 (view)
							AND n.nspname NOT IN ('pg_catalog', 'information_schema')  -- 排除系統檢視
						ORDER BY
							c.relname;";
            var result = _connection.Query<DatabaseSpecifications>(sql);
            return result;
        }

        public override IEnumerable<ProcedureSpecifications> GetStoredProcedureSpecifications()
        {
            //  預存
            var sql = @"SELECT
							p.proname AS ProcedureName,
							d.description AS Description
						FROM
							pg_proc p
							LEFT JOIN pg_description d ON d.objoid = p.oid
							LEFT JOIN pg_namespace n ON n.oid = p.pronamespace
						WHERE
							n.nspname NOT IN ('pg_catalog', 'information_schema')  -- 排除系統定義的存儲過程
							AND d.description IS NOT NULL  -- 過濾掉沒有描述的存儲過程
						ORDER BY
							ProcedureName;
						";
            var result = _connection.Query<ProcedureSpecifications>(sql);
            return result;
        }

        public override IEnumerable<TableSpecifications> GetTableSpecifications()
        {
            var sql = @"SELECT
                            t.table_name AS ""TableName"",
                            c.column_name AS ""ColumnName"",
                            CASE
                                WHEN c.data_type = 'character varying' THEN 'VARCHAR'
                                WHEN c.data_type = 'character' THEN 'CHAR'
                                WHEN c.data_type = 'text' THEN 'TEXT'
                                WHEN c.data_type = 'boolean' THEN 'BIT'
                                WHEN c.data_type = 'smallint' THEN 'SMALLINT'
                                WHEN c.data_type = 'integer' THEN 'INT'
                                WHEN c.data_type = 'bigint' THEN 'BIGINT'
                                WHEN c.data_type = 'decimal' THEN 'DECIMAL'
                                WHEN c.data_type = 'numeric' THEN 'DECIMAL'
                                WHEN c.data_type = 'real' THEN 'FLOAT'
                                WHEN c.data_type = 'double precision' THEN 'FLOAT'
                                WHEN c.data_type = 'money' THEN 'MONEY'
                                WHEN c.data_type = 'date' THEN 'DATE'
                                WHEN c.data_type = 'timestamp' THEN 'DATETIME'
                                WHEN c.data_type = 'timestamp without time zone' THEN 'DATETIME'
                                WHEN c.data_type = 'time' THEN 'TIME'
                                WHEN c.data_type = 'interval' THEN 'TIME'
                                WHEN c.data_type = 'uuid' THEN 'UNIQUEIDENTIFIER'
                                WHEN c.data_type = 'bytea' THEN 'BINARY'
                                WHEN c.data_type = 'timestamp with time zone' THEN 'DATETIME'
                                ELSE c.data_type
                            END AS ""DataType"",
                            CASE
                                WHEN c.is_nullable = 'YES' THEN 'N'
                                WHEN c.is_nullable = 'NO' THEN 'Y'
                                ELSE 'N'
                            END AS ""NotNull"",
                            CASE
                                WHEN c.data_type IN ('character varying', 'varchar', 'character', 'char', 'text') THEN
                                    c.character_maximum_length::TEXT  -- 將數字轉為字串
                                WHEN c.data_type IN ('numeric', 'decimal') THEN
                                    CONCAT(c.numeric_precision, ',', c.numeric_scale)
                                ELSE
                                    NULL
                            END AS ""Length"",
                            COALESCE(ep.description, '') AS ""Description"",
                            MAX(CASE WHEN k.constraint_type = 'PRIMARY KEY' THEN 'Y' ELSE 'N' END) AS ""IsPrimaryKey"",
                            MAX(CASE WHEN k.constraint_type = 'FOREIGN KEY' THEN 'Y' ELSE 'N' END) AS ""IsForeignKey""
                        FROM
                            information_schema.tables t
                        INNER JOIN
                            information_schema.columns c ON t.table_schema = c.table_schema AND t.table_name = c.table_name
                        LEFT JOIN
                            pg_catalog.pg_description ep ON ep.objoid = (SELECT c.oid FROM pg_catalog.pg_class c WHERE c.relname = t.table_name LIMIT 1)
                            AND ep.objsubid = (SELECT ordinal_position FROM information_schema.columns WHERE table_name = c.table_name AND column_name = c.column_name LIMIT 1)
                        LEFT JOIN
                            information_schema.constraint_column_usage ccu ON c.table_schema = ccu.table_schema AND c.table_name = ccu.table_name AND c.column_name = ccu.column_name
                        LEFT JOIN
                            information_schema.table_constraints k ON ccu.constraint_schema = k.constraint_schema AND ccu.constraint_name = k.constraint_name
                        WHERE t.table_schema NOT IN ('pg_catalog', 'information_schema') and t.table_type = 'BASE TABLE'
                        GROUP BY
                            t.table_schema, t.table_name, c.column_name, c.data_type, c.is_nullable, c.character_maximum_length,
                            c.numeric_precision, c.numeric_scale, ep.description
                        ORDER BY
                            ""TableName"", ""IsPrimaryKey"" DESC, ""IsForeignKey"" DESC, ""ColumnName"";
            ";
            var result = _connection.Query<TableSpecifications>(sql);
            return result;
        }

        public override IEnumerable<TriggerSpecifications> GetTriggerSpecifications()
        {
            var sql = @"SELECT
							c.relname AS ""TableName"",
							t.tgname AS ""TriggerName"",
							t.tgtype AS ""TypeDesc""
						FROM
							pg_trigger t
							INNER JOIN pg_class c ON c.oid = t.tgrelid
							INNER JOIN pg_namespace n ON n.oid = c.relnamespace
						WHERE
							NOT t.tgisinternal  -- 排除內部觸發器
						ORDER BY
							""TableName"",
							""TriggerName"";
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