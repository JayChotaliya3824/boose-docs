using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The PeekCommand class is implemented to retrieve values from arrays and store them into variables.
    /// Support is provided for both one-dimensional and two-dimensional arrays of integer or real types.
    /// </summary>
    public class PeekCommand : ICommand
    {
        /// <summary>
        /// The command is executed to read a value from a specified array index.
        /// The array name, target variable, and index coordinates are parsed from the arguments.
        /// The retrieved value is then assigned to the target variable within the global variable dictionary.
        /// </summary>
        /// <param name="canvas">The drawing canvas is passed to the method but is not utilized for data retrieval.</param>
        /// <param name="variables">The dictionary of variables is accessed to locate the source array and store the retrieved value.</param>
        /// <param name="args">The command arguments are processed to identify the target variable, source array, and indices.</param>
        /// <exception cref="ArgumentException">
        /// An exception is thrown if the syntax is incorrect, the array is not found, or the array type is unsupported.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 4)
                throw new ArgumentException("Peek requires: peek <targetVar> <arrayName> <index>  OR  peek <targetVar> = <arrayName> <index>");

            string targetVar;
            string arrayName;
            int row;
            int? col = null;

            if (args[2] == "=")
            {
                if (args.Length < 5)
                    throw new ArgumentException("Invalid peek syntax with '='. Expected: peek var = array index");

                targetVar = args[1];
                arrayName = args[3];
                row = CommandHelper.EvaluateInt(args[4], variables);

                if (args.Length > 5)
                {
                    col = CommandHelper.EvaluateInt(args[5], variables);
                }
            }
            else
            {
                targetVar = args[1];
                arrayName = args[2];
                row = CommandHelper.EvaluateInt(args[3], variables);

                if (args.Length > 4)
                {
                    col = CommandHelper.EvaluateInt(args[4], variables);
                }
            }

            if (!variables.ContainsKey(arrayName))
                throw new ArgumentException($"Array '{arrayName}' not found.");

            if (variables[arrayName] is int[] intArray)
            {
                variables[targetVar] = intArray[row];
            }
            else if (variables[arrayName] is int[,] intMatrix)
            {
                variables[targetVar] = intMatrix[row, col ?? 0];
            }
            else if (variables[arrayName] is double[] realArray)
            {
                variables[targetVar] = realArray[row];
            }
            else if (variables[arrayName] is double[,] realMatrix)
            {
                variables[targetVar] = realMatrix[row, col ?? 0];
            }
            else
            {
                throw new ArgumentException($"Unsupported array type for '{arrayName}'.");
            }
        }
    }
}