using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FCT.Client.Dto;
using FCT.Client.Dto.Common;
using FCT.Client.Dto.Consolidation;
using RBD.Client.Interfaces;
using RBD.Client.Services.Import.Bulk.Linker;
using RBD.Client.Services.Import.Bulk.SQL;
using RBD.Client.Services.Import.Common.Entities.ImportEntities;
using RBD.Client.Services.Import.DataSource;
using RBD.Client.Services.Import.ImportWizard.Base;
using RBD.Client.Services.Import.Messaging;
using RBD.Common.Enums;

namespace RBD.Client.Services.Import.Bulk.Executors
{
    public class GiaDataBulkUploader
    {
        private readonly string _connectionString;
        public GiaDataBulkUploader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void WriteRegionDataToDb(FileInfo zip)
        {
            var unzipped = ExtractFiles(zip);
            var deserialized = CreateDeserializedData(zip);
            var regionCode = ExtractRegionCode(unzipped);

            if (regionCode != Key.RegionCode)
            {
                var message = string.Format("Текущий код региона: {0}. Загружаемый архив принадлежит региону: {1}", Key.RegionCode, regionCode);
                MessageManager.SendExceptionMessage(message, null);
                throw new ApplicationException(message);
            }

            /*
             * XSD валидация файлов из архива 
             */
            var validationResult = XmlValidator.ValidateBySchema(unzipped);
            if (validationResult != null)
            {
                MessageManager.SendExceptionMessage("Импорт завершен с ошибками валидации схемы XSD", null);
                throw new ApplicationException(validationResult.ErrorMessage);
            }

            /*
             * Удаляем все данные по региону из БД
             */
            DeleteAllRegionDataFromDb(regionCode);

            /* Последовательный линковщих данных извлекаемых из XML файла */
            MessageManager.SendInfoMessage("Загрузка данных... ");
            var sw = new Stopwatch();
            sw.Start();

            deserialized.Data.CurrentRegions = new XmlBulkUploader<CurrentRegionDto>(_connectionString).Process(unzipped);
            deserialized.Data.Governments = new XmlBulkUploader<GovernmentsDto>(_connectionString).Process(unzipped);

            deserialized.Data.Areas = new XmlBulkUploader<AreasDto>(_connectionString).Process(unzipped);
            deserialized.Data.Schools = new XmlBulkUploader<SchoolsDto>(_connectionString).Process(unzipped);
            deserialized.Data.Stations = new XmlBulkUploader<StationsDto>(_connectionString).Process(unzipped);
            deserialized.Data.Auditoriums = new XmlBulkUploader<AuditoriumsDto>(_connectionString).Process(unzipped);
            deserialized.Data.Places = new XmlBulkUploader<PlacesDto>(_connectionString).Process(unzipped);
            deserialized.Data.StationsExams = new XmlBulkUploader<StationsExamsDto>(_connectionString).Process(unzipped);
            deserialized.Data.StationExamAuditory = new XmlBulkUploader<StationExamAuditoryDto>(_connectionString).Process(unzipped);

            deserialized.Data.SheetsC = new XmlBulkUploader<SheetsCDto>(_connectionString).Process(unzipped);
            deserialized.Data.Complects = new XmlBulkUploader<ComplectsDto>(_connectionString).Process(unzipped);
            deserialized.Data.Experts = new XmlBulkUploader<ExpertsDto>(_connectionString).Process(unzipped);
            deserialized.Data.ExpertsExams = new XmlBulkUploader<ExpertsExamsDto>(_connectionString).Process(unzipped);
            deserialized.Data.Alts = new XmlBulkUploader<AltsDto>(_connectionString).Process(unzipped);
            deserialized.Data.MarksC = new XmlBulkUploader<MarksCDto>(_connectionString).Process(unzipped);
            deserialized.Data.FinalMarksC = new XmlBulkUploader<FinalMarksCDto>(_connectionString).Process(unzipped);

            deserialized.Data.Participants = new XmlBulkUploader<ParticipantsDto>(_connectionString).Process(unzipped);
            deserialized.Data.ParticipantsExams = new XmlBulkUploader<ParticipantsExamsDto>(_connectionString).Process(unzipped);
            deserialized.Data.ParticipantsExamsOnStation = new XmlBulkUploader<ParticipantsExamsOnStationDto>(_connectionString).Process(unzipped);
            deserialized.Data.ParticipantProperties = new XmlBulkUploader<ParticipantPropertiesDto>(_connectionString).Process(unzipped);
            deserialized.Data.ParticipantsExamPlacesOnStation = new XmlBulkUploader<ParticipantsExamPlacesOnStationDto>(_connectionString).Process(unzipped);
            deserialized.Data.PrnfCertificatePrintMain = new XmlBulkUploader<PrnfCertificatePrintMainDto>(_connectionString).Process(unzipped);
            deserialized.Data.HumanTests = new XmlBulkUploader<HumanTestsDto>(_connectionString).Process(unzipped);

            // Создаем XX_StationWorkerOnStation.xml на основании XX_StationWorkerOnExam.xml
            unzipped = CreateStationWorkerOnStationFile(unzipped);

            deserialized.Data.StationWorkers = new XmlBulkUploader<StationWorkersDto>(_connectionString).Process(unzipped);
            deserialized.Data.StationWorkerOnStation = new XmlBulkUploader<StationWorkerOnStationDto>(_connectionString).Process(unzipped);
            deserialized.Data.StationWorkerOnExam = new XmlBulkUploader<StationWorkerOnExamDto>(_connectionString).Process(unzipped);

            deserialized.Data.Appeals = new XmlBulkUploader<AppealsDto>(_connectionString).Process(unzipped);
            deserialized.Data.AppealTasks = new XmlBulkUploader<AppealTasksDto>(_connectionString).Process(unzipped);

            deserialized.Data.Marks = new XmlBulkUploader<MarksDto>(_connectionString).Process(unzipped);
            deserialized.Data.Answers = new XmlBulkUploader<AnswersDto>(_connectionString).Process(unzipped);

            deserialized.Data.DatsGroups = new XmlBulkUploader<DatsGroupsDto>(_connectionString).Process(unzipped);
            deserialized.Data.DatsBorders = new XmlBulkUploader<DatsBordersDto>(_connectionString).Process(unzipped);

            sw.Stop();
            MessageManager.SendInfoMessage(string.Format("Загрузка завершена за {0} сек.", sw.Elapsed.TotalSeconds.ToString("0.00")));

            DataSourceManager.DeserializedSources.Add(deserialized);
        }

        private static FileInfo[] CreateStationWorkerOnStationFile(IEnumerable<FileInfo> unzipped)
        {
            var files = new List<FileInfo>(unzipped);
            var file = files.FirstOrDefault(x => x.Name.EndsWith("StationWorkerOnExam.xml"));
            if (file == null || file.Directory == null) return files.ToArray();
            var swoeFile = CopyStationWorkerOnExamFile(file);
            files.Add(swoeFile);
            return files.ToArray();
        }

        private static FileInfo CopyStationWorkerOnExamFile(FileInfo file)
        {
            if (file.Directory == null)
            {
                throw new ApplicationException("file.Directory is null");
            }
            var fileName = file.Name.Replace("StationWorkerOnExam", "StationWorkerOnStation");
            var fullFileName = Path.Combine(file.Directory.FullName, fileName);

            using (var writer = File.CreateText(fullFileName))
            using (var reader = File.OpenText(file.FullName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        line = line.Replace("StationWorkerOnExamDto", "StationWorkerOnStationDto");
                        writer.WriteLine(line);
                    }
                }
            }

            return new FileInfo(fullFileName);
        }

        private static FileInfo CopyStationWorkerOnExamFileOld(FileInfo file)
        {
            if (file.Directory == null)
            {
                throw new ApplicationException("file.Directory is null");
            }
            var fileName = file.Name.Replace("StationWorkerOnExam", "StationWorkerOnStation");
            var fullFileName = Path.Combine(file.Directory.FullName, fileName);
            var swoeFile = file.CopyTo(fullFileName);
            var content = File.ReadAllText(swoeFile.FullName);
            content = content.Replace("StationWorkerOnExamDto", "StationWorkerOnStationDto");
            File.WriteAllText(swoeFile.FullName, content);
            return swoeFile;
        }

        /* Пытаемся найти файл CurrentRegion.xml и достать из него номер региона */
        private static int ExtractRegionCode(IEnumerable<FileInfo> files)
        {
            var partname = typeof(CurrentRegionDto).GetBulkFileName();
            var file = files.FirstOrDefault(c =>
                c.Name.EndsWith(string.Format("{0}.xml", partname), StringComparison.InvariantCultureIgnoreCase));
            if (file == null)
            {
                MessageManager.SendExceptionMessage("Ошибка формата", null);
                throw new ApplicationException("Не удалось определить регион, не найден файл xx_CurrentRegion.xml");
            }
            var doc = XDocument.Load(file.FullName);
            var region = doc.Descendants("Region").FirstOrDefault();
            if (region == null) throw new ApplicationException("Не удалось определить регион");
            return Int32.Parse(region.Value);
        }

        private static FileInfo[] ExtractFiles(FileInfo zip)
        {
            MessageManager.SendInfoMessage(string.Format("Разархивирование файла {0} ...", zip.Name));

            /* Распаковка */
            FolderService.ClearTempFolder();
            if (!Compress.UnZipFiles(zip.FullName, FolderService.TempFolderPath))
            {
                throw new ApplicationException(string.Format("Не удалось распаковать файл {0}", zip.FullName));
            }

            return new DirectoryInfo(FolderService.TempFolderPath).GetFiles("*.xml");
        }

        private static DeserializedData CreateDeserializedData(FileInfo zip)
        {
            var senderInfo = new SenderInfo
            {
                SenderType = ImportSenderType.GiaDataCollect,
                ExportSettings = { BlockTypeSelections = true }
            };
            return new DeserializedData(new ImportSourceFile(Guid.NewGuid(), zip, SourceType.Import), senderInfo);
        }

        public void DeleteAllRegionDataFromDb(int currentRegionId)
        {
            MessageManager.SendInfoMessage("Очистка БД... ");
            SqlQueryExecutor.ExecuteQuery(string.Format(SQLResources.ClearRegionData, currentRegionId), _connectionString);
        }
    }
}
