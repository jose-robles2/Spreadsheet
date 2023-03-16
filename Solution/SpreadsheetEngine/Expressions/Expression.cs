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
        /// </summary>
        /// <param name="expression"> Expression. </param>
        /// <returns> List of strings. </returns>
        public static List<string> TokenizeExpression(string expression)
        {
            expression = expression.Replace(" ", string.Empty);

            List<string> vars = new(), consts = new(), expressionTokens = new();

            bool weSawAChar = false, weSawAConst = false;
            int currentVarsPos = 0, currentConstPos = 0;

            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];
                if (Expression.IsTokenAlphabetical(currentChar.ToString()))
                {
                    if (weSawAConst)
                    {
                        throw new ArgumentException("ERROR: Variables must start with a letter, not digit.");
                    }

                    if (weSawAChar)
                    {
                        vars[currentVarsPos] += currentChar;
                    }
                    else
                    {
                        vars.Add(currentChar.ToString());
                        weSawAChar = true;
                    }
                }
                else if (Expression.IsTokenADigit(currentChar.ToString()))
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
