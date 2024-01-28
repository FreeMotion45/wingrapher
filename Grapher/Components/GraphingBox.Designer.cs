namespace Grapher.Components
{
    partial class GraphingBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // GraphingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.Name = "GraphingBox";
            this.Size = new System.Drawing.Size(694, 403);
            this.Click += new System.EventHandler(this.GraphingBox_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraphingBox_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GraphingBox_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GraphingBox_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GraphingBox_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
