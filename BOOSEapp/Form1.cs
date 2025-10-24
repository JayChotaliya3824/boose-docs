using System.Diagnostics;
using BOOSE;
namespace BOOSEInterpreter
{
    public partial class Form1 : Form

    {
        DrawingCanvas canvas;
        Parser parser;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = new DrawingCanvas(picOutput);
            parser = new Parser(canvas);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            canvas.ClearCanvas(); // Clear from previous run
            string[] commands = txtProgramInput.Lines;

            foreach (string commandLine in commands)
            {
                parser.ParseCommand(commandLine); // Tell the parser to work
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
