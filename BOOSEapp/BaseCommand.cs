// BaseCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// A base class to share the evaluator and helper methods.
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        protected static ExpressionEvaluator evaluator = new ExpressionEvaluator();

        // This is the method all commands must implement
        public abstract void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args);

        // Helper method to evaluate an argument
        protected int EvaluateInt(string arg, Dictionary<string, object> variables)
        {
            // Evaluate the arg (it could be "x", "10", or "x + 5")
            object result = evaluator.Evaluate(arg, variables);
            return Convert.ToInt32(result);
        }
    }
}