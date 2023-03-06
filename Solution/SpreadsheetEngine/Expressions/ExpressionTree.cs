// <copyright file="ExpressionTree.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions.Nodes;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> Math expression. </param>
        public ExpressionTree(string expression = "")
        {
            this.expression = string.IsNullOrEmpty(expression) ? this.defaultExpression : expression;
            this.variableDictionary = new Dictionary<string, double>();
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
        /// Create tree for expression.
        /// </summary>
        private void CreateExpressionTree()
        {
            List<string>? expressionTokens = this.TokenizeExpression(this.expression);
        }

        private List<string>? TokenizeExpression(string expression)
        {
            return null;
        }
    }
}