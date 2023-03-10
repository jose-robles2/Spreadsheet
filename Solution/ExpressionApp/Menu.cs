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
        private bool appRunning;

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
            Console.WriteLine("Enter a new expression: ");
            string? expression = Console.ReadLine();

            if (expression != null)
            {
                this.expressionTree = new ExpressionTree(expression);
                Console.WriteLine("New expression created");
            }
        }

        /// <summary>
        /// Set certain variables within the expression.
        /// </summary>
        private void SetVariableOption()
        {
            Console.WriteLine("Enter the variable name you want to set: ");
            string? varName = Console.ReadLine();
            Console.WriteLine("Enter the value you want to set to this variable: ");
            string? varValue = Console.ReadLine();

            if (varName != null && varValue != null)
            {
                this.expressionTree.SetVariable(varName, double.Parse(varValue));
            }
        }

        /// <summary>
        /// Evaluate the tree.
        /// </summary>
        private void EvaluateTreeOption()
        {
            Console.WriteLine("Evaluating Tree...\nResult: " + this.expressionTree.Evaluate());
        }
    }
}