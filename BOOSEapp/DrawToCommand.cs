// DrawToCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class DrawToCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            canvas.DrawTo(int.Parse(args[1]), int.Parse(args[2]));
        }
    }
}