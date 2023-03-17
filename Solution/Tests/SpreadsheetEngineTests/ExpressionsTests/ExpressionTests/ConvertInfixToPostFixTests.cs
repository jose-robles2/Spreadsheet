using SpreadsheetEngine.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions;
using Expression = SpreadsheetEngine.Expressions.Expression;

namespace Tests.SpreadsheetEngineTests.ExpressionsTests.ExpressionTests
{
    /// <summary>
    /// Tests for ExpressionTree.ConvertInfixToPostFix()
    /// </summary>
    internal class ConvertInfixToPostFixTests
    {
        [Test]
        public void ConvertInfixToPostFixTestTwoTerms()
        {
            List<string> tokenInput = new List<string> { "A1", "+", "B1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+" };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixThreeTerms()
        {
            List<string> tokenInput = new List<string> { "A1", "-", "B1", "-", "C1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "-", "C1", "-" };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestFourTerms()
        {
            List<string> tokenInput = new List<string> { "A1", "*", "B1", "*", "C1", "*", "D1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "*", "C1", "*", "D1", "*" };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestFiveTerms()
        {
            List<string> tokenInput = new List<string> { "A1", "+", "B1", "+", "C1", "+", "D1", "+", "E1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+", "C1", "+", "D1", "+", "E1", "+" };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestSixTerms()
        {
            List<string> tokenInput = new List<string> { "A1", "+", "B1", "+", "C1", "+", "D1", "+", "E1", "+", "F1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+", "C1", "+", "D1", "+", "E1", "+", "F1", "+" };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }
        
        [Test]
        public void ConvertInfixToPostFixTestSevenTerms()
        {
            List<string> tokenInput = new List<string> { "A1", "/", "B1", "/", "C1", "/", "D1", "/", "E1", "/", "F1", "/", "G1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "/", "C1", "/", "D1", "/", "E1", "/", "F1", "/", "G1", "/", };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestParentheses()
        {
            List<string> tokenInput = new List<string> { "(", "A1", "+", "B1", ")", "*", "C1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+", "C1", "*" };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestParentheses2()
        {
            List<string> tokenInput = new List<string> { "(", "A1", "+", "B1", ")", "*", "C1", "/", "(", "D1", "-", "E1", ")" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+", "C1", "*", "D1", "E1", "-", "/"};
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestParentheses3()
        {
            List<string> tokenInput = new List<string> { "(", "A1", "+", "B1", ")", "/", "C1", "*", "(", "D1", "-", "E1", ")" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+", "C1", "/", "D1", "E1", "-", "*", };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestNestedParentheses()
        {
            List<string> tokenInput = new List<string> { "(", "A1", "+", "(", "B1", "-", "C1", ")", ")", "*", "D1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "C1", "-", "+", "D1", "*" };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestNestedParentheses2()
        {
            List<string> tokenInput = new List<string> { "(", "A1", "+", "(", "B1", "*", "C1", ")", ")", "/", "(", "D1", "+", "E1", ")", "-", "F1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "C1", "*", "+", "D1", "E1", "+", "/", "F1", "-" };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestNestedParentheses3()
        {
            List<string> tokenInput = new List<string> { "(", "A1", "+", "(", "B1", "-", "C1", ")", ")", "*", "(", "D1", "/", "(", "E1", "-", "F1", ")", ")" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "C1", "-", "+", "D1", "E1", "F1", "-", "/", "*", };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestWithConsecutiveParentheses()
        {
            List<string> tokenInput = new List<string> { "(", "(", "A1", "+", "B1", ")", "*", "C1", ")", "/", "(", "D1", "-", "E1", ")", "/", "(", "F1", "+", "G1", ")", "*", "H1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+", "C1", "*", "D1", "E1", "-", "/", "F1", "G1", "+", "/", "H1", "*", };
            List<string>? actualOutput = Expression.ConvertInfixToPostFix(tokenInput);

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestException()
        {
            List<string> tokenInput = new List<string>();

            Assert.Throws<ArgumentException>(() => Expression.ConvertInfixToPostFix(tokenInput));
        }
    }
}
