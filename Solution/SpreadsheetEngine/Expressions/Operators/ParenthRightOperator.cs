// <copyright file="ParenthRightOperator.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Expressions.Operators
{
    internal class ParenthRightOperator : Operator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParenthRightOperator"/> class.
        /// </summary>
        public ParenthRightOperator()
        {
            this.precedence = 0;
            this.associative = Associative.Left;
            this.op = OpStatic;
        }

        /// <summary>
        /// Gets the operator symbol.
        /// </summary>
        public static string OpStatic => ")";

        /// <summary>
        /// Method to be implemented in derived classes.
        /// </summary>
        /// <param name="left"> Value of left child. </param>
        /// <param name="right"> Value of right child. </param>
        /// <returns> double. </returns>
        public override double Evaluate(double left, double right)
        {
            return left + right;
        }
    }
}
