using System;
using System.Windows.Forms;

namespace BOOSEInterpreter
{
    public partial class Form1 : Form
    {
        DrawingCanvas canvas;
        Parser parser;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (canvas == null)
                {
                    canvas = new DrawingCanvas(picOutput);
                    parser = new Parser(canvas);
                }
                canvas.ClearCanvas();
                string[] commands = txtProgramInput.Lines;
                parser.ParseProgram(commands);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string versionInfo = BOOSE.AboutBOOSE.about();
            MessageBox.Show(versionInfo, "BOOSE Library Version");
        }
    }
}