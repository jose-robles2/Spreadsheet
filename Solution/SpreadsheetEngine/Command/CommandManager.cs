// <copyright file="CommandManager.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine.Command.Commands;

namespace SpreadsheetEngine.Command
{
    /// <summary>
    /// Invoker responsible for taking parameters from the SpreadsheetFrontend to initiate requests
    /// to the domain by triggering commands rather than directly sending them to the domain.
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// Contains commands that have been executed so that they can be undone.
        /// </summary>
        private Stack<ICommand> undoStack = new Stack<ICommand>();

        /// <summary>
        /// Contains commands that have been undone so that they can be redone.
        /// </summary>
        private Stack<ICommand> redoStack = new Stack<ICommand>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        /// <param name="command"> Command passed in from client. </param>
        public CommandManager(ICommand command = null)
        {
            if (command != null)
            {
                this.ExecuteCommand(command);
            }
        }

        /// <summary>
        /// Execute the command, add the newly done task to undo so it can be undone, and clear
        /// the redo stack since when something is executed, there's nothing left to redo.
        /// </summary>
        /// <param name="command"> command. </param>
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            this.undoStack.Push(command);
            this.redoStack.Clear();
        }

        /// <summary>
        /// Undo the most recent command.
        /// </summary>
        public void Undo()
        {
            if (this.undoStack.Count > 0)
            {
                ICommand command = this.undoStack.Pop();
                command.Unexecute();
                this.redoStack.Push(command);
            }
            else
            {
                throw new InvalidOperationException("Command history is empty, cannot undo");
            }
        }

        /// <summary>
        /// Redo the most recent command.
        /// </summary>
        public void Redo()
        {
            if (this.redoStack.Count > 0)
            {
                ICommand command = this.redoStack.Pop();
                command.Execute();
                this.undoStack.Push(command);
            }
            else
            {
                throw new InvalidOperationException("Command history is empty, cannot redo");
            }
        }

        /// <summary>
        /// Return undo stack count.
        /// </summary>
        /// <returns> int. </returns>
        public int GetUndoStackCount()
        {
            return this.undoStack.Count;
        }

        /// <summary>
        /// Return redo stack count.
        /// </summary>
        /// <returns> int. </returns>
        public int GetRedoStackCount()
        {
            return this.redoStack.Count;
        }

        /// <summary>
        /// Clear stacks.
        /// </summary>
        public void ClearStacks()
        {
            this.undoStack.Clear();
            this.redoStack.Clear();
        }
    }
}