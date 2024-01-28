namespace Grapher
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.nodeConfigurationTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.extraInfoTooltip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nodeConfigurationTextBox
            // 
            this.nodeConfigurationTextBox.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nodeConfigurationTextBox.Location = new System.Drawing.Point(12, 78);
            this.nodeConfigurationTextBox.Name = "nodeConfigurationTextBox";
            this.nodeConfigurationTextBox.Size = new System.Drawing.Size(333, 228);
            this.nodeConfigurationTextBox.TabIndex = 1;
            this.nodeConfigurationTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Configuration values:";
            // 
            // extraInfoTooltip
            // 
            this.extraInfoTooltip.AutoSize = true;
            this.extraInfoTooltip.Location = new System.Drawing.Point(12, 650);
            this.extraInfoTooltip.Name = "extraInfoTooltip";
            this.extraInfoTooltip.Size = new System.Drawing.Size(88, 15);
            this.extraInfoTooltip.TabIndex = 3;
            this.extraInfoTooltip.Text = "Hovering over: ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 683);
            this.Controls.Add(this.extraInfoTooltip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nodeConfigurationTextBox);
            this.Name = "MainForm";
            this.Text = "Grapher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox nodeConfigurationTextBox;
        private Label label1;
        private Label extraInfoTooltip;
    }
}