// <copyright file="TextChange.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine.Command.Changes;
using SpreadsheetEngine.Spreadsheet;

namespace SpreadsheetEngine.Command
{
    /// <summary>
    /// Class that interacts with the domain to invoke a text change. This is called by our command.
    /// </summary>
    public class TextChange : Change
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextChange"/> class.
        /// </summary>
        /// <param name="cell"> Cell whose text will be changed. </param>
        /// <param name="newText"> New text. </param>
        /// <param name="oldText"> Old text. </param>
        public TextChange(Cell cell, string newText, string oldText)
        : base(cell, newText, oldText)
        {
        }

        /// <summary>
        /// Undo a text change.
        /// </summary>
        public override void Undo()
        {
            this.cell.Text = this.oldValue;
        }

        /// <summary>
        /// Redo a text change that was undone.
        /// </summary>
        public override void Redo()
        {
            this.cell.Text = this.newValue;
        }
    }
}