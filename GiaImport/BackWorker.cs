using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GiaImport
{
    public class BackWorker
    {
        Action doworkAction;
        Action endworkAction;
        Action progressAction;
        BackgroundWorker bw;
        ProgressBarWindow progressBarWindow;
        ProgressBar pbarTotal;
        ProgressBar pbarLine;
        Label plabel;
        int maximum;

        public BackWorker(ProgressBarWindow pbw)
        {
            this.progressBarWindow = pbw;
            this.pbarTotal = pbw.GetProgressBarTotal();
            this.pbarLine = pbw.GetProgressBarLine();
            this.plabel = pbw.GetLabel();

            bw = new BackgroundWorker();
            bw.DoWork += Dowork;
            bw.RunWorkerCompleted += Endwork;
            bw.ProgressChanged += ProgressChange;
        }

        public void SetMaximum(int maximum)
        {
            this.maximum = maximum;
        }

        private void ProgressChange(object sender, ProgressChangedEventArgs e)
        {
            this.progressAction();
        }

        private void Endwork(object sender, RunWorkerCompletedEventArgs e)
        {
            this.endworkAction();
        }

        private void Dowork(object sender, DoWorkEventArgs e)
        {
            this.doworkAction();
        }

        public void SetWork(Action work)
        {
            doworkAction = work;
        }

        public void SetEndWork(Action endWork)
        {
            this.endworkAction = endWork;
        }
        public void SetProgress(Action progress)
        {
            this.progressAction = progress;
        }

        public void Start()
        {
            bw.RunWorkerAsync();
        }
    }
}
