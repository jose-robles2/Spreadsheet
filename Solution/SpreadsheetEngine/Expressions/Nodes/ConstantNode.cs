// <copyright file="ConstantNode.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Expressions.Nodes
{
    /// <summary>
    /// Node representing a constant value. Will always be a leaf node.
    /// </summary>
    public class ConstantNode : Node
    {
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value"> value. </param>
        public ConstantNode(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Overridden Evaluate.
        /// </summary>
        /// <returns> double. </returns>
        public override double Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}