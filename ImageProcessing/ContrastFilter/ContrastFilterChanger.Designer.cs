namespace ImageProcessing.ContrastFilter
{
    partial class ContrastFilterChanger
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonColor = new System.Windows.Forms.RadioButton();
            this.radioButtonGrayscale = new System.Windows.Forms.RadioButton();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonGrayscale);
            this.panel1.Controls.Add(this.radioButtonColor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 359);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 62);
            this.panel1.TabIndex = 0;
            // 
            // radioButtonColor
            // 
            this.radioButtonColor.AutoSize = true;
            this.radioButtonColor.Checked = true;
            this.radioButtonColor.Location = new System.Drawing.Point(13, 12);
            this.radioButtonColor.Name = "radioButtonColor";
            this.radioButtonColor.Size = new System.Drawing.Size(49, 17);
            this.radioButtonColor.TabIndex = 0;
            this.radioButtonColor.TabStop = true;
            this.radioButtonColor.Text = "Color";
            this.radioButtonColor.UseVisualStyleBackColor = true;
            this.radioButtonColor.CheckedChanged += new System.EventHandler(this.radioButtonColor_CheckedChanged);
            // 
            // radioButtonGrayscale
            // 
            this.radioButtonGrayscale.AutoSize = true;
            this.radioButtonGrayscale.Location = new System.Drawing.Point(13, 36);
            this.radioButtonGrayscale.Name = "radioButtonGrayscale";
            this.radioButtonGrayscale.Size = new System.Drawing.Size(72, 17);
            this.radioButtonGrayscale.TabIndex = 1;
            this.radioButtonGrayscale.TabStop = true;
            this.radioButtonGrayscale.Text = "Grayscale";
            this.radioButtonGrayscale.UseVisualStyleBackColor = true;
            this.radioButtonGrayscale.CheckedChanged += new System.EventHandler(this.radioButtonGrayscale_CheckedChanged);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar.Location = new System.Drawing.Point(0, 0);
            this.vScrollBar.Minimum = -100;
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(188, 359);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // ContrastFilterChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.panel1);
            this.Name = "ContrastFilterChanger";
            this.Size = new System.Drawing.Size(188, 421);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonGrayscale;
        private System.Windows.Forms.RadioButton radioButtonColor;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}
