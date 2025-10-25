// MoveToCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class MoveToCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            canvas.MoveTo(int.Parse(args[1]), int.Parse(args[2]));
        }
    }
}