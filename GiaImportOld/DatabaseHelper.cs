using System.Data;
using System.Data.SqlClient;

namespace RBD.Client.Services.Import.Bulk.Common
{
    public class DatabaseHelper
    {
        public static bool IsDataTableExists(string connectionString, string tableName)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand
            {
                Connection = connection,
                CommandTimeout = 0,
                CommandType = CommandType.Text,
                CommandText = string.Format(@"
                    SELECT COUNT(*)
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = '{0}'", tableName)
            })
            {
                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }

        public static bool IsColumnExists(string connectionString,  string tableName, string columnName)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand
            {
                Connection = connection,
                CommandTimeout = 0,
                CommandType = CommandType.Text,
                CommandText = string.Format(@"
                    SELECT COUNT(*)
                    FROM sys.columns
                    WHERE object_id = OBJECT_ID('{0}')
                        and Name = '{1}'", 
                    tableName,
                    columnName)
            })
            {
                connection.Open();
                return (int)command.ExecuteScalar() > 0;
            }
        }
    }
}
