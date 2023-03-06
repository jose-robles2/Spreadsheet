// <copyright file="ExpressionTree.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions.Nodes;
using SpreadsheetEngine.Spreadsheet;

namespace SpreadsheetEngine.Expressions
{
    /// <summary>
    /// ExpressionTree class.
    /// </summary>
    public class ExpressionTree
    {
        private readonly string defaultExpression = "A1+B1+C1";

        private readonly string expression;

        private Node? root = null;

        private Dictionary<string, double> variableDictionary;

        private List<string> expressionTokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> Math expression. </param>
        public ExpressionTree(string expression = "")
        {
            this.expression = string.IsNullOrEmpty(expression) ? this.defaultExpression : expression;
            this.variableDictionary = new Dictionary<string, double>();
            this.expressionTokens = this.TokenizeExpression(this.expression);
            this.CreateExpressionTree();
        }

        /// <summary>
        /// Gets the expression member.
        /// </summary>
        public string Expression
        {
            get { return this.expression; }
        }

        /// <summary>
        /// Set a variable with a value.
        /// </summary>
        /// <param name="varName"> Name of variable. </param>
        /// <param name="varValue"> Value of variable. </param>
        public void SetVariable(string varName, double varValue)
        {
            if (!this.variableDictionary.ContainsKey(varName))
            {
                throw new KeyNotFoundException("Variable with name '" + varName + "' not found.");
            }

            this.variableDictionary[varName] = varValue;
        }

        /// <summary>
        /// Return the variable value for a variable name.
        /// </summary>
        /// <param name="varName"> Name of variable. </param>
        /// <returns> double. </returns>
        public double GetVariable(string varName)
        {
            try
            {
                double value = this.variableDictionary[varName];
                return value;
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Variable with name '" + varName + "' not found.");
            }
        }

        /// <summary>
        /// Evaluate the expression.
        /// </summary>
        /// <returns> Result of expression. </returns>
        public double Evaluate()
        {
            return 0;
        }

        /// <summary>
        /// Parse the expression string into tokens. The string is scanned from left to right and checked to see
        /// if we are pointing at a alphabet char or a digit char so that it can be added to the appropriate list.
        /// </summary>
        /// <param name="expression"> Expression. </param>
        /// <returns> List of strings. </returns>
        private List<string> TokenizeExpression(string expression)
        {
            List<char> alphabet = Enumerable.Range('A', 26).Select(i => (char)i).ToList();
            List<char> digits = Enumerable.Range('0', 10).Select(i => (char)i).ToList();
            List<string> vars = new(), consts = new(), expressionTokens = new();

            bool weSawAChar = false, weSawAConst = false;
            int currentVarsPos = 0, currentConstPos = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];
                if (alphabet.Contains(currentChar))
                {
                    vars.Add(expression[i].ToString());
                    weSawAChar = true;
                }
                else if (digits.Contains(currentChar))
                {
                    if (weSawAChar)
                    {
                        vars[currentVarsPos] += currentChar;
                    }
                    else
                    {
                        if (weSawAConst)
                        {
                            consts[currentConstPos] += currentChar;
                        }
                        else
                        {
                            consts.Add(currentChar.ToString());
                            weSawAConst = true;
                        }
                    }
                }
                else
                {
                    if (weSawAConst)
                    {
                        weSawAConst = false;
                        expressionTokens.Add(consts[currentConstPos]);
                        currentConstPos++;
                    }
                    else
                    {
                        weSawAChar = false;
                        expressionTokens.Add(vars[currentVarsPos]);
                        currentVarsPos++;
                    }

                    expressionTokens.Add(currentChar.ToString());
                }
            }

            // We reached the end of an expression and need to add the last variable or constant
            if (weSawAChar)
            {
                expressionTokens.Add(vars[currentVarsPos]);
            }
            else if (weSawAConst)
            {
                expressionTokens.Add(consts[currentConstPos]);
            }

            if (expressionTokens.Count < 3)
            {
                throw new ArgumentException("ERROR: Expression must have at least two operands and one operator.");
            }

            foreach (string var in vars)
            {
                this.variableDictionary.Add(var, 0);
            }

            return expressionTokens;
        }

        /// <summary>
        /// Create tree for expression.
        /// </summary>
        private void CreateExpressionTree()
        {
        }
    }
}