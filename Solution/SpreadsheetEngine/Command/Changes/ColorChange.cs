// <copyright file="ColorChange.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine.Spreadsheet;

namespace SpreadsheetEngine.Command.Changes
{
    /// <summary>
    /// Class that interacts with the domain to invoke a text change. This is called by our command.
    /// </summary>
    public class ColorChange : Change
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChange"/> class.
        /// </summary>
        /// <param name="cell"> Cell whose text will be changed. </param>
        /// <param name="newColor"> New text. </param>
        /// <param name="oldColor"> Old text. </param>
        public ColorChange(Cell cell, uint newColor, uint oldColor)
        : base(cell, newColor, oldColor)
        {
        }

        /// <summary>
        /// Undo a text change.
        /// </summary>
        public override void Undo()
        {
            this.cell.BGColor = this.oldValue;
        }

        /// <summary>
        /// Redo a text change that was undone.
        /// </summary>
        public override void Redo()
        {
            this.cell.BGColor = this.newValue;
        }
    }
}