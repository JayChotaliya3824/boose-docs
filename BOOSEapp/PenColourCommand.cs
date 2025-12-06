using System;
using System.Collections.Generic;
using System.Drawing;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The PenColourCommand class is implemented to update the pen colour used for drawing operations on the canvas.
    /// Colours are defined using RGB values provided as parameters.
    /// </summary>
    public class PenColourCommand : ICommand
    {
        /// <summary>
        /// The command is executed to change the current drawing colour of the pen.
        /// The arguments are parsed to extract red, green, and blue components, which are then evaluated and applied to the canvas.
        /// </summary>
        /// <param name="canvas">The drawing canvas where the pen colour is updated.</param>
        /// <param name="variables">The dictionary of variables is accessed to evaluate colour component expressions.</param>
        /// <param name="args">The command arguments are processed to determine the RGB values.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the required RGB parameters are missing or invalid.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            string parameters = string.Join(" ", args, 1, args.Length - 1);
            string[] colors;

            if (parameters.Contains(","))
            {
                colors = parameters.Split(',');
            }
            else
            {
                colors = parameters.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (colors.Length != 3)
                throw new BOOSEException("PenColour requires exactly 3 parameters (red, green, blue).");

            int r = CommandHelper.EvaluateInt(colors[0].Trim(), variables);
            int g = CommandHelper.EvaluateInt(colors[1].Trim(), variables);
            int b = CommandHelper.EvaluateInt(colors[2].Trim(), variables);

            canvas.SetPenColour(Color.FromArgb(r, g, b));
        }
    }
}