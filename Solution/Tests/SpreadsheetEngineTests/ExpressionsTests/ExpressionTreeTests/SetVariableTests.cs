using SpreadsheetEngine.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SpreadsheetEngineTests.ExpressionTreeTests.ExpressionTree
{
    /// <summary>
    /// Tests for ExpressionTree.SetVariable(string varName, double varValue)
    /// </summary>
    internal class ExpressionTreeEvaluateTests
    {
        /// <summary>
        /// Tree for testing purposes.
        /// </summary>
        private SpreadsheetEngine.Expressions.ExpressionTree tree;
        
        /// <summary>
        /// Setup function used to setup different objects needed for testing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            tree = new SpreadsheetEngine.Expressions.ExpressionTree("A1+C1");
        }

        /// <summary>
        /// Test for ExpressionTree.SetVariable(string varName, double varValue) under normal conditions.
        /// </summary>
        [Test]
        public void SetVariableTestNormal()
        {
            tree.SetVariable("A1", 2);
            Assert.That(tree.GetVariable("A1"), Is.EqualTo(2));
        }

        /// <summary>
        /// Test for ExpressionTree.SetVariable(string varName, double varValue) under edge conditions.
        /// </summary>
        [Test]
        public void SetVariableTestEdge()
        {
            tree.SetVariable("C1", int.MaxValue);
            Assert.That(tree.GetVariable("C1"), Is.EqualTo(int.MaxValue));
        }

        /// <summary>
        /// Test for ExpressionTree.SetVariable(string varName, double varValue) under exception conditions.
        /// </summary>
        [Test]
        public void SetVariableTestNullTest()
        {
            Assert.Throws<KeyNotFoundException>(() => tree.SetVariable("nonExistant", 100));
        }
    }
}
