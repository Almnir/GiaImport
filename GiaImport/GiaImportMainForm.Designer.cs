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
            this.metroListView1 = new MetroFramework.Controls.MetroListView();
            this.metroContextMenu1 = new MetroFramework.Controls.MetroContextMenu(this.components);
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFilesButton = new MetroFramework.Controls.MetroButton();
            this.prepareFilesButton = new MetroFramework.Controls.MetroButton();
            this.validateButton = new MetroFramework.Controls.MetroButton();
            this.importButton = new MetroFramework.Controls.MetroButton();
            this.settingsButton = new MetroFramework.Controls.MetroButton();
            this.metroContextMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroListView1
            // 
            this.metroListView1.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.metroListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroListView1.CheckBoxes = true;
            this.metroListView1.ContextMenuStrip = this.metroContextMenu1;
            this.metroListView1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.metroListView1.FullRowSelect = true;
            this.metroListView1.Location = new System.Drawing.Point(20, 112);
            this.metroListView1.Name = "metroListView1";
            this.metroListView1.OwnerDraw = true;
            this.metroListView1.Size = new System.Drawing.Size(918, 378);
            this.metroListView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.metroListView1.TabIndex = 7;
            this.metroListView1.UseCompatibleStateImageBehavior = false;
            this.metroListView1.UseSelectable = true;
            this.metroListView1.View = System.Windows.Forms.View.List;
            // 
            // metroContextMenu1
            // 
            this.metroContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.uncheckAllToolStripMenuItem,
            this.clearListToolStripMenuItem});
            this.metroContextMenu1.Name = "metroContextMenu1";
            this.metroContextMenu1.Size = new System.Drawing.Size(166, 70);
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.checkAllToolStripMenuItem.Text = "Выделить все";
            this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click_1);
            // 
            // uncheckAllToolStripMenuItem
            // 
            this.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem";
            this.uncheckAllToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.uncheckAllToolStripMenuItem.Text = "Снять все";
            this.uncheckAllToolStripMenuItem.Click += new System.EventHandler(this.uncheckAllToolStripMenuItem_Click);
            // 
            // clearListToolStripMenuItem
            // 
            this.clearListToolStripMenuItem.Name = "clearListToolStripMenuItem";
            this.clearListToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.clearListToolStripMenuItem.Text = "Очистить список";
            this.clearListToolStripMenuItem.Click += new System.EventHandler(this.clearListToolStripMenuItem_Click);
            // 
            // openFilesButton
            // 
            this.openFilesButton.Location = new System.Drawing.Point(20, 64);
            this.openFilesButton.Name = "openFilesButton";
            this.openFilesButton.Size = new System.Drawing.Size(115, 42);
            this.openFilesButton.TabIndex = 1;
            this.openFilesButton.Text = "1     Выбрать";
            this.openFilesButton.UseSelectable = true;
            this.openFilesButton.Click += new System.EventHandler(this.openFilesButton_Click);
            // 
            // prepareFilesButton
            // 
            this.prepareFilesButton.Location = new System.Drawing.Point(262, 64);
            this.prepareFilesButton.Name = "prepareFilesButton";
            this.prepareFilesButton.Size = new System.Drawing.Size(115, 42);
            this.prepareFilesButton.TabIndex = 3;
            this.prepareFilesButton.Text = "3  Подготовить";
            this.prepareFilesButton.UseSelectable = true;
            this.prepareFilesButton.Click += new System.EventHandler(this.prepareFilesButton_Click);
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(141, 64);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(115, 42);
            this.validateButton.TabIndex = 2;
            this.validateButton.Text = "2    Проверить";
            this.validateButton.UseSelectable = true;
            this.validateButton.Click += new System.EventHandler(this.validateButton_Click);
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(383, 64);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(115, 42);
            this.importButton.TabIndex = 5;
            this.importButton.Text = "4 Импортировать";
            this.importButton.UseSelectable = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.Location = new System.Drawing.Point(823, 64);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(115, 42);
            this.settingsButton.TabIndex = 6;
            this.settingsButton.Text = "Настройки";
            this.settingsButton.UseSelectable = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // GiaImportMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 513);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.validateButton);
            this.Controls.Add(this.prepareFilesButton);
            this.Controls.Add(this.openFilesButton);
            this.Controls.Add(this.metroListView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(762, 200);
            this.Name = "GiaImportMainForm";
            this.Text = "Импорт ГИА-9";
            this.metroContextMenu1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroListView metroListView1;
        private MetroFramework.Controls.MetroButton openFilesButton;
        private MetroFramework.Controls.MetroButton prepareFilesButton;
        private MetroFramework.Controls.MetroButton validateButton;
        private MetroFramework.Controls.MetroButton importButton;
        private MetroFramework.Controls.MetroContextMenu metroContextMenu1;
        private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uncheckAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearListToolStripMenuItem;
        private MetroFramework.Controls.MetroButton settingsButton;
    }
}

