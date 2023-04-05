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
        private TextChange textChange;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextCommand"/> class.
        /// Add a color change domain obj.
        /// </summary>
        /// <param name="textChange"> text change. </param>
        public TextCommand(TextChange textChange)
        {
            this.textChange = textChange;
        }

        /// <summary>
        /// Redo a color change.
        /// </summary>
        public void Execute()
        {
            this.textChange.Redo();
        }

        /// <summary>
        /// Undo a color change.
        /// </summary>
        public void Unexecute()
        {
            this.textChange.Undo();
        }
    }
}