// PenColourCommand.cs
using System.Collections.Generic;
using System.Drawing; // Don't forget this!

namespace BOOSEInterpreter
{
    public class PenColourCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            canvas.SetPenColour(Color.FromName(args[1]));
        }
    }
}