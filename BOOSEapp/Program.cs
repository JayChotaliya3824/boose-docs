using System;
using System.Windows.Forms;

namespace BOOSEInterpreter
{
    /// <summary>
    /// The Program class serves as the main entry point for the BOOSE Interpreter application.
    /// It is responsible for initializing the application configuration and launching the main form.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application is defined here.
        /// The application configuration is initialized, and the main form (Form1) is instantiated and run.
        /// The STAThread attribute is applied to indicate that the COM threading model for the application is single-threaded apartment.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}