// <copyright file="Operator.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions.Nodes;

namespace SpreadsheetEngine.Expressions.Operators
{
    /// <summary>
    /// Interface operator.
    /// </summary>
    public abstract class Operator
    {
        /// <summary>
        /// Method to be implemented in derived classes.
        /// </summary>
        /// <param name="left"> Value of left child. </param>
        /// <param name="right"> Value of right child. </param>
        /// <returns> double. </returns>
        public abstract double Evaluate(double left, double right);
    }
}
