namespace CoalTrainMonitoringSystemServer
{
    partial class FormDisplay
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.DoubleBuffered = true;
            this.Name = "FormDisplay";
            this.Text = "FormInfraredImage";
            this.Load += new System.EventHandler(this.FormInfraredImage_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormInfraredImage_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormInfraredImage_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}