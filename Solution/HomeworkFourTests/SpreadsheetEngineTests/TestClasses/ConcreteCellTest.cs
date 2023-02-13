// <copyright file="ConcreteCellTest.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkFourTests.SpreadsheetEngineTests.TestClasses
{
    /// <summary>
    /// Implementation of abstract class cell.
    /// </summary>
    public class ConcreteCellTest : CellTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteCellTest"/> class.
        /// </summary>
        /// <param name="rowIndex"> Index of the row. </param>
        /// <param name="columnIndex"> Index of the column. </param>
        /// <param name="text"> Text content. </param>
        /// <param name="value"> Cell's value content. </param>
        public ConcreteCellTest(int rowIndex = 0, int columnIndex = 0, string text = "", string value = "")
            : base(rowIndex, columnIndex, text, value)
        {
        }
    }
}
