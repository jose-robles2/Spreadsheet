using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions;

namespace Tests.SpreadsheetEngineTests.ExpressionsTests.ExpressionTree
{
    internal class TokenizeExpressionTests
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


        /// <summary>
        /// Test for private method ExpressionTree.TokenizeExpression() under normal conditions.
        /// </summary>
        [Test]
        public void TokenizeExpressionTestNormal()
        {
            SpreadsheetEngine.Expressions.ExpressionTree classObject = new();
            string input = "A1+B1+C1+D1+E1";
            List<string> expectedOutput = new List<string> { "A1", "+", "B1", "+", "C1", "D1", "+", "E1"};

            MethodInfo methodInfo = this.GetMethod(classObject, "TokenizeExpression");

            List<string>? actualOutput = (List<string>?)methodInfo.Invoke(classObject, new object[] { input });

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Test for private method ExpressionTree.TokenizeExpression() under edge conditions.
        /// </summary>
        [Test]
        public void TokenizeExpressionTestEdge()
        {
            SpreadsheetEngine.Expressions.ExpressionTree classObject = new();
            string input = "A1+B1+C1";
            List<string> expectedOutput = new List<string> { "A1", "+", "B1", "+", "C1" };

            MethodInfo methodInfo = this.GetMethod(classObject, "TokenizeExpression");

            List<string>? actualOutput = (List<string>?)methodInfo.Invoke(classObject, new object[] { input });

            Assert.That(actualOutput, Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Test for private method ExpressionTree.TokenizeExpression() under exception conditions.
        /// </summary>
        [Test]
        public void TokenizeExpressionTestException()
        {
            SpreadsheetEngine.Expressions.ExpressionTree classObject = new();
            string input = "A1";

            MethodInfo methodInfo = this.GetMethod(classObject, "TokenizeExpression");

            List<string>? actualOutput = (List<string>?)methodInfo.Invoke(classObject, new object[] { input });

            Assert.Throws<ArgumentException>(() => methodInfo.Invoke(classObject, new object[] { input }));
        }
    }
}
