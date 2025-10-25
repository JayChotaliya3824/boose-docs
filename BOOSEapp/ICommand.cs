// ICommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public interface ICommand
    {
        // All commands will need the canvas, arguments, and variable store
        void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args);
    }
}