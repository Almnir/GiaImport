using System;
using System.Windows.Forms;

namespace GiaImport
{
    public partial class ProgressBarWindow : Form
    {
        public ProgressBarWindow()
        {
            InitializeComponent();
        }

        public void SetTitle(string title)
        {
            this.Text = title;
        }

        public ProgressBar GetProgressBarLine()
        {
            return this.progressBarLine;
        }

        public ProgressBar GetProgressBarTotal()
        {
            return this.progressBarTotal;
        }

        public Label GetLabel()
        {
            return this.pbLabel;
        }

        private void pbLabel_SizeChanged(object sender, EventArgs e)
        {
            pbLabel.Left = (this.ClientSize.Width - pbLabel.Size.Width) / 2;
        }

        private void ProgressBarWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            progressBarTotal.Dispose();
            progressBarLine.Dispose();
        }

        private void ProgressBarWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
