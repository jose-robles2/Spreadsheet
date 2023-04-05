// <copyright file="ColorChange.cs" company="Jose Robles">
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
    public class ColorChange
    {
        /// <summary>
        /// Cells we need to change.
        /// </summary>
        private Cell cell;

        /// <summary>
        /// Color we need to change to.
        /// </summary>
        private uint newColor;

        /// <summary>
        /// List of old colors we could change to.
        /// </summary>
        private uint oldColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChange"/> class.
        /// </summary>
        /// <param name="cell"> Cell whose color will be changed.</param>
        /// <param name="newColor"> New color. </param>
        /// <param name="oldColor"> Old color. </param>
        public ColorChange(Cell cell, uint newColor, uint oldColor)
        {
            this.cell = cell;
            this.newColor = newColor;
            this.oldColor = oldColor;
        }

        /// <summary>
        /// Undo a color change.
        /// </summary>
        public void Undo()
        {
            this.cell.BGColor = this.oldColor;
        }

        /// <summary>
        /// Redo a color change that was undone.
        /// </summary>
        public void Redo()
        {
            this.cell.BGColor = this.newColor;
        }
    }
}