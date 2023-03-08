﻿// <copyright file="AddOperator.cs" company="Jose Robles">
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
    /// Implementation for AddOperator.
    /// </summary>
    public class AddOperator : Operator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddOperator"/> class.
        /// </summary>
        public AddOperator()
        {
        }

        /// <summary>
        /// Gets the operator symbol.
        /// </summary>
        public static char OpChar => '+';

        /// <summary>
        /// Gets the operator symbol.
        /// </summary>
        public static string OpString => "+";

        /// <summary>
        /// Gets the precedance level.
        /// </summary>
        public static int Precedence => 0;

        /// <summary>
        /// Gets the associativity.
        /// </summary>
        public static Associative Associativity => Associative.Left;

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
