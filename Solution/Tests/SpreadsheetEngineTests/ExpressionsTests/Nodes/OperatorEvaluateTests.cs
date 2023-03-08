using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions.Nodes;
using SpreadsheetEngine.Expressions.Operators;

namespace Tests.SpreadsheetEngineTests.ExpressionTreeTests.Nodes
{
    /// <summary>
    /// Tests for OperatorNode.Evaluate()
    /// </summary>
    internal class OperatorEvaluateTests
    {
        /// <summary>
        /// Test for OperatorNode.Evaluate() under normal conditions.
        /// </summary>
        [Test]
        public void EvaluateTestNormal()
        {
            Node left = new ConstantNode(5);
            Node right = new ConstantNode(5);
            Node operatorNode = new OperatorNode(new AddOperator(), left, right);

            Assert.That(operatorNode.Evaluate(new Dictionary<string, double>()), Is.EqualTo(10));
        }

        /// <summary>
        /// Test for OperatorNode.Evaluate() under edge conditions.
        /// </summary>
        [Test]
        public void EvaluateTestEdge()
        {
            Node left = new ConstantNode(0);
            Node right = new ConstantNode(0);
            Node operatorNode = new OperatorNode(new AddOperator(), left, right);

            Assert.That(operatorNode.Evaluate(new Dictionary<string, double>()), Is.EqualTo(0));
        }

        /// <summary>
        /// Test for OperatorNode.Evaluate() under exception conditions.
        /// </summary>
        [Test]
        public void EvaluateTestException()
        {
            Node left = new ConstantNode(int.MaxValue);
            Node right = new ConstantNode(0);
            Node operatorNode = new OperatorNode(new DivOperator(), left, right);

            Assert.Throws<DivideByZeroException>(() => operatorNode.Evaluate(new Dictionary<string, double>()));
        }
    }
}