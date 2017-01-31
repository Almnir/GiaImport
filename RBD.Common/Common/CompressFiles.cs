using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
//using RBD.Client.Components;
using RBD.Client.Interfaces;

namespace RBD.Client.Services
{
    public class CompressFiles : ICompressFiles
    {
        private const int Chank = 4096;

        #region ICompressFiles Members

        public byte[] Compress(string[] fileNames, string destinationFolder)
        {
            return Compress(fileNames, destinationFolder, string.Format("Export_{0}.dat", DateTime.Now.ToString("d")));
        }

        public byte[] Compress(string[] fileNames, string destinationFolder, string destinationFile)
        {
            return Compress(fileNames, destinationFolder, destinationFile, new string[] {});
        }

        public byte[] Compress(string[] fileNames, string destinationFolder, string destinationFile,
                               string[] filesToKeep)
        {
            var result = new byte[] {};

            var fi = new FileInfo(Path.Combine(destinationFolder, destinationFile));
            using (FileStream fileDest = fi.Create())
            {
                using (var s = new ZipOutputStream(fileDest))
                {
                    s.UseZip64 = UseZip64.Off;
                    var buffer = new byte[Chank];
                    s.SetLevel(9); // 0-9, 9 being the highest compression

                    foreach (string file in fileNames)
                    {
                        if (!File.Exists(file))
                        {
                            //Logger.GetLogger().ErrorFormat(//    "Ошибка экспорта данных. Файл {0} не найден. В архив не помещен. экспорт продолжается в нормальном режиме",//    file);
                            continue;
                        }
                        var einfo = new FileInfo(file);
                        var entry = new ZipEntry(einfo.Name)
                                        {
                                            DateTime = DateTime.Now,
                                            CompressionMethod =
                                                einfo.Length == 0
                                                    ? CompressionMethod.Stored
                                                    : CompressionMethod.Deflated
                                        };


                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }

            if (fi.Exists)
            {
                using (FileStream fileDest = fi.OpenRead())
                {
                    result = new byte[fileDest.Length];
                    fileDest.Read(result, 0, (int) fileDest.Length);
                }
            }

            return result;
        }

        public bool UnZipFiles(string zipPathAndFile, string outputFolder)
        {
            try
            {
                using (var s = new ZipInputStream(File.OpenRead(zipPathAndFile)))
                {
                    ZipEntry theEntry;

                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = outputFolder;
                        string fileName = Path.GetFileName(theEntry.Name);
                        // create directory 
                        if (directoryName != "")
                        {
                            Directory.CreateDirectory(directoryName);
                        }
                        if (fileName != String.Empty)
                        {
                            if (theEntry.Name.IndexOf(".ini") < 0)
                            {
                                string fullPath = directoryName + "\\" + theEntry.Name;
                                fullPath = fullPath.Replace("\\ ", "\\");
                                string fullDirPath = Path.GetDirectoryName(fullPath);
                                if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);

                                if (File.Exists(fullPath))
                                {
                                    File.Delete(fullPath);
                                }

                                using (FileStream streamWriter = File.Create(fullPath))
                                {
                                    int size = Chank;
                                    var data = new byte[Chank];
                                    while (true)
                                    {
                                        size = s.Read(data, 0, data.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(data, 0, size);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //		DevExpress.XtraEditors.XtraMessageBox.Show(
                //			"Невозможно продолжить работу, т.к. файл поврежден.\nБолее подробная информация занесена в лог ошибок.",
                //			"Ошибка при обращении к файлу", MessageBoxButtons.OK);
                //Logger.GetLogger().Error(
                //    "Невозможно продолжить работу, т.к. файл поврежден.\nБолее подробная информация занесена в лог ошибок.",
                //    e);
                return false;
            }
            return true;
        }

        public bool UnZipFiles(Stream zipStream, string outputFolder)
        {
            try
            {
                using (var s = new ZipInputStream(zipStream))
                {
                    ZipEntry theEntry;

                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = outputFolder;
                        string fileName = Path.GetFileName(theEntry.Name);
                        // create directory 
                        if (directoryName != "")
                        {
                            Directory.CreateDirectory(directoryName);
                        }
                        if (fileName != String.Empty)
                        {
                            if (theEntry.Name.IndexOf(".ini") < 0)
                            {
                                string fullPath = directoryName + "\\" + theEntry.Name;
                                fullPath = fullPath.Replace("\\ ", "\\");
                                string fullDirPath = Path.GetDirectoryName(fullPath);
                                if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);

                                if (File.Exists(fullPath))
                                {
                                    File.Delete(fullPath);
                                }

                                using (FileStream streamWriter = File.Create(fullPath))
                                {
                                    int size;
                                    var data = new byte[Chank];
                                    while (true)
                                    {
                                        size = s.Read(data, 0, data.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(data, 0, size);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //		DevExpress.XtraEditors.XtraMessageBox.Show(
                //			"Невозможно продолжить работу, т.к. файл поврежден.\nБолее подробная информация занесена в лог ошибок.",
                //			"Ошибка при обращении к файлу", MessageBoxButtons.OK);
                //Logger.GetLogger().Error(
                //    "Невозможно продолжить работу, т.к. файл поврежден.\nБолее подробная информация занесена в лог ошибок.",
                //    e);
                return false;
            }
            return true;
        }

        #endregion
    }
}