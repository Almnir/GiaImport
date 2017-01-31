using System;
using System.Linq;
using System.Reflection;

namespace FCT.Client.Dto.Common
{
    public class BulkColumnAttribute : Attribute
    {
        public string DbName { get; set; }
        public Type DbType { get; set; }

        public BulkColumnAttribute() {}
        public BulkColumnAttribute(string dbName)
        {
            DbName = dbName;
        }

        public BulkColumnAttribute(string dbName, Type dbType)
        {
            DbName = dbName;
            DbType = dbType;
        }
    }

    public class BulkTableAttribute : Attribute
    {
        public string TableName { get; set; }
        public string FileName { get; set; }
        public string RootTagName { get; set; }
        public bool ExportExclude { get; set; }
        /// <summary>
        /// Признак того что таблица относится к результатам
        /// </summary>
        public bool IsResTable { get; set; }

        
        public BulkTableAttribute() {}
        public BulkTableAttribute(string tableName)
        {
            TableName = tableName;
        }

        public BulkTableAttribute(string tableName, string fileName)
        {
            TableName = tableName;
            FileName = fileName;
        }
    }

    public static class Extensions
    {
        public static BulkColumnAttribute AsBulkColumn(this PropertyInfo pi)
        {
            return pi.GetCustomAttributes(typeof (BulkColumnAttribute), true)
              .FirstOrDefault() as BulkColumnAttribute;
        }

        public static string GetBulkTableName(this Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(BulkTableAttribute), true)
              .FirstOrDefault() as BulkTableAttribute;
            return attribute != null ? attribute.TableName : string.Empty;
        }

        public static string GetBulkFileName(this Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(BulkTableAttribute), true)
              .FirstOrDefault() as BulkTableAttribute;
            return attribute != null ? attribute.FileName : string.Empty;
        }

        public static string GetBulkRootTagName(this Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(BulkTableAttribute), true)
              .FirstOrDefault() as BulkTableAttribute;
            return attribute != null ? attribute.RootTagName : string.Empty;
        }
    }
}
