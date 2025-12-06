using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The CastCommand class is implemented to handle variable type casting.
    /// Real number values are converted into integers and stored in destination variables.
    /// </summary>
    public class CastCommand : ICommand
    {
        /// <summary>
        /// The command is executed to perform the casting operation.
        /// The source variable is retrieved and its real value is converted to an integer, which is then assigned to the destination variable.
        /// </summary>
        /// <param name="canvas">The drawing canvas is passed to the method execution context.</param>
        /// <param name="variables">The global variable dictionary is accessed to retrieve source values and update destination variables.</param>
        /// <param name="args">The command arguments are parsed to identify the source and destination variables.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the source variable does not exist in the variable dictionary.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            string destVar = args[1];
            string sourceVar = args[3];

            if (!variables.ContainsKey(sourceVar))
                throw new BOOSEException($"Source variable '{sourceVar}' not found.");

            double realVal = Convert.ToDouble(variables[sourceVar]);
            variables[destVar] = (int)realVal;
        }
    }
}