// <copyright file="VariableNode.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpreadsheetEngine.Expressions.Nodes
{
    /// <summary>
    /// Node representing a variable value. Will always be a leaf node.
    /// </summary>
    public class VariableNode : Node
    {
        private double value;

        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="value"> Variable value. </param>
        /// <param name="name"> Variable name. </param>
        public VariableNode(string name, double value = 0)
        {
            this.name = name;
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
        /// Gets or sets name.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Abstract evaluate.
        /// </summary>
        /// <param name="variables"> Dict of vars. </param>
        /// <returns> double. </returns>
        public override double Evaluate(Dictionary<string, double> variables)
        {
            if (variables.ContainsKey(this.name))
            {
                return variables[this.name];
            }
            else
            {
                variables[this.name] = 0.0; // Default value for undefined variables
                return 0.0;
            }
        }
    }
}
