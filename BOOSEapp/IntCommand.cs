// IntCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class IntCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            // Usage: "int x = 10"
            string varName = args[1];
            int value = int.Parse(args[3]);
            variables[varName] = value;
        }
    }
}