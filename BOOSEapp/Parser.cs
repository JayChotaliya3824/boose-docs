// Parser.cs
using System;
using System.Drawing;
using System.Diagnostics;

namespace BOOSEInterpreter
{
    /// <summary>
    /// Parses and executes commands for the BOOSE interpreter.
    /// </summary>
    public class Parser
    {
        private DrawingCanvas canvas;

        /// <summary>
        /// Creates a new parser linked to a specific canvas.
        /// </summary>
        public Parser(DrawingCanvas canvas)
        {
            this.canvas = canvas;
        }

        /// <summary>
        /// Parses a single line of BOOSE code.
        /// </summary>
        public void ParseCommand(string commandLine)
        {
            string[] parts = commandLine.Trim().Split(' ');
            string command = parts[0].ToLower();

            // --- Exception Handling (Task 4 - 5 marks) ---
            try
            {
                switch (command)
                {
                    // --- Basic Drawing (Task 6 - 12 marks) ---
                    case "moveto":
                        canvas.MoveTo(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;

                    case "drawto":
                        canvas.DrawTo(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;

                    case "pencolour":
                        canvas.SetPenColour(Color.FromName(parts[1]));
                        break;

                    case "rect":
                        canvas.DrawRectangle(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;

                    case "circle":
                        canvas.DrawCircle(int.Parse(parts[1]));
                        break;

                    case "": // Ignore empty lines
                        break;

                    default:
                        Debug.WriteLine($"Unknown command: {command}");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Graceful error handling
                Debug.WriteLine($"Error processing '{commandLine}': {ex.Message}");
            }
        }
    }
}