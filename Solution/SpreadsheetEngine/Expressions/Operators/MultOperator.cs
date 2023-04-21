// <copyright file="MultOperator.cs" company="Jose Robles">
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
    public class MultOperator : Operator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultOperator"/> class.
        /// </summary>
        public MultOperator()
        {
            this.precedence = 1;
            this.associative = Associative.Left;
            this.op = OpStatic;
        }

        /// <summary>
        /// Gets the operator symbol.
        /// </summary>
        public static string OpStatic => "*";

        /// <summary>
        /// Method to be implemented in derived classes.
        /// </summary>
        /// <param name="left"> Value of left child. </param>
        /// <param name="right"> Value of right child. </param>
        /// <returns> double. </returns>
        public override double Evaluate(double left, double right)
        {
            double result;
            try
            {
                result = left * right;
                if (double.IsInfinity(result))
                {
                    result = double.MaxValue;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e);
                result = double.MaxValue;
            }

            return result;
        }
    }
}