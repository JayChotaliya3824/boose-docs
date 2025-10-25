// RectangleCommand.cs
using System.Collections.Generic;
namespace BOOSEInterpreter
{
    public class RectangleCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            int width = CommandHelper.EvaluateInt(args[1], variables); 
            int height = CommandHelper.EvaluateInt(args[2], variables); 
            canvas.DrawRectangle(width, height);
        }
    }
}