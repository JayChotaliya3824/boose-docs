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

            canvas.ClearCanvas();
            string[] commands = txtProgramInput.Lines;
            parser.ParseProgram(commands);


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
