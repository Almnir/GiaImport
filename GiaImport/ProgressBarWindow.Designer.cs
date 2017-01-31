namespace GiaImport
{
    partial class ProgressBarWindow
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
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.pbLabel = new MetroFramework.Controls.MetroLabel();
            this.progressBarLine = new MetroFramework.Controls.MetroProgressBar();
            this.progressBarTotal = new MetroFramework.Controls.MetroProgressBar();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.pbLabel);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(10, 64);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(598, 23);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // pbLabel
            // 
            this.pbLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbLabel.AutoSize = true;
            this.pbLabel.Location = new System.Drawing.Point(241, 4);
            this.pbLabel.Name = "pbLabel";
            this.pbLabel.Size = new System.Drawing.Size(81, 19);
            this.pbLabel.TabIndex = 2;
            this.pbLabel.Text = "%filename%";
            this.pbLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarLine
            // 
            this.progressBarLine.Location = new System.Drawing.Point(10, 94);
            this.progressBarLine.Name = "progressBarLine";
            this.progressBarLine.ProgressBarStyle = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarLine.Size = new System.Drawing.Size(598, 23);
            this.progressBarLine.TabIndex = 1;
            this.progressBarLine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(10, 124);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(598, 44);
            this.progressBarTotal.TabIndex = 2;
            // 
            // ProgressBarWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 178);
            this.ControlBox = false;
            this.Controls.Add(this.progressBarTotal);
            this.Controls.Add(this.progressBarLine);
            this.Controls.Add(this.metroPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressBarWindow";
            this.Resizable = false;
            this.ShowIcon = false;
            this.Text = "Выполнение";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProgressBarWindow_FormClosed);
            this.Load += new System.EventHandler(this.ProgressBarWindow_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel pbLabel;
        private MetroFramework.Controls.MetroProgressBar progressBarLine;
        private MetroFramework.Controls.MetroProgressBar progressBarTotal;
    }
}