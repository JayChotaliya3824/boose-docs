using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The PokeCommand class is implemented to assign values to specific elements within arrays.
    /// It supports updating values in both one-dimensional and two-dimensional arrays of integer or real types.
    /// </summary>
    public class PokeCommand : ICommand
    {
        /// <summary>
        /// The command is executed to update a specific array element with a new value.
        /// The array name, index or coordinates, and the value to be stored are parsed from the command arguments.
        /// The target array is identified, and the value is assigned to the specified location.
        /// </summary>
        /// <param name="canvas">The drawing canvas is passed to the method but is not utilized for array manipulation.</param>
        /// <param name="variables">The dictionary of variables is accessed to locate the array and evaluate any variable references.</param>
        /// <param name="args">The command arguments are processed to determine the target array, indices, and the value to be saved.</param>
        /// <exception cref="ArgumentException">
        /// An exception is thrown if the specified array is not found.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            string arrayName = args[1];

            if (!variables.ContainsKey(arrayName))
                throw new ArgumentException($"Array '{arrayName}' not found.");

            object valueToSave;
            string lastArg = args[args.Length - 1];

            if (variables.ContainsKey(lastArg))
            {
                valueToSave = variables[lastArg];
            }
            else
            {
                if (lastArg.Contains("."))
                {
                    valueToSave = double.Parse(lastArg);
                }
                else
                {
                    valueToSave = int.Parse(lastArg);
                }
            }

            if (variables[arrayName] is int[] intArray)
            {
                int index = CommandHelper.EvaluateInt(args[2], variables);
                intArray[index] = Convert.ToInt32(valueToSave);
            }
            else if (variables[arrayName] is int[,] intMatrix)
            {
                int row = CommandHelper.EvaluateInt(args[2], variables);
                int col = CommandHelper.EvaluateInt(args[3], variables);
                intMatrix[row, col] = Convert.ToInt32(valueToSave);
            }
            else if (variables[arrayName] is double[] realArray)
            {
                int index = CommandHelper.EvaluateInt(args[2], variables);
                realArray[index] = Convert.ToDouble(valueToSave);
            }
            else if (variables[arrayName] is double[,] realMatrix)
            {
                int row = CommandHelper.EvaluateInt(args[2], variables);
                int col = CommandHelper.EvaluateInt(args[3], variables);
                realMatrix[row, col] = Convert.ToDouble(valueToSave);
            }
        }
    }
}