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
        private Spreadsheet spreadsheet;

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

            // Connect the dgv cell events to the form1.cs delegates.
            this.dataGridView1.CellBeginEdit += this.HandleDgvCellBeginEdit;
            this.dataGridView1.CellEndEdit += this.HandleDgvCellEndEdit;
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
                else if (e.PropertyName == "Bad Reference")
                {
                    MessageBox.Show("There are one or more bad references in the inputted formula (Self or circular reference, non existing cell name, unrecognized operator, etc.).");
                }
            }
        }

        /// <summary>
        /// When a cell starts being edited, show the text property instead of value.
        /// </summary>
        /// <param name="sender"> object. </param>
        /// <param name="e"> event. </param>
        private void HandleDgvCellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            Cell? cell = this.spreadsheet.GetCell(row, col);

            if (cell != null)
            {
                DataGridViewCell dgvCell = this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex];

                dgvCell.Value = cell.Text;
            }
        }

        ///// <summary>
        ///// When a cell is being edited, text property is shown, once edit ends, show value property.
        ///// </summary>
        ///// <param name="sender"> object. </param>
        ///// <param name="e"> event. </param>
        private void HandleDgvCellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            Cell? cell = this.spreadsheet.GetCell(row, col);

            if (cell != null)
            {
                DataGridViewCell dgvCell = this.dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex];

                bool dgvCellIsNotNullAndStartsWithEqual = dgvCell.Value != null && dgvCell.Value.ToString().StartsWith("=");
                bool cellTextIsUnique = dgvCell.Value != null && cell.Text != dgvCell.Value.ToString();

                if (cellTextIsUnique || dgvCellIsNotNullAndStartsWithEqual)
                {
                    // Assign the dgv value being edited to/back to the cell text field and assign
                    // The current cell value back to the dgv value since editing has stopped
                    cell.Text = dgvCell.Value.ToString();
                    dgvCell.Value = cell.Value;
                }
                else
                {
                    // If the dgv value is null, then user entered/cleared the existing cell text
                    if (dgvCell.Value == null)
                    {
                        cell.Text = string.Empty;
                    }
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
            // Lambda expression to generate unique random indices to assign "Hello 321" to cells randomly.
            // Stylcop treated this lambda as a variable so camelcase was used.
            Func<List<Tuple<int, int>>, Tuple<int, int>> generateIndices = (indicesOfRandomCells) =>
            {
                Random random = new Random();
                Tuple<int, int> tuple = new Tuple<int, int>(random.Next(0, NUMROWS), random.Next(0, NUMCOLS));

                while (true)
                {
                    if (indicesOfRandomCells.Contains(tuple))
                    {
                        Tuple<int, int> newTuple = new Tuple<int, int>(random.Next(0, NUMROWS), random.Next(0, NUMCOLS));
                        tuple = newTuple;
                        continue;
                    }

                    indicesOfRandomCells.Add(tuple);
                    break;
                }

                return tuple;
            };

            // Reset the spreadsheet object for this demo
            this.spreadsheet = new Spreadsheet(NUMROWS, NUMCOLS);
            this.spreadsheet.CellPropertyChanged += this.HandleCellPropertyChanged;

            // Set the text in 50 random Cells
            List<Tuple<int, int>> indicesOfRandomCells = new List<Tuple<int, int>>();

            for (int i = 0; i < NUMCOLS; i++)
            {
                Tuple<int, int> tuple = generateIndices(indicesOfRandomCells);
                this.spreadsheet.GetCell(tuple.Item1, tuple.Item2).Text = "Hello 321";
            }

            // Set the text in every cell in column B to "This is cell B#"
            int columnIndex = 1;
            for (int row = 0; row < NUMROWS; row++)
            {
                this.spreadsheet.GetCell(row, columnIndex).Text = "This is cell B" + (row + 1); // Add one to corres. w/ the GUI indexes
            }

            // Set the text in every cell in column A to "=B#"
            columnIndex = 0;
            for (int row = 0; row < NUMROWS; row++)
            {
                this.spreadsheet.GetCell(row, columnIndex).Text = "=B" + (row + 1); // Add one to corres. w/ the GUI indexes
            }
        }
    }
}