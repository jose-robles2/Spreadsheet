﻿// <copyright file="Expression.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System.Text;
using System.Xml.Schema;
using SpreadsheetEngine.Expressions.Operators;
using static SpreadsheetEngine.Expressions.Operators.Operator;

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
        /// When an operator is seen, the current token, either a var or const, is added to the output, as is the operator.
        /// </summary>
        /// <param name="expression"> Expression. </param>
        /// <returns> List of strings. </returns>
        public static List<string> TokenizeExpression(string expression)
        {
            expression = expression.Replace(" ", string.Empty);

            StringBuilder vars = new(), consts = new();
            List<string> expressionTokens = new List<string>();

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
                else if (IsTokenAParenthesis(currentChar.ToString()))
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
        /// Convert an infix expression to postfix. Uses the shunting yard algorithm.
        /// </summary>
        /// <param name="expression"> Infix expression. </param>
        /// <returns> List of strings. </returns>
        public static List<string> ConvertInfixToPostFix(List<string> expression)
        {
            if (expression.Count < 3)
            {
                throw new ArgumentException("ERROR: Expression of tokens must have at least three tokens");
            }

            Stack<Operator> opStack = new Stack<Operator>();
            List<string> output = new List<string>();

            int leftParenthesesCount = 0;

            foreach (string token in expression)
            {
                if (IsTokenADigit(token) || IsTokenAlphabetical(token))
                {
                    output.Add(token);
                }
                else if (IsTokenAnOperator(token))
                {
                    Operator currentOp = GetOperator(token);

                    // pop ops off the opStack and push them onto the output UNTIL an operator with (lower OR equal precedence [on opStack compared to our current Op]) AND (left associativity) is encountered
                    while (opStack.Count > 0 &&
                           opStack.Peek().Precedence <= currentOp.Precedence &&
                           opStack.Peek() is not ParenthLeftOperator &&
                           opStack.Peek().Associativity == Associative.Left)
                    {
                        output.Add(opStack.Pop().OperatorToken);
                    }

                    opStack.Push(currentOp);
                }
                else if (IsTokenLeftParenths(token))
                {
                    opStack.Push(new ParenthLeftOperator());
                    leftParenthesesCount++;
                }
                else if (IsTokenRightParenths(token))
                {
                    while (opStack.Count > 0 && opStack.Peek() is not ParenthLeftOperator)
                    {
                        output.Add(opStack.Pop().OperatorToken);
                    }

                    // discard the left parens
                    opStack.Pop();
                    leftParenthesesCount--;

                    // account for precedence & associativity if an operator follows a right parens e.g "..) * B1"
                    if (opStack.Count > 0 && IsTokenAnOperator(opStack.Peek().OperatorToken))
                    {
                        Operator currentOp = GetOperator(opStack.Peek().OperatorToken);
                        while (opStack.Count > 0 &&
                           opStack.Peek().Precedence <= currentOp.Precedence &&
                           opStack.Peek() is not ParenthLeftOperator &&
                           opStack.Peek().Associativity == Associative.Left)
                        {
                            output.Add(opStack.Pop().OperatorToken);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException($"ERROR: Unkown token: {token} encountered");
                }
            }

            if (leftParenthesesCount != 0)
            {
                throw new ArgumentException("ERROR: Mismatched parentheses");
            }

            while (opStack.Count > 0)
            {
                output.Add(opStack.Pop().OperatorToken);
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

        /// <summary>
        /// Checks to see if a token is a parenthesis.
        /// </summary>
        /// <param name="token"> token string. </param>
        /// <returns> bool. </returns>
        public static bool IsTokenAParenthesis(string token)
        {
            return token == "(" || token == ")";
        }

        /// <summary>
        /// Checks to see if a token is a left parenthesis.
        /// </summary>
        /// <param name="token"> token string. </param>
        /// <returns> bool. </returns>
        public static bool IsTokenLeftParenths(string token)
        {
            return token == "(";
        }

        /// <summary>
        /// Checks to see if a token is a right parenthesis.
        /// </summary>
        /// <param name="token"> token string. </param>
        /// <returns> bool. </returns>
        public static bool IsTokenRightParenths(string token)
        {
            return token == ")";
        }

        /// <summary>
        /// Return an operator object.
        /// </summary>
        /// <param name="token"> string token. </param>
        /// <returns> Operator. </returns>
        public static Operator GetOperator(string token)
        {
            var op = OperatorNodeFactory.SupportedOps[token];
            return (Operator)op();
        }
    }
}