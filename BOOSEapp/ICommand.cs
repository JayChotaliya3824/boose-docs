// ICommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public interface ICommand
    {
        void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args);
    }
}