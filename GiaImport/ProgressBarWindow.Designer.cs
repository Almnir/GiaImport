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
            this.pbLabel = new System.Windows.Forms.Label();
            this.progressBarLine = new System.Windows.Forms.ProgressBar();
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // pbLabel
            // 
            this.pbLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbLabel.AutoEllipsis = true;
            this.pbLabel.AutoSize = true;
            this.pbLabel.Location = new System.Drawing.Point(301, 22);
            this.pbLabel.Name = "pbLabel";
            this.pbLabel.Size = new System.Drawing.Size(0, 13);
            this.pbLabel.TabIndex = 0;
            this.pbLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pbLabel.SizeChanged += new System.EventHandler(this.pbLabel_SizeChanged);
            // 
            // progressBarLine
            // 
            this.progressBarLine.Location = new System.Drawing.Point(12, 51);
            this.progressBarLine.Name = "progressBarLine";
            this.progressBarLine.Size = new System.Drawing.Size(588, 21);
            this.progressBarLine.TabIndex = 1;
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(12, 78);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(588, 35);
            this.progressBarTotal.TabIndex = 2;
            // 
            // ProgressBarWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 122);
            this.ControlBox = false;
            this.Controls.Add(this.progressBarTotal);
            this.Controls.Add(this.progressBarLine);
            this.Controls.Add(this.pbLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressBarWindow";
            this.ShowIcon = false;
            this.Text = "Выполнение";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProgressBarWindow_FormClosed);
            this.Load += new System.EventHandler(this.ProgressBarWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pbLabel;
        private System.Windows.Forms.ProgressBar progressBarLine;
        private System.Windows.Forms.ProgressBar progressBarTotal;
    }
}