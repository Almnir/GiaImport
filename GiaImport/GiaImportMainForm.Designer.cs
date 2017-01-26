namespace GiaImport
{
    partial class GiaImportMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GiaImportMainForm));
            this.giaMenuStrip = new System.Windows.Forms.MenuStrip();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filesOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filesCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseImportantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleImportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.giaToolStrip = new System.Windows.Forms.ToolStrip();
            this.fileListView = new System.Windows.Forms.ListView();
            this.filenameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.giaMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // giaMenuStrip
            // 
            this.giaMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem,
            this.chooseToolStripMenuItem,
            this.actionsToolStripMenuItem});
            this.giaMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.giaMenuStrip.Name = "giaMenuStrip";
            this.giaMenuStrip.Size = new System.Drawing.Size(719, 24);
            this.giaMenuStrip.TabIndex = 0;
            this.giaMenuStrip.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesOpenToolStripMenuItem,
            this.filesCloseToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.filesToolStripMenuItem.Text = "Файлы";
            // 
            // filesOpenToolStripMenuItem
            // 
            this.filesOpenToolStripMenuItem.Name = "filesOpenToolStripMenuItem";
            this.filesOpenToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.filesOpenToolStripMenuItem.Text = "Открыть файлы...";
            this.filesOpenToolStripMenuItem.Click += new System.EventHandler(this.filesOpenToolStripMenuItem_Click);
            // 
            // filesCloseToolStripMenuItem
            // 
            this.filesCloseToolStripMenuItem.Name = "filesCloseToolStripMenuItem";
            this.filesCloseToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.filesCloseToolStripMenuItem.Text = "Закрыть файлы";
            this.filesCloseToolStripMenuItem.Click += new System.EventHandler(this.filesCloseToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exitToolStripMenuItem.Text = "Выйти";
            // 
            // chooseToolStripMenuItem
            // 
            this.chooseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseAllToolStripMenuItem,
            this.chooseImportantToolStripMenuItem});
            this.chooseToolStripMenuItem.Name = "chooseToolStripMenuItem";
            this.chooseToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.chooseToolStripMenuItem.Text = "Выборка";
            // 
            // chooseAllToolStripMenuItem
            // 
            this.chooseAllToolStripMenuItem.Name = "chooseAllToolStripMenuItem";
            this.chooseAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.chooseAllToolStripMenuItem.Text = "Выбрать все";
            this.chooseAllToolStripMenuItem.Click += new System.EventHandler(this.chooseAllToolStripMenuItem_Click);
            // 
            // chooseImportantToolStripMenuItem
            // 
            this.chooseImportantToolStripMenuItem.Name = "chooseImportantToolStripMenuItem";
            this.chooseImportantToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.chooseImportantToolStripMenuItem.Text = "Выбрать важные";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.validationToolStripMenuItem,
            this.simpleImportToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.actionsToolStripMenuItem.Text = "Операции";
            // 
            // validationToolStripMenuItem
            // 
            this.validationToolStripMenuItem.Name = "validationToolStripMenuItem";
            this.validationToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.validationToolStripMenuItem.Text = "Валидация";
            this.validationToolStripMenuItem.Click += new System.EventHandler(this.validationToolStripMenuItem_Click);
            // 
            // simpleImportToolStripMenuItem
            // 
            this.simpleImportToolStripMenuItem.Name = "simpleImportToolStripMenuItem";
            this.simpleImportToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.simpleImportToolStripMenuItem.Text = "Импорт";
            this.simpleImportToolStripMenuItem.Click += new System.EventHandler(this.simpleImportToolStripMenuItem_Click);
            // 
            // giaToolStrip
            // 
            this.giaToolStrip.Location = new System.Drawing.Point(0, 24);
            this.giaToolStrip.Name = "giaToolStrip";
            this.giaToolStrip.Size = new System.Drawing.Size(719, 25);
            this.giaToolStrip.TabIndex = 2;
            this.giaToolStrip.Text = "toolStrip1";
            // 
            // fileListView
            // 
            this.fileListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileListView.CheckBoxes = true;
            this.fileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.filenameColumn});
            this.fileListView.FullRowSelect = true;
            this.fileListView.GridLines = true;
            this.fileListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.fileListView.Location = new System.Drawing.Point(0, 49);
            this.fileListView.Name = "fileListView";
            this.fileListView.Size = new System.Drawing.Size(719, 433);
            this.fileListView.TabIndex = 3;
            this.fileListView.UseCompatibleStateImageBehavior = false;
            this.fileListView.View = System.Windows.Forms.View.Details;
            // 
            // filenameColumn
            // 
            this.filenameColumn.Text = "";
            this.filenameColumn.Width = 500;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(0, 488);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(182, 23);
            this.progressBar.TabIndex = 4;
            // 
            // GiaImportMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 513);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.fileListView);
            this.Controls.Add(this.giaToolStrip);
            this.Controls.Add(this.giaMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.giaMenuStrip;
            this.MinimumSize = new System.Drawing.Size(182, 0);
            this.Name = "GiaImportMainForm";
            this.Text = "GiaImport";
            this.giaMenuStrip.ResumeLayout(false);
            this.giaMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip giaMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filesOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseImportantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleImportToolStripMenuItem;
        private System.Windows.Forms.ToolStrip giaToolStrip;
        private System.Windows.Forms.ToolStripMenuItem filesCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader filenameColumn;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

