// <copyright file="Expression.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Expressions
{
    /// <summary>
    /// Helper class for ExpressionTree.cs.
    /// </summary>
    public class Expression
    {
        /// <summary>
        /// Parse the expression string into tokens. The string is scanned from left to right and checked to see
        /// if we are pointing at a alphabet char or a digit char so that it can be added to the appropriate list.
        /// If we saw an alphabetical letter first, we will build a variable token, else a const token will be built.
        /// </summary>
        /// <param name="expression"> Expression. </param>
        /// <returns> List of strings. </returns>
        public static List<string> TokenizeExpression(string expression)
        {
            expression = expression.Replace(" ", string.Empty);

            var vars = new StringBuilder();
            var consts = new StringBuilder();
            var expressionTokens = new List<string>();

            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];

                if (IsTokenAlphabetical(currentChar.ToString()))
                {
                    if (consts.Length > 0)
                    {
                        throw new ArgumentException("ERROR: Variables must start with a letter, not digit.");
                    }

                    vars.Append(currentChar);
                }
                else if (IsTokenADigit(currentChar.ToString()))
                {
                    if (vars.Length > 0)
                    {
                        vars.Append(currentChar);
                    }
                    else
                    {
                        consts.Append(currentChar);
                    }
                }
                else
                {
                    if (vars.Length > 0)
                    {
                        expressionTokens.Add(vars.ToString());
                        vars.Clear();
                    }
                    else if (consts.Length > 0)
                    {
                        expressionTokens.Add(consts.ToString());
                        consts.Clear();
                    }

                    expressionTokens.Add(currentChar.ToString());
                }
            }

            if (vars.Length > 0)
            {
                expressionTokens.Add(vars.ToString());
            }
            else if (consts.Length > 0)
            {
                expressionTokens.Add(consts.ToString());
            }

            if (expressionTokens.Count < 3)
            {
                throw new ArgumentException("ERROR: Expression must have at least two operands and one operator.");
            }

            return expressionTokens;
        }

        /// <summary>
        /// Convert an infix expression to postfix. For HW5 assume that operators are the same
        /// and no parentheses are included, so precedence and associativity aren't dealt with now.
        /// Because of this, this is a simplified version of the shunting yard algorithm.
        /// </summary>
        /// <param name="expression"> Infix expression. </param>
        /// <returns> List of strings. </returns>
        public static List<string> ConvertInfixToPostFix(List<string> expression)
        {
            if (expression.Count < 3)
            {
                throw new ArgumentException("ERROR: Expression of tokens must have at least three tokens");
            }

            Stack<string> opStack = new Stack<string>();
            List<string> output = new List<string>();

            foreach (string token in expression)
            {
                if (IsTokenADigit(token))
                {
                    output.Add(token);
                }
                else if (IsTokenAnOperator(token))
                {
                    if (output.Count >= 2)
                    {
                        if (opStack.Count > 0)
                        {
                            output.Add(opStack.Pop());
                        }
                    }

                    opStack.Push(token);
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
        public static bool IsTokenAnOperator(string token)
        {
            return OperatorNodeFactory.SupportedOps.ContainsKey(token);
        }

        /// <summary>
        /// If a token starts with a digit, then it is a digit token.
        /// </summary>
        /// <param name="token"> token string. </param>
        /// <returns> bool. </returns>
        public static bool IsTokenADigit(string token)
        {
            return char.IsDigit(token[0]);
        }

        /// <summary>
        /// If a token starts with a letter, then it is a variable token.
        /// </summary>
        /// <param name="token"> token string. </param>
        /// <returns> bool. </returns>
        public static bool IsTokenAlphabetical(string token)
        {
            return char.IsLetter(token[0]);
        }
    }
}
