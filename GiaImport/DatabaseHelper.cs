using DataModels;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

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
                using (var connection = new SqlConnection(connectionString))
                {
                    foreach (var table in BulkManager.tablesList)
                    {
                        sqlTrunc = "TRUNCATE TABLE loader." + table;
                        SqlCommand cmd = new SqlCommand(sqlTrunc, connection);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TruncateException(string.Format("При выполнении {0}, ошибка {1}", sqlTrunc, ex));
            }
        }
    }
}
