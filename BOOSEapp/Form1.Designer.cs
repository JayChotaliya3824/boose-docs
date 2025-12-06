namespace BOOSEInterpreter
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btnRun = new Button();
            picOutput = new PictureBox();
            txtProgramInput = new TextBox();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)picOutput).BeginInit();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(787, 577);
            btnRun.Margin = new Padding(2, 2, 2, 2);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(84, 23);
            btnRun.TabIndex = 0;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += button1_Click;
            // 
            // picOutput
            // 
            picOutput.BackColor = SystemColors.Desktop;
            picOutput.Location = new Point(62, 176);
            picOutput.Margin = new Padding(2, 2, 2, 2);
            picOutput.Name = "picOutput";
            picOutput.Size = new Size(809, 379);
            picOutput.TabIndex = 1;
            picOutput.TabStop = false;
            // 
            // txtProgramInput
            // 
            txtProgramInput.Location = new Point(62, 38);
            txtProgramInput.Margin = new Padding(2, 2, 2, 2);
            txtProgramInput.Multiline = true;
            txtProgramInput.Name = "txtProgramInput";
            txtProgramInput.Size = new Size(809, 119);
            txtProgramInput.TabIndex = 2;
            txtProgramInput.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(62, 19);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 3;
            label1.Text = "Input";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(62, 159);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(86, 15);
            label2.TabIndex = 4;
            label2.Text = "Canvas Output";
            // 
            // button1
            // 
            button1.Location = new Point(692, 577);
            button1.Margin = new Padding(2, 2, 2, 2);
            button1.Name = "button1";
            button1.Size = new Size(84, 23);
            button1.TabIndex = 5;
            button1.Text = "About";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(931, 611);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtProgramInput);
            Controls.Add(picOutput);
            Controls.Add(btnRun);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Form1";
            Text = "BOOSE Interpreter";
            Load += Form1_Load_1;
            ((System.ComponentModel.ISupportInitialize)picOutput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRun;
        private PictureBox picOutput;
        private TextBox txtProgramInput;
        private Label label1;
        private Label label2;
        private Button button1;
    }
}