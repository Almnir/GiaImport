using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Castle.ActiveRecord;
using Microsoft.VisualBasic.FileIO;
using RBD.Client.BL;
using RBD.Client.Components;
using RBD.Client.Components.Version;
using RBD.Client.Domain;
using RBD.Client.Forms.Dialogs.Export;
using RBD.Client.Interfaces;
using RBD.Client.Services.Import.Executors;
using RBD.Common.Enums;
using RBD.Resources;

namespace RBD.Client.Services
{
    public class FolderService : IFolderService
    {
        private DirectoryInfo FCTFolder
        {
            get { return GetAppDataFolder(); }
        }

        public void SetBaseName(string baseName)
        {
            _baseName = string.Format("{0}_{1}", baseName, VersionHelper.ApplicationVersion());
        }

        private string _baseName;

        private DirectoryInfo GetAppDataFolder()
        {
            var di = new DirectoryInfo(string.Format("{0}/{1}/",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _baseName));
            if (!di.Exists)
            {
                di.Create();
            }
            return di;
        }

        public DirectoryInfo TempReportImageFolder
        {
            get
            {
                var di = new DirectoryInfo(string.Format("{0}/TempImageReport/", FCTFolder.FullName));
                if (!di.Exists)
                {
                    di.Create();
                }
                return di;
            }
        }

        public DirectoryInfo TempImageFolder
        {
            get
            {
                var di = new DirectoryInfo(string.Format("{0}/TempImage/", FCTFolder.FullName));
                if (!di.Exists)
                {
                    di.Create();
                }
                return di;
            }
        }

        public string GiaLicense
        {
            get { return Path.Combine(Application.StartupPath, "gia.lic"); }
        }

        public bool CanWriteAppPath()
        {
            var fileName = Path.Combine(Application.StartupPath, Guid.NewGuid().ToString());
            try
            {
                var file = new FileInfo(fileName);
                using (file.Create())
                {
                }
                file.Delete();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //private static IProgressBarService ProgressBar
        //{
        //    get { return WinsdorContainer.GetContainer.Resolve<IProgressBarService>(); }
        //}

        //private static ICompressFiles CompressFiles
        //{
        //    get { return WinsdorContainer.GetContainer.Resolve<ICompressFiles>(); }
        //}

        //public IKeyCode Key
        //{
        //    get { return WinsdorContainer.GetContainer.Resolve<IKeyCode>(); }
        //}

        #region IFolderService Members

        public DirectoryInfo TempFolder
        {
            get
            {
                var di = new DirectoryInfo(string.Format("{0}/{1}/TempExport/",
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _baseName));
                if (!di.Exists)
                {
                    di.Create();
                }
                return di;
            }
        }

        public FileInfo GetNewTempFileName()
        {
            return new FileInfo(Path.Combine(TempFolder.FullName, Guid.NewGuid().ToString()));
        }

        public DirectoryInfo ValidatorFolder
        {
            get
            {
                var di = new DirectoryInfo(string.Format("{0}/{1}/Validator/",
                                                         Environment.GetFolderPath(
                                                             Environment.SpecialFolder.ApplicationData), _baseName));
                if (!di.Exists)
                {
                    di.Create();
                }
                return di;
            }
        }


        //public DirectoryInfo SettingsFolder
        //{
        //    get
        //    {
        //        object[] info = GetType().Assembly.GetCustomAttributes(typeof(AssemblyRBDVersionAttribute), true);
        //        string ver = (info[0] as AssemblyRBDVersionAttribute).ToString();

        //        var di = new DirectoryInfo(string.Format("{0}/{2}/TmpSettings_{1}/",
        //                                                 Environment.GetFolderPath(
        //                                                     Environment.SpecialFolder.ApplicationData),
        //                                                 ver, _baseName
        //                                       ));
        //        if (!di.Exists)
        //        {
        //            di.Create();
        //        }
        //        return di;
        //    }
        //}

        //public DirectoryInfo CurrentExportTempFolder(KeyCode key)
        //{
        //    var di = new DirectoryInfo(string.Format("{0}/{1}_{2}_{3}/", TempFolder, key.Region, key.MOYOId, key.OYId));
        //    if (!di.Exists)
        //    {
        //        di.Create();
        //    }
        //    return di;
        //}

        public DirectoryInfo CurrentExportDictionaryFolder()
        {
            var di = new DirectoryInfo(string.Format("{0}/Dictionaryes/", TempFolder));
            if (!di.Exists)
            {
                di.Create();
            }
            return di;
        }


        public DirectoryInfo CSVExportDictionaryFolder()
        {
            var di = new DirectoryInfo(string.Format("{0}/CSVDictionaries/", TempFolder));
            if (!di.Exists)
            {
                di.Create();
            }
            return di;
        }

        public DirectoryInfo CSVExportEntityFolder(string entity)
        {
            var di = new DirectoryInfo(string.Format("{0}/CSVEntities/{1}/", TempFolder, entity));
            if (!di.Exists)
            {
                di.Create();
            }
            return di;
        }

        public FileInfo UserSettingsFile
        {
            get
            {
                var di = new DirectoryInfo(string.Format("{0}/UserSettings/", TempFolder));
                if (!di.Exists)
                {
                    di.Create();
                }
                var fullFileName = Path.Combine(di.FullName, "settings.xml");
                return new FileInfo(fullFileName);
            }
        }

        public string DictionaryFileName
        {
            get { return "Dictionary.txt"; }
        }

        public string KeyFileName
        {
            get { return "key.txt"; }
        }

        public string VersionFileName
        {
            get { return "version.txt"; }
        }

        public string SborDataFileName
        {
            get { return "dataEntity.txt"; }
        }

        public string PlanningDataEntityFileName
        {
            get { return "planningDataEntity.txt"; }
        }

        //public string KeyFileFolder(KeyCode key)
        //{
        //    return CurrentExportTempFolder(key).FullName + KeyFileName;
        //}

        //public string SborDataFileFolder(KeyCode key)
        //{
        //    return CurrentExportTempFolder(key).FullName + SborDataFileName;
        //}

        public string LocalBackupFolder
        {
            get
            {
                var di = new DirectoryInfo(string.Format("{0}/LocalBackups/", FCTFolder));
                if (!di.Exists)
                {
                    di.Create();
                }
                return di.FullName;
            }
        }

        //public string PlanningDataFileFolder(KeyCode key)
        //{
        //    return CurrentExportTempFolder(key).FullName + PlanningDataEntityFileName;
        //}

        public bool VersionFileExists
        {
            get { return File.Exists(TempFolderPath + VersionFileName); }
        }

        public string UpdateUpdaterVersion
        {
            get
            {
                var di = new DirectoryInfo(string.Format("{0}/{1}/UpdateUpdaterVersion/",
                                                         Environment.GetFolderPath(
                                                             Environment.SpecialFolder.ApplicationData), _baseName));
                if (!di.Exists)
                {
                    di.Create();
                }
                return di.FullName;
            }
        }

        public void CreateDistributive(string distributivePath)
        {
            ThreadPool.QueueUserWorkItem(CreateDistributiveThread, distributivePath);
        }

        public FileInfo CreateTempFile()
        {
            string tempName = string.Format("temp_{0}.tmp", Guid.NewGuid());
            using (FileStream f = File.Create(TempFolder.FullName + tempName))
            {
                f.Close();
            }
            var fi = new FileInfo(TempFolder.FullName + tempName);
            if (fi.Exists)
                return fi;

            throw new Exception("Невозможно создать временный файл");
        }

        public void DeleteTempFile(string FullFileName)
        {
            if (File.Exists(FullFileName))
            {
                try
                {
                    File.Delete(FullFileName);
                }
                catch (Exception e)
                {
                    Logger.GetLogger().Error("Невозможно удалить файл", e);
                }
            }
        }

        public void ClearUpdateFolder()
        {
            var di = new DirectoryInfo(UpdateUpdaterVersion);
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception e)
                {
                    Logger.GetLogger().Warn(e.Message, e);
                }
            }
            DelSubFolder(di);
        }

        public string VersionFileFolder
        {
            get { return CurrentExportVersionFolder().FullName + VersionFileName; }
        }

        public string VersionImportFileFolder
        {
            get { return ImportExportVersionFolder().FullName + VersionFileName; }
        }

        public void ClearTempFolder()
        {
            DelSubFolder(TempFolder);
        }

        public string DictionaryDataFileFolder
        {
            get { return CurrentExportDictionaryFolder().FullName + DictionaryFileName; }
        }

        public string DestinationFile(string baseFolder, KeyCode key)
        {
            return DestinationFile(baseFolder, key, null);
        }

        public string DestinationFile(string baseFolder, KeyCode key, Guid? areaId)
        {
            string moyoName = key.MOYOId != Guid.Empty
                                  ? ActiveRecordBase<RbdGovernments>.Find(key.MOYOId).GovernmentCode.ToString()
                                  : "";
            string oyName = key.OYId != Guid.Empty
                                ? ActiveRecordBase<RbdSchools>.Find(key.OYId).SchoolCode.ToString()
                                : "";

            int areaCode = areaId.HasValue && areaId != Guid.Empty ? RbdAreas.Find(areaId).AreaCode : 0;

            string dirName = string.Format(
                baseFolder + "/{0}_MCY_{1}{2}{3}{4}/",
                key.RegionCode, moyoName,
                ((oyName == string.Empty) ? string.Empty : ("_OO_" + oyName)),
                areaId.HasValue && Key.IsRcoi ? "_ATE" : string.Empty,
                areaCode > 0 ? string.Format("_{0}", areaCode) : string.Empty
                );
            var di =
                new DirectoryInfo(dirName);

            if (!di.Exists)
            {
                di.Create();
            }
            return di.FullName;
        }

        public string DestinationFileName(string baseFolder, KeyCode key, DistribType distribType)
        {
            return DestinationFileName(baseFolder, key, null, distribType);
        }

        private string GetPreffixByDistitType(DistribType distribType)
        {
            switch (distribType)
            {
                case DistribType.Gia9: return "_GIA_";
                case DistribType.Uege2015App: return "_APP_";
                default: return "_";
            }
        }

        public string DestinationFileName(string baseFolder, KeyCode key, Guid? areaId, DistribType distribType)
        {
            string moyoName = key.MOYOId != Guid.Empty
                                  ? ActiveRecordBase<RbdGovernments>.Find(key.MOYOId).GovernmentCode.ToString("000")
                                  : "";
            string oyName = key.OYId != Guid.Empty
                                ? ActiveRecordBase<RbdSchools>.Find(key.OYId).SchoolCode.ToString("000000")
                                : "";

            int areaCode = areaId.HasValue && areaId != Guid.Empty ? RbdAreas.Find(areaId).AreaCode : 0;

            var sb = new StringBuilder();
            sb.Append(key.RegionCode + "_");

            if (oyName == string.Empty)
            {
                sb.Append("MCY_" + moyoName);

                if (areaId.HasValue && Key.IsRcoi)
                {
                    sb.Append("_ATE");

                    if (areaCode > 0)
                    {
                        sb.Append(string.Format("_{0}", areaCode));
                    }
                }
            }
            else
                sb.Append("OO_" + oyName);

            sb.Append("_Export_From_" + key.RegionCode);

            if (Key.MOYOId != Guid.Empty)
            {
                sb.Append("MCY_" + ActiveRecordBase<RbdGovernments>.Find(Key.MOYOId).GovernmentCode.ToString("000"));

                if (areaId.HasValue)
                {
                    sb.Append("_ATE");

                    if (areaCode > 0)
                    {
                        sb.Append(string.Format("_{0}", areaCode));
                    }
                }
            }

            if (Key.OYId != Guid.Empty)
            {
                sb.Append("OO_" + ActiveRecordBase<RbdSchools>.Find(Key.OYId).SchoolCode.ToString("000000"));
            }
            object[] info = GetType().Assembly.GetCustomAttributes(typeof(AssemblyRBDVersionAttribute), true);
            string ver = (info[0] as AssemblyRBDVersionAttribute).ToString().Replace(" ", "_");

            var preffix = GetPreffixByDistitType(distribType);
            sb.Append(preffix);

            sb.AppendFormat("v{0}", ver);
            sb.Append("_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + ".dat");
            return sb.ToString();
        }

        public string CSVExportDestFileName(Guid ppeId, int code, string examDate, DistribType distribType)
        {
            object[] info = GetType().Assembly.GetCustomAttributes(typeof(AssemblyRBDVersionAttribute), true);
            string ver = (info[0] as AssemblyRBDVersionAttribute).ToString().Replace(" ", "_");

            RbdStations ppe = ActiveRecordBase<RbdStations>.Find(ppeId);
            var preffix = GetPreffixByDistitType(distribType);
            return string.Format("{0}_PPE_{1}_Export_From_{0}{2}{5}v{4}_{3}.zip",
                                 code,
                                 ppe.StationCode.ToString("0000"),
                                 string.IsNullOrEmpty(examDate)
                                     ? string.Empty
                                     : string.Format("_ExamDate_{0}", examDate),
                                    DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year,
                                 ver,
                                 preffix
                ).Replace(" ", "_");
        }

        public string TempFolderPath
        {
            get { return TempFolder.FullName; }
        }

        public string FCTFolderPath
        {
            get { return FCTFolder.FullName; }
        }

        public string StationReportPath
        {
            get
            {
                var di = new DirectoryInfo(string.Format("{0}/StationReport/", FCTFolderPath));
                if (!di.Exists)
                {
                    di.Create();
                }
                return di.FullName;
            }
        }

        public void ClearStationReportPath()
        {
            var di = new DirectoryInfo(StationReportPath);
            try
            {
                di.Delete();
            }
            catch (Exception)
            {
            }
        }

        #endregion

        public DirectoryInfo CurrentExportVersionFolder()
        {
            var di = new DirectoryInfo(string.Format("{0}/Version/", TempFolder));
            if (!di.Exists)
            {
                di.Create();
            }
            return di;
        }

        public DirectoryInfo ImportExportVersionFolder()
        {
            var di = new DirectoryInfo(string.Format("{0}/ImportedVersion/", TempFolder));
            if (!di.Exists)
            {
                di.Create();
            }
            return di;
        }


        private void CreateDistributiveThread(object state)
        {
            ProgressBar.ProgressBarChange<ExportDistributiveControl>("DistribProgressBar", 0);
            string distributivePath = state.ToString();

            var di = new DirectoryInfo(distributivePath);

            DirectoryInfo programmPath;

            if (!di.Exists)
            {
                di.Create();
                programmPath = di.CreateSubdirectory("RBD");
            }
            else
            {
                if (di.GetDirectories("RBD").Length > 0)
                {
                    programmPath = di.GetDirectories("RBD")[0];
                    try
                    {
                        if (FileSystem.FileExists(programmPath.FullName + @"\LOCALDB.FDB"))
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(
                                "В этой папке есть сформированный дистрибутив, перед созданием удалите или переместите существующий.",
                                "Конфликт", MessageBoxButtons.OK);

                            ProgressBar.ProgressBarChange<ExportDistributiveControl>("DistribProgressBar", 0);
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                else
                {
                    programmPath = di.CreateSubdirectory("RBD");
                }
            }
            ProgressBar.ProgressBarChange<ExportDistributiveControl>("DistribProgressBar", 10);
            try
            {
                FileSystem.CopyDirectory(AppDomain.CurrentDomain.BaseDirectory, programmPath.FullName, true);

                //удаляем ненужный файлы по МП формамъ
                /*
                 FineObj.dll
				gdiplus.dll
				msvcr90.dll
				TextPrint.dll
				13_02_desc.xslt
				13_02_tom_desc.xslt
				18_desc.xslt
				stylesheet.xslt
				13_02_blank.tif
				13_02_tom_blank.tif
				18_blank.tif
                 */
                //DeleteDistribFiles(programmPath.FullName, "FineObj.dll");
                DeleteDistribFiles(programmPath.FullName, "gdiplus.dll");
                DeleteDistribFiles(programmPath.FullName, "msvcr90.dll");
                //DeleteDistribFiles(programmPath.FullName, "TextPrint.dll");
                DeleteDistribFiles(programmPath.FullName, "13_02_desc.xslt");
                DeleteDistribFiles(programmPath.FullName, "13_02_tom_desc.xslt");
                DeleteDistribFiles(programmPath.FullName, "18_desc.xslt");
                DeleteDistribFiles(programmPath.FullName, "stylesheet.xslt");
                DeleteDistribFiles(programmPath.FullName, "13_02_blank.tif");
                DeleteDistribFiles(programmPath.FullName, "13_02_tom_blank.tif");
                DeleteDistribFiles(programmPath.FullName, "18_blank.tif");
                DeleteDir(Path.Combine(programmPath.FullName, CopyImportedFilesExecutor.ImportedPathName));

                ProgressBar.ProgressBarChange<ExportDistributiveControl>("DistribProgressBar", 60);

                var db = ResourceWrapper.GetLocalDataBaseRar();
                using (var s = new MemoryStream(db))
                {
                    CompressFiles.UnZipFiles(s, programmPath.FullName);
                }

                ProgressBar.ProgressBarChange<ExportDistributiveControl>("DistribProgressBar", 70);
            }
            catch (Exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Локальная база данных отсутствует. Воспользуйтесь дистрибутивом для установки программы");
                Logger.GetLogger().Error(
                    "Локальная база данных отсутствует. Воспользуйтесь дистрибутивом для установки программы");
                return;
            }
            ProgressBar.ProgressBarChange<ExportDistributiveControl>("DistribProgressBar", 75);
            var doc = new XmlDocument();
            doc.Load(programmPath.FullName + "\\" +
                     new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile).Name);

            XmlNode versionNode = doc.SelectSingleNode("//configuration/appSettings/add[@key='Version']");
            versionNode.Attributes["value"].InnerText = "local";

            XmlNode appSettingsNode = doc.SelectSingleNode("//configuration/appSettings");
            appSettingsNode.RemoveAll();

            appSettingsNode.AppendChild(versionNode);

            doc.Save(programmPath.FullName + "\\" +
                     new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile).Name);
            ProgressBar.ProgressBarChange<ExportDistributiveControl>("DistribProgressBar", 80);
            using (FileStream stream = File.Create(programmPath.FullName + "/bootstraper.exe"))
            {
                byte[] buffer = ResourceWrapper.GetBootStrapper();
                stream.Write(buffer, 0, buffer.Length);
            }
            ProgressBar.ProgressBarChange<ExportDistributiveControl>("DistribProgressBar", 95);

            using (FileStream stream = File.Create(distributivePath + "/RBD.bat"))
            {
                const string command = "cd rbd \n\rbootstraper.exe";
                byte[] buffer = Encoding.ASCII.GetBytes(command);
                stream.Write(buffer, 0, buffer.Length);
            }
            ProgressBar.ProgressBarChange<ExportDistributiveControl>("DistribProgressBar", 100);
            DevExpress.XtraEditors.XtraMessageBox.Show("Дистрибутив сформирован");
        }

        void DeleteDistribFiles(string folder, string file)
        {
            var pname = new DirectoryInfo(folder);
            if (pname.GetFiles(file).Length > 0)
            {
                pname.GetFiles(file).Do(x => x.Delete());
            }
        }

        private static void DeleteDir(string folderName)
        {
            var directory = new DirectoryInfo(folderName);
            if (directory.Exists)
            {
                directory.Delete(true);
            }
        }

        private static void DelSubFolder(DirectoryInfo dir)
        {
            DeleteFiles(dir);
            foreach (DirectoryInfo directory in dir.GetDirectories())
            {
                if (directory.Name == "UserSettings") continue;
                DelSubFolder(directory);
                try
                {
                    directory.Delete();
                }
                catch (Exception e)
                {
                    Logger.GetLogger().Warn(e.Message, e);
                }
            }
        }

        public bool SaveFile(string fileName, byte[] data)
        {
            try
            {
                if (File.Exists(fileName)) File.Delete(fileName);

                var stream = File.Create(fileName);

                stream.Write(data, 0, data.Length);

                stream.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private static void DeleteFiles(DirectoryInfo directory)
        {
            foreach (FileInfo tempFile in directory.GetFiles())
            {
                try
                {
                    tempFile.Delete();
                }
                catch (Exception e)
                {
                    Logger.GetLogger().Warn(e.Message, e);
                }
            }
        }

        public void ClearTempImageFolder()
        {
            DeleteFiles(TempImageFolder);
        }
    }
}