using NLog;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaImport
{
    public partial class SettingsWindow : MetroFramework.Forms.MetroForm
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

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
            pbw.FormClosed += (a, e) => { source.Cancel(); };
            pbw.Show();
            try
            {
                Task task = Task.Run(() => RunDelete(pbarTotal, pbarLine, plabel, source.Token), source.Token);
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

        private void RunDelete(ProgressBar pbarTotal, ProgressBar pbarLine, Label plabel, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            if (!plabel.IsDisposed)
            {
                plabel.Invoke((MethodInvoker)(() => plabel.Text = "Процесс очистки основных таблиц..."));
            }
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
            RunDeletedSynchronize();
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

        public void RunDeletedSynchronize()
        {
            int errorCount = 0;
            try
            {
                using (var conn = new SqlConnection(Globals.GetConnectionString()))
                using (var command = new SqlCommand("loader.CleanupTables", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    command.Parameters.Add("@TableGroup", SqlDbType.SmallInt).Value = 0;
                    command.Parameters.Add("@SkipErrors", SqlDbType.Bit).Value = 0;
                    command.CommandTimeout = 3600;
                    SqlParameter returnParameter = command.Parameters.Add("@error_count", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    conn.Open();
                    command.ExecuteNonQuery();
                    errorCount = (int)returnParameter.Value;
                    if (errorCount != 0)
                    {
                        log.Error("Ошибки очистки основных таблиц: " + errorCount);
                    }
                }
            }
            catch (Exception ex)
            {
                string status = string.Format("При выполнении очистки основных таблиц была обнаружена ошибка: {0}", ex.ToString());
                log.Error(status);
                throw new SyncException(status);
            }
        }

        private void DeleteTable(string tableName)
        {
            string query = string.Empty;
            try
            {
                using (var connection = new SqlConnection(Globals.GetConnectionString()))
                {
                    connection.Open();
                    if (DatabaseHelper.IsDataTableExists(Globals.GetConnectionString(), "dbo", tableName))
                    {
                        query = "DELETE FROM dbo." + tableName;
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DeleteException(string.Format("При выполнении {0}, ошибка {1}", query, ex));
            }
        }

    }
}
