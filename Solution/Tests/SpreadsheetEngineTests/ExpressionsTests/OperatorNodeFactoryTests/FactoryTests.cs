using SpreadsheetEngine.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions;
using SpreadsheetEngine.Expressions.Operators;
using SpreadsheetEngine.Expressions.Nodes;

namespace Tests.SpreadsheetEngineTests.ExpressionsTests.OperatorNodeFactoryTests
{
    /// <summary>
    /// Tests for ExpressionTree.OperatorNodeFactory
    /// </summary>
    internal class FactoryTests
    {
        /// <summary>
        /// Private operator node members for these tests.
        /// </summary>
        private OperatorNode add;
        private OperatorNode sub;
        private OperatorNode div;
        private OperatorNode mult; 

        /// <summary>
        /// Setup function used to setup different objects needed for testing.
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SpreadsheetEngine.dll");
            Assembly.LoadFrom(path);


            this.add = OperatorNodeFactory.Builder("+");
            this.sub = OperatorNodeFactory.Builder("-");
            this.div = OperatorNodeFactory.Builder("/");
            this.mult = OperatorNodeFactory.Builder("*");
        }

        /// <summary>
        /// Test for ExpressionTree.OperatorNodeFactory 
        /// </summary>
        [Test]
        public void PrecedenceTests()
        {
            Assert.That(this.add.Operator.Precedence, Is.EqualTo(0));
            Assert.That(this.sub.Operator.Precedence, Is.EqualTo(0));
            Assert.That(this.div.Operator.Precedence, Is.EqualTo(1));
            Assert.That(this.mult.Operator.Precedence, Is.EqualTo(1));
        }

        /// <summary>
        /// Test for ExpressionTree.OperatorNodeFactory 
        /// </summary>
        [Test]
        public void AssociativityTests()
        {
            Assert.That(this.add.Operator.Associativity, Is.EqualTo(Operator.Associative.Left));
            Assert.That(this.sub.Operator.Associativity, Is.EqualTo(Operator.Associative.Left));
            Assert.That(this.div.Operator.Associativity, Is.EqualTo(Operator.Associative.Left));
            Assert.That(this.mult.Operator.Associativity, Is.EqualTo(Operator.Associative.Left));
        }
    }
}
