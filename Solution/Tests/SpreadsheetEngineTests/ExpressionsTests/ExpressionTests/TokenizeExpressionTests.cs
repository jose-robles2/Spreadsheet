using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions;
using Expression = SpreadsheetEngine.Expressions.Expression;

namespace Tests.SpreadsheetEngineTests.ExpressionsTests.ExpressionTests
{
    /// <summary>
    /// Test for private method ExpressionTree.TokenizeExpression().
    /// </summary>
    internal class TokenizeExpressionTests
    {
        [Test]
        public void TokenizeExpressionTestNormal()
        {
            string input = "A1+B1+C1+D1+E1";
            List<string> expectedOutput = new List<string> { "A1", "+", "B1", "+", "C1", "+", "D1", "+", "E1" };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestEdge()
        {
            string input = "A1 - b1-cD2 -10-       eF3";
            List<string> expectedOutput = new List<string> { "A1", "-", "b1", "-", "cD2", "-", "10", "-", "eF3" };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestEdgeUpperCase()
        {
            string input = "A1-B1-CD2-10-EF3";
            List<string> expectedOutput = new List<string> { "A1", "-", "B1", "-", "CD2", "-", "10", "-", "EF3" };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestEdgeLowerCase()
        {
            string input = "a1*b1*cd2*10*ef3";
            List<string> expectedOutput = new List<string> { "a1", "*", "b1", "*", "cd2", "*", "10", "*", "ef3" };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestEdgeMixedCase()
        {
            string input = "A1-b1-Cd2-10-eF3";
            List<string> expectedOutput = new List<string> { "A1", "-", "b1", "-", "Cd2", "-", "10", "-", "eF3" };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestEdgeMixedCharsAndDigits()
        {
            string input = "a11kl13klb12*b1*cd2aB12c*10*ef3";
            List<string> expectedOutput = new List<string> { "a11kl13klb12", "*", "b1", "*", "cd2aB12c", "*", "10", "*", "ef3" };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestParenthesisNormal()
        {
            string input = "(A1+B1)+(C1+D1)+E1";
            List<string> expectedOutput = new List<string> { "(", "A1", "+", "B1", ")", "+", "(", "C1", "+", "D1", ")", "+", "E1" };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestParenthesisEdge()
        {
            string input = "(A1+B1)+((C1+D1))+(((E1)))";
            List<string> expectedOutput = new List<string> { "(", "A1", "+", "B1", ")", "+", "(", "(", "C1", "+", "D1", ")", ")", "+", "(", "(", "(", "E1", ")", ")", ")", };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestParenthesisEdgeLowerCase()
        {
            string input = "(a1*b1)*cd2*(10*ef3)";
            List<string> expectedOutput = new List<string> { "(", "a1","*", "b1",  ")", "*", "cd2", "*",  "(", "10", "*", "ef3", ")" };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestEdgeParenthesisMixedCase()
        {
            string input = "(A1-b1)-(Cd2-10)-eF3";
            List<string> expectedOutput = new List<string> { "(", "A1", "-", "b1", ")", "-", "(", "Cd2", "-", "10", ")", "-", "eF3" };
            List<string>? actualOutput = Expression.TokenizeExpression(input);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TokenizeExpressionTestException()
        {
            string input = "4C";
            Assert.Throws<ArgumentException>(() => new ExpressionTree(input));
        }
    }
}
