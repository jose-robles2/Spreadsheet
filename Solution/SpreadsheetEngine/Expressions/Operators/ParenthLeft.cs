// <copyright file="ParenthLeftOperator.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Expressions.Operators
{
    /// <summary>
    /// Class representing a left parenths. Not a binary operator, but it inherits from Operator
    /// for convenience within SpreadsheetEngine.Expressions.Expression. Cannot be evaluated AND is not
    /// included in SpreadsheetEngine.Expressions.OperatorNodeFactory's supported Ops dictionary.
    /// </summary>
    internal class ParenthLeft : Operator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParenthLeft"/> class.
        /// </summary>
        public ParenthLeft()
        {
            this.precedence = 0;
            this.associative = Associative.Left;
            this.op = OpStatic;
        }

        /// <summary>
        /// Gets the operator symbol.
        /// </summary>
        public static string OpStatic => "(";

        /// <summary>
        /// Method to be implemented in derived classes.
        /// </summary>
        /// <param name="left"> Value of left child. </param>
        /// <param name="right"> Value of right child. </param>
        /// <returns> double. </returns>
        /// <exception cref="System.NotImplementedException"> Parenths cannot be evaluated. </exception>
        public override double Evaluate(double left, double right)
        {
            throw new System.NotImplementedException("ERROR: Parentheses cannot be evaluated" +
                "something went wrong as these should not be included in the expr tree." +
                "These objects are made for convenience within SpreadsheetEngine.Expressions.Expression");
        }
    }
}
