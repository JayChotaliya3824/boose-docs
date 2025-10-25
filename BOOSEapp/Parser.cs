// Parser.cs
using System;
using System.Diagnostics;
using System.Collections.Generic; // Added for Dictionary

namespace BOOSEInterpreter
{
    public class Parser
    {
        private DrawingCanvas canvas;
        private Dictionary<string, object> variables; // For Task 5
        private CommandFactory factory;

        public Parser(DrawingCanvas canvas)
        {
            this.canvas = canvas;
            this.variables = new Dictionary<string, object>();
            this.factory = CommandFactory.Instance; // Get the Singleton factory
        }

        // This method will expand in the next steps
        public void ParseProgram(string[] programLines)
        {
            variables.Clear(); // Reset variables for each run

            foreach (string line in programLines)
            {
                ParseCommand(line);
            }
        }

        public void ParseCommand(string commandLine)
        {
            string[] parts = commandLine.Trim().Split(' ');
            string commandName = parts[0].ToLower();

            if (string.IsNullOrEmpty(commandName)) return; // Skip empty lines

            try
            {
                // --- Factory Pattern in use! ---
                ICommand command = factory.GetCommand(commandName);
                command.Execute(canvas, variables, parts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error processing '{commandLine}': {ex.Message}");
            }
        }
    }
}