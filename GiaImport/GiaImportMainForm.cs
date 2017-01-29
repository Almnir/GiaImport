using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaImport
{
    public partial class GiaImportMainForm : Form
    {
        Dictionary<string, FileInfo> loadedFiles = new Dictionary<string, FileInfo>();
        Dictionary<string, FileInfo> actualCheckedFiles = new Dictionary<string, FileInfo>();

        public GiaImportMainForm()
        {
            InitializeComponent();
        }

        private void SetActualCheckedFiles()
        {
            var checkedItems = this.fileListView.CheckedItems
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
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            openFileDialog.Multiselect = true;

            DialogResult userClicked = openFileDialog.ShowDialog();

            if (userClicked == DialogResult.OK)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    FileInfo fi = new FileInfo(file);
                    // загружаем только те, которые ещё не загружали
                    if (!loadedFiles.ContainsKey(file) && !loadedFiles.ContainsValue(fi))
                    {
                        loadedFiles.Add(file, fi);
                        ListViewItem lvi = new ListViewItem(fi.Name);
                        fileListView.Items.Add(lvi);
                    }
                }
                fileListView.Refresh();
            }
        }

        private void chooseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileListView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = true);
        }

        private void simpleImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void validationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetActualCheckedFiles();
            if (!this.actualCheckedFiles.Any())
            {
                MessageBox.Show("Внимание", "Ни одного файла не выбрано!");
                return;
            }
            ProgressBarWindow pbw = new ProgressBarWindow();
            pbw.SetTitle("Валидация выполняется...");
            pbw.Show();
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
        }

        private void filesCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileListView.Items.Clear();
            loadedFiles.Clear();
            fileListView.Refresh();
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
                Thread.Sleep(100);
            }
            return verifier.errorDict;
        }

        private void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileListView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = true);
        }

        private void unchekAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fileListView.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = false);
        }

        private void fileListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (fileListView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox abx = new AboutBox();
            abx.ShowDialog();
        }
    }
}
