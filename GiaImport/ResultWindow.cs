using System;
using System.Collections.Generic;
using System.Data;

namespace GiaImport
{
    public partial class ResultWindow : MetroFramework.Forms.MetroForm
    {
        public ResultWindow()
        {
            InitializeComponent();
            Load += ResultWindow_Load;
        }

        private void ResultWindow_Load(object sender, EventArgs e)
        {
            this.resultGrid.AutoResizeColumns();
        }

        public ResultWindow(DataTable dataTable)
        {
            InitializeComponent();
            this.resultGrid.Columns.Clear();
            this.resultGrid.AutoGenerateColumns = true;
            this.resultGrid.DataSource = dataTable;
        }

        public void SetTableData(List<TableInfo> tableData)
        {
            foreach (var td in tableData)
            {
                this.resultGrid.Rows.Add(td.Name, td.Description, td.Status);
            }
            this.resultGrid.Refresh();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
