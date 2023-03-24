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

        [Test]
        public void EvaluateTestLongExpression1()
        {
            SpreadsheetEngine.Expressions.ExpressionTree tree = new("(A + 5) * (B - 10) / (C + 2) + D");
            tree.SetVariable("A", 10);
            tree.SetVariable("B", 20);
            tree.SetVariable("C", 30);
            tree.SetVariable("D", 40);

            double expectedResult = 44.6875d;

            Assert.That(tree.Evaluate(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void EvaluateTestLongExpression2()
        {
            SpreadsheetEngine.Expressions.ExpressionTree tree = new("(A - B) * (C + D) - E / F");
            tree.SetVariable("A", 10);
            tree.SetVariable("B", 5);
            tree.SetVariable("C", 3);
            tree.SetVariable("D", 7);
            tree.SetVariable("E", 15);
            tree.SetVariable("F", 3);

            double expectedResult = 45.0d;

            Assert.That(tree.Evaluate(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void EvaluateTestParentheses()
        {
            SpreadsheetEngine.Expressions.ExpressionTree tree = new("(A + B) * C - (D / E)");
            tree.SetVariable("A", 5);
            tree.SetVariable("B", 10);
            tree.SetVariable("C", 3);
            tree.SetVariable("D", 12);
            tree.SetVariable("E", 4);

            double expectedResult = 42.0d;

            Assert.That(tree.Evaluate(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void EvaluateTestParentheses2()
        {
            SpreadsheetEngine.Expressions.ExpressionTree tree = new("(A * (B + C) - D) / E");
            tree.SetVariable("A", 2);
            tree.SetVariable("B", 5);
            tree.SetVariable("C", 3);
            tree.SetVariable("D", 10);
            tree.SetVariable("E", 4);

            double expectedResult = 1.5d;

            Assert.That(tree.Evaluate(), Is.EqualTo(expectedResult));
        }

    }
}
