using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaImport
{
    public partial class GiaImportMainForm : MetroFramework.Forms.MetroForm
    {
        Dictionary<string, FileInfo> loadedFiles = new Dictionary<string, FileInfo>();

        /// <summary>
        /// Файл - инфо
        /// </summary>
        Dictionary<string, FileInfo> actualCheckedFiles = new Dictionary<string, FileInfo>();
        // Имя файл - таблица
        Dictionary<string, string> preparedFilesTables = new Dictionary<string, string>();

        public GiaImportMainForm()
        {
            InitializeComponent();
            Load += new EventHandler(LoadForm);
        }

        private void LoadForm(object sender, EventArgs e)
        {
            if (Directory.Exists(Globals.TEMP_DIR))
            {
                var files = Directory.GetFiles(Globals.TEMP_DIR);
                foreach (var file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (!loadedFiles.Keys.Contains(file) && !loadedFiles.Values.Contains(fi))
                    {
                        loadedFiles.Add(file, fi);
                        ListViewItem lvi = new ListViewItem(fi.Name);
                        metroListView1.Items.Add(lvi);
                    }
                }
            }
        }

        private void SetActualCheckedFiles()
        {
            var checkedItems = this.metroListView1.CheckedItems
                                 .Cast<ListViewItem>().Select(a => a.Text).ToList();
            this.actualCheckedFiles.Clear();
            foreach (string ci in checkedItems)
            {
                foreach (var lf in loadedFiles)
                {
                    if (lf.Value.Name.Equals(ci))
                    {
                        this.actualCheckedFiles.Add(lf.Key, lf.Value);
                    }
                }
            }
        }

        private void filesOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFiles();
        }

        private void OpenFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog.Filter = "Zip Files (.zip)|*.zip|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            openFileDialog.Multiselect = false;

            DialogResult userClicked = openFileDialog.ShowDialog();

            if (userClicked == DialogResult.OK)
            {
                if (!Directory.Exists(Globals.TEMP_DIR))
                {
                    // создать временный каталог
                    Directory.CreateDirectory(Globals.TEMP_DIR);
                }
                // распаковать выбранный архив во временный каталог
                UnpackFiles(openFileDialog.FileName);
            }
        }

        private void chooseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.metroListView1.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = true);
        }

        private void simpleImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void validationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidateFiles();
        }

        private bool UnpackFiles(string zipfilename)
        {
            bool error = false;
            ProgressBarWindow pbw = new ProgressBarWindow();
            pbw.SetTitle("Распаковка файлов выполняется...");
            ProgressBar pbarTotal = pbw.GetProgressBarTotal();
            ProgressBar pbarLine = pbw.GetProgressBarLine();
            Label plabel = pbw.GetLabel();
            FileInfo fi = new FileInfo(zipfilename);
            plabel.Text = fi.Name;
            CancellationTokenSource source = new CancellationTokenSource();
            int entriesCount = 0;
            using (var zip = ZipFile.OpenRead(zipfilename))
            {
                entriesCount = zip.Entries.Count();
            }
            pbarTotal.Maximum = entriesCount - 1;
            IProgress<int> progress = new Progress<int>(value =>
            {
                ProgressBar pb = pbw.GetProgressBarTotal();
                if (!pb.IsDisposed)
                {
                    pb.Invoke((MethodInvoker)(() =>
                    {
                        pb.Value = value;
                    }));
                }
            });
            pbw.FormClosed += (a, e) => { source.Cancel(); };
            openFilesButton.Enabled = false;
            prepareFilesButton.Enabled = false;
            validateButton.Enabled = false;
            importButton.Enabled = false;
            pbw.Show();
            try
            {
                Task task = Task.Run(() => RunUnpacker(pbarTotal, pbarLine, plabel, zipfilename, progress, source.Token), source.Token);
                task.ContinueWith(taskc => EndUnpacker(pbw));
            }
            catch (TaskCanceledException)
            {
                error = true;
                MessageBox.Show("Операция отменена!");
            }
            catch (Exception ex)
            {
                error = true;
                MessageBox.Show(string.Format("Ошибка: ", ex.ToString()));
            }
            return error;
        }

        private void EndUnpacker(ProgressBarWindow pbw)
        {
            pbw.Invoke((MethodInvoker)(() => { pbw.Close(); }));
            Invoke(new Action(() =>
            {
                openFilesButton.Enabled = true;
                prepareFilesButton.Enabled = true;
                validateButton.Enabled = true;
                importButton.Enabled = true;
            }));
            foreach (var file in loadedFiles)
            {
                ListViewItem lvi = new ListViewItem(file.Value.Name);
                Invoke(new Action(() => { metroListView1.Items.Add(lvi); }));
            }
            Invoke(new Action(() => { metroListView1.Refresh(); }));
        }

        private void ValidateFiles()
        {
            SetActualCheckedFiles();
            if (!this.actualCheckedFiles.Any())
            {
                MessageBox.Show("Ни одного файла не выбрано!", "Внимание!");
                return;
            }
            if (!Globals.frmSettings.ServerText.Any() || !Globals.frmSettings.DatabaseText.Any() || !Globals.frmSettings.LoginText.Any())
            {
                MessageBox.Show("Настройки базы данных не установлены!", "Внимание!");
                return;
            }
            ProgressBarWindow pbw = new ProgressBarWindow();
            pbw.SetTitle("Валидация выполняется...");
            ProgressBar pbarTotal = pbw.GetProgressBarTotal();
            ProgressBar pbarLine = pbw.GetProgressBarLine();
            pbarTotal.Maximum = actualCheckedFiles.Count - 1;
            Label plabel = pbw.GetLabel();
            CancellationTokenSource source = new CancellationTokenSource();
            IProgress<int> progress = new Progress<int>(value =>
            {
                ProgressBar pb = pbw.GetProgressBarTotal();
                if (!pb.IsDisposed)
                {
                    pb.Invoke((MethodInvoker)(() =>
                    {
                        pb.Value = value;
                    }));
                }
            });
            pbw.FormClosed += (a, e) => { source.Cancel(); };
            openFilesButton.Enabled = false;
            prepareFilesButton.Enabled = false;
            validateButton.Enabled = false;
            importButton.Enabled = false;
            pbw.Show();
            try
            {
                Task<ConcurrentDictionary<string, string>> task = Task.Run(() => RunVerifier(pbarLine, plabel, progress, source.Token), source.Token);
                task.ContinueWith(taskc => EndVerifier(taskc, pbw));
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Операция отменена!");
            }
        }

        private void EndVerifier(Task<ConcurrentDictionary<string, string>> task, ProgressBarWindow pbw)
        {
            pbw.Invoke((MethodInvoker)(() => { pbw.Close(); }));
            ConcurrentDictionary<string, string> result = task.Result;
            if (result.Count != 0)
            {
                Invoke(new Action(() => { MessageShowControl.ShowValidationErrors(result); }));
            }
            else
            {
                Invoke(new Action(() => { MessageShowControl.ShowValidationSuccess(); }));
            }
            Invoke(new Action(() =>
            {
                openFilesButton.Enabled = true;
                prepareFilesButton.Enabled = true;
                validateButton.Enabled = true;
                importButton.Enabled = true;
            }));
        }

        private void ShrinkFiles()
        {
            SetActualCheckedFiles();
            if (!this.actualCheckedFiles.Any())
            {
                MessageBox.Show("Ни одного файла не выбрано!", "Внимание!");
                return;
            }
            ProgressBarWindow pbw = new ProgressBarWindow();
            pbw.SetTitle("Подготовка выполняется...");
            ProgressBar pbarTotal = pbw.GetProgressBarTotal();
            ProgressBar pbarLine = pbw.GetProgressBarLine();
            pbarTotal.Maximum = actualCheckedFiles.Count - 1;
            Label plabel = pbw.GetLabel();
            CancellationTokenSource source = new CancellationTokenSource();
            IProgress<int> progress = new Progress<int>(value =>
            {
                ProgressBar pb = pbw.GetProgressBarTotal();
                if (!pb.IsDisposed)
                {
                    pb.Invoke((MethodInvoker)(() =>
                    {
                        pb.Value = value;
                    }));
                }
            });
            pbw.FormClosed += (a, e) => { source.Cancel(); };
            openFilesButton.Enabled = false;
            prepareFilesButton.Enabled = false;
            validateButton.Enabled = false;
            importButton.Enabled = false;
            pbw.Show();
            try
            {
                //if (!File.Exists(Globals.TEMP_DIR + @"\XMLcut.exe"))
                //{
                //    File.Copy(Directory.GetCurrentDirectory() + @"\XMLcut.exe", Globals.TEMP_DIR + @"\XMLcut.exe");
                //}
                Task task = Task.Run(() => RunShrinker(pbarLine, plabel, progress, source.Token), source.Token);
                task.ContinueWith(taskc => EndShrinker(pbw));
            }
            catch (Exception ex)
            {
                MessageShowControl.ShowPrepareErrors(ex.ToString());
            }
        }

        private void EndShrinker(ProgressBarWindow pbw)
        {
            pbw.Invoke((MethodInvoker)(() => { pbw.Close(); }));
            Invoke(new Action(() => { MessageShowControl.ShowPrepareSuccess(); }));
            // поищем вообще все файлы, даже те что кусками нарезаны
            FindShrinkedFiles();
            Invoke(new Action(() =>
            {
                openFilesButton.Enabled = true;
                prepareFilesButton.Enabled = true;
                validateButton.Enabled = true;
                importButton.Enabled = true;
            }));
        }

        private void FindShrinkedFiles()
        {
            this.preparedFilesTables.Clear();
            foreach (var file in this.actualCheckedFiles)
            {
                // имя_файла_без_расширения_1
                string regexPattern = Path.GetFileNameWithoutExtension(file.Key) + @"_\d+.*$";
                Regex regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
                // имя*.расширение
                string filePattern = Path.GetFileNameWithoutExtension(file.Key) + @"*";
                string rootFileName = Path.GetFileNameWithoutExtension(file.Key);
                string[] allFiles = Directory.GetFiles(Globals.TEMP_DIR, filePattern);
                if (allFiles.Length > 1)
                {
                    List<string> resultList = allFiles.Where(f => regex.IsMatch(f)).ToList();
                    if (resultList.Count != 0)
                    {
                        foreach (var rl in resultList)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(rl);
                            string tableName = BulkManager.tablesList.Where(x => x.Equals(rootFileName, StringComparison.OrdinalIgnoreCase)).Single().ToString();
                            if (!preparedFilesTables.Keys.Contains(rl))
                            {
                                preparedFilesTables.Add(rl, tableName);
                            }
                        }
                    }
                    else
                    {
                        foreach (var af in allFiles)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(af);
                            string tableName = BulkManager.tablesList.Where(x => x.Equals(fileName, StringComparison.OrdinalIgnoreCase)).Single().ToString();
                            // если нет в списке подготовки и есть в списке актуально выбранных
                            if (!preparedFilesTables.Keys.Contains(af) && this.actualCheckedFiles.Keys.Contains(af))
                            {
                                preparedFilesTables.Add(af, tableName);
                            }
                        }
                    }
                }
                else if (allFiles.Length == 1)
                {
                    string tableName = BulkManager.tablesList.Where(x => x.Equals(rootFileName, StringComparison.OrdinalIgnoreCase)).Single().ToString();
                    if (!preparedFilesTables.Keys.Contains(file.Key))
                    {
                        preparedFilesTables.Add(file.Key, tableName);
                    }
                }
            }
        }

        private void Import()
        {
            SetActualCheckedFiles();
            if (!this.actualCheckedFiles.Any())
            {
                MessageBox.Show("Ни одного файла не выбрано!", "Внимание!");
                return;
            }
            if (!Globals.frmSettings.ServerText.Any() || !Globals.frmSettings.DatabaseText.Any() || !Globals.frmSettings.LoginText.Any())
            {
                MessageBox.Show("Настройки базы данных не установлены!", "Внимание!");
                return;
            }
            FindShrinkedFiles();
            List<string> guf = GetUnpreparedFiles();
            if (guf.Count != 0)
            {
                MessageShowControl.ShowImportPrepareErrors(guf);
                return;
            }
            ProgressBarWindow pbw = new ProgressBarWindow();
            pbw.SetTitle("Импорт выполняется...");
            ProgressBar pbarTotal = pbw.GetProgressBarTotal();
            ProgressBar pbarLine = pbw.GetProgressBarLine();
            // если порезали файлы
            if (preparedFilesTables.Any())
            {
                pbarTotal.Maximum = preparedFilesTables.Count;
            }
            else
            {
                // на 1 больше для запуска хранимки
                pbarTotal.Maximum = actualCheckedFiles.Count;
            }
            Label plabel = pbw.GetLabel();
            CancellationTokenSource source = new CancellationTokenSource();
            IProgress<int> progress = new Progress<int>(value =>
            {
                ProgressBar pb = pbw.GetProgressBarTotal();
                if (!pb.IsDisposed)
                {
                    pb.Invoke((MethodInvoker)(() =>
                    {
                        pb.Value = value;
                    }));
                }
            });
            pbw.FormClosed += (a, e) => { source.Cancel(); };
            openFilesButton.Enabled = false;
            prepareFilesButton.Enabled = false;
            validateButton.Enabled = false;
            importButton.Enabled = false;
            pbw.Show();
            try
            {
                Task<ConcurrentDictionary<string, string>> task = Task.Run(() => RunImport(pbw, pbarLine, plabel, progress, source.Token), source.Token);
                task.ContinueWith(taskc => EndImport(taskc, pbw));
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Операция отменена!");
            }
        }

        /// <summary>
        /// Проверка выбранных файлов
        /// </summary>
        /// <param name="actualCheckedFiles"></param>
        /// <returns></returns>
        private List<string> GetUnpreparedFiles()
        {
            List<string> result = new List<string>();
            IEnumerable<string> notIn = actualCheckedFiles.Keys.Where(p => !preparedFilesTables.Keys.Any(p1 => p1.Equals(p)));
            if (notIn != null && notIn.Any())
            {
                foreach (string ni in notIn)
                {
                    if (!preparedFilesTables.ContainsValue(Path.GetFileNameWithoutExtension(ni)))
                    {
                        long threshold = GetAvailableRAM() / 20;
                        FileInfo fi = new FileInfo(ni);
                        long fsizemb = fi.Length / (1024 * 1024);
                        if (fsizemb >= threshold)
                        {
                            result.Add(fi.Name);
                        }
                    }
                }
            }
            return result;
        }

        private void EndImport(Task taskc, ProgressBarWindow pbw)
        {
            if (!pbw.IsDisposed)
            {
                pbw.Invoke((MethodInvoker)(() => { pbw.Close(); }));
                Invoke(new Action(() => { MessageShowControl.ShowImportSuccess(); }));
            }
            Invoke(new Action(() =>
            {
                openFilesButton.Enabled = true;
                prepareFilesButton.Enabled = true;
                validateButton.Enabled = true;
                importButton.Enabled = true;
            }));
        }

        private void filesCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseFiles();
        }

        private void CloseFiles()
        {
            metroListView1.Items.Clear();
            loadedFiles.Clear();
            metroListView1.Refresh();
        }

        private ConcurrentDictionary<string, string> RunImport(ProgressBarWindow pbw, ProgressBar pbarLine, Label plabel, IProgress<int> progress, CancellationToken ct)
        {

            Verifier verifier = new Verifier();
            try
            {
                int i = 0;
                for (; i <= preparedFilesTables.Count - 1; i++)
                {
                    progress.Report(i);
                    ct.ThrowIfCancellationRequested();
                    string tableName = preparedFilesTables.ElementAt(i).Value;
                    string xmlFilePath = preparedFilesTables.ElementAt(i).Key;
                    if (!plabel.IsDisposed)
                    {
                        plabel.Invoke((MethodInvoker)(() => plabel.Text = tableName));
                    }
                    pbarLine.Invoke((MethodInvoker)(() =>
                    {
                        pbarLine.Style = ProgressBarStyle.Marquee;
                        pbarLine.MarqueeAnimationSpeed = 30;
                        pbarLine.Visible = true;
                    }));
                    BulkManager.BulkStart(tableName, xmlFilePath, ((rows) =>
                    {
                        if (!plabel.IsDisposed)
                        {
                            plabel.Invoke((MethodInvoker)(() => plabel.Text = string.Format("{0} - {1}", tableName, rows.RowsCopied.ToString())));
                        }
                    }));
                    pbarLine.Invoke((MethodInvoker)(() =>
                    {
                        pbarLine.Style = ProgressBarStyle.Continuous;
                        pbarLine.MarqueeAnimationSpeed = 0;
                    }));
                }
                progress.Report(i);
                if (!plabel.IsDisposed)
                {
                    plabel.Invoke((MethodInvoker)(() => plabel.Text = "Табличное преобразование..."));
                }
                pbarLine.Invoke((MethodInvoker)(() =>
                {
                    pbarLine.Style = ProgressBarStyle.Marquee;
                    pbarLine.MarqueeAnimationSpeed = 30;
                    pbarLine.Visible = true;
                }));
                BulkManager.RunStoredSynchronize();
                pbarLine.Invoke((MethodInvoker)(() =>
                {
                    pbarLine.Style = ProgressBarStyle.Continuous;
                    pbarLine.MarqueeAnimationSpeed = 0;
                }));

            }
            catch (Exception ex)
            {
                pbw.Invoke((MethodInvoker)(() => { pbw.Close(); }));
                Invoke(new Action(() => { MessageShowControl.ShowImportErrors(ex.ToString()); }));
                Invoke(new Action(() =>
                {
                    openFilesButton.Enabled = true;
                    prepareFilesButton.Enabled = true;
                    validateButton.Enabled = true;
                    importButton.Enabled = true;
                }));

            }
            return verifier.errorDict;
        }



        private ConcurrentDictionary<string, string> RunVerifier(ProgressBar pbarLine, Label plabel, IProgress<int> progress, CancellationToken ct)
        {
            Verifier verifier = new Verifier();
            for (int i = 0; i <= actualCheckedFiles.Count - 1; i++)
            {
                progress.Report(i);
                ct.ThrowIfCancellationRequested();
                string fileName = actualCheckedFiles[actualCheckedFiles.Keys.ElementAt(i)].Name;
                string xmlFilePath = actualCheckedFiles[actualCheckedFiles.Keys.ElementAt(i)].FullName;
                string nm = fileName.Substring(0, fileName.Count() - 4);
                string name = nm + ".xsd";
                string xsdFilePath = Directory.GetCurrentDirectory() + @"\XSD\" + name;
                if (!plabel.IsDisposed)
                {
                    plabel.Invoke((MethodInvoker)(() => plabel.Text = fileName));
                }
                pbarLine.Invoke((MethodInvoker)(() =>
                {
                    pbarLine.Style = ProgressBarStyle.Marquee;
                    pbarLine.MarqueeAnimationSpeed = 30;
                    pbarLine.Visible = true;
                }));
                verifier.VerifySingleFile(xsdFilePath, xmlFilePath, ct);
                //Thread.Sleep(2000);
                pbarLine.Invoke((MethodInvoker)(() =>
                {
                    pbarLine.Style = ProgressBarStyle.Continuous;
                    pbarLine.MarqueeAnimationSpeed = 0;
                }));
                //Thread.Sleep(100);
            }
            return verifier.errorDict;
        }

        private void RunShrinker(ProgressBar pbarLine, Label plabel, IProgress<int> progress, CancellationToken ct)
        {
            PreparationStage prepare = new PreparationStage();
            for (int i = 0; i <= actualCheckedFiles.Count - 1; i++)
            {
                progress.Report(i);
                ct.ThrowIfCancellationRequested();
                string fileName = actualCheckedFiles[actualCheckedFiles.Keys.ElementAt(i)].Name;
                string xmlFilePath = actualCheckedFiles[actualCheckedFiles.Keys.ElementAt(i)].FullName;
                string tempDir = string.Empty;
                if (string.IsNullOrEmpty(Globals.TEMP_DIR))
                {
                    tempDir = Directory.GetCurrentDirectory();
                }
                else
                {
                    tempDir = Globals.TEMP_DIR;

                }
                // TODO: хардкод - исправить
                long threshold = GetAvailableRAM() / 20;
                FileInfo fi = new FileInfo(xmlFilePath);
                long fsizemb = fi.Length / (1024 * 1024);
                if (fsizemb >= threshold)
                {
                    if (!plabel.IsDisposed)
                    {
                        plabel.Invoke((MethodInvoker)(() => plabel.Text = fileName));
                    }
                    pbarLine.Invoke((MethodInvoker)(() =>
                    {
                        pbarLine.Style = ProgressBarStyle.Marquee;
                        pbarLine.MarqueeAnimationSpeed = 30;
                        pbarLine.Visible = true;
                    }));
                    prepare.Shrinker(xmlFilePath, threshold);
                    //prepare.ShrinkSingleFile(xmlFilePath, threshold, tempDir, ct);
                    pbarLine.Invoke((MethodInvoker)(() =>
                    {
                        pbarLine.Style = ProgressBarStyle.Continuous;
                        pbarLine.MarqueeAnimationSpeed = 0;
                    }));
                }
            }
        }

        private void RunUnpacker(ProgressBar pbarTotal, ProgressBar pbarLine, Label plabel, string zipfilename, IProgress<int> progress, CancellationToken ct)
        {
            pbarLine.Invoke((MethodInvoker)(() =>
            {
                pbarLine.Style = ProgressBarStyle.Marquee;
                pbarLine.MarqueeAnimationSpeed = 30;
                pbarLine.Visible = true;
            }));
            pbarTotal.Invoke((MethodInvoker)(() =>
            {
                pbarTotal.Style = ProgressBarStyle.Marquee;
                pbarTotal.MarqueeAnimationSpeed = 30;
                pbarTotal.Visible = true;
            }));
            //ZipFile.ExtractToDirectory(zipfilename, Globals.TEMP_DIR);
            int counter = 0;
            using (ZipArchive zip = ZipFile.OpenRead(zipfilename))
            {
                foreach (var e in zip.Entries)
                {
                    string filenewpath = Path.Combine(Globals.TEMP_DIR, e.FullName);
                    e.ExtractToFile(filenewpath, true);
                    FileInfo fi = new FileInfo(filenewpath);
                    if (!loadedFiles.Keys.Contains(filenewpath) && !loadedFiles.Values.Contains(fi))
                    {
                        progress.Report(counter);
                        counter++;
                        loadedFiles.Add(filenewpath, fi);
                    }
                }
            }
            pbarLine.Invoke((MethodInvoker)(() =>
            {
                pbarLine.Style = ProgressBarStyle.Continuous;
                pbarLine.MarqueeAnimationSpeed = 0;
            }));
            pbarTotal.Invoke((MethodInvoker)(() =>
            {
                pbarTotal.Style = ProgressBarStyle.Continuous;
                pbarTotal.MarqueeAnimationSpeed = 0;
            }));
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


        private void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckAll();
        }

        private void CheckAll()
        {
            this.metroListView1.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = true);
        }

        private void unchekAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
        }

        private void UncheckAll()
        {
            this.metroListView1.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = false);
        }

        private void fileListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (metroListView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    metroContextMenu1.Show(Cursor.Position);
                }
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            Import();
        }

        private void openFilesButton_Click(object sender, EventArgs e)
        {
            OpenFiles();
        }

        private void prepareFilesButton_Click(object sender, EventArgs e)
        {
            ShrinkFiles();
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            ValidateFiles();
        }

        private void checkAllToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CheckAll();
        }

        private void uncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
        }

        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadedFiles.Clear();
            this.actualCheckedFiles.Clear();
            this.preparedFilesTables.Clear();
            this.metroListView1.Clear();
            this.metroListView1.Refresh();
            string[] files = Directory.GetFiles(Globals.TEMP_DIR);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.ShowDialog(); 
        }

        private void eraseButton_Click(object sender, EventArgs e)
        {
            if (!Globals.frmSettings.ServerText.Any() || !Globals.frmSettings.DatabaseText.Any() || !Globals.frmSettings.LoginText.Any())
            {
                MessageBox.Show("Настройки базы данных не установлены!", "Внимание!");
                return;
            }
            DialogResult result = MessageBox.Show("Данная операция очистит содержимое таблиц предварительной загрузки. \n Подтвердите или отклоните операцию.", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                DatabaseHelper.DeleteLoaderTables(Globals.GetConnectionString());
                MessageShowControl.ShowTruncateSuccess();
            }
        }
    }
}
