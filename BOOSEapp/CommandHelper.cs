using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The CommandHelper class is implemented to provide utility methods for command execution.
    /// Common operations such as argument evaluation are centralized to ensure consistency and reduce code duplication.
    /// </summary>
    public static class CommandHelper
    {
        /// <summary>
        /// A static instance of the ExpressionEvaluator is maintained for evaluating expressions.
        /// </summary>
        private static ExpressionEvaluator evaluator = new ExpressionEvaluator();

        /// <summary>
        /// An argument string is evaluated and returned as an integer.
        /// The expression evaluator is used to process the argument within the context of the provided variables.
        /// </summary>
        /// <param name="arg">The argument string to be evaluated.</param>
        /// <param name="variables">The dictionary of variables is accessed to resolve variable references.</param>
        /// <returns>The evaluated result is returned as a 32-bit signed integer.</returns>
        public static int EvaluateInt(string arg, Dictionary<string, object> variables)
        {
            object result = evaluator.Evaluate(arg, variables);
            return Convert.ToInt32(result);
        }
    }
}