using System;
using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The CommandFactory class is implemented to manage the creation and retrieval of command objects.
    /// The Factory design pattern is utilized to instantiate commands based on string identifiers, ensuring a decoupled architecture.
    /// The Singleton pattern is applied to ensure a single instance of the factory manages command generation throughout the application lifecycle.
    /// </summary>
    public class CommandFactory
    {
        /// <summary>
        /// The single instance of the CommandFactory is stored for global access.
        /// </summary>
        private static CommandFactory _instance;

        /// <summary>
        /// A thread-safe lock object is used to prevent concurrent creation of the singleton instance.
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// The singleton instance of the CommandFactory is retrieved.
        /// If the instance does not exist, it is created in a thread-safe manner.
        /// </summary>
        /// <param name="parser">The parser instance is provided to facilitate command initialization where necessary.</param>
        /// <returns>The unique instance of the CommandFactory is returned.</returns>
        public static CommandFactory Instance(Parser parser = null)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CommandFactory(parser);
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// A dictionary is maintained to map command keyword strings to their corresponding ICommand implementations.
        /// </summary>
        private readonly Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        /// <summary>
        /// A reference to the Parser is stored for dependency injection into commands requiring parser context.
        /// </summary>
        private Parser parser;

        /// <summary>
        /// A new instance of the CommandFactory is initialized.
        /// The internal command dictionary is populated with all supported command types and their associated implementations.
        /// </summary>
        /// <param name="parser">The Parser instance is injected to support commands such as CallCommand.</param>
        private CommandFactory(Parser parser)
        {
            this.parser = parser;

            commands["moveto"] = new MoveToCommand();
            commands["drawto"] = new DrawToCommand();
            commands["rect"] = new RectangleCommand();
            commands["circle"] = new CircleCommand();
            commands["pencolour"] = new PenColourCommand();
            commands["pen"] = new PenColourCommand();
            commands["fill"] = new FillCommand();
            commands["write"] = new WriteCommand();
            commands["int"] = new IntCommand();
            commands["real"] = new RealCommand();
            commands["array"] = new ArrayCommand();
            commands["poke"] = new PokeCommand();
            commands["peek"] = new PeekCommand();
            commands["boolean"] = new BooleanCommand();
            commands["cast"] = new CastCommand();
            commands["var"] = new VarCommand();
            commands["call"] = new CallCommand(parser);
        }

        /// <summary>
        /// A command object is instantiated based on the provided command type string.
        /// The command string is normalized, and the corresponding command object is retrieved from the registry.
        /// </summary>
        /// <param name="commandType">The string identifier for the requested command type.</param>
        /// <returns>The ICommand implementation corresponding to the specified type is returned.</returns>
        /// <exception cref="BOOSEException">
        /// An exception is thrown if the requested command type is not recognized by the factory.
        /// </exception>
        public ICommand MakeCommand(string commandType)
        {
            string cmd = commandType.ToLower().Trim();

            if (commands.ContainsKey(cmd))
            {
                return commands[cmd];
            }

            throw new BOOSEException($"Factory Error: Unknown command '{commandType}'");
        }

        /// <summary>
        /// Checks are performed to determine if a command with the specified name exists within the factory.
        /// </summary>
        /// <param name="commandName">The name of the command to verify.</param>
        /// <returns>True is returned if the command exists; otherwise, false is returned.</returns>
        public bool HasCommand(string commandName)
        {
            return commands.ContainsKey(commandName.ToLower());
        }

        /// <summary>
        /// The command corresponding to the specified name is retrieved.
        /// This method serves as a wrapper for the MakeCommand method.
        /// </summary>
        /// <param name="commandName">The name of the command to retrieve.</param>
        /// <returns>The requested ICommand object is returned.</returns>
        public ICommand GetCommand(string commandName)
        {
            return MakeCommand(commandName);
        }
    }
}