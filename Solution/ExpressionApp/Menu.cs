// <copyright file="Menu.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions;

namespace ExpressionApp
{
    /// <summary>
    /// Internal class handling the menu.
    /// </summary>
    internal class Menu
    {
        /// <summary>
        /// Is the app running.
        /// </summary>
        private bool appRunning;

        /// <summary>
        /// Tree representing user's input expression.
        /// </summary>
        private ExpressionTree expressionTree;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        public Menu()
        {
            this.expressionTree = new ExpressionTree();
            this.appRunning = true;
            this.RunApp();
        }

        /// <summary>
        /// Contains the main loop for the console app.
        /// </summary>
        private void RunApp()
        {
            while (this.appRunning)
            {
                this.RenderMenu();
            }
        }

        /// <summary>
        /// Display the menu.
        /// </summary>
        private void RenderMenu()
        {
            switch (this.GetMenuUserInput())
            {
                case 1:
                    this.CreateExpressionOption();
                    break;
                case 2:
                    this.SetVariableOption();
                    break;
                case 3:
                    this.EvaluateTreeOption();
                    break;
                case 4:
                    this.appRunning = false;
                    break;
                default:
                    Console.WriteLine("Please enter a valid int from 1-4\nPress any key to continue...");
                    Console.ReadLine();
                    break;
            }
        }

        /// <summary>
        /// Get user input for the menu.
        /// </summary>
        /// <returns> Integer. </returns>
        private int GetMenuUserInput()
        {
            Console.WriteLine("Menu [Current expression: " + this.expressionTree.Expression + "]");
            Console.WriteLine("Enter a menu option...");
            Console.WriteLine("1. Enter a new expresseion");
            Console.WriteLine("2. Set a variable value");
            Console.WriteLine("3. Evaluate tree");
            Console.WriteLine("4. Quit");
            int.TryParse(Console.ReadLine(), out int result);
            return result;
        }

        /// <summary>
        /// Create a new expression.
        /// </summary>
        private void CreateExpressionOption()
        {
        }

        /// <summary>
        /// Set certain variables within the expression.
        /// </summary>
        private void SetVariableOption()
        {
        }

        /// <summary>
        /// Evaluate the tree.
        /// </summary>
        private void EvaluateTreeOption()
        {
        }
    }
}