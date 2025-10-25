// ArrayCommand.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    public class ArrayCommand : ICommand
    {
        public void Execute(DrawingCanvas canvas, Dictionary<string, object> variables, string[] args)
        {
            // Usage: "array myArr 10" (for a 1D array of size 10)
            string varName = args[1];
            int size = int.Parse(args[2]);
            // Store it as a simple object array
            variables[varName] = new object[size];
        }
    }
}