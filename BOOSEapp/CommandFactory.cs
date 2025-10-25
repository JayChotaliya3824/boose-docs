// CommandFactory.cs
using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// Uses the Factory and Singleton patterns to create ICommand objects.
    /// </summary>
    public class CommandFactory
    {
        // --- Singleton Pattern ---
        private static readonly CommandFactory _instance = new CommandFactory();
        public static CommandFactory Instance => _instance;
        // --- End Singleton ---

        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        // Private constructor for Singleton
        private CommandFactory()
        {
            // --- Factory Pattern ---
            commands["moveto"] = new MoveToCommand();
            commands["circle"] = new CircleCommand();
             commands["drawto"] = new DrawToCommand();
             commands["rect"] = new RectangleCommand();
             commands["pencolour"] = new PenColourCommand();
        }

        /// <summary>
        /// Factory method to get a command object.
        /// </summary>
        public ICommand GetCommand(string commandName)
        {
            commandName = commandName.ToLower();
            if (!commands.ContainsKey(commandName))
            {
                throw new ArgumentException($"Unknown command: {commandName}");
            }
            return commands[commandName];
        }
    }
}