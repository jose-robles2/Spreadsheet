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
        /// Precedence.
        /// </summary>
        protected int precedence;

        /// <summary>
        /// Associativity.
        /// </summary>
        protected Associative associative;

        /// <summary>
        /// String rep. of operator.
        /// </summary>
        protected string op;

        /// <summary>
        /// Associativity of the operators.
        /// </summary>
        public enum Associative
        {
            /// <summary>
            /// Right associativity.
            /// </summary>
            Right,

            /// <summary>
            /// Left associativity.
            /// </summary>
            Left,
        }

        /// <summary>
        /// Gets the associativity.
        /// </summary>
        public Associative Associativity { get; }

        /// <summary>
        /// Gets the precedence.
        /// </summary>
        public int Precedence { get; }

        /// <summary>
        /// Gets the operator string.
        /// </summary>
        public string OperatorToken
        {
            get { return this.op; }
        }

        /// <summary>
        /// Method to be implemented in derived classes.
        /// </summary>
        /// <param name="left"> Value of left child. </param>
        /// <param name="right"> Value of right child. </param>
        /// <returns> double. </returns>
        public abstract double Evaluate(double left, double right);
    }
}