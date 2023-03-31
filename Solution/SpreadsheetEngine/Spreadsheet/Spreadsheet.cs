// <copyright file="Spreadsheet.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine.Expressions;

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
        private Cell[,] matrix;

        /// <summary>
        /// Dictionary that serves to allow for quick access of Cells when only given a cell name.
        /// </summary>
        private Dictionary<string, Tuple<int, int>> cellIndexes;

        /// <summary>
        /// Dictionary that maps a string cellname to a hashset of cell name dependencies.
        /// </summary>
        private Dictionary<string, HashSet<string>> cellDependencies;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows"> Number of rows. </param>
        /// <param name="cols"> Number of columns. </param>
        public Spreadsheet(int rows, int cols)
        {
            this.matrix = new ConcreteCell[rows, cols];
            this.cellIndexes = new Dictionary<string, Tuple<int, int>>();
            this.cellDependencies = new Dictionary<string, HashSet<string>>();
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

            return this.matrix[row, column];
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
                return this.matrix[indices.Item1, indices.Item2];
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Cell with name '" + cellName + "' not found.");
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
                    string formula = cell.Text.Substring(1);

                    ExpressionTree tree = new ExpressionTree(formula);

                    this.AddCellDependency(cell.Name, tree.GetVariables());
                }
                else
                {
                    cell.SetValue(cell.Text);
                }

                this.Evaluate(cell);
            }
            else if (e.PropertyName == "Value")
            {
                this.CellPropertyChanged?.Invoke(cell, e);
            }
        }

        /// <summary>
        /// If a cell contains a formula, iterate over the set of variables in the formula to add
        /// the current cell name as a dependency to that variable. If the variable changes, it will
        /// alert its dependencies to update their values.
        /// </summary>
        /// <param name="cellName"> Current cell containing some formula. </param>
        /// <param name="variables"> Tokenized variables within the current cell formula. </param>
        private void AddCellDependency(string cellName, HashSet<string> variables)
        {
            foreach (string variable in variables)
            {
                if (!this.cellDependencies.ContainsKey(variable))
                {
                    this.cellDependencies.Add(variable, new HashSet<string>());
                }

                this.cellDependencies[variable].Add(cellName);
            }
        }

        /// <summary>
        /// Evaluate the cell's text (can be formula) that is currently being edited.
        /// </summary>
        /// <param name="cell"> cell being edited. </param>
        private void Evaluate(Cell cell)
        {
            if (string.IsNullOrEmpty(cell.Text))
            {
                cell.SetValue(string.Empty);
            }
            else if (cell.Text[0] == '=')
            {
                if (!this.IsFormulaInputValid(cell))
                {
                    return;
                }

                this.EvaluateFormula(cell);
            }
            else
            {
                cell.SetValue(cell.Text);
            }

            // Check if current cell has dependencies that need to be updated
            if (this.cellDependencies.ContainsKey(cell.Name))
            {
                // Iterate over each dependent for the current cell and evaluate to make sure theyre up to date.
                foreach (string dependentCellName in this.cellDependencies[cell.Name])
                {
                    Cell dependentCell = this.GetCell(dependentCellName);

                    if (dependentCell != null)
                    {
                        this.Evaluate(dependentCell);
                    }
                }
            }
        }

        /// <summary>
        /// Validate user input's formula. No circular references and no self cell references.
        /// </summary>
        /// <param name="cell"> Current cell. </param>
        /// <returns> bool. </returns>
        private bool IsFormulaInputValid(Cell cell)
        {
            // TODO: Implement checks for circular references and a cell referencing itself
            // Not required for HW7
            return true;
        }

        /// <summary>
        /// Evaluate the cell's formula.
        /// </summary>
        /// <param name="cell"> Current cell. </param>
        private void EvaluateFormula(Cell cell)
        {
            ExpressionTree exprTree = new ExpressionTree(cell.Text.Substring(1));
            HashSet<string> variables = exprTree.GetVariables();

            foreach (string variable in variables)
            {
                Cell varCell = this.GetCell(variable);
                double value;

                if (string.IsNullOrEmpty(varCell.Value) || !double.TryParse(varCell.Value, out value))
                {
                    exprTree.SetVariable(varCell.Name, 0);
                }
                else
                {
                    exprTree.SetVariable(varCell.Name, value);
                }
            }

            cell.SetValue(exprTree.Evaluate().ToString());
        }
    }
}