// <copyright file="Cell.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System.ComponentModel;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Abstract base class that represents a Cell object in the spreadsheet.
    /// Implements the INotifyPropertyChanged class since this class follows
    /// the observer/subscriber pattern.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// Index for a row.
        /// </summary>
        protected int rowIndex;

        /// <summary>
        /// Index for a column.
        /// </summary>
        protected int columnIndex;

        /// <summary>
        /// The actual text contained in the cell.
        /// </summary>
        protected string text;

        /// <summary>
        /// Represents the evaluated value fo the cell since cells can contain formulas.
        /// </summary>
        protected string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex"> Index of the row. </param>
        /// <param name="columnIndex"> Index of the column. </param>
        /// <param name="text"> Text content. </param>
        /// <param name="value"> Cell's value content. </param>
        public Cell(int rowIndex = 0, int columnIndex = 0, string text = "", string value = "")
        {
            this.rowIndex = rowIndex;
            this.columnIndex = columnIndex;
            this.text = text;
            this.value = value;
        }

        /// <summary>
        /// Event object representing a list of delegates (subscribers) that need to be notified
        /// when a certain event is triggered. Can add or remove delegates. When a cell's Text
        /// property is changed, we must call this.PropertyChanges() to invoke the delegates
        /// subsribed to this event.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets readonly rowIndex.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets readonly columnIndex.
        /// </summary>
        public int ColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Gets or sets the text for the cell. If new text is being set, notify all
        /// delegates subscribed (Spreadsheet.cs) to this observer about a change in property "Text".
        /// Since each cell has this PropertyChanged field, if any cell is changed, the corresponding
        /// cell will invoke the delegate so that spreadsheet.cs knows that that specific cell was changed.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text != value)
                {
                    this.text = value;
                }

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets the value field. Set can only be done internally within this namespace.
        /// </summary>
        public string Value
        {
            get { return this.value; }
            internal set { this.value = value; }
        }
    }
}