﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace GiaImport
{
    public class DatabaseHelper
    {
        public static bool IsDataTableExists(string connectionString, string schemaName, string tableName)
        {
            bool result = false;
            string commandText = string.Format(@"
                    SELECT COUNT(*)
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = @TABLE_NAME AND TABLE_SCHEMA = @TABLE_SCHEMA;");
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(commandText, connection))
            {
                command.Parameters.Add(new SqlParameter("TABLE_SCHEMA", schemaName));
                command.Parameters.Add(new SqlParameter("TABLE_NAME", tableName));

                connection.Open();
                result = (int)command.ExecuteScalar() > 0;
            }
            return result;
        }

        public static void DeleteLoaderTables(string connectionString)
        {
            string sqlTrunc = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    foreach (var table in Globals.TABLES_NAMES)
                    {
                        sqlTrunc = "TRUNCATE TABLE loader." + table;
                        SqlCommand cmd = new SqlCommand(sqlTrunc, con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TruncateException(string.Format("При выполнении {0}, ошибка {1}", sqlTrunc, ex.ToString()));
            }
        }

        internal static bool CheckConnection()
        {
            bool result = false;
            try
            {
                using (var connection = new SqlConnection(Globals.GetConnectionString()))
                {
                    var query = "select 1";
                    var command = new SqlCommand(query, connection);
                    connection.Open();
                    command.ExecuteScalar();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
