using Grapher.Components.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grapher.Components
{
    public partial class NodeSearchForm : Form
    {
        private readonly List<string> _inputs = new();
        private readonly List<string> _outputs = new();
        private readonly List<string> _configValues = new();

        public NodeSearchForm() {
            InitializeComponent();
        }

        public GraphNode Node { get; private set; }
        public string NodeName { get; private set; }
        public Image? NodeImage { get; private set; }
        public bool Ok { get; private set; }

        private void button1_Click(object sender, EventArgs e) {
            NodeName = textBox.Text;
            Node = new(NodeName, _inputs.ToArray(), _outputs.ToArray(), _configValues.ToArray());
            Ok = true;
            Close();
        }

        private void inputsListBox_Click(object sender, EventArgs e) {
            string inputName = (inputsListBox.Items.Count + 1).ToString();
            inputsListBox.Items.Add(inputName);
            _inputs.Add(inputName);
        }

        private void outputsListBox_Click(object sender, EventArgs e) {
            string outputName = (outputsListBox.Items.Count + 1).ToString();
            outputsListBox.Items.Add(outputName);
            _outputs.Add(outputName);
        }

        private void configurationValuesListBox_Click(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(textBox.Text)) {
                _configValues.Add(textBox.Text);
                configurationValuesListBox.Items.Add(textBox.Text);
                textBox.Text = "";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            using OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Images|*.jpg;*.png";

            if (fd.ShowDialog() == DialogResult.OK) {
                NodeImage = Image.FromFile(fd.FileName);
                pictureBox1.Image = NodeImage;
            }
        }
    }
}
