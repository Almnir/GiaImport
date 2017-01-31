using System.Drawing;
using System.Windows.Forms;

namespace GiaImport
{
    public partial class MessageForm : MetroFramework.Forms.MetroForm
    {
        public enum EnumMessageStyle
        {
            Error,
            Information
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

        public void SetStyling(EnumMessageStyle messageStyle)
        {
            metroTile1.UseTileImage = true;
            metroTile1.TileCount = 1;
            switch (messageStyle)
            {
                case EnumMessageStyle.Error:
                    metroTile1.Style = MetroFramework.MetroColorStyle.Red;
                    metroTile1.Text = "Error";
                    break;
                case EnumMessageStyle.Information:
                    metroTile1.Style = MetroFramework.MetroColorStyle.Default;
                    metroTile1.Text = "Info";
                    break;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        public static void ShowDialog(string title, string content, string extendedcontent, EnumMessageStyle messageStyle)
        {
            MessageForm mform = new MessageForm();
            mform.SetTitle(title);
            mform.SetContent(content);
            mform.SetExtendedContent(extendedcontent);
            mform.SetStyling(messageStyle);
            mform.ShowDialog();
        }

        private void metroButton1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
