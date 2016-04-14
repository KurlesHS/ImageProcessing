namespace ImageProcessing.GradientFilter
{
    partial class GradientSettingChanger
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonSobelFeldman = new System.Windows.Forms.RadioButton();
            this.radioButtonPrewitt = new System.Windows.Forms.RadioButton();
            this.radioButtonSobel = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonSobelFeldman);
            this.groupBox1.Controls.Add(this.radioButtonPrewitt);
            this.groupBox1.Controls.Add(this.radioButtonSobel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 89);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kernel";
            // 
            // radioButtonSobelFeldman
            // 
            this.radioButtonSobelFeldman.AutoSize = true;
            this.radioButtonSobelFeldman.Location = new System.Drawing.Point(7, 67);
            this.radioButtonSobelFeldman.Name = "radioButtonSobelFeldman";
            this.radioButtonSobelFeldman.Size = new System.Drawing.Size(95, 17);
            this.radioButtonSobelFeldman.TabIndex = 2;
            this.radioButtonSobelFeldman.TabStop = true;
            this.radioButtonSobelFeldman.Text = "Sobel-Feldman";
            this.radioButtonSobelFeldman.UseVisualStyleBackColor = true;
            this.radioButtonSobelFeldman.CheckedChanged += new System.EventHandler(this.radioButtonSobelFeldman_CheckedChanged);
            // 
            // radioButtonPrewitt
            // 
            this.radioButtonPrewitt.AutoSize = true;
            this.radioButtonPrewitt.Location = new System.Drawing.Point(7, 43);
            this.radioButtonPrewitt.Name = "radioButtonPrewitt";
            this.radioButtonPrewitt.Size = new System.Drawing.Size(60, 17);
            this.radioButtonPrewitt.TabIndex = 1;
            this.radioButtonPrewitt.Text = "Prewitt ";
            this.radioButtonPrewitt.UseVisualStyleBackColor = true;
            this.radioButtonPrewitt.CheckedChanged += new System.EventHandler(this.radioButtonPrewitt_CheckedChanged);
            // 
            // radioButtonSobel
            // 
            this.radioButtonSobel.AutoSize = true;
            this.radioButtonSobel.Checked = true;
            this.radioButtonSobel.Location = new System.Drawing.Point(7, 20);
            this.radioButtonSobel.Name = "radioButtonSobel";
            this.radioButtonSobel.Size = new System.Drawing.Size(52, 17);
            this.radioButtonSobel.TabIndex = 0;
            this.radioButtonSobel.TabStop = true;
            this.radioButtonSobel.Text = "Sobel";
            this.radioButtonSobel.UseVisualStyleBackColor = true;
            this.radioButtonSobel.CheckedChanged += new System.EventHandler(this.radioButtonSobel_CheckedChanged);
            // 
            // GradientSettingChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "GradientSettingChanger";
            this.Size = new System.Drawing.Size(248, 90);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonSobelFeldman;
        private System.Windows.Forms.RadioButton radioButtonPrewitt;
        private System.Windows.Forms.RadioButton radioButtonSobel;
    }
}
