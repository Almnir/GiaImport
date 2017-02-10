namespace GiaImport
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.checkConnectionButton = new MetroFramework.Controls.MetroButton();
            this.passwordTextBox = new MetroFramework.Controls.MetroTextBox();
            this.loginTextBox = new MetroFramework.Controls.MetroTextBox();
            this.databaseTextBox = new MetroFramework.Controls.MetroTextBox();
            this.serverTextBox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.okButton = new MetroFramework.Controls.MetroButton();
            this.cancelButton = new MetroFramework.Controls.MetroButton();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.clearMainTablesButton = new MetroFramework.Controls.MetroButton();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.checkConnectionButton);
            this.metroPanel1.Controls.Add(this.passwordTextBox);
            this.metroPanel1.Controls.Add(this.loginTextBox);
            this.metroPanel1.Controls.Add(this.databaseTextBox);
            this.metroPanel1.Controls.Add(this.serverTextBox);
            this.metroPanel1.Controls.Add(this.metroLabel4);
            this.metroPanel1.Controls.Add(this.metroLabel3);
            this.metroPanel1.Controls.Add(this.metroLabel2);
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(10, 64);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(517, 135);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // checkConnectionButton
            // 
            this.checkConnectionButton.Location = new System.Drawing.Point(105, 102);
            this.checkConnectionButton.Name = "checkConnectionButton";
            this.checkConnectionButton.Size = new System.Drawing.Size(201, 23);
            this.checkConnectionButton.TabIndex = 10;
            this.checkConnectionButton.Text = "Проверить соединение";
            this.checkConnectionButton.UseSelectable = true;
            this.checkConnectionButton.Click += new System.EventHandler(this.checkConnectionButton_Click);
            // 
            // passwordTextBox
            // 
            // 
            // 
            // 
            this.passwordTextBox.CustomButton.Image = null;
            this.passwordTextBox.CustomButton.Location = new System.Drawing.Point(387, 1);
            this.passwordTextBox.CustomButton.Name = "";
            this.passwordTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.passwordTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.passwordTextBox.CustomButton.TabIndex = 1;
            this.passwordTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.passwordTextBox.CustomButton.UseSelectable = true;
            this.passwordTextBox.CustomButton.Visible = false;
            this.passwordTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.passwordTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.passwordTextBox.Lines = new string[0];
            this.passwordTextBox.Location = new System.Drawing.Point(105, 72);
            this.passwordTextBox.MaxLength = 32767;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '●';
            this.passwordTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.passwordTextBox.SelectedText = "";
            this.passwordTextBox.SelectionLength = 0;
            this.passwordTextBox.SelectionStart = 0;
            this.passwordTextBox.ShortcutsEnabled = true;
            this.passwordTextBox.Size = new System.Drawing.Size(409, 23);
            this.passwordTextBox.TabIndex = 9;
            this.passwordTextBox.UseSelectable = true;
            this.passwordTextBox.UseSystemPasswordChar = true;
            this.passwordTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.passwordTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // loginTextBox
            // 
            // 
            // 
            // 
            this.loginTextBox.CustomButton.Image = null;
            this.loginTextBox.CustomButton.Location = new System.Drawing.Point(387, 1);
            this.loginTextBox.CustomButton.Name = "";
            this.loginTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.loginTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.loginTextBox.CustomButton.TabIndex = 1;
            this.loginTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.loginTextBox.CustomButton.UseSelectable = true;
            this.loginTextBox.CustomButton.Visible = false;
            this.loginTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.loginTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.loginTextBox.Lines = new string[0];
            this.loginTextBox.Location = new System.Drawing.Point(105, 49);
            this.loginTextBox.MaxLength = 32767;
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.PasswordChar = '\0';
            this.loginTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.loginTextBox.SelectedText = "";
            this.loginTextBox.SelectionLength = 0;
            this.loginTextBox.SelectionStart = 0;
            this.loginTextBox.ShortcutsEnabled = true;
            this.loginTextBox.Size = new System.Drawing.Size(409, 23);
            this.loginTextBox.TabIndex = 8;
            this.loginTextBox.UseSelectable = true;
            this.loginTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.loginTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // databaseTextBox
            // 
            // 
            // 
            // 
            this.databaseTextBox.CustomButton.Image = null;
            this.databaseTextBox.CustomButton.Location = new System.Drawing.Point(387, 1);
            this.databaseTextBox.CustomButton.Name = "";
            this.databaseTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.databaseTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.databaseTextBox.CustomButton.TabIndex = 1;
            this.databaseTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.databaseTextBox.CustomButton.UseSelectable = true;
            this.databaseTextBox.CustomButton.Visible = false;
            this.databaseTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.databaseTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.databaseTextBox.Lines = new string[0];
            this.databaseTextBox.Location = new System.Drawing.Point(105, 26);
            this.databaseTextBox.MaxLength = 32767;
            this.databaseTextBox.Name = "databaseTextBox";
            this.databaseTextBox.PasswordChar = '\0';
            this.databaseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.databaseTextBox.SelectedText = "";
            this.databaseTextBox.SelectionLength = 0;
            this.databaseTextBox.SelectionStart = 0;
            this.databaseTextBox.ShortcutsEnabled = true;
            this.databaseTextBox.Size = new System.Drawing.Size(409, 23);
            this.databaseTextBox.TabIndex = 7;
            this.databaseTextBox.UseSelectable = true;
            this.databaseTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.databaseTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // serverTextBox
            // 
            // 
            // 
            // 
            this.serverTextBox.CustomButton.Image = null;
            this.serverTextBox.CustomButton.Location = new System.Drawing.Point(387, 1);
            this.serverTextBox.CustomButton.Name = "";
            this.serverTextBox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.serverTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.serverTextBox.CustomButton.TabIndex = 1;
            this.serverTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.serverTextBox.CustomButton.UseSelectable = true;
            this.serverTextBox.CustomButton.Visible = false;
            this.serverTextBox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.serverTextBox.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.serverTextBox.Lines = new string[0];
            this.serverTextBox.Location = new System.Drawing.Point(105, 3);
            this.serverTextBox.MaxLength = 32767;
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.PasswordChar = '\0';
            this.serverTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.serverTextBox.SelectedText = "";
            this.serverTextBox.SelectionLength = 0;
            this.serverTextBox.SelectionStart = 0;
            this.serverTextBox.ShortcutsEnabled = true;
            this.serverTextBox.Size = new System.Drawing.Size(409, 23);
            this.serverTextBox.TabIndex = 6;
            this.serverTextBox.UseSelectable = true;
            this.serverTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.serverTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(4, 73);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(54, 19);
            this.metroLabel4.TabIndex = 5;
            this.metroLabel4.Text = "Пароль";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(4, 50);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(47, 19);
            this.metroLabel3.TabIndex = 4;
            this.metroLabel3.Text = "Логин";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(4, 27);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(86, 19);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "База данных";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(4, 4);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(55, 19);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Сервер";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(114, 238);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(118, 43);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseSelectable = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(299, 238);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(118, 43);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseSelectable = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // metroPanel2
            // 
            this.metroPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel2.Controls.Add(this.clearMainTablesButton);
            this.metroPanel2.Controls.Add(this.metroLabel5);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(10, 205);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(517, 21);
            this.metroPanel2.TabIndex = 3;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // clearMainTablesButton
            // 
            this.clearMainTablesButton.Location = new System.Drawing.Point(375, -1);
            this.clearMainTablesButton.Name = "clearMainTablesButton";
            this.clearMainTablesButton.Size = new System.Drawing.Size(139, 20);
            this.clearMainTablesButton.TabIndex = 3;
            this.clearMainTablesButton.Text = "Очистить таблицы";
            this.clearMainTablesButton.UseSelectable = true;
            this.clearMainTablesButton.Click += new System.EventHandler(this.clearMainTablesButton_Click);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroLabel5.Location = new System.Drawing.Point(0, 0);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(170, 19);
            this.metroLabel5.TabIndex = 2;
            this.metroLabel5.Text = "Очистка основных таблиц";
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 297);
            this.ControlBox = false;
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.metroPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.Resizable = false;
            this.ShowIcon = false;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Text = "Настройки";
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroButton okButton;
        private MetroFramework.Controls.MetroButton cancelButton;
        private MetroFramework.Controls.MetroTextBox serverTextBox;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox passwordTextBox;
        private MetroFramework.Controls.MetroTextBox loginTextBox;
        private MetroFramework.Controls.MetroTextBox databaseTextBox;
        private MetroFramework.Controls.MetroButton checkConnectionButton;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroButton clearMainTablesButton;
        private MetroFramework.Controls.MetroLabel metroLabel5;
    }
}