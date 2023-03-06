using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SpreadsheetEngineTests.ExpressionsTests.ExpressionTree
{
    internal class TokenizeExpressionTests
    {
        private TokenizeExpressionTests classObject = new();

        private System.Reflection.MethodInfo GetMethod(string methodName)
        {
            if (string.IsNullOrWhiteSpace(methodName))
            {
                Assert.Fail("methodName cannot be null or whitespace");
            }

            var method = this.classObject.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            if (method == null)
            {
                Assert.Fail(string.Format("{0} method not found", methodName));
            }

            return method;
        }


        /// <summary>
        /// Test for ExpressionTree.Evaluate() under normal conditions.
        /// </summary>
        [Test]
        public void EvaluateTestNormal()
        {
            MethodInfo methodInfo = this.GetMethod("TokenizeExpression");

            Assert.AreEqual
        }

        /// <summary>
        /// Test for ExpressionTree.Evaluate() under edge conditions.
        /// </summary>
        [Test]
        public void EvaluateTestEdge()
        {
        }

        /// <summary>
        /// Test for ExpressionTree.Evaluate() under exception conditions.
        /// </summary>
        [Test]
        public void EvaluateTestException()
        {
        }
    }
}
