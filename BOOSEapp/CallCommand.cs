// CallCommand.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BOOSEInterpreter
{
    public class CallCommand : ICommand
    {
        private readonly Parser parser;
        private readonly ExpressionEvaluator evaluator = new ExpressionEvaluator();

        public CallCommand(Parser parser)
        {
            this.parser = parser;
        }

        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            // Input example: args = [ "call", "draw(x)" ]
            string fullCall = string.Join(" ", args.Skip(1)); // Combine everything after 'call'

            // --- Parse method name and arguments ---
            string methodName;
            string[] callArgs = Array.Empty<string>();

            int openParen = fullCall.IndexOf('(');
            int closeParen = fullCall.LastIndexOf(')');

            if (openParen > 0 && closeParen > openParen)
            {
                methodName = fullCall.Substring(0, openParen).Trim();
                string paramList = fullCall.Substring(openParen + 1, closeParen - openParen - 1);
                callArgs = paramList.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                    .Select(p => p.Trim())
                                    .ToArray();
            }
            else
            {
                methodName = fullCall.Trim();
            }

            // --- Validate method existence ---
            if (!parser.Methods.ContainsKey(methodName))
                throw new Exception($"Method '{methodName}' not defined.");

            MethodDefinition method = parser.Methods[methodName];

            // --- Validate argument count ---
            if (callArgs.Length != method.Parameters.Count)
                throw new Exception($"Argument count mismatch for method '{methodName}'");

            // --- Local variable scope for method ---
            var localVars = new Dictionary<string, object>(variables);

            for (int i = 0; i < method.Parameters.Count; i++)
            {
                string paramName = method.Parameters[i];
                string argValue = callArgs[i];

                // Evaluate the argument (e.g., x or 10 + 5)
                object evaluated = evaluator.Evaluate(argValue, variables);
                localVars[paramName] = evaluated;
            }

            // --- Execute the method block ---
            MethodInfo execBlock = typeof(Parser).GetMethod(
                "ExecuteBlock",
                BindingFlags.NonPublic | BindingFlags.Instance
            );

            execBlock.Invoke(parser, new object[] { method.BodyLines.ToArray(), 0, method.BodyLines.Count - 1, localVars });
        }
    }
}
