// <copyright file="Change.cs" company="Jose Robles">
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
    /// Abstract change object that represents possible changes that can be triggered on the domain.
    /// </summary>
    public abstract class Change
    {
        /// <summary>
        /// Cell being changed.
        /// </summary>
        protected Cell cell;

        /// <summary>
        /// Old value.
        /// </summary>
        protected dynamic oldValue;

        /// <summary>
        /// New value.
        /// </summary>
        protected dynamic newValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Change"/> class.
        /// </summary>
        /// <param name="cell"> cell. </param>
        /// <param name="newValue"> new val. </param>
        /// <param name="oldValue"> old val. </param>
        public Change(Cell cell, dynamic newValue, dynamic oldValue)
        {
            this.cell = cell;
            this.newValue = newValue;
            this.oldValue = oldValue;
        }

        /// <summary>
        /// Undo a cell change.
        /// </summary>
        public abstract void Undo();

        /// <summary>
        /// Redo a cell change.
        /// </summary>
        public abstract void Redo();
    }
}
