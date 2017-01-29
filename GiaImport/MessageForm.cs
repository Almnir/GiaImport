using System.Drawing;
using System.Windows.Forms;

namespace GiaImport
{
    public partial class MessageForm : MetroFramework.Forms.MetroForm
    {
        public enum EnumMessageIcon
        {
            Error,
            Warning,
            Information,
            Question,
        }

        public MessageForm()
        {
            InitializeComponent();
        }

        public void SetTitle(string title)
        {
            this.Text = title;
        }

        public void SetContent(string content)
        {
            this.metroLabel1.Text = content;
        }

        public void SetExtendedContent(string content)
        {
            this.metroTextBox1.Text = content;
        }

        public void SetIcon(EnumMessageIcon messageIcon)
        {
            metroTile1.UseTileImage = true;
            switch (messageIcon)
            {
                case EnumMessageIcon.Error:
                    metroTile1.Image = SystemIcons.Error.ToBitmap();
                    break;
                case EnumMessageIcon.Information:
                    metroTile1.Image = SystemIcons.Information.ToBitmap();
                    break;
                case EnumMessageIcon.Question:
                    metroTile1.Image = SystemIcons.Question.ToBitmap();
                    break;
                case EnumMessageIcon.Warning:
                    metroTile1.Image = SystemIcons.Warning.ToBitmap();
                    break;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        public static void ShowDialog(string title, string content, string extendedcontent, EnumMessageIcon icon)
        {
            MessageForm mform = new MessageForm();
            mform.SetTitle(title);
            mform.SetContent(content);
            mform.SetExtendedContent(extendedcontent);
            mform.SetIcon(icon);
            mform.ShowDialog();
        }

        private void metroButton1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
