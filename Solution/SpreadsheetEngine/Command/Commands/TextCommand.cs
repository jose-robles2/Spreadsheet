// <copyright file="TextCommand.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Command.Commands
{
    /// <summary>
    /// Implements ICommand, supports for undoing operations.
    /// </summary>
    public class TextCommand : ICommand
    {
        /// <summary>
        /// Domain object to change color.
        /// </summary>
        private List<TextChange> textChanges = new List<TextChange>();

        /// <summary>
        /// Add a color change domain obj.
        /// </summary>
        /// <param name="colorChange"> color change. </param>
        public void AddColorChange(TextChange colorChange)
        {
            this.textChanges.Add(colorChange);
        }

        /// <summary>
        /// Redo a color change.
        /// </summary>
        public void Execute()
        {
            foreach (TextChange textChange in this.textChanges)
            {
                textChange.Redo();
            }
        }

        /// <summary>
        /// Undo a color change.
        /// </summary>
        public void Unexecute()
        {
            foreach (TextChange textChange in this.textChanges)
            {
                textChange.Undo();
            }
        }
    }
}