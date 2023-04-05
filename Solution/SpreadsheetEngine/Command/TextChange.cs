// <copyright file="TextChange.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Command
{
    /// <summary>
    /// Class that interacts with the domain to invoke a text change. This is called by our command.
    /// </summary>
    public class TextChange
    {
        /// <summary>
        /// Undo a text change.
        /// </summary>
        public void Undo()
        {
        }

        /// <summary>
        /// Redo a text change that was undone.
        /// </summary>
        public void Redo()
        {
        }
    }
}