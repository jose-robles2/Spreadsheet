// <copyright file="ExpressionTree.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions;
using SpreadsheetEngine.Expressions.Nodes;
using Expression = SpreadsheetEngine.Expressions.Expression;

namespace SpreadsheetEngine.Expressions
{
    /// <summary>
    /// ExpressionTree class.
    /// </summary>
    public class ExpressionTree
    {
        //private readonly string defaultExpression = "A1+B1+C1";

        private readonly string expression;

        private Node? root = null;

        private Dictionary<string, double> variableDictionary;

        private List<string>? inFixExpressionTokens;

        private int size = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> Math expression. </param>
        public ExpressionTree(string expression = "")
        {
            this.expression = expression; //  string.IsNullOrEmpty(expression) ? this.defaultExpression : expression;
            this.variableDictionary = new Dictionary<string, double>();

            if (expression != string.Empty)
            {
                this.ParseExpressionAndInitVarsDict();
                this.CreateExpressionTree();
            }
        }

        /// <summary>
        /// Gets the expression member.
        /// </summary>
        public string Expression
        {
            get { return this.expression; }
        }

        /// <summary>
        /// Gets the node count of tree.
        /// </summary>
        public int Size
        {
            get => this.size;
        }

        /// <summary>
        /// Set a variable with a value.
        /// </summary>
        /// <param name="varName"> Name of variable. </param>
        /// <param name="varValue"> Value of variable. </param>
        public void SetVariable(string varName, double varValue)
        {
            if (!this.variableDictionary.ContainsKey(varName))
            {
                throw new KeyNotFoundException("Variable with name '" + varName + "' not found.");
            }

            this.variableDictionary[varName] = varValue;
        }

        /// <summary>
        /// Return the variable value for a variable name.
        /// </summary>
        /// <param name="varName"> Name of variable. </param>
        /// <returns> double. </returns>
        public double GetVariable(string varName)
        {
            try
            {
                double value = this.variableDictionary[varName];
                return value;
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Variable with name '" + varName + "' not found.");
            }
        }

        /// <summary>
        /// Return all variables.
        /// </summary>
        /// <returns> Set of unique vars. </returns>
        public HashSet<string> GetVariables()
        {
            return new HashSet<string>(this.variableDictionary.Keys);
        }

        /// <summary>
        /// Evaluate the expression.
        /// </summary>
        /// <returns> Result of expression. </returns>
        public double Evaluate()
        {
            if (this.root != null)
            {
                return this.root.Evaluate(this.variableDictionary);
            }

            return 0;
        }

        /// <summary>
        /// Parse the string expression into a list of tokens and init the vars dictionary.
        /// </summary>
        private void ParseExpressionAndInitVarsDict()
        {
            this.inFixExpressionTokens = Expressions.Expression.TokenizeExpression(this.expression);

            if (this.inFixExpressionTokens == null)
            {
                // Originally was throwing an exception, catch and return null
                return;
            }

            foreach (string token in this.inFixExpressionTokens)
            {
                if (Expressions.Expression.IsTokenAlphabetical(token) && !this.variableDictionary.ContainsKey(token))
                {
                    this.variableDictionary.Add(token, 0);
                }
            }
        }

        /// <summary>
        /// Create tree for expression. Using a stack. NOTE: For future HW we can add a node factory so we do not
        /// need to explicitly state the kinda node we're making.
        /// </summary>
        private void CreateExpressionTree()
        {
            Stack<Node> nodeStack = new Stack<Node>();

            List<string>? postfixExpression = Expressions.Expression.ConvertInfixToPostFix(this.inFixExpressionTokens);

            if (postfixExpression == null)
            {
                // Originally was throwing an exception, catch and return null
                return;
            }

            foreach (string token in postfixExpression)
            {
                if (Expressions.Expression.IsTokenADigit(token))
                {
                    nodeStack.Push(new ConstantNode(double.Parse(token)));
                }
                else if (Expressions.Expression.IsTokenAnOperator(token))
                {
                    OperatorNode opNode = OperatorNodeFactory.Builder(token);
                    opNode.Right = nodeStack.Pop();
                    opNode.Left = nodeStack.Pop();
                    nodeStack.Push(opNode);
                }
                else
                {
                    nodeStack.Push(new VariableNode(token, this.variableDictionary[token]));
                }

                this.size += 1;
            }

            this.root = nodeStack.Pop();
        }
    }
}