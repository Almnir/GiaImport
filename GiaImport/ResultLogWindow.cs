using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GiaImport
{
    public partial class ResultLogWindow : MetroFramework.Forms.MetroForm
    {
        public ResultLogWindow()
        {
            InitializeComponent();
        }

        public ResultLogWindow(DataTable dataTable, string logText)
        {
            InitializeComponent();
            this.resultGrid.Columns.Clear();
            this.resultGrid.AutoGenerateColumns = true;
            this.resultGrid.DataSource = dataTable;
            this.logTextBox.Clear();
            this.logTextBox.Text = logText;
            this.resultGrid.DataBindingComplete += ResultGrid_DataBindingComplete;
        }

        private void ResultGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in this.resultGrid.Rows)
            {
                if ((int)row.Cells[Globals.GRID_TOTAL].Value != (int)row.Cells[Globals.GRID_LOADER].Value)
                {
                    row.Cells[Globals.GRID_TOTAL].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.White };
                    row.Cells[Globals.GRID_LOADER].Style = new DataGridViewCellStyle { ForeColor = Color.Red, BackColor = Color.White };
                }
            }
            this.resultGrid.AutoResizeColumns();
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
