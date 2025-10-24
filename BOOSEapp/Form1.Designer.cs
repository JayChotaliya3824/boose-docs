namespace BOOSEInterpreter
{
    partial class Form1
    {
       
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRun = new Button();
            picOutput = new PictureBox();
            txtProgramInput = new TextBox();
            ((System.ComponentModel.ISupportInitialize)picOutput).BeginInit();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(514, 523);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(120, 39);
            btnRun.TabIndex = 0;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += button1_Click;
            // 
            // picOutput
            // 
            picOutput.BackColor = SystemColors.Desktop;
            picOutput.Location = new Point(219, 185);
            picOutput.Name = "picOutput";
            picOutput.Size = new Size(415, 323);
            picOutput.TabIndex = 1;
            picOutput.TabStop = false;
            // 
            // txtProgramInput
            // 
            txtProgramInput.Location = new Point(219, 95);
            txtProgramInput.Multiline = true;
            txtProgramInput.Name = "txtProgramInput";
            txtProgramInput.Size = new Size(415, 61);
            txtProgramInput.TabIndex = 2;
            txtProgramInput.TextChanged += textBox1_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 684);
            Controls.Add(txtProgramInput);
            Controls.Add(picOutput);
            Controls.Add(btnRun);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picOutput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRun;
        private PictureBox picOutput;
        private TextBox txtProgramInput;
    }
}
