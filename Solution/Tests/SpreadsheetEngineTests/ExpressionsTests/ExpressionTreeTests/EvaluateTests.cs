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
    /// Tests for ExpressionTree.Evaluate()
    /// </summary>
    internal class EvaluateTests
    {
        /// <summary>
        /// Test for ExpressionTree.Evaluate() under normal conditions.
        /// </summary>
        [Test]
        public void EvaluateTestAdd()
        {
            SpreadsheetEngine.Expressions.ExpressionTree tree = new("A+B+C");
            tree.SetVariable("A", 1);
            tree.SetVariable("B", 1);
            tree.SetVariable("C", 1);

            double expectedResult = 3;

            Assert.That(tree.Evaluate(), Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Test for ExpressionTree.Evaluate() under normal conditions.
        /// </summary>
        [Test]
        public void EvaluateTestSub()
        {
            SpreadsheetEngine.Expressions.ExpressionTree tree = new("A-B-C");
            tree.SetVariable("A", 10);
            tree.SetVariable("B", 10);
            tree.SetVariable("C", 10);

            double expectedResult = -10;

            Assert.That(tree.Evaluate(), Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Test for ExpressionTree.Evaluate() under normal conditions.
        /// </summary>
        [Test]
        public void EvaluateTestMult()
        {
            SpreadsheetEngine.Expressions.ExpressionTree tree = new("A*B*C");
            tree.SetVariable("A", 10);
            tree.SetVariable("B", 10);
            tree.SetVariable("C", 10);

            int expectedResult = 1000;

            Assert.That(tree.Evaluate(), Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Test for ExpressionTree.Evaluate() under normal conditions.
        /// </summary>
        [Test]
        public void EvaluateTestDiv()
        {
            SpreadsheetEngine.Expressions.ExpressionTree tree = new("A/B/C");
            tree.SetVariable("A", 1000);
            tree.SetVariable("B", 10);
            tree.SetVariable("C", 10);

            int expectedResult = 10;

            Assert.That(tree.Evaluate(), Is.EqualTo(expectedResult));
        }
    }
}
