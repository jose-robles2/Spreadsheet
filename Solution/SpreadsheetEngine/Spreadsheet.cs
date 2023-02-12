// <copyright file="Spreadsheet.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// Container and factory object for a 2D array of cell objects.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// 2D array that contains the cells that correspond to the UI's cells.
        /// </summary>
        private Cell[,] matrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows"> Number of rows. </param>
        /// <param name="cols"> Number of columns. </param>
        public Spreadsheet(int rows, int cols)
        {
            this.matrix = new Cell[rows, cols];
        }
    }
}
