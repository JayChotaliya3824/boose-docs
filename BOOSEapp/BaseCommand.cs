using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The BaseCommand abstract class is implemented to provide shared functionality across specific command implementations.
    /// A common expression evaluator and helper methods are exposed to derived classes to facilitate argument processing.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// A static instance of the ExpressionEvaluator is maintained to ensure consistent expression parsing across all commands.
        /// </summary>
        protected static ExpressionEvaluator evaluator = new ExpressionEvaluator();

        /// <summary>
        /// The command execution logic is defined by the derived class.
        /// The method is called to perform the specific action associated with the command using the provided canvas and variable context.
        /// </summary>
        /// <param name="canvas">The drawing canvas is targeted for rendering operations.</param>
        /// <param name="variables">The scope of variables is provided for parameter evaluation.</param>
        /// <param name="args">The arguments for the command are supplied as a string array.</param>
        public abstract void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args);

        /// <summary>
        /// An argument string is evaluated and converted into an integer value.
        /// The expression evaluator is utilized to resolve variable references or mathematical expressions contained within the argument.
        /// </summary>
        /// <param name="arg">The argument string is processed.</param>
        /// <param name="variables">The current variable state is referenced during evaluation.</param>
        /// <returns>The evaluated result is returned as an integer.</returns>
        protected int EvaluateInt(string arg, Dictionary<string, object> variables)
        {
            // Evaluate the arg (it could be "x", "10", or "x + 5")
            object result = evaluator.Evaluate(arg, variables);
            return Convert.ToInt32(result);
        }
    }
}