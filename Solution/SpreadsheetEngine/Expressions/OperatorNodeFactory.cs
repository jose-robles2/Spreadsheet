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
        public static readonly Dictionary<string, Operator> SupportedOps = new Dictionary<string, Operator>
        {
            { AddOperator.OpString, new AddOperator() },
            { SubOperator.OpString, new SubOperator() },
            { MultOperator.OpString, new MultOperator() },
            { DivOperator.OpString, new DivOperator() },
        };

        public static Node Builder(string op, Node left, Node right)
        {
            if (SupportedOps.ContainsKey(op))
            {
                return new OperatorNode(SupportedOps[op], left, right);
            }
            else
            {
                throw new KeyNotFoundException($"Token {op} is not a supported operator");
            }
        }
    }
}
