using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The CircleCommand class is implemented to facilitate the drawing of circles on the canvas.
    /// The radius expression is evaluated, and the corresponding drawing operation is invoked.
    /// </summary>
    public class CircleCommand : ICommand
    {
        /// <summary>
        /// The command is executed to draw a circle.
        /// The arguments are joined and evaluated to determine the radius, which is then passed to the canvas for rendering.
        /// </summary>
        /// <param name="canvas">The drawing canvas is targeted for the drawing operation.</param>
        /// <param name="variables">The dictionary of variables is accessed to evaluate the radius expression.</param>
        /// <param name="args">The arguments are processed to extract the radius parameter.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the radius parameter is not provided.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 2)
                throw new BOOSEException("Circle command requires a radius parameter.");

            // Fix: Join all arguments after the command to handle expressions with spaces (e.g., "radius * 10")
            string expression = string.Join(" ", args, 1, args.Length - 1);

            int radius = CommandHelper.EvaluateInt(expression, variables);
            canvas.DrawCircle(radius);
        }
    }
}