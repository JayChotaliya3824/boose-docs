using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The ICommand interface is defined to establish a contract for all command implementations.
    /// A standardized Execute method is required, ensuring consistent execution across different command types within the interpreter.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// The command is executed using the provided canvas and variable context.
        /// This method is called to perform the specific logic associated with the implementing command class.
        /// </summary>
        /// <param name="canvas">The drawing canvas is targeted for rendering operations.</param>
        /// <param name="variables">The dictionary of variables is accessed for parameter evaluation and storage.</param>
        /// <param name="args">The array of command arguments is processed to direct the command's behavior.</param>
        void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args);
    }
}