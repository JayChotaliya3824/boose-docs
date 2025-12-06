using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The BooleanCommand class is implemented to facilitate the creation and assignment of boolean variables.
    /// Logical expressions are evaluated, and the resulting truth values are stored within the application state for later use.
    /// </summary>
    public class BooleanCommand : ICommand
    {
        /// <summary>
        /// The command is executed to parse the provided arguments and establish the boolean variable.
        /// The expression is isolated, evaluated using the expression evaluator, and the result is cast to a boolean before being stored in the variable dictionary.
        /// </summary>
        /// <param name="canvas">The drawing canvas is passed to the method but is not required for boolean variable assignment.</param>
        /// <param name="variables">The dictionary of variables is accessed to store the newly defined boolean value.</param>
        /// <param name="args">The array of command arguments is processed to extract the variable name and the expression to be evaluated.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the syntax is incorrect or if the evaluated result cannot be converted to a boolean value.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 4)
                throw new BOOSEException("Invalid boolean syntax. Use: boolean name = expression");

            string varName = args[1];
            string expression = string.Join(" ", args, 3, args.Length - 3);
            var evaluator = new ExpressionEvaluator();
            object result = evaluator.Evaluate(expression, variables);

            bool value;
            if (result is bool b)
                value = b;
            else if (result is int i)
                value = i != 0;
            else if (result is double d)
                value = d != 0.0;
            else
                throw new BOOSEException($"Cannot convert '{result}' to boolean.");

            variables[varName] = value;
        }
    }
}