// MethodDefinition.cs
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// A simple class to store a method's details.
    /// </summary>
    public class MethodDefinition
    {
        public List<string> Parameters { get; set; }
        public List<string> BodyLines { get; set; }

        public MethodDefinition()
        {
            Parameters = new List<string>();
            BodyLines = new List<string>();
        }
    }
}