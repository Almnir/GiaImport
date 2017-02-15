using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Xml;
using System.Threading;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using NLog;

namespace GiaImport
{
    class PreparationStage
    {

        private string tempFolder = "";

        private string zipfilename;

        private static Logger log = LogManager.GetCurrentClassLogger();

        public PreparationStage()
        {

        }

        public PreparationStage(string tempFolder, string zipfilename)
        {
            this.zipfilename = zipfilename;
            this.tempFolder = tempFolder;
        }

        public void PrepareFiles(out Dictionary<string, string> filesErrors)
        {
            filesErrors = new Dictionary<string, string>();
            // unzip to temp folder
            try
            {
                ZipFile.ExtractToDirectory(zipfilename, tempFolder);
                // пройти и проверить на соответствия вторых элементам названиями таблиц
                string[] files = Directory.GetFiles(tempFolder);
                List<string> tableElements = new List<string>();
                CheckAllFilesElements(files, out tableElements, out filesErrors);
                if (filesErrors.Count != 0)
                {
                    return;
                }
                // порезать файлы
                ShrinkLargeFiles(files);
            }
            catch (Exception ex)
            {
                throw new PreparationStageException(ex.ToString());
            }
        }

        private void ShrinkLargeFiles(string[] files)
        {
            long threshold = GetAvailableRAM() / 4;
            Dictionary<string, long> filesToShrink = new Dictionary<string, long>();
            foreach (var file in files)
            {
                FileInfo fi = new FileInfo(file);
                long fsizemb = fi.Length / (1024 * 1024);
                if (fsizemb >= threshold)
                {
                    //filesToShrink.Add(file, );
                }
            }
        }

        public static async Task<int> RunProcessAsync(string fileName, string args)
        {
            using (var process = new Process
            {
                StartInfo =
                {
                    FileName = fileName, Arguments = args,
                    UseShellExecute = false, CreateNoWindow = true,
                    RedirectStandardOutput = true, RedirectStandardError = true
                },
                EnableRaisingEvents = true
            })
            {
                return await RunProcessAsync(process).ConfigureAwait(false);
            }
        }
        private static Task<int> RunProcessAsync(Process process)
        {
            var tcs = new TaskCompletionSource<int>();

            process.Exited += (s, ea) => tcs.SetResult(process.ExitCode);
            //process.OutputDataReceived += (s, ea) => Console.WriteLine(ea.Data);
            //process.ErrorDataReceived += (s, ea) => Console.WriteLine("ERR: " + ea.Data);

            bool started = process.Start();
            if (!started)
            {
                throw new InvalidOperationException("Невозможно запустить процесс: " + process);
            }

            //process.BeginOutputReadLine();
            //process.BeginErrorReadLine();

            return tcs.Task;
        }

        public long GetAvailableRAM()
        {
            ComputerInfo CI = new ComputerInfo();
            long mem = long.Parse(CI.AvailablePhysicalMemory.ToString());
            long result = mem / (1024 * 1024);
            //string text = (mem / (1024 * 1024) + " MB").ToString();
            //return text;
            return result;
        }

        private void CheckAllFilesElements(string[] files, out List<string> tableElements, out Dictionary<string, string> filesErrors)
        {
            List<string> arrayElements = new List<string>();
            filesErrors = new Dictionary<string, string>();
            tableElements = new List<string>();
            foreach (var file in files)
            {
                string arrayElement = string.Empty;
                string errorString = string.Empty;
                if (CheckRootAndGetArrayElement(file, out arrayElement, out errorString))
                {
                    filesErrors.Add(file, errorString);
                }
                else
                {
                    tableElements.Add(arrayElement);
                }
            }
        }

        private static bool CheckRootAndGetArrayElement(string filename, out string arrayElement, out string errorString)
        {
            bool error = false;
            arrayElement = string.Empty;
            errorString = string.Empty;
            string element = string.Empty;
            int countElements = 0;
            using (XmlReader reader = XmlReader.Create(filename))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        element = reader.Name;
                        countElements++;
                        // если первый элемент соответствует рутовому, то продолжаем дальше, иначе ругаемся и сваливаем
                        if (countElements == 1 && element.Equals(Globals.ROOT_ELEMENT))
                        {
                            continue;
                        }
                        if (countElements == 2)
                        {
                            arrayElement = element;
                            break;
                        }
                        else
                        {
                            errorString = "Неверный корневой элемент!";
                            error = true;
                            break;
                        }
                    }
                }
            }
            return error;
        }

        /// <summary>
        /// Подсчёт количества элементов
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="element"></param>
        /// <param name="errorString"></param>
        /// <returns></returns>
        public static long GetElementsCount(string filename, string element)
        {
            long countElements = 0;
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.ReadToFollowing(element))
                    {
                        countElements += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                string errorStr = string.Format("При чтении xml файла {0} произошла ошибка {1}.", filename, ex.ToString());
                log.Error(errorStr);
                throw new PreparationStageException(errorStr);
            }
            return countElements;
        }

        public bool Shrinker(string filepath, long chunksize)
        {
            bool error = false;
            int countElements = 0;
            int countFiles = 1;
            string filename = Path.GetFileNameWithoutExtension(filepath);
            string newfilepath = Path.Combine(Path.GetDirectoryName(filepath), filename + "_" + countFiles.ToString() + ".xml");
            FileStream fs = new FileStream(newfilepath, FileMode.Create, FileAccess.Write);
            WriteStartRoot(fs);
            string innerXML;
            try
            {
                using (XmlReader reader = XmlReader.Create(filepath))
                {
                    reader.ReadToFollowing(Globals.ROOT_ELEMENT);
                    reader.Read();
                    string sss = reader.Name;
                    do
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == sss)
                        {
                            using (var subReader = reader.ReadSubtree())
                            {
                                var element = XElement.Load(subReader);
                                innerXML = element.ToString();
                            }
                            countElements += innerXML.Length;
                            byte[] innerBytes = new UTF8Encoding(true).GetBytes(innerXML);
                            fs.Write(innerBytes, 0, innerBytes.Length);
                            if ((countElements / (1024 * 1024)) >= chunksize)
                            {
                                countFiles += 1;
                                countElements = 0;
                                WriteEndRoot(fs);
                                fs.Close();
                                newfilepath = Path.Combine(Path.GetDirectoryName(filepath), filename + "_" + countFiles.ToString() + ".xml");
                                fs = new FileStream(newfilepath, FileMode.Create, FileAccess.Write, FileShare.None);
                                WriteStartRoot(fs);
                            }
                        }
                    } while (reader.Read());
                }
                // конец последнего куска
                WriteEndRoot(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new ShrinkFilesException(string.Format("Не удалось разбить файл {0} на части по причине: {1}", filename, ex.ToString()));
            }
            return error;
        }

        private void WriteStartRoot(FileStream fs)
        {
            string giadbsetStart = @"<ns1:GIADBSet xmlns:ns1=""http://www.rustest.ru/giadbset"">";
            byte[] innerBytes = new UTF8Encoding(true).GetBytes(giadbsetStart);
            fs.Write(innerBytes, 0, innerBytes.Length);
        }

        private void WriteEndRoot(FileStream fs)
        {
            string giadbsetStart = @"</ns1:GIADBSet>";
            byte[] innerBytes = new UTF8Encoding(true).GetBytes(giadbsetStart);
            fs.Write(innerBytes, 0, innerBytes.Length);
        }

        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            XElement xmlDocumentWithoutNs = removeAllNamespaces(xmlDocument);
            return xmlDocumentWithoutNs;
        }

        private static XElement removeAllNamespaces(XElement xmlDocument)
        {
            var stripped = new XElement(xmlDocument.Name.LocalName);
            foreach (var attribute in
                    xmlDocument.Attributes().Where(
                    attribute =>
                        !attribute.IsNamespaceDeclaration &&
                        String.IsNullOrEmpty(attribute.Name.NamespaceName)))
            {
                stripped.Add(new XAttribute(attribute.Name.LocalName, attribute.Value));
            }
            if (!xmlDocument.HasElements)
            {
                stripped.Value = xmlDocument.Value;
                return stripped;
            }
            stripped.Add(xmlDocument.Elements().Select(
                el =>
                    RemoveAllNamespaces(el)));
            return stripped;
        }

        internal void ShrinkSingleFile(string xmlFilePath, long partSizeMB, string tempDir, CancellationToken ct)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.CreateNoWindow = true;
            //startInfo.UseShellExecute = false;
            startInfo.FileName = Path.Combine(Globals.frmSettings.TempDirectoryText == null ? Path.GetTempPath() + @"\Tempdir\" : Globals.frmSettings.TempDirectoryText + @"\Tempdir\", "XMLcut.exe");
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            FileInfo fi = new FileInfo(xmlFilePath);
            startInfo.Arguments = partSizeMB.ToString() + " " + fi.Name;
            startInfo.WorkingDirectory = Globals.frmSettings.TempDirectoryText == null ? Path.GetTempPath() + @"\Tempdir\" : Globals.frmSettings.TempDirectoryText + @"\Tempdir\";
            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    ct.ThrowIfCancellationRequested();
                    exeProcess.WaitForExit();
                    int code = exeProcess.ExitCode;
                }
            }
            catch (Exception ex)
            {
                throw new ShrinkFilesException(ex);
            }
        }
    }
}
