// <copyright file="CellChangedEventArgs.cs" company="Jose Robles">
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
    /// Custom cell changed event args. Currently it is only used to contain the oldFormula when a cell with an existing formula
    /// is overwritten with a new one. This is needed because: A=B+C and then A=D+E. B and C no longer have A as a dependent, so
    /// A should not be updated with B or C's value changes. This can be achieved by adding this new event event changed class that
    /// contains the old formula.
    /// </summary>
    public class CellChangedEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellChangedEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName"> property name. </param>
        /// <param name="oldFormula"> old formula. </param>
        /// <param name="newFormula"> new formula. </param>
        public CellChangedEventArgs(string propertyName, string oldFormula, string newFormula)
            : base(propertyName)
        {
            this.OldFormula = oldFormula;
            this.NewFormula = newFormula;
        }

        /// <summary>
        /// Gets the old formula.
        /// </summary>
        public string OldFormula { get; }

        /// <summary>
        /// Gets the new formula.
        /// </summary>
        public string NewFormula { get; }
    }
}