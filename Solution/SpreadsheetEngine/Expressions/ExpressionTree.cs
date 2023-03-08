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
using SpreadsheetEngine.Expressions.Operators;
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

        private List<string> inFixExpressionTokens;

        private Dictionary<string, Operator> supportedOps = new Dictionary<string, Operator> {
            { AddOperator.OpString, new AddOperator() },
            { SubOperator.OpString, new SubOperator() },
            { MultOperator.OpString, new MultOperator() },
            { DivOperator.OpString, new DivOperator() },
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> Math expression. </param>
        public ExpressionTree(string expression = "")
        {
            this.expression = string.IsNullOrEmpty(expression) ? this.defaultExpression : expression;
            this.variableDictionary = new Dictionary<string, double>();
            this.inFixExpressionTokens = this.TokenizeExpression(this.expression);
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
                    if (weSawAConst)
                    {
                        throw new ArgumentException("ERROR: Variables must start with a letter, not digit.");
                    }

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

            // Added check for NUnit testing purposes
            if (this.variableDictionary.Count == 0)
            {
                foreach (string var in vars)
                {
                    this.variableDictionary.Add(var, 0);
                }
            }

            return expressionTokens;
        }

        /// <summary>
        /// Create tree for expression.
        /// </summary>
        private void CreateExpressionTree()
        {
        }

        /// <summary>
        /// Convert an infix expression to postfix. For HW5 assume that operators are the same
        /// and no parentheses are included, so precedence and associativity aren't dealt with now.
        /// Because of this, this is a simplified version of the shunting yard algorithm.
        /// </summary>
        /// <param name="expression"> Infix expression. </param>
        /// <returns> List of strings. </returns>
        private List<string> ConvertInfixToPostFix(List<string> expression)
        {
            if (expression.Count < 3)
            {
                throw new ArgumentException("ERROR: Expression of tokens must have at least three tokens");
            }

            Stack<string> opStack = new Stack<string>();
            List<string> output = new List<string>();

            foreach (string token in expression)
            {
                if (this.IsTokenADigit(token))
                {
                    output.Add(token);
                }
                else if (this.IsTokenAnOperator(token))
                {
                    if (output.Count >= 2)
                    {
                        if (opStack.Count > 0)
                        {
                            output.Add(opStack.Pop());
                        }

                        opStack.Push(token);
                    }
                    else
                    {
                        opStack.Push(token);
                    }
                }
                else
                {
                    output.Add(token);
                }
            }

            while (opStack.Count > 0)
            {
                output.Add(opStack.Pop());
            }

            return output;
        }

        /// <summary>
        /// Checks to see if token is a supported operator.
        /// </summary>
        /// <param name="token"> token string. </param>
        /// <returns> bool. </returns>
        private bool IsTokenAnOperator(string token)
        {
            return this.supportedOps.ContainsKey(token) ? true : false;
        }

        /// <summary>
        /// If a token starts with a digit, then it is a digit token.
        /// </summary>
        /// <param name="token"> token string. </param>
        /// <returns> bool. </returns>
        private bool IsTokenADigit(string token)
        {
            List<char> digits = Enumerable.Range('0', 10).Select(i => (char)i).ToList();

            return digits.Contains(token[0]);
        }
    }
}