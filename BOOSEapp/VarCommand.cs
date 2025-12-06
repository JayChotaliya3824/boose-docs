using System;
using System.Collections.Generic;
using System.Linq;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The VarCommand class is implemented to handle the assignment of values to existing variables.
    /// It parses the assignment expression, evaluates the result, and updates the variable's value in the shared state.
    /// </summary>
    public class VarCommand : ICommand
    {
        /// <summary>
        /// An instance of ExpressionEvaluator is utilized to process mathematical or logical expressions during assignment.
        /// </summary>
        private ExpressionEvaluator evaluator = new ExpressionEvaluator();

        /// <summary>
        /// The command is executed to assign a new value to a specified variable.
        /// The variable name and the expression to be evaluated are extracted from the arguments.
        /// The expression is then processed, and the resulting value is stored in the variables dictionary.
        /// </summary>
        /// <param name="canvas">The drawing canvas is passed to the method but is not directly used for variable assignment.</param>
        /// <param name="variables">The dictionary of global variables is accessed to update the value of the target variable.</param>
        /// <param name="args">The command arguments are processed to identify the variable name and the expression for assignment.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the assignment syntax is invalid or if no expression is provided.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 3)
                throw new BOOSEException("Invalid variable assignment syntax. Use: name = expression");

            string varName = args[0];
            int startIndex = 2;
            int count = args.Length - startIndex;

            if (count <= 0)
                throw new BOOSEException($"No expression provided for variable '{varName}'");

            string expression = string.Join(" ", args, startIndex, count);
            var evaluator = new ExpressionEvaluator();
            object value = evaluator.Evaluate(expression, variables);
            variables[varName] = value;
        }
    }
}