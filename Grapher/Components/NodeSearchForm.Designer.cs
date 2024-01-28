namespace Grapher.Components
{
    partial class NodeSearchForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.textBox = new System.Windows.Forms.TextBox();
            this.inputsListBox = new System.Windows.Forms.ListBox();
            this.outputsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.configurationValuesListBox = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(124, 58);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(100, 23);
            this.textBox.TabIndex = 0;
            // 
            // inputsListBox
            // 
            this.inputsListBox.FormattingEnabled = true;
            this.inputsListBox.ItemHeight = 15;
            this.inputsListBox.Location = new System.Drawing.Point(28, 97);
            this.inputsListBox.Name = "inputsListBox";
            this.inputsListBox.Size = new System.Drawing.Size(120, 94);
            this.inputsListBox.TabIndex = 1;
            this.inputsListBox.Click += new System.EventHandler(this.inputsListBox_Click);
            // 
            // outputsListBox
            // 
            this.outputsListBox.FormattingEnabled = true;
            this.outputsListBox.ItemHeight = 15;
            this.outputsListBox.Location = new System.Drawing.Point(181, 97);
            this.outputsListBox.Name = "outputsListBox";
            this.outputsListBox.Size = new System.Drawing.Size(120, 94);
            this.outputsListBox.TabIndex = 2;
            this.outputsListBox.Click += new System.EventHandler(this.outputsListBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Text:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(124, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // configurationValuesListBox
            // 
            this.configurationValuesListBox.FormattingEnabled = true;
            this.configurationValuesListBox.ItemHeight = 15;
            this.configurationValuesListBox.Location = new System.Drawing.Point(349, 97);
            this.configurationValuesListBox.Name = "configurationValuesListBox";
            this.configurationValuesListBox.Size = new System.Drawing.Size(120, 94);
            this.configurationValuesListBox.TabIndex = 5;
            this.configurationValuesListBox.Click += new System.EventHandler(this.configurationValuesListBox_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(520, 106);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(112, 85);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // NodeSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 264);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.configurationValuesListBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputsListBox);
            this.Controls.Add(this.inputsListBox);
            this.Controls.Add(this.textBox);
            this.Name = "NodeSearchForm";
            this.Text = "NodeSearchForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox;
        private ListBox inputsListBox;
        private ListBox outputsListBox;
        private Label label1;
        private Button button1;
        private ListBox configurationValuesListBox;
        private PictureBox pictureBox1;
    }
}