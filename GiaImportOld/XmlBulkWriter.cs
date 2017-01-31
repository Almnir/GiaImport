using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using FCT.Client.Dto.Common;
using RBD.Client.Interfaces;
using FCT.Client.Dto.Interfaces;

namespace RBD.Client.Services.Import.Bulk
{
    public class XmlBulkWriter<TDto> where TDto : DtoBase, new()
    {
        public int UploadedCount { get; private set; }

        private readonly List<SqlBulkCopyColumnMapping> _bulkColumnMapping = new List<SqlBulkCopyColumnMapping>();
        private readonly string _connectionString;
        private DataTable _bulkDataTable;

        public XmlBulkWriter(string connectionString)
        {
            _connectionString = connectionString;
            InitializeTableAndMapping();
        }

        /// <summary>
        /// Инициализация bulk таблицы и маппинга bcp
        /// </summary>
        private void InitializeTableAndMapping()
        {
            _bulkDataTable = new DataTable(typeof(TDto).Name);
            const BindingFlags propFilter = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance;
            var properties = typeof (TDto).GetProperties(propFilter).ToArray();
            foreach (var propety in properties)
            {
                var bulkedColumn = propety.AsBulkColumn();
                if (bulkedColumn == null) continue;

                /* Маппинг для bulk загрузки */
                _bulkColumnMapping.Add(new SqlBulkCopyColumnMapping(propety.Name, bulkedColumn.DbName ?? propety.Name));
                try
                {
                    var propertyType = propety.PropertyType;
                    if (propertyType.IsGenericType &&
                        propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        propertyType = propertyType.GetGenericArguments()[0];
                    }
                    
                    var column = new DataColumn
                    {
                        DataType = bulkedColumn.DbType ?? propertyType,
                        ColumnName = propety.Name,
                    };
                    _bulkDataTable.Columns.Add(column);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Отправляем Bulk пачку на сервер
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public void BulkWriteToDb(XDocument document)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        SqlBulkCopy bcp;
                        using (bcp = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, tran))
                        {
                            bcp.BulkCopyTimeout = 0;
                            using (var ms = new MemoryStream())
                            {
                                using (var writer = XmlWriter.Create(ms))
                                {
                                    document.WriteTo(writer);
                                }

                                ms.Position = 0;

                                TDto[] items;
                                try
                                {
                                    items = Serializer.Deserialize(ms, typeof(TDto[])) as TDto[];
                                    if (items == null)
                                    {
                                        throw new InvalidCastException();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new ApplicationException(
                                        string.Format("Ошибка десериализации типа {0}", typeof(TDto[]).Name), ex);
                                }

                                //MessageManager.SendInfoMessage(
                                //    string.Format("Загрузка {0} ({1})... ", typeof(TDto).GetDescription(), UploadedCount + items.Length));

                                Fill(bcp, items);
                                bcp.WriteToServer(_bulkDataTable);
                                UploadedCount += _bulkDataTable.Rows.Count;
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        //MessageManager.SendExceptionMessage(ex.Message, ex);
                        throw;
                    }
                }
            }
        }

        void Fill(SqlBulkCopy bcp, IEnumerable<TDto> items)
        {
            _bulkDataTable.Clear();

            /* Заполняем bcp */
            bcp.ColumnMappings.Clear();
            bcp.DestinationTableName = typeof(TDto).GetBulkTableName();
            _bulkColumnMapping.ForEach(mapping => bcp.ColumnMappings.Add(mapping));
        }
    }
}
