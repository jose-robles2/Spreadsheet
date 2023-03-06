using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions.Nodes;

namespace Tests.SpreadsheetEngineTests.ExpressionTreeTests.Nodes
{
    /// <summary>
    /// Tests for OperatorNode.Evaluate()
    /// </summary>
    internal class OperatorEvaluateTests
    {
        /// <summary>
        /// Setup function used to setup different objects needed for testing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test for OperatorNode.Evaluate() under normal conditions.
        /// </summary>
        [Test]
        public void EvaluateTestNormal()
        {
            Node left = new ConstantNode(5);
            Node right = new ConstantNode(5);
            Node operatorNode = new OperatorNode('*', left, right);

            Assert.That(operatorNode.Evaluate(), Is.EqualTo(25));
        }

        /// <summary>
        /// Test for OperatorNode.Evaluate() under edge conditions.
        /// </summary>
        [Test]
        public void EvaluateTestEdge()
        {
            Node left = new ConstantNode(5);
            Node right = new ConstantNode(-5);
            Node operatorNode = new OperatorNode('+', left, right);

            Assert.That(operatorNode.Evaluate(), Is.EqualTo(0));
        }

        /// <summary>
        /// Test for OperatorNode.Evaluate() under exception conditions.
        /// </summary>
        [Test]
        public void EvaluateTestException()
        {
            Node left = new ConstantNode(int.MaxValue);
            Node right = new ConstantNode(int.MaxValue);
            Node operatorNode = new OperatorNode('&', left, right);

            Assert.Throws<ArgumentException>(() => operatorNode.Evaluate());
        }
    }
}