// <copyright file="ColorCommand.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine.Command.Changes;

namespace SpreadsheetEngine.Command.Commands
{
    /// <summary>
    /// Implements ICommand, supports for undoing operations.
    /// </summary>
    public class ColorCommand : ICommand
    {
        /// <summary>
        /// Domain object to change color.
        /// </summary>
        private List<ColorChange> colorChanges = new List<ColorChange>();

        /// <summary>
        /// Get change count.
        /// </summary>
        /// <returns> int. </returns>
        public int GetColorChangesCount() => this.colorChanges.Count;

        /// <summary>
        /// Add a color change domain obj.
        /// </summary>
        /// <param name="colorChange"> color change. </param>
        public void AddColorChange(ColorChange colorChange)
        {
            this.colorChanges.Add(colorChange);
        }

        /// <summary>
        /// Redo a color change.
        /// </summary>
        public void Execute()
        {
            foreach (ColorChange colorChange in this.colorChanges)
            {
                colorChange.Redo();
            }
        }

        /// <summary>
        /// Undo a color change.
        /// </summary>
        public void Unexecute()
        {
            foreach (ColorChange colorChange in this.colorChanges)
            {
                colorChange.Undo();
            }
        }
    }
}