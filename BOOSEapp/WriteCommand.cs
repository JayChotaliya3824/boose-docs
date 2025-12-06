using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The WriteCommand class is implemented to display text or values on the drawing canvas.
    /// It processes arguments which may include text strings, variables, or expressions, and renders the result at the current pen position.
    /// </summary>
    public class WriteCommand : ICommand
    {
        /// <summary>
        /// The command is executed to write text onto the canvas.
        /// The arguments are combined and the relevant expression part is extracted and evaluated.
        /// The result is then formatted as a string (handling double and integer types specifically) and passed to the canvas for rendering.
        /// </summary>
        /// <param name="canvas">The drawing canvas where the text is displayed.</param>
        /// <param name="variables">The dictionary of variables is accessed to evaluate any expressions contained in the write command.</param>
        /// <param name="args">The command arguments are processed to determine the content to be written.</param>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if no text or expression parameter is provided.
        /// </exception>
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            if (args.Length < 2)
                throw new BOOSEException("Write command requires at least 1 parameter.");

            string fullLine = string.Join(" ", args);
            string expressionPart = fullLine.Substring(5).Trim();
            var evaluator = new ExpressionEvaluator();
            object result = evaluator.Evaluate(expressionPart, variables);

            if (result is double d)
                canvas.WriteText(d.ToString("F2"));
            else if (result is int i)
                canvas.WriteText(i.ToString());
            else
                canvas.WriteText(result.ToString());
        }
    }
}