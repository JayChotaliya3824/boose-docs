using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The MoveToCommand class is implemented to handle the movement of the drawing pen.
    /// The pen position is updated to the specified coordinates without performing any drawing operation on the canvas.
    /// </summary>
    public class MoveToCommand : ICommand
    {
        /// <summary>
        /// The command is executed to update the pen's current position.
        /// The target coordinates are extracted from the arguments, evaluated, and applied to the canvas.
        /// </summary>
        /// <param name="canvas">The drawing canvas where the pen position is updated.</param>
        /// <param name="variables">The variable dictionary is accessed to evaluate coordinate expressions.</param>
        /// <param name="args">The command arguments are processed to determine the new x and y coordinates.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the required number of parameters is not provided.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 2) throw new BOOSEException("MoveTo requires 2 parameters.");

            int x, y;
            // Join args back together to handle spaces (e.g., "100, 100")
            string parameters = string.Join(" ", args, 1, args.Length - 1);
            string[] coords;

            if (parameters.Contains(","))
            {
                coords = parameters.Split(',');
            }
            else
            {
                coords = parameters.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (coords.Length != 2)
                throw new BOOSEException("MoveTo requires exactly 2 parameters (x, y).");

            x = CommandHelper.EvaluateInt(coords[0].Trim(), variables);
            y = CommandHelper.EvaluateInt(coords[1].Trim(), variables);
            canvas.MoveTo(x, y);
        }
    }
}