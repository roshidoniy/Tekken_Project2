namespace Tekken_Project2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label helloLabel;

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
            this.helloLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.helloLabel.AutoSize = true;
            this.helloLabel.Location = new System.Drawing.Point(90, 90);
            this.helloLabel.Name = "helloLabel";
            this.helloLabel.Size = new System.Drawing.Size(100, 23);
            this.helloLabel.TabIndex = 0;
            this.helloLabel.Text = "Hello World";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.helloLabel);
            this.Name = "Form1";
            this.Text = "Tekken";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
