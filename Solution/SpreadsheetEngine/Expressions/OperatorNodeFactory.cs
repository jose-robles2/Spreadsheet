// <copyright file="OperatorNodeFactory.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using SpreadsheetEngine.Expressions.Nodes;
using SpreadsheetEngine.Expressions.Operators;

namespace SpreadsheetEngine.Expressions
{
    /// <summary>
    /// Class responsible for creating operator nodes.
    /// </summary>
    public class OperatorNodeFactory
    {
        /// <summary>
        /// Static dictionary of supported types.
        /// </summary>
        private static readonly Dictionary<string, Type> SupportedOps = new Dictionary<string, Type>();

        /// <summary>
        /// Delegate utilized for assemble reflection. Takes in a operator and a type.
        /// </summary>
        /// <param name="op"> Operator token. </param>
        /// <param name="type"> Type of operator. </param>
        private delegate void OnOperator(string op, Type type);

        /// <summary>
        /// Static Builder method. Factory returns an Operator node that contains a
        /// certain kind of Operator object.Inserts the left and right children as well.
        /// </summary>
        /// <param name="op"> string operator. </param>
        /// <param name="left"> left child. </param>
        /// <param name="right"> right child. </param>
        /// <returns> new OperatorNode. </returns>
        public static OperatorNode Builder(string op, Node left, Node right)
        {
            InitFactory();
            if (SupportedOps.ContainsKey(op))
            {
                object? operatorObject = System.Activator.CreateInstance(SupportedOps[op]);

                if (operatorObject != null && operatorObject is Operator)
                {
                    return new OperatorNode((Operator)operatorObject, left, right);
                }
            }

            throw new Exception("Unhandled operator");
        }

        /// <summary>
        /// Static Builder method. Factory returns an Operator node that contains a
        /// certain kind of Operator object.Inserts the left and right children as well.
        /// </summary>
        /// <param name="op"> string operator. </param>
        /// <returns> new OperatorNode. </returns>
        public static OperatorNode Builder(string op)
        {
            InitFactory();
            if (SupportedOps.ContainsKey(op))
            {
                object? operatorObject = System.Activator.CreateInstance(SupportedOps[op]);

                if (operatorObject != null && operatorObject is Operator)
                {
                    return new OperatorNode((Operator)operatorObject);
                }
            }

            throw new Exception("Unhandled operator");
        }

        /// <summary>
        /// Checks to see if the input token is a supported operator.
        /// </summary>
        /// <param name="op"> string token op. </param>
        /// <returns> bool. </returns>
        public static bool IsOperatorSupported(string op)
        {
            InitFactory();

            if (SupportedOps.ContainsKey(op))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Initialize the factory by checking for supported operators to be added to the dictionary.
        /// </summary>
        private static void InitFactory()
        {
            OnOperator onOperator = (op, type) => SupportedOps.Add(op, type);
            TraverseAvailableOperators(onOperator);
        }

        /// <summary>
        /// Utilize assembly reflection to search for supported ops by grabbing all subclasses of the Operator class.
        /// </summary>
        /// <param name="onOperator"> delegate. </param>
        private static void TraverseAvailableOperators(OnOperator onOperator)
        {
            Type operatorType = typeof(Operator);
            IEnumerable<Type>? operatorTypes = null;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorType));

                foreach (var type in operatorTypes)
                {
                    // For each subclass, retrieve the 'OpStatic' property
                    PropertyInfo? operatorField = type.GetProperty("OpStatic");
                    if (operatorField != null)
                    {
                        // Get the character of the Operator
                        object? value = operatorField.GetValue(type);
                        if (value is string)
                        {
                            string operatorSymbol = (string)value;

                            if (!SupportedOps.ContainsKey(operatorSymbol) && !Expression.IsTokenAParenthesis(operatorSymbol))
                            {
                                onOperator(operatorSymbol, type); // Invoke the delegate
                            }
                        }
                    }
                }
            }
        }
    }
}