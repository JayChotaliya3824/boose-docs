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
        private static CommandFactory _instance;
        public static CommandFactory Instance(Parser parser = null)
        {
            // Only create once; reuse same instance later
            if (_instance == null)
            {
                _instance = new CommandFactory(parser);
            }

            // If a new parser is passed later, keep the latest reference
            if (parser != null)
            {
                _instance.parser = parser;
            }

            return _instance;
        }
        // --- End Singleton ---

        private readonly Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
        private Parser parser;

        // Private constructor
        private CommandFactory(Parser parser)
        {
            this.parser = parser;

            // --- Register all commands here ---
            commands["moveto"] = new MoveToCommand();
            commands["drawto"] = new DrawToCommand();
            commands["rect"] = new RectangleCommand();
            commands["circle"] = new CircleCommand();
            commands["pencolour"] = new PenColourCommand();
            commands["fill"] = new FillCommand();

            // Variables
            commands["int"] = new IntCommand();
            commands["real"] = new RealCommand();
            commands["array"] = new ArrayCommand();
            commands["var"] = new VarCommand();

            // Method-related
            commands["call"] = new CallCommand(parser);
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

        public bool HasCommand(string commandName)
        {
            return commands.ContainsKey(commandName.ToLower());
        }

        /// <summary>
        /// Gives commands access to parser functions like EvaluateExpression().
        /// </summary>
        public Parser GetParser() => parser;
    }
}
