using System;
using System.Configuration;

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
            Globals.frmSettings.ServerText = serverTextBox.Text;
            Globals.frmSettings.DatabaseText = databaseTextBox.Text;
            Globals.frmSettings.LoginText = loginTextBox.Text;
            Globals.frmSettings.PasswordText = passwordTextBox.Text;
            Globals.frmSettings.Save();
            this.Close();
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
