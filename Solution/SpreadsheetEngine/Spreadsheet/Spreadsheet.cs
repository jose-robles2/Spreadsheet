// <copyright file="Spreadsheet.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
            /// Determine if a our cell is equal to another cell.
            /// </summary>
            /// <param name="otherCell"> cell we are comparing. </param>
            /// <returns> bool. </returns>
            public bool Equals(ConcreteCell otherCell)
            {
                if (otherCell == null)
                {
                    return false;
                }

                if (this == otherCell)
                {
                    return true;
                }

                if (this.Text == otherCell.Text &&
                    this.Value == otherCell.Value &&
                    this.Name == otherCell.Name &&
                    this.RowIndex == otherCell.RowIndex &&
                    this.ColumnIndex == otherCell.ColumnIndex)
                {
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Sets the value and invokes a new property changed event.
            /// </summary>
            /// <param name="value"> New value. </param>
            public void SetValue(string value)
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
        private readonly Cell[,] matrix;

        /// <summary>
        /// Keep a list of all changed cells.
        /// </summary>
        private readonly List<Cell> changedCells = new List<Cell>();

        /// <summary>
        /// Dictionary that serves to allow for quick access of Cells when only given a cell name.
        /// </summary>
        private Dictionary<string, Tuple<int, int>> cellIndexes = new Dictionary<string, Tuple<int, int>>();

        /// <summary>
        /// Dictionary that maps a string cellname to a hashset of cell name dependencies.
        /// </summary>
        private Dictionary<string, HashSet<string>> cellDependencies = new Dictionary<string, HashSet<string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows"> Number of rows. </param>
        /// <param name="cols"> Number of columns. </param>
        public Spreadsheet(int rows, int cols)
        {
            this.matrix = new ConcreteCell[rows, cols];
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
        /// Gets the changed cells.
        /// </summary>
        public List<Cell> ChangedCells
        {
            get { return this.changedCells; }
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
        public Cell? GetCell(string cellName)
        {
            try
            {
                Tuple<int, int> indices = this.cellIndexes[cellName];
                return this.matrix[indices.Item1, indices.Item2];
            }
            catch (KeyNotFoundException)
            {
                // Originally was throwing an exception, catch and return null
                return null;
            }
        }

        /// <summary>
        /// Add a cell to list of changed cells if it's been changed, if it's been restored to default remove it.
        /// </summary>
        /// <param name="cell"> cell. </param>
        private void HandleChangedCell(ConcreteCell cell)
        {
            if (cell.Text == string.Empty && cell.BGColor == Cell.DEFAULTCOLOR && this.changedCells.Contains(cell))
            {
                this.changedCells.Remove(cell);
            }
            else
            {
                this.AddChangedCell(cell);
            }
        }

        /// <summary>
        /// Add a change cell to a list if its fields are not default values.
        /// </summary>
        /// <param name="cell"> cell. </param>
        private void AddChangedCell(ConcreteCell cell)
        {
            if (cell.Text != string.Empty || cell.BGColor != Cell.DEFAULTCOLOR)
            {
                if (!this.changedCells.Contains(cell))
                {
                    this.changedCells.Add(cell);
                }
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
            ConcreteCell? cell = (ConcreteCell?)sender;

            if (cell == null)
            {
                return;
            }

            if (e.PropertyName == "Text")
            {
                if (cell.Text.StartsWith("="))
                {
                    string formula = cell.Text.Substring(1);

                    if (!this.ValidateFormula(cell, formula))
                    {
                        this.HandleBadCellReference(cell);
                        return;
                    }
                }

                this.Evaluate(cell);
            }
            else if (e.PropertyName == "Value")
            {
                this.CellPropertyChanged?.Invoke(cell, e);
            }
            else if (e.PropertyName == "OverWriteFormula")
            {
                if (e is CellChangedEventArgs newE)
                {
                    this.RemoveCellDependency(cell, newE.OldFormula);
                    this.HandleCellPropertyChanged(cell, new PropertyChangedEventArgs("Text"));
                }
            }
            else if (e.PropertyName == "Color")
            {
                this.CellPropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Color"));
            }

            this.HandleChangedCell(cell);
        }

        /// <summary>
        /// Validate all aspects of the inputted formula.
        /// </summary>
        /// <param name="cell"> cell. </param>
        /// <param name="expression"> expression. </param>
        /// <returns> bool. </returns>
        private bool ValidateFormula(ConcreteCell cell, string expression)
        {
            ExpressionTree exprTree = new ExpressionTree(expression);

            // ONLY handles formulas that don't follow variable naming
            // scheme. Unrecognized ops, mismatched parenths, bad syntax.
            if (exprTree.Size == 0)
            {
                return false;
            }

            // Handles self and circular references and bad var names
            if (!this.IsFormulaInputValid(cell, exprTree))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate user input's formula. No circular references and no self cell references.
        /// </summary>
        /// <param name="currentCell"> Current cell. </param>
        /// <param name="currentCellExpr"> Tree for cell. </param>
        /// <returns> bool. </returns>
        private bool IsFormulaInputValid(ConcreteCell currentCell, ExpressionTree currentCellExpr)
        {
            if (this.IsSelfReference(currentCell, currentCellExpr))
            {
                return false;
            }

            foreach (string variable in currentCellExpr.GetVariables())
            {
                if (!this.IsCellNameValid(variable))
                {
                    return false;
                }
            }

            // No self reference or bad reference, add cell dependency now to check if there's a circular reference
            this.AddCellDependency(currentCell, currentCellExpr.GetVariables());

            if (this.IsCircularReference(currentCell, currentCellExpr))
            {
                // Circular reference found, remove the dependency we just added
                this.RemoveCellDependency(currentCell, "=" + currentCellExpr.Expression);
                return false;
            }

            return true;
        }

        /// <summary>
        /// If a cell contains a formula, iterate over the set of variables in the formula to add
        /// the current cell name as a dependency to that variable. If the variable changes, it will
        /// alert its dependencies to update their values.
        /// </summary>
        /// <param name="cell"> Current cell containing some formula. </param>
        /// <param name="variables"> Tokenized variables within the current cell formula. </param>
        private void AddCellDependency(ConcreteCell cell, HashSet<string> variables)
        {
            foreach (string variable in variables)
            {
                if (!this.cellDependencies.ContainsKey(variable))
                {
                    this.cellDependencies.Add(variable, new HashSet<string>());
                }

                this.cellDependencies[variable].Add(cell.Name);
            }
        }

        /// <summary>
        /// A cell with an existing formula has been overwritten with a new formula, remove old dependencies.
        /// </summary>
        /// <param name="cell"> cell. </param>
        /// <param name="oldFormula"> old formula. </param>
        private void RemoveCellDependency(ConcreteCell cell, string oldFormula)
        {
            ExpressionTree exprTree = new ExpressionTree(oldFormula.Substring(1));

            foreach (string variable in exprTree.GetVariables())
            {
                try
                {
                    if (this.cellDependencies[variable].Contains(cell.Name))
                    {
                        this.cellDependencies[variable].Remove(cell.Name);

                        if (this.cellDependencies[variable].Count == 0)
                        {
                            this.cellDependencies.Remove(variable);
                        }
                    }
                }
                catch
                {
                    return; // When a bad reference is caught, the formula is stored in text but doesnt contain any vars in cellDeps dict.
                }
            }
        }

        /// <summary>
        /// Evaluate the cell's text (can be formula) that is currently being edited.
        /// </summary>
        /// <param name="cell"> cell being edited. </param>
        private void Evaluate(ConcreteCell cell)
        {
            if (string.IsNullOrEmpty(cell.Text))
            {
                cell.SetValue(string.Empty);
                this.UpdateCellDependencies(cell, this.Evaluate);
            }
            else if (cell.Text.StartsWith("=") && !string.IsNullOrEmpty(cell.Text.Substring(1)))
            {
                this.EvaluateFormula(cell);
                this.UpdateCellDependencies(cell, this.Evaluate);
            }
            else
            {
                double value;
                Action<ConcreteCell> setTextHelper = dependentCell => { };
                setTextHelper = dependentCell =>
                {
                    if (dependentCell.Text.StartsWith("="))
                    {
                        string formula = dependentCell.Text.Substring(1);
                        if (Expression.TokenizeExpression(formula)?.Count > 1)
                        {
                            dependentCell.SetValue("#VALUE!");
                            this.UpdateCellDependencies(dependentCell, setTextHelper);
                        }
                        else
                        {
                            this.EvaluateFormula(dependentCell);
                            this.UpdateCellDependencies(dependentCell, setTextHelper);
                        }
                    }
                };

                cell.SetValue(cell.Text);

                // Are we setting a double or string to the cell's value
                if (!double.TryParse(cell.Text, out value))
                {
                    this.UpdateCellDependencies(cell, setTextHelper);
                }
                else
                {
                    this.UpdateCellDependencies(cell, this.Evaluate);
                }
            }
        }

        /// <summary>
        /// Evaluate the cell's formula.
        /// </summary>
        /// <param name="cell"> Current cell. </param>
        private void EvaluateFormula(ConcreteCell cell)
        {
            ExpressionTree exprTree = new ExpressionTree(cell.Text.Substring(1));
            HashSet<string> variables = exprTree.GetVariables();

            foreach (string variable in variables)
            {
                double value;
                ConcreteCell? varCell = (ConcreteCell?)this.GetCell(variable);
                if (varCell == null)
                {
                    return;
                }

                if (!double.TryParse(varCell.Value, out value))
                {
                    // Parse failed, is varCell empty or does it contain string?
                    if (string.IsNullOrEmpty(varCell.Value))
                    {
                        exprTree.SetVariable(varCell.Name, 0);
                    }
                    else
                    {
                        cell.SetValue(varCell.Value);
                        return;
                    }
                }
                else
                {
                    exprTree.SetVariable(varCell.Name, value);
                }
            }

            cell.SetValue(exprTree.Evaluate().ToString());
        }

        /// <summary>
        /// Update cell dependencies. This method is used multiple times for different reasons, so take
        /// in a lambda dependentAction that takes a dependent cell as a parameter.
        /// </summary>
        /// <param name="cell"> current cell. </param>
        /// <param name="dependentAction"> lambda. </param>
        private void UpdateCellDependencies(ConcreteCell cell, Action<ConcreteCell> dependentAction)
        {
            if (this.cellDependencies.ContainsKey(cell.Name))
            {
                foreach (string dependentCellName in this.cellDependencies[cell.Name])
                {
                    ConcreteCell? dependentCell = (ConcreteCell?)this.GetCell(dependentCellName);

                    if (dependentCell != null)
                    {
                        dependentAction(dependentCell);
                    }
                }
            }
        }

        /// <summary>
        /// Notify the UI about a bad cell reference.
        /// </summary>
        /// <param name="cell"> Current cell. </param>
        private void HandleBadCellReference(ConcreteCell cell)
        {
            this.CellPropertyChanged?.Invoke(cell, new PropertyChangedEventArgs("Bad Reference"));
        }

        /// <summary>
        /// Check if a cell has a circular reference contained within its formula.
        /// </summary>
        /// <param name="cell"> cell. </param>
        /// <param name="exprTree"> tree. </param>
        /// <returns> bool. </returns>
        private bool IsCircularReference(ConcreteCell cell, ExpressionTree exprTree)
        {
            HashSet<string> variables = exprTree.GetVariables();

            foreach (string varName in variables)
            {
                if (this.IsCircularHelper(cell, varName))
                {
                    this.HandleBadCellReference(cell);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// For each cell, check its dependencies for circular references by utilizing a dfs lambda.
        /// </summary>
        /// <param name="cell"> cell. </param>
        /// <param name="varName"> varName within cell's formula. </param>
        /// <returns> bool. </returns>
        private bool IsCircularHelper(ConcreteCell cell, string varName)
        {
            Func<string, string, bool> isCircularDFS = (curName, varName) => { return false; };
            isCircularDFS = (curName, varName) =>
            {
                if (curName == varName)
                {
                    return true;
                }

                foreach (string depName in this.cellDependencies[curName])
                {
                    if (this.cellDependencies.ContainsKey(depName))
                    {
                        return isCircularDFS(depName, varName);
                    }
                }

                return false;
            };

            if (cell.Name == varName)
            {
                return true;
            }

            if (!this.cellDependencies.ContainsKey(cell.Name))
            {
                return false;
            }

            return isCircularDFS(cell.Name, varName);
        }

        /// <summary>
        /// Checks to see if a cell contains a reference to itself.
        /// </summary>
        /// <param name="cell"> cell we are checking. </param>
        /// <param name="exprTree"> tree for cell we are checking. </param>
        /// <returns> bool. </returns>
        private bool IsSelfReference(ConcreteCell cell, ExpressionTree exprTree)
        {
            return exprTree.GetVariables().Contains(cell.Name) ? true : false;
        }

        /// <summary>
        /// Check if the cell name is valid.
        /// </summary>
        /// <param name="cellName"> name of cell. </param>
        /// <returns> bool. </returns>
        private bool IsCellNameValid(string cellName)
        {
            return this.GetCell(cellName) != null ? true : false;
        }
    }
}