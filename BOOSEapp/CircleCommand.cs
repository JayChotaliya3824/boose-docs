// CircleCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class CircleCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            canvas.DrawCircle(int.Parse(args[1]));
        }
    }
}