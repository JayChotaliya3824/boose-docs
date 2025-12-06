using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The RealCommand class is implemented to handle the declaration and assignment of real (floating-point) variables.
    /// It provides functionality to parse variable declaration syntax and evaluate initial values if they are supplied.
    /// </summary>
    public class RealCommand : ICommand
    {
        /// <summary>
        /// The command is executed to declare a new real variable.
        /// The argument list is validated, and if an assignment expression is present, it is evaluated and assigned to the variable.
        /// If no assignment is provided, the variable is initialized to 0.0.
        /// </summary>
        /// <param name="canvas">The drawing canvas is passed to the method execution context.</param>
        /// <param name="variables">The dictionary of global variables is accessed to store the new real variable.</param>
        /// <param name="args">The command arguments are parsed to extract the variable name and optional initialization expression.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the syntax is invalid or if a required expression is missing.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 2)
                throw new BOOSEException("Invalid real syntax. Use: real name [ = expression ]");

            string varName = args[1];

            if (args.Length >= 4 && args[2] == "=")
            {
                int startIndex = 3;
                int count = args.Length - startIndex;

                if (count <= 0)
                    throw new BOOSEException($"No expression provided for variable '{varName}'");

                string expression = string.Join(" ", args, startIndex, count);
                var evaluator = new ExpressionEvaluator();
                object result = evaluator.Evaluate(expression, variables);
                variables[varName] = Convert.ToDouble(result);
            }
            else
            {
                variables[varName] = 0.0;
            }
        }
    }
}