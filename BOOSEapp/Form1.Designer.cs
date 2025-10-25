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
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)picOutput).BeginInit();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(743, 697);
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
            picOutput.Location = new Point(89, 269);
            picOutput.Name = "picOutput";
            picOutput.Size = new Size(774, 411);
            picOutput.TabIndex = 1;
            picOutput.TabStop = false;
            // 
            // txtProgramInput
            // 
            txtProgramInput.Location = new Point(89, 64);
            txtProgramInput.Multiline = true;
            txtProgramInput.Name = "txtProgramInput";
            txtProgramInput.Size = new Size(774, 153);
            txtProgramInput.TabIndex = 2;
            txtProgramInput.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(89, 31);
            label1.Name = "label1";
            label1.Size = new Size(54, 25);
            label1.TabIndex = 3;
            label1.Text = "Input";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(89, 231);
            label2.Name = "label2";
            label2.Size = new Size(130, 25);
            label2.TabIndex = 4;
            label2.Text = "Canvas Output";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 766);
            Controls.Add(label2);
            Controls.Add(label1);
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
        private Label label1;
        private Label label2;
    }
}
