// <copyright file="ExpressionTree.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Expressions
{
    /// <summary>
    /// ExpressionTree class.
    /// </summary>
    public class ExpressionTree
    {
        /// <summary>
        /// Default expression if none is assigned at instantiation.
        /// </summary>
        private readonly string defaultExpression = "A1+B1+C1";

        /// <summary>
        /// Math expression.
        /// </summary>
        private string expression;

        // private node root = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> Math expression. </param>
        public ExpressionTree(string expression = "")
        {
            this.expression = string.IsNullOrEmpty(expression) ? this.defaultExpression : expression;
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
    }
}