using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Interfaces;
using RBD.Client.Services.Import.Bulk.Common;
using RBD.Client.Services.Import.DataSource;
using RBD.Common.Enums;

namespace RBD.Client.Services.Import.Bulk
{
    public class XmlBulkUploader<TDto> where TDto : DtoBase, new()
    {
        private const int BulkBatchSize = 100000;
        private readonly string _connectionString;
        private readonly XmlBatchReader<TDto> _xmlBacthReader;
        private readonly XmlBulkWriter<TDto> _xmlBulkWriter;

        public XmlBulkUploader(string connectionString)
        {
            _connectionString = connectionString;
            _xmlBacthReader = new XmlBatchReader<TDto>(BulkBatchSize);
            _xmlBulkWriter = new XmlBulkWriter<TDto>(_connectionString);
        }

        /// <summary>
        /// Поступательное чтение и загрузка объектов из файла в БД
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="files">Файлы среди которых будем искать нужный для загрузки в БД файл</param>
        /// <returns>Возвращаем коллекцию объектов которые не были загружены в БД</returns>
        public void Process(IEnumerable<FileInfo> files)
        {
            /* Ищем файл из которого будем грузить */
            var partname = typeof (TDto).GetBulkFileName();
            var file = files.FirstOrDefault(c => 
                c.Name.EndsWith(string.Format("_{0}.xml", partname), StringComparison.InvariantCultureIgnoreCase));
            if (file == null)
            {
                /* Если файла нет - пропускаем загрузку */
                return;
            }

            /* Проверяем есть-ли в БД таблица в кот орую мы собираемся грузить данные */
            var bulkTableName = typeof (TDto).GetBulkTableName();
            if (!DatabaseHelper.IsDataTableExists(_connectionString, bulkTableName))
            {
                //MessageManager.SendWarningMessage(
                //    string.Format("Внимание! В БД отсутствует таблица назначения {0}. Данные не были загружены", bulkTableName));
                return;
            }

            /* Извлекаем и грузим в БД */
            _xmlBacthReader.ProcessFileWithAction(file.FullName, _xmlBulkWriter.BulkWriteToDb);

            /* Извлекаем все ошибочные объекты, которые накопились за время загрузки */
            var source = new ImportSourceFile(Guid.NewGuid(), file, SourceType.Import);
            
            ///* Добавляем пустышку только для того, чтобы иметь возможность выводить кол-во 
            // * удачно загруженных объектов в БД */
            //results.Add(new TImportEntity
            //{
            //    GiaUploadedItemsCount = _xmlBulkWriter.UploadedCount,
            //    Item = new TDto()
            //});

            //if (_xmlBulkWriter.BrokenItems.Count == 0)
            //    return results;
            
            //_xmlBulkWriter.BrokenItems.ForEach(c =>
            //{
            //    var e = ((TDto) c.Dto).ToImportEntity<TDto, TImportEntity>(source);
            //    e.IsBroken = (c.ExcludeType == BrokenDto.ExcludeTypeEnum.Broken);
            //    e.IsExcluded = (c.ExcludeType == BrokenDto.ExcludeTypeEnum.Excluded);
            //    e.AddInfo(c.ErrorMessage);
            //    results.Add(e);
            //});

            /* Сжимаем одинаковые сообщения об ошибках и отображаем их в ui */
            //var errors = new HashSet<string>();
            //errors.UnionWith(_xmlBulkWriter.BrokenItems.Take(1000).Select(c => c.ToString()));
            //errors.ForEach(c => MessageManager.SendWarningMessage(c));
            //if (_xmlBulkWriter.BrokenItems.Count > 1000)
            //{
            //    MessageManager.SendExceptionMessage(
            //        "Внимание! Слишком большое кол-во ошибок! Вывод в лог остановлен на первой 1000 замечаний", null);
            //}
            //return results;
        }

    }
}