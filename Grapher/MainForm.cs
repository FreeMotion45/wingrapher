using Grapher.Components;
using Grapher.Components.Graph;

namespace Grapher
{
    public partial class MainForm : Form
    {
        private readonly GraphingBox _graphingBox;

        public MainForm() {
            InitializeComponent();

            Point loc = new Point(350, 10);
            Size fillSize = new(this.Size.Width - loc.X - 30, this.Size.Height - loc.Y - 50);
            _graphingBox = new GraphingBox();
            _graphingBox.Location = loc;
            _graphingBox.Size = fillSize;
            _graphingBox.TabIndex = 0;

            _graphingBox.OnFocusedNode += ShowNodeConfiguration;
            _graphingBox.OnUnfocusedNode += HideNodeConfiguration;

            _graphingBox.OnMouseHovering += ChangeHoverInfo;
            
            this.Controls.Add(_graphingBox);

            _graphingBox.Focus();
        }

        public void ShowNodeConfiguration(GraphNode node) {
            nodeConfigurationTextBox.Text = "";

            if (node.ConfigurableValues.Count() > 0) {
                foreach (string configurableValue in node.ConfigurableValues) {
                    nodeConfigurationTextBox.Text += configurableValue + ": <ENTER VALUE>" + Environment.NewLine;
                }
            }
            else {
                nodeConfigurationTextBox.Text = "This node has no configurable values.";
            }
        }

        public void HideNodeConfiguration() {
            nodeConfigurationTextBox.Text = "Select a node to view it's configurable values.";
        }

        public void ChangeHoverInfo(object obj) {
            if (obj == null) {
                extraInfoTooltip.Text = "";
            }
            else if (obj is NodeInput inp) {
                extraInfoTooltip.Text = $"Hovering over: INPUT `{inp.Name}` OF `{inp.Owner.Name}`";
            }
            else if (obj is NodeOutput outp) {
                extraInfoTooltip.Text = $"Hovering over: OUTPUT `{outp.Name}` OF `{outp.Owner.Name}`";
            }
            else if (obj is GraphNode node) {
                extraInfoTooltip.Text = $"Hovering over: NODE `{node.Name}`";
            } else {
                extraInfoTooltip.Text = "";
            }

        }
    }
}