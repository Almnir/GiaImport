using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaImport
{
    public partial class SettingsWindow : MetroFramework.Forms.MetroForm
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Load += SettingsWindowLoad;
        }

        private void SettingsWindowLoad(object sender, EventArgs e)
        {
            serverTextBox.Text = Globals.frmSettings.ServerText;
            databaseTextBox.Text = Globals.frmSettings.DatabaseText;
            loginTextBox.Text = Globals.frmSettings.LoginText;
            passwordTextBox.Text = Globals.frmSettings.PasswordText;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            SaveSettings();
            this.Close();
        }

        private void SaveSettings()
        {
            Globals.frmSettings.ServerText = serverTextBox.Text;
            Globals.frmSettings.DatabaseText = databaseTextBox.Text;
            Globals.frmSettings.LoginText = loginTextBox.Text;
            Globals.frmSettings.PasswordText = passwordTextBox.Text;
            Globals.frmSettings.Save();
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void checkConnectionButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            if (DatabaseHelper.CheckConnection())
            {
                MessageBox.Show("Соединение успешно!", "Проверено");
            }
            else
            {
                MessageBox.Show("Нет соединения!", "Внимание!");
            }
        }

        private void clearMainTablesButton_Click(object sender, EventArgs e)
        {
            if (!DatabaseHelper.CheckConnection())
            {
                MessageBox.Show("Нет соединения!", "Внимание!");
                return;
            }
            DeleteTables();
        }

        public void DeleteTables()
        {
            ProgressBarWindow pbw = new ProgressBarWindow();
            pbw.SetTitle("Очистка основных таблиц выполняется...");
            ProgressBar pbarTotal = pbw.GetProgressBarTotal();
            ProgressBar pbarLine = pbw.GetProgressBarLine();
            pbarTotal.Maximum = BulkManager.tablesList.Count - 1;
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
            pbw.Show();
            try
            {
                Task task = Task.Run(() => RunDelete(pbw, pbarLine, plabel, progress, source.Token), source.Token);
                task.ContinueWith(taskc => EndDelete(taskc, pbw));
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Операция отменена!");
            }
        }

        private void EndDelete(Task taskc, ProgressBarWindow pbw)
        {
            pbw.Invoke((MethodInvoker)(() => { pbw.Close(); }));
            Invoke(new Action(() => { MessageShowControl.ShowDeleteSuccess(); }));
        }

        private void RunDelete(ProgressBarWindow pbw, ProgressBar pbarLine, Label plabel, IProgress<int> progress, CancellationToken token)
        {
            for (int i = 0; i <= BulkManager.tablesList.Count - 1; i++)
            {
                progress.Report(i);
                token.ThrowIfCancellationRequested();
                string tableName = BulkManager.tablesList[i];
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
                DeleteTable(tableName);
                pbarLine.Invoke((MethodInvoker)(() =>
                {
                    pbarLine.Style = ProgressBarStyle.Continuous;
                    pbarLine.MarqueeAnimationSpeed = 0;
                }));
            }
        }

        private void DeleteTable(string tableName)
        {
            string query = string.Empty;
            try
            {
                using (var connection = new SqlConnection(Globals.GetConnectionString()))
                {
                    query = "DELETE FROM dbo." + tableName;
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new DeleteException(string.Format("При выполнении {0}, ошибка {1}", query, ex));
            }
        }

    }
}
