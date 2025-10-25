// RectangleCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class RectangleCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            canvas.DrawRectangle(int.Parse(args[1]), int.Parse(args[2]));
        }
    }
}