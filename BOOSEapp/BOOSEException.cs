using System;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The BOOSEException class is implemented to handle application-specific errors within the BOOSE Interpreter.
    /// It is derived from the standard Exception class to provide custom error messages and handling.
    /// </summary>
    public class BOOSEException : Exception
    {
        /// <summary>
        /// A new instance of the BOOSEException class is initialized with default properties.
        /// </summary>
        public BOOSEException() { }

        /// <summary>
        /// A new instance of the BOOSEException class is initialized with a specific error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BOOSEException(string message) : base(message) { }

        /// <summary>
        /// A new instance of the BOOSEException class is initialized with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public BOOSEException(string message, Exception inner) : base(message, inner) { }
    }
}