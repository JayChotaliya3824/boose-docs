using System.Collections.Generic;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The MethodDefinition class is implemented to store the structure of user-defined methods.
    /// Information regarding parameters and the sequence of method body commands is maintained for execution.
    /// </summary>
    public class MethodDefinition
    {
        /// <summary>
        /// A list of parameter names is stored to define the method signature.
        /// </summary>
        public List<string> Parameters { get; set; }

        /// <summary>
        /// A list of strings representing the method body is maintained.
        /// Each string corresponds to a line of code to be executed when the method is invoked.
        /// </summary>
        public List<string> BodyLines { get; set; }

        /// <summary>
        /// A new instance of the MethodDefinition class is initialized.
        /// The parameter and body line lists are instantiated to prepare for method definition storage.
        /// </summary>
        public MethodDefinition()
        {
            Parameters = new List<string>();
            BodyLines = new List<string>();
        }
    }
}