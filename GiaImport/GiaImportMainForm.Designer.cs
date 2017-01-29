using System;

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
            this.components = new System.ComponentModel.Container();
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.fileListView = new System.Windows.Forms.ListView();
            this.filenameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unchekAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.giaMenuStrip.SuspendLayout();
            this.giaToolStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // giaMenuStrip
            // 
            this.giaMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem,
            this.chooseToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
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
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.filesToolStripMenuItem.Text = "Файлы";
            // 
            // filesOpenToolStripMenuItem
            // 
            this.filesOpenToolStripMenuItem.Name = "filesOpenToolStripMenuItem";
            this.filesOpenToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.filesOpenToolStripMenuItem.Text = "Открыть файлы...";
            this.filesOpenToolStripMenuItem.Click += new System.EventHandler(this.filesOpenToolStripMenuItem_Click);
            // 
            // filesCloseToolStripMenuItem
            // 
            this.filesCloseToolStripMenuItem.Name = "filesCloseToolStripMenuItem";
            this.filesCloseToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.filesCloseToolStripMenuItem.Text = "Закрыть файлы";
            this.filesCloseToolStripMenuItem.Click += new System.EventHandler(this.filesCloseToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.exitToolStripMenuItem.Text = "Выйти";
            // 
            // chooseToolStripMenuItem
            // 
            this.chooseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseAllToolStripMenuItem,
            this.chooseImportantToolStripMenuItem});
            this.chooseToolStripMenuItem.Name = "chooseToolStripMenuItem";
            this.chooseToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.chooseToolStripMenuItem.Text = "Выборка";
            // 
            // chooseAllToolStripMenuItem
            // 
            this.chooseAllToolStripMenuItem.Name = "chooseAllToolStripMenuItem";
            this.chooseAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.chooseAllToolStripMenuItem.Text = "Выбрать все";
            this.chooseAllToolStripMenuItem.Click += new System.EventHandler(this.chooseAllToolStripMenuItem_Click);
            // 
            // chooseImportantToolStripMenuItem
            // 
            this.chooseImportantToolStripMenuItem.Name = "chooseImportantToolStripMenuItem";
            this.chooseImportantToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
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
            this.validationToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.validationToolStripMenuItem.Text = "Валидация";
            this.validationToolStripMenuItem.Click += new System.EventHandler(this.validationToolStripMenuItem_Click);
            // 
            // simpleImportToolStripMenuItem
            // 
            this.simpleImportToolStripMenuItem.Name = "simpleImportToolStripMenuItem";
            this.simpleImportToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.simpleImportToolStripMenuItem.Text = "Импорт";
            this.simpleImportToolStripMenuItem.Click += new System.EventHandler(this.simpleImportToolStripMenuItem_Click);
            // 
            // giaToolStrip
            // 
            this.giaToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.toolStripButton4});
            this.giaToolStrip.Location = new System.Drawing.Point(0, 24);
            this.giaToolStrip.Name = "giaToolStrip";
            this.giaToolStrip.Size = new System.Drawing.Size(719, 25);
            this.giaToolStrip.TabIndex = 2;
            this.giaToolStrip.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.fileListView.Size = new System.Drawing.Size(719, 439);
            this.fileListView.TabIndex = 3;
            this.fileListView.UseCompatibleStateImageBehavior = false;
            this.fileListView.View = System.Windows.Forms.View.Details;
            this.fileListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fileListView_MouseClick);
            // 
            // filenameColumn
            // 
            this.filenameColumn.Text = "";
            this.filenameColumn.Width = 500;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 491);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(719, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.unchekAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 48);
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.checkAllToolStripMenuItem.Text = "Выделить все";
            this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
            // 
            // unchekAllToolStripMenuItem
            // 
            this.unchekAllToolStripMenuItem.Name = "unchekAllToolStripMenuItem";
            this.unchekAllToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.unchekAllToolStripMenuItem.Text = "Снять все";
            this.unchekAllToolStripMenuItem.Click += new System.EventHandler(this.unchekAllToolStripMenuItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // GiaImportMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 513);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.fileListView);
            this.Controls.Add(this.giaToolStrip);
            this.Controls.Add(this.giaMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.giaMenuStrip;
            this.MinimumSize = new System.Drawing.Size(182, 39);
            this.Name = "GiaImportMainForm";
            this.Text = "GiaImport";
            this.giaMenuStrip.ResumeLayout(false);
            this.giaMenuStrip.PerformLayout();
            this.giaToolStrip.ResumeLayout(false);
            this.giaToolStrip.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unchekAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
    }
}

