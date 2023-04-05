// <copyright file="ICommand.cs" company="Jose Robles">
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
    /// Command interface for executing a command. Concrete commands do not perform
    /// thework on their own, but rather pass the call to one of the domain objects.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes a command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Unexecutes a command.
        /// </summary>
        void Unexecute();
    }
}