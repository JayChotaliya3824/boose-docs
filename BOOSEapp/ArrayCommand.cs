using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The ArrayCommand class is implemented to handle the creation and initialization of arrays.
    /// Support is provided for both integer and real number arrays, including single and multi-dimensional definitions.
    /// </summary>
    public class ArrayCommand : ICommand
    {
        /// <summary>
        /// The array command is executed to parse the provided arguments and instantiate the requested array.
        /// The resulting array object is stored within the shared variables dictionary for subsequent access.
        /// </summary>
        /// <param name="canvas">The drawing canvas is passed to the method but is not utilized for array creation.</param>
        /// <param name="variables">The global variable storage is accessed to store the newly created array instance.</param>
        /// <param name="args">The command arguments are processed to determine the array type, name, and dimensions.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the argument count is insufficient or if an unsupported array type is specified.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 4)
            {
                throw new BOOSEException("Array command requires 3 parameters: type, name, size");
            }

            string type = args[1].ToLower();
            string name = args[2];
            string sizeStr = args[3];

            if (type != "int" && type != "real")
            {
                throw new BOOSEException($"Invalid array type '{type}'. Only 'int' and 'real' are supported.");
            }

            if (sizeStr.Contains(","))
            {
                string[] parts = sizeStr.Split(',');
                int rows = int.Parse(parts[0]);
                int cols = int.Parse(parts[1]);

                if (type == "int")
                    variables[name] = new int[rows, cols];
                else
                    variables[name] = new double[rows, cols];
            }
            else
            {
                int size = int.Parse(sizeStr);

                if (type == "int")
                    variables[name] = new int[size];
                else
                    variables[name] = new double[size];
            }
        }
    }
}