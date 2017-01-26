namespace FtcReportControls.WindowsSystem.VistaTaskDialog
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Helper class required by LockSystem feature.
    /// </summary>
    internal class TaskDialogLockSystemParameters
    {
        public IntPtr NewDesktop;
        public Bitmap Background;

        public TaskDialogLockSystemParameters(IntPtr newDesktop, Bitmap background)
        {
            NewDesktop = newDesktop;
            Background = background;
        }
    }
}