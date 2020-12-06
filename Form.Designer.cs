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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SliderC = new System.Windows.Forms.TrackBar();
            this.SliderA = new System.Windows.Forms.TrackBar();
            this.SliderB = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LeafSizeTx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AutoRegistrationButton = new System.Windows.Forms.Button();
            this.IntervalTx = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ThreshTx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IterationsTx = new System.Windows.Forms.TextBox();
            this.SaveCheckBox = new System.Windows.Forms.CheckBox();
            this.OpenButton3 = new System.Windows.Forms.Button();
            this.ColorCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderB)).BeginInit();
            this.groupBox3.SuspendLayout();
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OpenButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 104);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 209);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Open viewer from custom setting";
            // 
            // SliderC
            // 
            this.SliderC.Location = new System.Drawing.Point(6, 161);
            this.SliderC.Maximum = 100;
            this.SliderC.Minimum = -100;
            this.SliderC.Name = "SliderC";
            this.SliderC.Size = new System.Drawing.Size(188, 45);
            this.SliderC.TabIndex = 9;
            // 
            // SliderA
            // 
            this.SliderA.Location = new System.Drawing.Point(6, 59);
            this.SliderA.Maximum = 100;
            this.SliderA.Minimum = -100;
            this.SliderA.Name = "SliderA";
            this.SliderA.Size = new System.Drawing.Size(188, 45);
            this.SliderA.TabIndex = 10;
            // 
            // SliderB
            // 
            this.SliderB.Location = new System.Drawing.Point(6, 110);
            this.SliderB.Maximum = 100;
            this.SliderB.Minimum = -100;
            this.SliderB.Name = "SliderB";
            this.SliderB.Size = new System.Drawing.Size(188, 45);
            this.SliderB.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LeafSizeTx);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.AutoRegistrationButton);
            this.groupBox3.Controls.Add(this.IntervalTx);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.IterationsTx);
            this.groupBox3.Controls.Add(this.SaveCheckBox);
            this.groupBox3.Controls.Add(this.OpenButton3);
            this.groupBox3.Location = new System.Drawing.Point(218, 126);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(199, 209);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ICP Matching";
            // 
            // LeafSizeTx
            // 
            this.LeafSizeTx.Location = new System.Drawing.Point(109, 137);
            this.LeafSizeTx.Name = "LeafSizeTx";
            this.LeafSizeTx.Size = new System.Drawing.Size(73, 23);
            this.LeafSizeTx.TabIndex = 10;
            this.LeafSizeTx.Text = "5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Leaf Size";
            // 
            // AutoRegistrationButton
            // 
            this.AutoRegistrationButton.Location = new System.Drawing.Point(7, 166);
            this.AutoRegistrationButton.Name = "AutoRegistrationButton";
            this.AutoRegistrationButton.Size = new System.Drawing.Size(118, 35);
            this.AutoRegistrationButton.TabIndex = 8;
            this.AutoRegistrationButton.Text = "Auto Registration";
            this.AutoRegistrationButton.UseVisualStyleBackColor = true;
            this.AutoRegistrationButton.Click += new System.EventHandler(this.AutoRegistrationButton_Click);
            // 
            // IntervalTx
            // 
            this.IntervalTx.Location = new System.Drawing.Point(109, 111);
            this.IntervalTx.Name = "IntervalTx";
            this.IntervalTx.Size = new System.Drawing.Size(73, 23);
            this.IntervalTx.TabIndex = 7;
            this.IntervalTx.Text = "4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Interval";
            // 
            // ThreshTx
            // 
            this.ThreshTx.Location = new System.Drawing.Point(327, 22);
            this.ThreshTx.Name = "ThreshTx";
            this.ThreshTx.Size = new System.Drawing.Size(73, 23);
            this.ThreshTx.TabIndex = 5;
            this.ThreshTx.Text = "10000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Distance Thresh";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Max Iterations";
            // 
            // IterationsTx
            // 
            this.IterationsTx.Location = new System.Drawing.Point(109, 86);
            this.IterationsTx.Name = "IterationsTx";
            this.IterationsTx.Size = new System.Drawing.Size(73, 23);
            this.IterationsTx.TabIndex = 2;
            this.IterationsTx.Text = "0";
            // 
            // SaveCheckBox
            // 
            this.SaveCheckBox.AutoSize = true;
            this.SaveCheckBox.Location = new System.Drawing.Point(7, 51);
            this.SaveCheckBox.Name = "SaveCheckBox";
            this.SaveCheckBox.Size = new System.Drawing.Size(118, 19);
            this.SaveCheckBox.TabIndex = 1;
            this.SaveCheckBox.Text = "Merge and Save ?";
            this.SaveCheckBox.UseVisualStyleBackColor = true;
            // 
            // OpenButton3
            // 
            this.OpenButton3.Location = new System.Drawing.Point(7, 22);
            this.OpenButton3.Name = "OpenButton3";
            this.OpenButton3.Size = new System.Drawing.Size(75, 23);
            this.OpenButton3.TabIndex = 0;
            this.OpenButton3.Text = "Open";
            this.OpenButton3.UseVisualStyleBackColor = true;
            this.OpenButton3.Click += new System.EventHandler(this.OpenButton3_Click);
            // 
            // ColorCheck
            // 
            this.ColorCheck.AutoSize = true;
            this.ColorCheck.Location = new System.Drawing.Point(225, 54);
            this.ColorCheck.Name = "ColorCheck";
            this.ColorCheck.Size = new System.Drawing.Size(62, 19);
            this.ColorCheck.TabIndex = 9;
            this.ColorCheck.Text = "Color ?";
            this.ColorCheck.UseVisualStyleBackColor = true;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 342);
            this.Controls.Add(this.ColorCheck);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ThreshTx);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form";
            this.Text = "PCL Sample";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SliderC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SliderB)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenButton1;
        private System.Windows.Forms.Button OpenButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar SliderC;
        private System.Windows.Forms.TrackBar SliderA;
        private System.Windows.Forms.TrackBar SliderB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button OpenButton3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IterationsTx;
        private System.Windows.Forms.CheckBox SaveCheckBox;
        private System.Windows.Forms.TextBox ThreshTx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IntervalTx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button AutoRegistrationButton;
        private System.Windows.Forms.TextBox LeafSizeTx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ColorCheck;
    }
}

