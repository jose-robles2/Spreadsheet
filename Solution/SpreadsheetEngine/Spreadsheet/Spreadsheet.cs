// <copyright file="Spreadsheet.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Spreadsheet
{
    /// <summary>
    /// Container and factory object for a 2D array of cell objects.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// Private class that implements abstract class cell. Only accessible via Spreadsheet.cs.
        /// </summary>
        private class ConcreteCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ConcreteCell"/> class.
            /// </summary>
            /// <param name="rowIndex"> Index of the row. </param>
            /// <param name="columnIndex"> Index of the column. </param>
            /// <param name="text"> Text content. </param>
            /// <param name="value"> Cell's value content. </param>
            public ConcreteCell(int rowIndex = 0, int columnIndex = 0, string text = "", string value = "")
                : base(rowIndex, columnIndex, text, value)
            {
            }

            /// <summary>
            /// Sets the value and invokes a new property changed event.
            /// </summary>
            /// <param name="value"> New value. </param>
            public override void SetValue(string value)
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.InvokePropertyChanged("Value");
                }
            }
        }

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
        private ConcreteCell[,] matrix;

        /// <summary>
        /// Dictionary that serves to allow for quick access of Cells when only given a cell name.
        /// </summary>
        private Dictionary<string, Tuple<int, int>> cellIndexes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows"> Number of rows. </param>
        /// <param name="cols"> Number of columns. </param>
        public Spreadsheet(int rows, int cols)
        {
            this.matrix = new ConcreteCell[rows, cols];
            this.cellIndexes = new Dictionary<string, Tuple<int, int>>();
            this.rowCount = rows;
            this.columnCount = cols;
            this.FillMatrix();
        }

        /// <summary>
        /// Event that is triggered when any property of cells in the 2D array changes.
        /// </summary>
        public event PropertyChangedEventHandler? CellPropertyChanged;

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
            bool upperBound = row >= this.RowCount || column >= this.ColumnCount;
            bool lowerBount = row < 0 || column < 0;

            if (upperBound || lowerBount)
            {
                throw new ArgumentException("Row or column index exceed or fall below size of the matrix.");
            }
            else if (this.matrix[row, column] == null)
            {
                return null;
            }

            return (Cell)this.matrix[row, column];
        }

        /// <summary>
        /// Return the cell corresponding to the cellName.
        /// </summary>
        /// <param name="cellName"> Cell name to search for. </param>
        /// <returns> Cell base object. </returns>
        public Cell GetCell(string cellName)
        {
            try
            {
                Tuple<int, int> indices = this.cellIndexes[cellName];
                ConcreteCell cell = this.matrix[indices.Item1, indices.Item2];
                return (Cell)cell;
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Cell with name '" + cellName + "' not found.");
            }
        }

        /// <summary>
        /// Set the text in 50 random cells to "Hello 321".
        /// Then set the text in every cell in column B to "This is cell B#" where # is the row number.
        /// Then set the text in every cell in column A to "=B#" where # is the row number. This will make
        /// every cell in column A equal the value contained within column B's cells.
        /// </summary>
        public void HomeworkFourDemo()
        {
            // Lambda expression to generate unique random indices to assign "Hello 321" to cells randomly.
            // Stylcop treated this lambda as a variable so camelcase was used.
            Func<List<Tuple<int, int>>, Tuple<int, int>> generateIndices = (indicesOfRandomCells) =>
            {
                Random random = new Random();
                Tuple<int, int> tuple = new Tuple<int, int>(random.Next(0, this.rowCount), random.Next(0, this.columnCount));

                while (true)
                {
                    if (indicesOfRandomCells.Contains(tuple))
                    {
                        Tuple<int, int> newTuple = new Tuple<int, int>(random.Next(0, this.rowCount), random.Next(0, this.columnCount));
                        tuple = newTuple;
                        continue;
                    }

                    indicesOfRandomCells.Add(tuple);
                    break;
                }

                return tuple;
            };

            // Set the text in 50 random Cells
            List<Tuple<int, int>> indicesOfRandomCells = new List<Tuple<int, int>>();

            for (int i = 0; i < this.ColumnCount; i++)
            {
                Tuple<int, int> tuple = generateIndices(indicesOfRandomCells);
                this.matrix[tuple.Item1, tuple.Item2].Text = "Hello 321";
            }

            // Set the text in every cell in column B to "This is cell B#"
            int columnIndex = 1;
            for (int row = 0; row < this.RowCount; row++)
            {
                this.matrix[row, columnIndex].Text = "This is cell B" + (row + 1); // Add one to corres. w/ the GUI indexes
            }

            // Set the text in every cell in column A to "=B#"
            columnIndex = 0;
            for (int row = 0; row < this.RowCount; row++)
            {
                this.matrix[row, columnIndex].Text = "=B" + (row + 1); // Add one to corres. w/ the GUI indexes
            }
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
                    ConcreteCell cell = new ConcreteCell(row, col);
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
            Cell? cell = (Cell?)sender;

            if (cell == null)
            {
                return;
            }

            if (e.PropertyName == "Text")
            {
                if (cell.Text[0] == '=')
                {
                    string cellName = cell.Text.Substring(1);
                    Cell refCell = this.GetCell(cellName);
                    cell.SetValue(refCell.Value);
                }
                else
                {
                    cell.SetValue(cell.Text);
                }
            }
            else if (e.PropertyName == "Value")
            {
                this.CellPropertyChanged?.Invoke(cell, e);
            }
        }
    }
}