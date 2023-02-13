// <copyright file="SpreadsheetTest.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System.ComponentModel;

namespace HomeworkFourTests.SpreadsheetEngineTests.TestClasses
{
    /// <summary>
    /// Testing class for the spreadsheet.cs class. In order to test certain methods, some setup needs
    /// to occur first, which cannot be done with the production version of spreadsheet.cs. We need access
    /// to certain internal methods like setCell() to setup our test spreadsheet.
    /// </summary>
    public class SpreadsheetTest
    {
        /// <summary>
        /// Number of rows.
        /// </summary>
        private readonly int rowCount;

        /// <summary>
        /// Number of columns.
        /// </summary>
        private readonly int columnCount;

        /// <summary>
        /// 2D array that contains the cells that correspond to the UI's cells.
        /// </summary>
        private ConcreteCellTest[,] matrix;

        /// <summary>
        /// Dictionary that serves to allow for quick access of Cells when only given a cell name.
        /// </summary>
        private Dictionary<string, Tuple<int, int>> cellIndexes;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetTest"/> class.
        /// </summary>
        /// <param name="rows"> Number of rows. </param>
        /// <param name="cols"> Number of columns. </param>
        public SpreadsheetTest(int rows, int cols)
        {
            this.matrix = new ConcreteCellTest[rows, cols];
            this.cellIndexes = new Dictionary<string, Tuple<int, int>>();
            this.rowCount = rows;
            this.columnCount = cols;
            this.FillMatrix();
        }

        /// <summary>
        /// Event that is triggered when any property of cells in the 2D array changes.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs>? CellPropertyChanged;

        /// <summary>
        /// Gets the row count.
        /// </summary>
        public int RowCount
        {
            get { return this.rowCount; }
        }

        /// <summary>
        /// Gets the column count.
        /// </summary>
        public int ColumnCount
        {
            get { return this.columnCount; }
        }

        /// <summary>
        /// Retrieve the cell given row and index.
        /// </summary>
        /// <param name="row"> Row index. </param>
        /// <param name="column"> Column index. </param>
        /// <returns> Return abstract Cell base type. </returns>
        public CellTest? GetCell(int row, int column)
        {
            if (row >= this.RowCount && column >= this.ColumnCount)
            {
                throw new ArgumentException("Row or column exceed the index size of the matrix.");
            }
            else if (this.matrix[row, column] == null)
            {
                return null;
            }

            return this.matrix[row, column];
        }

        /// <summary>
        /// Return the cell corresponding to the cellName. Make 
        /// </summary>
        /// <param name="cellName"> Cell name to search for. </param>
        /// <returns> Cell base object. </returns>
        public CellTest SearchCell(string cellName)
        {
            try
            {
                Tuple<int, int> indices = this.cellIndexes[cellName];
                ConcreteCellTest cell = this.matrix[indices.Item1, indices.Item2];
                return (CellTest)cell;
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Cell with name '" + cellName + "' not found."); 
            }
        }

        /// <summary>
        /// Internal SetCell method used for testing the GetCell method.
        /// </summary>
        /// <param name="row"> Row index. </param>
        /// <param name="col"> Col index. </param>
        /// <param name="text"> Text value. </param>
        internal void SetCell(int row, int col, string text)
        {
            this.matrix[row, col].Text = text;
        }

        /// <summary>
        /// Instantiates each cell of the matrix with a new concrete cell object.
        /// </summary>
        private void FillMatrix()
        {
            for (int row = 0; row < this.RowCount; row++)
            {
                for (int col = 0; col < this.ColumnCount; col++)
                {
                    // Instantiate each cell and make the Spreadsheet's HandleCellPropertyChanged
                    // delegate subscribe to the Cell objects event. Also add name, indices to dict.
                    ConcreteCellTest cell = new ConcreteCellTest(row, col);
                    this.cellIndexes.Add(cell.Name, new Tuple<int, int>(row, col));
                    this.matrix[row, col] = cell;
                    this.matrix[row, col].PropertyChanged += this.HandleCellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Method for the observer. The broadcaster (Cell) will add this method as a
        /// delegate subscriber and will notify this observer of property changes in broadcaster cell.
        /// The broadcasting Cell will tell the Spreadsheet that a certain Cell's property changed, so the
        /// spreadsheet will invoke the CellPropertyChanged event, which has its own subscribers (the UI).
        /// </summary>
        /// <param name="sender"> Object sender. </param>
        /// <param name="e"> Event. </param>
        private void HandleCellPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                CellTest? cell = (CellTest?)sender;
                if (cell != null)
                {
                    if (cell.Text[0] == '=')
                    {
                        // Support pulling the value from another cell. if starting with ‘=’ then assume
                        // the remaining part is the name of the cell we need to copy a value from.
                        string cellName = cell.Text.Substring(1);

                        CellTest? refCell = this.SearchCell(cellName);
                    }
                    else
                    {
                        cell.Value = cell.Text;
                    }

                    this.CellPropertyChanged?.Invoke(cell, e);
                }
            }
        }
    }
}
