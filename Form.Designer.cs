namespace PclSample
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenButton1 = new System.Windows.Forms.Button();
            this.OpenButton2 = new System.Windows.Forms.Button();
            this.ColorModeButton = new System.Windows.Forms.RadioButton();
            this.GrayModeButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SliderB = new System.Windows.Forms.TrackBar();
            this.SliderC = new System.Windows.Forms.TrackBar();
            this.SliderA = new System.Windows.Forms.TrackBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderA)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenButton1
            // 
            this.OpenButton1.Location = new System.Drawing.Point(6, 22);
            this.OpenButton1.Name = "OpenButton1";
            this.OpenButton1.Size = new System.Drawing.Size(75, 23);
            this.OpenButton1.TabIndex = 0;
            this.OpenButton1.Text = "Open";
            this.OpenButton1.UseVisualStyleBackColor = true;
            this.OpenButton1.Click += new System.EventHandler(this.OpenButton1_Click);
            // 
            // OpenButton2
            // 
            this.OpenButton2.Location = new System.Drawing.Point(6, 22);
            this.OpenButton2.Name = "OpenButton2";
            this.OpenButton2.Size = new System.Drawing.Size(75, 23);
            this.OpenButton2.TabIndex = 1;
            this.OpenButton2.Text = "Open";
            this.OpenButton2.UseVisualStyleBackColor = true;
            this.OpenButton2.Click += new System.EventHandler(this.OpenButton2_Click);
            // 
            // ColorModeButton
            // 
            this.ColorModeButton.AutoSize = true;
            this.ColorModeButton.Checked = true;
            this.ColorModeButton.Location = new System.Drawing.Point(6, 51);
            this.ColorModeButton.Name = "ColorModeButton";
            this.ColorModeButton.Size = new System.Drawing.Size(84, 19);
            this.ColorModeButton.TabIndex = 2;
            this.ColorModeButton.TabStop = true;
            this.ColorModeButton.Text = "ColorMode";
            this.ColorModeButton.UseVisualStyleBackColor = true;
            // 
            // GrayModeButton
            // 
            this.GrayModeButton.AutoSize = true;
            this.GrayModeButton.Location = new System.Drawing.Point(6, 75);
            this.GrayModeButton.Name = "GrayModeButton";
            this.GrayModeButton.Size = new System.Drawing.Size(80, 19);
            this.GrayModeButton.TabIndex = 3;
            this.GrayModeButton.Text = "GrayMode";
            this.GrayModeButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OpenButton1);
            this.groupBox1.Controls.Add(this.ColorModeButton);
            this.groupBox1.Controls.Add(this.GrayModeButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Open viewer from a source file";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SliderC);
            this.groupBox2.Controls.Add(this.SliderA);
            this.groupBox2.Controls.Add(this.SliderB);
            this.groupBox2.Controls.Add(this.OpenButton2);
            this.groupBox2.Location = new System.Drawing.Point(232, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 242);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Open viewer from custom setting";
            // 
            // SliderB
            // 
            this.SliderB.Location = new System.Drawing.Point(6, 126);
            this.SliderB.Maximum = 100;
            this.SliderB.Minimum = -100;
            this.SliderB.Name = "SliderB";
            this.SliderB.Size = new System.Drawing.Size(188, 45);
            this.SliderB.TabIndex = 8;
            // 
            // SliderC
            // 
            this.SliderC.Location = new System.Drawing.Point(6, 177);
            this.SliderC.Maximum = 100;
            this.SliderC.Minimum = -100;
            this.SliderC.Name = "SliderC";
            this.SliderC.Size = new System.Drawing.Size(188, 45);
            this.SliderC.TabIndex = 9;
            // 
            // SliderA
            // 
            this.SliderA.Location = new System.Drawing.Point(6, 75);
            this.SliderA.Maximum = 100;
            this.SliderA.Minimum = -100;
            this.SliderA.Name = "SliderA";
            this.SliderA.Size = new System.Drawing.Size(188, 45);
            this.SliderA.TabIndex = 10;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 266);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form";
            this.Text = "PCL Sample";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenButton1;
        private System.Windows.Forms.Button OpenButton2;
        private System.Windows.Forms.RadioButton ColorModeButton;
        private System.Windows.Forms.RadioButton GrayModeButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar SliderC;
        private System.Windows.Forms.TrackBar SliderA;
        private System.Windows.Forms.TrackBar SliderB;
    }
}

