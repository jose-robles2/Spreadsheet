// <copyright file="Form1.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System.ComponentModel;
using SpreadsheetEngine.Spreadsheet;

namespace SpreadsheetFrontEnd
{
    /// <summary>
    /// MyForm class that points to the running instance of the form for this program.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Constant pointing to the number of rows for the UI of the app.
        /// </summary>
        private const int NUMROWS = 50;

        /// <summary>
        /// Constant pointint to the number of cols for the UI of the app.
        /// </summary>
        private const int NUMCOLS = 26;

        /// <summary>
        /// Spreadsheet object that runs in the backend of the UI.
        /// </summary>
        private Spreadsheet? spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
            this.InitializeSpreadSheetObject();
        }

        /// <summary>
        /// Initialize the data grid front end component by creating columns A to Z. Create a list of 26
        /// chars and use ToString() so that they can be inserted as column name, and header using the Add() method.
        /// Then create 50 rows.
        /// </summary>
        private void InitializeDataGrid()
        {
            IEnumerable<int> range = Enumerable.Range('A', 26);
            List<char> alphabet = range.Select(i => (char)i).ToList();

            foreach (char c in alphabet)
            {
                this.dataGridView1.Columns.Add(c.ToString(), c.ToString());
            }

            for (int i = 1; i <= NUMROWS; i++)
            {
                this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

            // Increase header width so that the full number can be shown without the user manually expanding it.
            this.dataGridView1.RowHeadersWidth += 10;
        }

        /// <summary>
        /// Initialize the spreadsheet object with 26 rows and 50 columns, also
        /// subscribe to the spreadsheet's CellPropertyChanged event. So that the
        /// datagridview object can be updated accordingly.
        /// </summary>
        private void InitializeSpreadSheetObject()
        {
            this.spreadsheet = new Spreadsheet(NUMROWS, NUMCOLS);
            this.spreadsheet.CellPropertyChanged += this.HandleCellPropertyChanged;
        }

        /// <summary>
        /// On CellPropertyChanged, update the datagridview on the UI.
        /// </summary>
        /// <param name="sender"> Object associated with the event. </param>
        /// <param name="e"> The event. </param>
        private void HandleCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Cell? cell = (Cell?)sender;

            if (cell != null)
            {
                DataGridViewCell dgvCell = this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex];

                if (e.PropertyName == "Value")
                {
                    dgvCell.Value = cell.Value;
                }
            }
        }

        /// <summary>
        /// Button that initiates a demo of the spreadsheet by filling random cells with text values in order
        /// to show that the broadcaster/observer pattern is working. When a cell object is modified, the cell
        /// signals to the spreadsheet object, which signals to the UI (Form.cs).
        /// </summary>
        /// <param name="sender"> Object sender. </param>
        /// <param name="e"> The event. </param>
        private void DemoButton_Click(object sender, EventArgs e)
        {
            this.spreadsheet?.HomeworkFourDemo();
        }
    }
}