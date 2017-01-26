namespace FtcReportControls.WindowsSystem.VistaTaskDialog
{
    using System.Windows.Forms;

    /// <summary>
    /// Helper methods…
    /// </summary>
    internal class TaskDialogHelpers
    {
        public static string GetButtonName(TaskDialogResult button)
        {
            switch (button)
            {
                case TaskDialogResult.Abort:
                    return ResourceStrings.AbortText;

                case TaskDialogResult.Cancel:
                    return ResourceStrings.CancelText;

                case TaskDialogResult.Close:
                    return ResourceStrings.CloseText;

                case TaskDialogResult.Continue:
                    return ResourceStrings.ContinueText;

                case TaskDialogResult.Ignore:
                    return ResourceStrings.IgnoreText;

                case TaskDialogResult.No:
                    return ResourceStrings.NoText;

                case TaskDialogResult.NoToAll:
                    return ResourceStrings.NoToAllText;

                case TaskDialogResult.OK:
                    return ResourceStrings.OKText;

                case TaskDialogResult.Retry:
                    return ResourceStrings.RetryText;

                case TaskDialogResult.Yes:
                    return ResourceStrings.YesText;

                case TaskDialogResult.YesToAll:
                    return ResourceStrings.YesToAllText;

                default:
                    return ResourceStrings.NoneText;
            }
        }

        public static DialogResult MakeDialogResult(TaskDialogResult Result)
        {
            switch (Result)
            {
                case TaskDialogResult.Abort:
                    return DialogResult.Abort;
                case TaskDialogResult.Cancel:
                case TaskDialogResult.Close:
                    return DialogResult.Cancel;
                case TaskDialogResult.Ignore:
                    return DialogResult.Ignore;
                case TaskDialogResult.No:
                case TaskDialogResult.NoToAll:
                    return DialogResult.No;
                case TaskDialogResult.OK:
                case TaskDialogResult.Continue:
                    return DialogResult.OK;
                case TaskDialogResult.Retry:
                    return DialogResult.Retry;
                case TaskDialogResult.Yes:
                case TaskDialogResult.YesToAll:
                    return DialogResult.Yes;
                default:
                    return DialogResult.None;
            }
        }

        public static TaskDialogDefaultButton MakeTaskDialogDefaultButton(MessageBoxDefaultButton DefaultButton)
        {
            switch (DefaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                    return TaskDialogDefaultButton.Button1;
                case MessageBoxDefaultButton.Button2:
                    return TaskDialogDefaultButton.Button2;
                case MessageBoxDefaultButton.Button3:
                    return TaskDialogDefaultButton.Button3;
                default:
                    return TaskDialogDefaultButton.None;
            }
        }
    }
}
