﻿using SpreadsheetEngine.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SpreadsheetEngineTests.ExpressionTreeTests.ExpressionTree
{
    /// <summary>
    /// Tests for ExpressionTree.ConvertInfixToPostFix()
    /// </summary>
    internal class ConvertInfixToPostFixTests
    {
        /// <summary>
        /// Helper method for reflection. Retrives information of a private method in order to invoke it.
        /// </summary>
        /// <param name="classObject"> Object that has the private method. </param>
        /// <param name="methodName"> Target name. </param>
        /// <returns></returns>
        private System.Reflection.MethodInfo GetMethod(SpreadsheetEngine.Expressions.ExpressionTree classObject, string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                Assert.Fail("methodName cannot be null or whitespace");
            }

            var method = classObject.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (method == null)
            {
                Assert.Fail(string.Format("{0} method not found", methodName));
            }

            return method;
        }

        [Test]
        public void ConvertInfixToPostFixTestTwoTerms()
        {
            string input = "A1+B1";
            List<string> tokenInput = new List<string> { "A1", "+", "B1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+" };

            SpreadsheetEngine.Expressions.ExpressionTree classObject = new(input);

            MethodInfo methodInfo = this.GetMethod(classObject, "ConvertInfixToPostFix");

            List<string>? actualOutput = (List<string>?)methodInfo.Invoke(classObject, new object[] { tokenInput });

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Test for private method ExpressionTree.ConvertInfixToPostFix() under normal conditions.
        /// </summary>
        [Test]
        public void ConvertInfixToPostFixThreeTerms()
        {
            string input = "A1+B1+C1";
            List<string> tokenInput = new List<string> { "A1", "-", "B1", "-", "C1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "-", "C1", "-" };

            SpreadsheetEngine.Expressions.ExpressionTree classObject = new(input);

            MethodInfo methodInfo = this.GetMethod(classObject, "ConvertInfixToPostFix");

            List<string>? actualOutput = (List<string>?)methodInfo.Invoke(classObject, new object[] { tokenInput });

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestFourTerms()
        {
            string input = "A1+B1+C1+D1";
            List<string> tokenInput = new List<string> { "A1", "*", "B1", "*", "C1", "*", "D1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "*", "C1", "*", "D1", "*" };

            SpreadsheetEngine.Expressions.ExpressionTree classObject = new(input);

            MethodInfo methodInfo = this.GetMethod(classObject, "ConvertInfixToPostFix");

            List<string>? actualOutput = (List<string>?)methodInfo.Invoke(classObject, new object[] { tokenInput });

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestFiveTerms()
        {
            string input = "A1+B1+C1+D1+E1";
            List<string> tokenInput = new List<string> { "A1", "+", "B1", "+", "C1", "+", "D1", "+", "E1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+", "C1", "+", "D1", "+", "E1", "+" };

            SpreadsheetEngine.Expressions.ExpressionTree classObject = new(input);

            MethodInfo methodInfo = this.GetMethod(classObject, "ConvertInfixToPostFix");

            List<string>? actualOutput = (List<string>?)methodInfo.Invoke(classObject, new object[] { tokenInput });

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestSixTerms()
        {
            string input = "A1+B1+C1+D1+E1+F1";
            List<string> tokenInput = new List<string> { "A1", "+", "B1", "+", "C1", "+", "D1", "+", "E1", "+", "F1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "+", "C1", "+", "D1", "+", "E1", "+", "F1", "+" };

            SpreadsheetEngine.Expressions.ExpressionTree classObject = new(input);

            MethodInfo methodInfo = this.GetMethod(classObject, "ConvertInfixToPostFix");

            List<string>? actualOutput = (List<string>?)methodInfo.Invoke(classObject, new object[] { tokenInput });

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ConvertInfixToPostFixTestSevenTerms()
        {
            string input = "A1+B1+C1+D1+E1+F1+G1";
            List<string> tokenInput = new List<string> { "A1", "/", "B1", "/", "C1", "/", "D1", "/", "E1", "/", "F1", "/", "G1" };
            List<string> expectedOutput = new List<string> { "A1", "B1", "/", "C1", "/", "D1", "/", "E1", "/", "F1", "/", "G1", "/", };

            SpreadsheetEngine.Expressions.ExpressionTree classObject = new(input);

            MethodInfo methodInfo = this.GetMethod(classObject, "ConvertInfixToPostFix");

            List<string>? actualOutput = (List<string>?)methodInfo.Invoke(classObject, new object[] { tokenInput });

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Test for private method ExpressionTree.ConvertInfixToPostFix() under exception conditions.
        /// </summary>
        [Test]
        public void ConvertInfixToPostFixTestException()
        {
            string input = "";
            List<string> tokenInput = new List<string>();

            SpreadsheetEngine.Expressions.ExpressionTree classObject = new(input);

            MethodInfo methodInfo = this.GetMethod(classObject, "ConvertInfixToPostFix");

            Assert.Throws<TargetInvocationException>(() => methodInfo.Invoke(classObject, new object[] { tokenInput }));
        }
    }
}