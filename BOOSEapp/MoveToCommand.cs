// MoveToCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class MoveToCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            int x = CommandHelper.EvaluateInt(args[1], variables);
            int y = CommandHelper.EvaluateInt(args[2], variables);
            canvas.MoveTo(x, y);
        }
    }
}