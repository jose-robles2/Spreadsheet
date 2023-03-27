// <copyright file="OperatorNodeFactory.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions.Nodes;
using SpreadsheetEngine.Expressions.Operators;

namespace SpreadsheetEngine.Expressions
{
    internal class OperatorNodeFactory
    {
        public static readonly Dictionary<string, Func<Operator>> SupportedOps = new Dictionary<string, Func<Operator>>
        {
            { AddOperator.OpStatic, () => new AddOperator() },
            { SubOperator.OpStatic, () => new SubOperator() },
            { MultOperator.OpStatic, () => new MultOperator() },
            { DivOperator.OpStatic, () => new DivOperator() },
        };

        /// <summary>
        /// Static Builder method. Factory returns an Operator node that contains a
        /// certain kind of Operator object.Inserts the left and right children as well.
        /// </summary>
        /// <param name="op"> string operator. </param>
        /// <param name="left"> left child. </param>
        /// <param name="right"> right child. </param>
        /// <returns> new OperatorNode. </returns>
        public static OperatorNode Builder(string op, Node left, Node right)
        {
            if (SupportedOps.ContainsKey(op))
            {
                return new OperatorNode(SupportedOps[op](), left, right);
            }
            else
            {
                throw new KeyNotFoundException($"Token {op} is not a supported operator");
            }
        }

        /// <summary>
        /// Static Builder method. Factory returns an Operator node that contains a
        /// certain kind of Operator object.Inserts the left and right children as well.
        /// </summary>
        /// <param name="op"> string operator. </param>
        /// <returns> new OperatorNode. </returns>
        public static OperatorNode Builder(string op)
        {
            if (SupportedOps.ContainsKey(op))
            {
                return new OperatorNode(SupportedOps[op]());
            }
            else
            {
                throw new KeyNotFoundException($"Token {op} is not a supported operator");
            }
        }
    }
}