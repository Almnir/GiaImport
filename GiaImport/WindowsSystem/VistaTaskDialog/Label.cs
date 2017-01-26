namespace FtcReportControls.WindowsSystem.VistaTaskDialog
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Label which measures its size in a “message box”-like way.
    /// </summary>
    internal partial class Label : LinkLabel
    {
        public Label()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            LinkColor = Color.FromArgb(0, 102, 204);
            ActiveLinkColor = LinkColor;
            DisabledLinkColor = Color.FromArgb(126, 133, 156);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            proposedSize = base.GetPreferredSize(proposedSize);
            int w = Screen.FromControl(this).WorkingArea.Width / 2;
            proposedSize.Width = w < proposedSize.Width ? w : proposedSize.Width;
            return base.GetPreferredSize(proposedSize);
        }
    }
}