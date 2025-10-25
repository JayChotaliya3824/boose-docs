// RealCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class RealCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            // Usage: "real y = 10.5"
            string varName = args[1];
            double value = double.Parse(args[3]); // Use double for real
            variables[varName] = value;
        }
    }
}