using System;
using System.Collections.Generic;
using System.Linq;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The CallCommand class is implemented to facilitate the invocation of user-defined methods.
    /// Parameters are parsed, evaluated, and passed to the method body for execution within a local variable scope.
    /// </summary>
    public class CallCommand : ICommand
    {
        /// <summary>
        /// A reference to the main Parser instance is maintained to access the list of defined methods and execution logic.
        /// </summary>
        private readonly Parser parser;

        /// <summary>
        /// An instance of the ExpressionEvaluator is utilized to resolve parameter values before method execution.
        /// </summary>
        private readonly ExpressionEvaluator evaluator = new ExpressionEvaluator();

        /// <summary>
        /// A new instance of the CallCommand class is initialized with a reference to the Parser.
        /// </summary>
        /// <param name="parser">The Parser instance is provided to allow access to method definitions and execution context.</param>
        public CallCommand(Parser parser)
        {
            this.parser = parser;
        }

        /// <summary>
        /// The command is executed to call a specific method with the provided arguments.
        /// The method definition is retrieved, arguments are evaluated and mapped to parameters, and the method body is executed.
        /// Return values are captured and stored in the global variable scope if applicable.
        /// </summary>
        /// <param name="canvas">The drawing canvas is passed to the method execution context.</param>
        /// <param name="variables">The global variable dictionary is accessed for argument evaluation and return value storage.</param>
        /// <param name="args">The command arguments are processed to identify the method name and parameters.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the method is not found, or if there is a mismatch in the number of arguments provided.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            // Expected syntax: "call methodName arg1 arg2" OR "call methodName(arg1, arg2)"
            if (args == null || args.Length < 2)
                throw new BOOSEException("Call command requires at least a method name.");

            string rawMethodName = args[1].Trim();
            string methodName;
            string[] callArgs;

            // Fix: Support "call draw(x)" syntax
            if (rawMethodName.Contains("(") && rawMethodName.EndsWith(")"))
            {
                int pFrom = rawMethodName.IndexOf("(") + 1;
                int pTo = rawMethodName.LastIndexOf(")");
                methodName = rawMethodName.Substring(0, pFrom - 1).ToLower();
                string argsContent = rawMethodName.Substring(pFrom, pTo - pFrom);

                if (string.IsNullOrWhiteSpace(argsContent))
                {
                    callArgs = new string[0];
                }
                else
                {
                    // Assuming args are comma separated inside ()
                    callArgs = argsContent.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else
            {
                // Classic space separated syntax
                methodName = rawMethodName.ToLower();
                callArgs = new string[args.Length - 2];
                if (callArgs.Length > 0)
                {
                    Array.Copy(args, 2, callArgs, 0, callArgs.Length);
                }
            }

            if (!parser.Methods.TryGetValue(methodName, out MethodDefinition method))
            {
                throw new BOOSEException($"Method '{methodName}' is not defined.");
            }

            if (callArgs.Length != method.Parameters.Count)
            {
                throw new BOOSEException(
                    $"Argument count mismatch for method '{methodName}'. " +
                    $"Expected {method.Parameters.Count} arguments, but got {callArgs.Length}.");
            }

            var localVars = new Dictionary<string, object>();
            for (int i = 0; i < method.Parameters.Count; i++)
            {
                string paramName = method.Parameters[i];
                // Trim args to remove spaces from "x, y" -> " y"
                object evaluatedValue = evaluator.Evaluate(callArgs[i].Trim(), variables);
                localVars[paramName] = evaluatedValue;
            }

            parser.ExecuteMethodBodyBlock(method.BodyLines.ToArray(), localVars);

            // Fix: Check for return value case-insensitively.
            // The user might assign "testMethod = ..." inside the method, matching the method name casing.
            string returnVariable = localVars.Keys.FirstOrDefault(k => k.Equals(methodName, StringComparison.OrdinalIgnoreCase));

            if (returnVariable != null)
            {
                variables[methodName] = localVars[returnVariable];
            }
            else
            {
                // If no return value found (void method), default to 0
                variables[methodName] = 0;
            }
        }
    }
}