// VarCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class VarCommand : ICommand
    {
        private ExpressionEvaluator evaluator = new ExpressionEvaluator();

        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            // Usage: "x = 10 + 5" or "y = x + 1"
            string varName = args[0];

            // Join all parts after '=' into one string
            string expression = string.Join(" ", args, 2, args.Length - 2);

            // Evaluate the expression
            object value = evaluator.Evaluate(expression, variables);

            // Store the result
            variables[varName] = value;
        }
    }
}