// FillCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class FillCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            bool fillState = args[1].ToLower() == "on";
            canvas.SetFill(fillState);
        }
    }
}