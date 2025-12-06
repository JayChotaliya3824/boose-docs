using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The RectangleCommand class is implemented to facilitate the drawing of rectangles on the canvas.
    /// It processes width and height arguments, evaluating expressions if provided, and invokes the rectangle drawing method on the canvas.
    /// </summary>
    public class RectangleCommand : ICommand
    {
        /// <summary>
        /// The command is executed to draw a rectangle with the specified dimensions.
        /// The arguments are parsed to extract the width and height, handling optional commas and spaces.
        /// The dimensions are evaluated against the current variables before the drawing operation is performed.
        /// </summary>
        /// <param name="canvas">The drawing canvas is targeted for the rectangle drawing operation.</param>
        /// <param name="variables">The variable dictionary is accessed to evaluate dimension expressions.</param>
        /// <param name="args">The command arguments are processed to determine the width and height of the rectangle.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the incorrect number of parameters is provided.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 2) throw new BOOSEException("Rect requires 2 parameters.");

            int width, height;
            string parameters = string.Join(" ", args, 1, args.Length - 1);
            string[] dims;

            if (parameters.Contains(","))
            {
                dims = parameters.Split(',');
            }
            else
            {
                dims = parameters.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (dims.Length != 2)
                throw new BOOSEException("Rect requires exactly 2 parameters (width, height).");

            width = CommandHelper.EvaluateInt(dims[0].Trim(), variables);
            height = CommandHelper.EvaluateInt(dims[1].Trim(), variables);
            canvas.DrawRectangle(width, height);
        }
    }
}