// <copyright file="Node.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Expressions.Nodes
{
    /// <summary>
    /// Abstract node class.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Abstract evaluate.
        /// </summary>
        /// <param name="variables"> Dict of vars. </param>
        /// <returns> double </returns>
        public abstract double Evaluate(Dictionary<string, double> variables);
    }
}