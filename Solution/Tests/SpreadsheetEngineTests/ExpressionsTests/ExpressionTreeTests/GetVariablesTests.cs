using SpreadsheetEngine.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SpreadsheetEngineTests.ExpressionsTests.ExpressionTreeTests
{
    /// <summary>
    /// Tests for ExpressionTree.GetVariables()
    /// </summary>
    internal class GetVariablesTests
    {
        [Test]
        public void GetVariablesTest1()
        {
            ExpressionTree e = new ExpressionTree("A+B");

            HashSet<string> expectedResult = new HashSet<string>{"A", "B"};

            Assert.That(e.GetVariables(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetVariablesTest2()
        {
            ExpressionTree e = new ExpressionTree("A + (B * C) / D");

            HashSet<string> expectedResult = new HashSet<string> { "A", "B", "C", "D" };

            Assert.That(e.GetVariables(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetVariablesTest3()
        {
            ExpressionTree e = new ExpressionTree("(X + Y) * (Y - X)");

            HashSet<string> expectedResult = new HashSet<string> { "X", "Y" };

            Assert.That(e.GetVariables(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetVariablesTest4()
        {
            ExpressionTree e = new ExpressionTree("(A1 + B2) * (C3 - D4)");

            HashSet<string> expectedResult = new HashSet<string> { "A1", "B2", "C3", "D4" };

            Assert.That(e.GetVariables(), Is.EqualTo(expectedResult));
        }
    }
}