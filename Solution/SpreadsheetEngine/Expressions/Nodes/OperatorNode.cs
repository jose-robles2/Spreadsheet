// <copyright file="OperatorNode.cs" company="Jose Robles">
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
    /// Node representing binary operator. Will always be an internal node with two children.
    /// </summary>
    public class OperatorNode : Node
    {
        private char @operator;

        private Node left;

        private Node right;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="operator"> Op. </param>
        /// <param name="left"> Left. </param>
        /// <param name="right"> Right. </param>
        public OperatorNode(char @operator, Node left, Node right)
        {
            this.@operator = @operator;
            this.left = left;
            this.right = right;
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
        /// Overridden Evaluate.
        /// </summary>
        /// <returns> string. </returns>
        public override double Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}