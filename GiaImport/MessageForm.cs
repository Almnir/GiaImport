using System.Drawing;
using System.Windows.Forms;

namespace GiaImport
{
    public partial class MessageForm : Form
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
            this.label1.Text = content;
        }

        public void SetExtendedContent(string content)
        {
            this.richTextBox1.Text = content;
        }

        public void SetIcon(EnumMessageIcon messageIcon)
        {
            switch (messageIcon)
            {
                case EnumMessageIcon.Error:
                    pictureBox1.Image = SystemIcons.Error.ToBitmap();
                    break;
                case EnumMessageIcon.Information:
                    pictureBox1.Image = SystemIcons.Information.ToBitmap();
                    break;
                case EnumMessageIcon.Question:
                    pictureBox1.Image = SystemIcons.Question.ToBitmap();
                    break;
                case EnumMessageIcon.Warning:
                    pictureBox1.Image = SystemIcons.Warning.ToBitmap();
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
    }
}
