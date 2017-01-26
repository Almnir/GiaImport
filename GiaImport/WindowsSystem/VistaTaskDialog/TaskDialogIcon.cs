namespace FtcReportControls.WindowsSystem.VistaTaskDialog
{
    using System.Drawing;

    /// <summary>
    /// Specifies constants defining which icon to display on what background.
    /// </summary>
    public enum TaskDialogIcon
    {
        /// <summary>
        /// The TaskDialog contains no icon. The background is white.
        /// </summary>
        None,
        /// <summary>
        /// The TaskDialog contains a symbol consisting of a lowercase letter i in a circle. The background is white.
        /// </summary>
        Information,
        /// <summary>
        /// The TaskDialog contains a symbol consisting of a question mark in a circle. The background is white.
        /// </summary>
        Question,
        /// <summary>
        /// The TaskDialog contains a symbol consisting of an exclamation point in a yellow triangle. The background is white.
        /// </summary>
        Warning,
        /// <summary>
        /// The TaskDialog contains a symbol consisting of white X in a red circle. The background is white.
        /// </summary>
        Error,
        /// <summary>
        /// The TaskDialog contains a symbol consisting of white check mark in a green shield. The background is green.
        /// </summary>
        SecuritySuccess,
        /// <summary>
        /// The TaskDialog contains a symbol consisting of a question mark in a blue shield. The background is blue.
        /// </summary>
        SecurityQuestion,
        /// <summary>
        /// The TaskDialog contains a symbol consisting of an exclamation point in a yellow shield. The background is yellow.
        /// </summary>
        SecurityWarning,
        /// <summary>
        /// The TaskDialog contains a symbol consisting of white X in a red shield. The background is red.
        /// </summary>
        SecurityError,
        /// <summary>
        /// The TaskDialog contains a symbol of a multicolored shield. The background is white.
        /// </summary>
        SecurityShield,
        /// <summary>
        /// The TaskDialog contains a symbol of a multicolored shield. The background is blue-to-green gradient.
        /// </summary>
        SecurityShieldBlue,
        /// <summary>
        /// The TaskDialog contains a symbol of a multicolored shield. The background is gray.
        /// </summary>
        SecurityShieldGray
    }

    public class TaskDialogBigIcon
    {
        public static readonly Image Question = FtcReportControls.Properties.Resources.BigQuestion;
        public static readonly Image Information = FtcReportControls.Properties.Resources.BigInformation;
        public static readonly Image Warning = FtcReportControls.Properties.Resources.BigWarning;
        public static readonly Image Error = FtcReportControls.Properties.Resources.BigError;
        public static readonly Image Security = FtcReportControls.Properties.Resources.BigSecurity;
        public static readonly Image SecuritySuccess = FtcReportControls.Properties.Resources.BigSecuritySuccess;
        public static readonly Image SecurityQuestion = FtcReportControls.Properties.Resources.BigSecurityQuestion;
        public static readonly Image SecurityWarning = FtcReportControls.Properties.Resources.BigSecurityWarning;
        public static readonly Image SecurityError = FtcReportControls.Properties.Resources.BigSecurityError;
    }

    public class TaskDialogMediumIcon
    {
        public static readonly Image Question = FtcReportControls.Properties.Resources.Question;
        public static readonly Image Information = FtcReportControls.Properties.Resources.Information;
        public static readonly Image Warning = FtcReportControls.Properties.Resources.Warning;
        public static readonly Image Error = FtcReportControls.Properties.Resources.Error;
        public static readonly Image Security = FtcReportControls.Properties.Resources.Security;
        public static readonly Image SecuritySuccess = FtcReportControls.Properties.Resources.SecuritySuccess;
        public static readonly Image SecurityQuestion = FtcReportControls.Properties.Resources.SecurityQuestion;
        public static readonly Image SecurityWarning = FtcReportControls.Properties.Resources.SecurityWarning;
        public static readonly Image SecurityError = FtcReportControls.Properties.Resources.SecurityError;
    }

    public class TaskDialogSmallIcon
    {
        public static readonly Image Question = FtcReportControls.Properties.Resources.SmallQuestion;
        public static readonly Image Information = FtcReportControls.Properties.Resources.SmallInformation;
        public static readonly Image Warning = FtcReportControls.Properties.Resources.SmallWarning;
        public static readonly Image Error = FtcReportControls.Properties.Resources.SmallError;
        public static readonly Image Security = FtcReportControls.Properties.Resources.SmallSecurity;
        public static readonly Image SecuritySuccess = FtcReportControls.Properties.Resources.SmallSecuritySucess;
        public static readonly Image SecurityQuestion = FtcReportControls.Properties.Resources.SmallSecurityQuestion;
        public static readonly Image SecurityWarning = FtcReportControls.Properties.Resources.SmallSecurityWarning;
        public static readonly Image SecurityError = FtcReportControls.Properties.Resources.SmallSecurityError;
    }

}