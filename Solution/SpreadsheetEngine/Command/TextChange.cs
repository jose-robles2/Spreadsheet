// <copyright file="TextChange.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine.Spreadsheet;

namespace SpreadsheetEngine.Command
{
    /// <summary>
    /// Class that interacts with the domain to invoke a text change. This is called by our command.
    /// </summary>
    public class TextChange
    {
        /// <summary>
        /// Cell we need to change.
        /// </summary>
        private Cell cell;

        /// <summary>
        /// Text we need to change to.
        /// </summary>
        private string newText;

        /// <summary>
        /// Old text we could change to.
        /// </summary>
        private string oldText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextChange"/> class.
        /// </summary>
        /// <param name="cell"> Cell whose text will be changed. </param>
        /// <param name="newText"> New text. </param>
        /// <param name="oldText"> Old text. </param>
        public TextChange(Cell cell, string newText, string oldText)
        {
            this.cell = cell;
            this.newText = newText;
            this.oldText = oldText;
        }

        /// <summary>
        /// Undo a text change.
        /// </summary>
        public void Undo()
        {
            this.cell.Text = this.oldText;
        }

        /// <summary>
        /// Redo a text change that was undone.
        /// </summary>
        public void Redo()
        {
            this.cell.Text = this.newText;
        }
    }
}