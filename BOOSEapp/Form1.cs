using System.Diagnostics;
using BOOSE;
namespace BOOSEInterpreter
{
    public partial class Form1 : Form

    {
        
        public Form1()
        {
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            AboutBOOSE about = new AboutBOOSE();

            
            string aboutInfo = about.ToString();

            Debug.WriteLine("--- BOOSE Info ---");
            Debug.WriteLine(aboutInfo);
            Debug.WriteLine("--------------------");
        


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
