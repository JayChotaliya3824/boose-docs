using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The DrawToCommand class is implemented to handle drawing lines to specific coordinates.
    /// The pen is moved from its current position to the specified target coordinates, creating a line on the canvas.
    /// </summary>
    public class DrawToCommand : ICommand
    {
        /// <summary>
        /// The command is executed to draw a line to the target coordinates.
        /// The arguments are parsed to extract the x and y coordinates, which are then evaluated and passed to the canvas drawing method.
        /// </summary>
        /// <param name="canvas">The drawing canvas is targeted for the line drawing operation.</param>
        /// <param name="variables">The variable dictionary is accessed to evaluate coordinate expressions.</param>
        /// <param name="args">The command arguments are processed to determine the destination coordinates.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the incorrect number of parameters is provided.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 2) throw new BOOSEException("DrawTo requires 2 parameters.");

            int x, y;
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
                throw new BOOSEException("DrawTo requires exactly 2 parameters (x, y).");

            x = CommandHelper.EvaluateInt(coords[0].Trim(), variables);
            y = CommandHelper.EvaluateInt(coords[1].Trim(), variables);
            canvas.DrawTo(x, y);
        }
    }
}