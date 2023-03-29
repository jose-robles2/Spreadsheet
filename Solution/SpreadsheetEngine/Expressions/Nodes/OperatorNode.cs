// <copyright file="OperatorNode.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions.Operators;

namespace SpreadsheetEngine.Expressions.Nodes
{
    /// <summary>
    /// Node representing binary operator. Will always be an internal node with two children.
    /// </summary>
    public class OperatorNode : Node
    {
        private Operator @operator;

        private Node? left;

        private Node? right;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="operator"> Op. </param>
        /// <param name="left"> Left. </param>
        /// <param name="right"> Right. </param>
        public OperatorNode(Operator @operator, Node left, Node right)
        {
            this.@operator = @operator;
            this.left = left;
            this.right = right;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="operator"> Op. </param>
        public OperatorNode(Operator @operator)
        {
            this.@operator = @operator;
            this.left = null;
            this.right = null;
        }

        /// <summary>
        /// Gets or sets left node.
        /// </summary>
        public Node Left
        {
            get { return this.left; }
            set { this.left = value; }
        }

        /// <summary>
        /// Gets or sets right node.
        /// </summary>
        public Node Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        /// <summary>
        /// Gets the operator object.
        /// </summary>
        public Operator Operator
        {
            get { return this.@operator; }
        }

        /// <summary>
        /// Abstract evaluate.
        /// </summary>
        /// <param name="variables"> Dict of vars. </param>
        /// <returns> double. </returns>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            if (this.left != null && this.right != null)
            {
                return this.@operator.Evaluate(this.left.Evaluate(variables), this.right.Evaluate(variables));
            }

            return 0;
        }
    }
}