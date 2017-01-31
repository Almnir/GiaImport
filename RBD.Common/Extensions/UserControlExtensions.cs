using System.Drawing;
using System.Windows.Forms;

namespace RBD.Common.Extensions
{
//    internal class  A : IDisposable
//    {
//        public void ParceTree(Control control)
//        {
//            if (control is GridControl)
//            {
//                foreach (GridView view in (control as GridControl).Views)
//                {
//                    var appearance = view.Appearance;
//                    appearance.OddRow.BackColor =
//                        appearance.OddRow.BackColor2 = Color.White;

//                    appearance.EvenRow.BackColor2 = Color.White;
//#if GIA
//                    appearance.EvenRow.BackColor = Color.FromArgb(227, 233, 255);
//#else
//                    appearance.EvenRow.BackColor = System.Drawing.Color.Gainsboro;
//#endif
//                }
//                return;
//            }

//            control.ControlAdded += ControlAddedHandler;

//            foreach (Control c in control.Controls)
//            {
//                ParceTree(c);
//            } 
//        }

//        void ControlAddedHandler(object sender, ControlEventArgs e)
//        {
//            var cntr = sender as Control;
//            if (cntr == null) return;
//            ParceTree(cntr);
//        }

//        public void Dispose()
//        {
            
//        }
//    }

    public static class UserControlExtensions
    {
        public static Form CreateFormByControl(this UserControl view, string caption, Icon icon, IWin32Window owner)
        {
            var f = new Form
            {
                ShowIcon = true,
                ShowInTaskbar = true,
                AutoScroll = false,
                AutoSize = false,
                Icon = icon,
                Width = view.Width,
                Height = view.Height,
                MinimumSize = new Size(view.Width, view.Height),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            view.Dock = DockStyle.Fill;
            f.Text = caption;
            f.Controls.Add(view);
            f.ShowDialog(owner);
            return f;
        }
    }
}
