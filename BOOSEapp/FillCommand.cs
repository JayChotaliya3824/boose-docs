using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The FillCommand class is implemented to control the fill state of shapes drawn on the canvas.
    /// The fill status is toggled based on the provided argument, determining whether subsequent shapes are filled or outlined.
    /// </summary>
    public class FillCommand : ICommand
    {
        /// <summary>
        /// The command is executed to update the fill state of the drawing canvas.
        /// The argument is evaluated to determine if the fill mode should be enabled ("on") or disabled.
        /// The canvas configuration is then updated accordingly.
        /// </summary>
        /// <param name="canvas">The drawing canvas where the fill setting is applied.</param>
        /// <param name="variables">The variable dictionary is passed but not utilized for this specific command.</param>
        /// <param name="args">The command arguments are processed to extract the desired fill state.</param>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            bool fillState = args[1].ToLower() == "on";
            canvas.SetFill(fillState);
        }
    }
}