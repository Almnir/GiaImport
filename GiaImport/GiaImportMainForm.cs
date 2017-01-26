using Microsoft.Samples;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        DataSet mainDataSet = new DataSet();
        public GiaImportMainForm()
        {
            InitializeComponent();
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

        private void simpleImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, FileInfo> selectedFiles = new Dictionary<string, FileInfo>();
            ListView.SelectedListViewItemCollection selected = fileListView.SelectedItems;
            foreach (ListViewItem sel in selected)
            {
                foreach (var lf in loadedFiles)
                {
                    if (lf.Value.Name.Equals(sel.Text))
                    {
                        selectedFiles.Add(lf.Key, lf.Value);
                    }
                }
            }
            foreach (string sel in selectedFiles.Keys)
            {
                DataTable dt = new DataTable();
                dt.ReadXml(sel);
                mainDataSet.Tables.Add(dt);
            }
            SqlConnection connection = new SqlConnection("DB ConnectionSTring");
            SqlBulkCopy sbc = new SqlBulkCopy(connection);
            sbc.DestinationTableName = "yourXMLTable";
        }

        public void BulkWriteToDb(string connectionString, DataTable dataTable)
        {
            SqlTransaction tran = null;
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (tran = connection.BeginTransaction())
                    {
                        using (SqlBulkCopy bcp = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, tran))
                        {
                            bcp.DestinationTableName = dataTable.TableName;
                            bcp.BatchSize = 500;
                            bcp.NotifyAfter = 1000;
                            bcp.SqlRowsCopied +=
                                new SqlRowsCopiedEventHandler(bulkCopy_SqlRowsCopied);
                            bcp.WriteToServer(dataTable);
                        }
                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                throw new BulkException("Ошибка в методе BulkWriteToDb.", ex);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        static async Task<int> HandeBulkCopyAsync(string connectionString, DataTable dataTable)
        {
            int count = 0;
            SqlTransaction tran = null;
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (tran = connection.BeginTransaction())
                    {
                        using (SqlBulkCopy bcp = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, tran))
                        {
                            bcp.DestinationTableName = dataTable.TableName;
                            bcp.BatchSize = 500;
                            bcp.NotifyAfter = 1000;
                            await bcp.WriteToServerAsync(dataTable);
                            count++;
                        }
                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                throw new BulkException("Ошибка в методе BulkWriteToDb.", ex);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return count;
        }

        private void bulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static DataTable LoadXmlToDataTable(string fileName)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataTable.ReadXml(fileName);
            }
            catch (Exception ex)
            {
                throw new LoadXMLException("Всё плохо!", ex);
            }
            return dataTable;
        }

        private void chooseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void validationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Verifier.GetPath("ac_Appeals.xsd"));
            //CancellationTokenSource source = new CancellationTokenSource();
            //ProgressBarWindow pbw = new ProgressBarWindow();
            //pbw.Disposed += (a, b) => source.Cancel();
            //pbw.Show();
            //ProgressBar pbarTotal = pbw.GetProgressBarTotal();
            //pbarTotal.Maximum = loadedFiles.Count - 1;
            //Label plabel = pbw.GetLabel();
            //IProgress<int> progress = new Progress<int>(value => { pbarTotal.Value = value; });
            //await Task.Run(() => RunVerifier(plabel, progress, source.Token), source.Token);
            // TODO: показать диалог завершения
            //pbw.Close();
        }

        private void RunVerifier(Label plabel, IProgress<int> progress, CancellationToken cancellationToken)
        {
            for (int i = 0; i <= loadedFiles.Count - 1; i++)
            {
                try
                {
                    Thread.Sleep(1000);
                    Verifier verifier = new Verifier();
                    Task task = Task.Run(() => verifier.VerifySingleFile("fdf", "", progress));
                    progress.Report(i);
                    cancellationToken.ThrowIfCancellationRequested();
                    if (!plabel.IsDisposed)
                        plabel.Invoke((MethodInvoker)(() => plabel.Text = loadedFiles.Keys.ElementAt(i)));
                }
                catch (OperationCanceledException)
                {
                    MessageBox.Show("Прервано!");
                }
                catch (Exception)
                {
                   // TODO: логировать
                }

            }
        }

        private void filesCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileListView.Items.Clear();
            loadedFiles.Clear();
            fileListView.Refresh();
        }
    }
}
