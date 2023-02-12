// <copyright file="SpreadsheetTest.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System.ComponentModel;
using SpreadsheetEngine;

namespace HomeworkFourTests
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
        private int rowCount;

        /// <summary>
        /// Number of columns.
        /// </summary>
        private int columnCount;

        /// <summary>
        /// 2D array that contains the cells that correspond to the UI's cells.
        /// </summary>
        private ConcreteCell[,] matrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetTest"/> class.
        /// </summary>
        /// <param name="rows"> Number of rows. </param>
        /// <param name="cols"> Number of columns. </param>
        public SpreadsheetTest(int rows, int cols)
        {
            this.matrix = new ConcreteCell[rows, cols];
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
        public Cell? GetCell(int row, int column)
        {
            if (row >= this.RowCount && column >= this.ColumnCount)
            {
                throw new ArgumentException("Row or column exceed the index size of the matrix.");
            }
            else if (this.matrix[row, column] == null)
            {
                return null;
            }

            return (Cell)this.matrix[row, column];
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
                    // Instantiate each cell and make the Spreadsheet's CellPropertyChanged
                    // delegate subscribe to the Cell objects event.
                    this.matrix[row, col] = new ConcreteCell(row, col);
                    this.matrix[row, col].PropertyChanged += this.HandleCellPropertyChanged;
                }
            }
        }

        /// <summary>
        /// Method for the observer. The broadcaster (Cell) will add this method as a delegate and will notify
        /// this observer of property changes. This observer will need to update accordingly. In this case,
        /// the broadcasting Cell will tell the Spreadsheet that a certain Cell's property changed, so the
        /// spreadsheet will invoke the CellPropertyChanged event, which has its own subscribers (the UI).
        /// </summary>
        /// <param name="sender"> Object sender. </param>
        /// <param name="e"> Event. </param>
        private void HandleCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = (Cell)sender;
            this.CellPropertyChanged?.Invoke(cell, e);
        }
    }
}
