using Grapher.Components.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grapher.Components
{
    public partial class GraphingBox : UserControl
    {
        private readonly static Size NODE_REC_SIZE = new(70, 70);

        // private readonly Dictionary<string, GraphNode> _graph = new();
        private readonly Graph.Graph _graph = new();
        private readonly List<UIGraphNode> _visualNodes = new();

        private Tuple<UIGraphNode, string, UIGraphNode, string>? _selectedConnection;
        private UIGraphNode? _selectedNode;
        private string? _selectedOutput;

        private Image? _imageOfNodeToPlace;
        private string? _nameOfNodeToPlace;
        private GraphNode? _nodeToPlaceOnNextClick;

        private UIGraphNode? _nodeToDrag;
        private Point _nodeDragStartPosition;
        private Point _mouseDragStart;

        enum Mode { Default, SelectedOutput }
        private Mode _mode;

        public event Action<GraphNode> OnFocusedNode;
        public event Action OnUnfocusedNode;
        public event Action<object> OnMouseHovering;

        public GraphingBox() {
            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e) {
            // Render graph.
            Graphics graphics = e.Graphics;

            graphics.Clear(BackColor);

            foreach (var node in _visualNodes) {
                node.Draw(graphics);

                Font font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
                SizeF stringSize = graphics.MeasureString(node.Name, font);

                float drawPointX = node.NodeRectangle.Location.X + node.NodeRectangle.Width / 2 - stringSize.Width / 2;
                float drawPointY = node.NodeRectangle.Location.Y - font.Height;
                graphics.DrawString(node.Name, font, Brushes.Black, drawPointX, drawPointY);
            }

            foreach (GraphNode graphNode in _graph) {
                foreach (KeyValuePair<string, NodeOutput> outputNameAndNode in graphNode.Outputs) {
                    foreach (NodeInput connectedInput in outputNameAndNode.Value.ConnectedInputs) {
                        UIGraphNode outputNodeOwner = VisualNodeByName(graphNode.Name);
                        UIGraphNode inputNodeOwner = VisualNodeByName(connectedInput.Owner.Name);

                        DrawOutputToInputConnection(graphics, outputNodeOwner, outputNameAndNode.Key, inputNodeOwner, connectedInput.Name);
                    }
                }
            }
            // End render graph.

            if (_selectedNode != null) {
                int x = _selectedNode.NodeRectangle.Left - 30;
                int y = _selectedNode.NodeRectangle.Top - 5;
                int width = _selectedNode.NodeRectangle.Width + 60;
                int height = _selectedNode.NodeRectangle.Height + 10;

                using Pen p = new Pen(Brushes.Black, 2);
                graphics.DrawRectangle(p, new Rectangle(x, y, width, height));
            }
        }

        public void RerenderGraph() {
            Invalidate();
        }

        private void DrawOutputToInputConnection(Graphics graphics, UIGraphNode source, string sourceOutput, UIGraphNode dest, string destInput) {
            Point start = source.OutputMapping[sourceOutput].Center();
            Point end = dest.InputMapping[destInput].Center();

            Vector2 lineEnd;

            // Draw a little arrow at the tip of the line, lol.
            Vector2 endVec = new(end.X, end.Y);
            Vector2 direction = Vector2.Normalize(endVec - new Vector2(start.X, start.Y));

            Vector2 rotatedDir1 = Vector2.Transform(-direction, Matrix3x2.CreateRotation((float)(40f * Math.PI / 180)));
            Vector2 rotatedDir2 = Vector2.Transform(-direction, Matrix3x2.CreateRotation((float)(-40f * Math.PI / 180)));

            Vector2 polygonEdge1 = endVec + rotatedDir1 * 15;
            Vector2 polygonEdge2 = endVec + rotatedDir2 * 15;

            lineEnd = endVec - Vector2.Normalize(direction) * 5;
            Point lineEndPoint = new Point((int)lineEnd.X, (int)lineEnd.Y);

            bool isSelectedConnection = false;
            if (_selectedConnection != null) {
                if (source == _selectedConnection.Item1 && sourceOutput == _selectedConnection.Item2) {
                    if (dest == _selectedConnection.Item3 && destInput == _selectedConnection.Item4) {
                        isSelectedConnection = true;
                    }
                }
            }

            Point[] arrowTipPolygonPoints = new Point[] {
                    end, new Point((int)polygonEdge1.X, (int)polygonEdge1.Y), new Point((int)polygonEdge2.X, (int)polygonEdge2.Y) };

            Brush brush = isSelectedConnection ? Brushes.Red : Brushes.DeepSkyBlue;
            using Pen p = new Pen(brush, 6);   

            graphics.FillPolygon(brush, arrowTipPolygonPoints);

            // Finished drawing arrow, draw the line itself.
            graphics.DrawLine(p, start, lineEndPoint);
        }

        private void AddNewNode(string name, GraphNode node, Point location, Image? nodeImage) {
            _graph.CreateNode(node);

            IEnumerable<string> namesOfOutputs = node.Outputs.Keys;
            IEnumerable<string> namesOfInputs = node.Inputs.Keys;

            var visualNode = new UIGraphNode(name, new Rectangle(location, NODE_REC_SIZE), namesOfOutputs, namesOfInputs);
            visualNode.NodeImage = nodeImage;
            _visualNodes.Add(visualNode);

            RerenderGraph();
        }

        private void RemoveNode(string name) {
            _graph.Remove(_graph.GetNodeByName(name));

            _visualNodes.Remove(_visualNodes.Find(x => x.Name == name));
            RerenderGraph();
        }

        private void RemoveConnection(string sourceNodeName, string sourceOutputName, string destNodeName, string destNodeOutputName) {
            _graph.Disconnect(_graph.GetNodeByName(sourceNodeName), sourceOutputName, _graph.GetNodeByName(destNodeName), destNodeOutputName);
            RerenderGraph();
        }

        private void ConnectOutputToInput(UIGraphNode source, string sourceOutput, UIGraphNode dest, string destInput) {
            _graph.Connect(_graph.GetNodeByName(source.Name), sourceOutput, _graph.GetNodeByName(dest.Name), destInput);
            RerenderGraph();
        }

        private void SetFocusOnNode(UIGraphNode? node) {
            _selectedNode = node;

            if (node != null) {
                OnFocusedNode?.Invoke(GetGraphNodeOfVisualNode(node));
            }
            else {
                OnUnfocusedNode?.Invoke();
            }

            RerenderGraph();
        }

        private void ResetFocus() {
            _selectedNode = null;
            OnUnfocusedNode?.Invoke();
            RerenderGraph();
        }

        private void GraphingBox_Click(object sender, EventArgs e) {
            MouseEventArgs ev = (MouseEventArgs)e;

            if (_nodeToPlaceOnNextClick != null && _nameOfNodeToPlace != null) {
                Point topLeft = new(ev.X - NODE_REC_SIZE.Width / 2, ev.Y - NODE_REC_SIZE.Height / 2);
                AddNewNode(_nameOfNodeToPlace, _nodeToPlaceOnNextClick, topLeft, _imageOfNodeToPlace);

                // Instantly focus on the new node.
                SetFocusOnNode(VisualNodeByName(_nameOfNodeToPlace));

                _nameOfNodeToPlace = null;
                _nodeToPlaceOnNextClick = null;
                _imageOfNodeToPlace = null;

                return;
            }

            Tuple<UIGraphNode, string>? clickedOutput = GetOutputNodeAtPosition(ev.X, ev.Y);
            if (_mode == Mode.Default && clickedOutput != null) {
                _selectedNode = clickedOutput.Item1;
                _selectedOutput = clickedOutput.Item2;
                _mode = Mode.SelectedOutput;
                return;
            }

            Tuple<UIGraphNode, string>? clickedInput = GetInputNodeAtPosition(ev.X, ev.Y);
            if (_mode == Mode.SelectedOutput && clickedInput != null) {

                if (clickedInput.Item1 != _selectedNode) {
                    ConnectOutputToInput(_selectedNode, _selectedOutput, clickedInput.Item1, clickedInput.Item2);
                }

                _mode = Mode.Default;
                _selectedNode = null;
                _selectedOutput = null;
                return;
            }

            UIGraphNode? clickedNode = GetGraphNodeAtPosition(ev.X, ev.Y);
            if (_mode == Mode.Default) {
                if (clickedNode != null) {
                    SetFocusOnNode(clickedNode);
                }
                else {
                    ResetFocus();
                }
            }

            if (_mode == Mode.Default) {
                Tuple<UIGraphNode, string, UIGraphNode, string>? connection = GetConnectionAtPosition(ev.X, ev.Y);
                if (connection != null) {
                    _selectedConnection = connection;
                }
                else {
                    _selectedConnection = null;
                }
                RerenderGraph();
            }

            _mode = Mode.Default;
        }

        private UIGraphNode? GetGraphNodeAtPosition(int x, int y) {
            foreach (UIGraphNode node in _visualNodes) {
                if (node.NodeRectangle.Contains(x, y)) {
                    return node;
                }
            }
            return null;
        }

        private Tuple<UIGraphNode, string>? GetInputNodeAtPosition(int x, int y) {
            foreach (UIGraphNode node in _visualNodes) {
                foreach (KeyValuePair<string, Rectangle> input in node.InputMapping) {
                    if (input.Value.Contains(x, y)) {
                        return new(node, input.Key);
                    }
                }
            }
            return null;
        }

        private Tuple<UIGraphNode, string>? GetOutputNodeAtPosition(int x, int y) {
            foreach (UIGraphNode node in _visualNodes) {
                foreach (KeyValuePair<string, Rectangle> output in node.OutputMapping) {
                    if (output.Value.Contains(x, y)) {
                        return new(node, output.Key);
                    }
                }
            }
            return null;
        }

        private Tuple<UIGraphNode, string, UIGraphNode, string>? GetConnectionAtPosition(int x, int y) {
            Point pos = new(x, y);

            foreach (GraphNode node in _graph) {
                foreach (NodeOutput output in node.Outputs.Values) {
                    foreach (NodeInput connectedInput in output.ConnectedInputs) {
                        Point outputCenter = VisualNodeByName(node.Name).OutputMapping[output.Name].Center();
                        Point inputCenter = VisualNodeByName(connectedInput.Owner.Name).InputMapping[connectedInput.Name].Center();

                        // We don't really have any "line" here, so to check if this position is on the line we do the following:
                        // 1. Move along a virtual line from outputCenter to inputCenter in small steps.
                        // 2. Check if the current step is in some little distance from pos.

                        const int STEP_COUNT = 70;
                        const int MAX_RADIUS = 4;

                        for (int i = 0; i < STEP_COUNT; i++) {
                            double lerp = 1.0 * (i + 1) / STEP_COUNT;
                            Point p = outputCenter.Lerp(inputCenter, lerp);

                            if (p.Distance(pos) <= MAX_RADIUS) {
                                return new(VisualNodeByName(node.Name), output.Name, VisualNodeByName(connectedInput.Owner.Name), connectedInput.Name);
                            }
                        }
                    }
                }
            }

            return null;
        }

        private void GraphingBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F) {
                NodeSearchForm nodeSearchForm = new NodeSearchForm();
                nodeSearchForm.ShowDialog();

                if (nodeSearchForm.Ok) {
                    _nameOfNodeToPlace = nodeSearchForm.NodeName;
                    _nodeToPlaceOnNextClick = nodeSearchForm.Node;
                    _imageOfNodeToPlace = nodeSearchForm.NodeImage;
                }
                else {
                    _nameOfNodeToPlace = null;
                    _nodeToPlaceOnNextClick = null;
                }
            }
            else if (e.KeyCode == Keys.G) {
                Random random = new Random();
                _nameOfNodeToPlace = "Node_Bruh_" + random.Next(1000);

                IEnumerable<string> randomInputs = Enumerable.Range(1, random.Next(1, 4)).Select(x => x.ToString());
                IEnumerable<string> randomOutputs = Enumerable.Range(1, random.Next(1, 4)).Select(x => x.ToString());
                _nodeToPlaceOnNextClick = new GraphNode(_nameOfNodeToPlace, randomInputs, randomOutputs, new string[] { });
            }
            else if (e.KeyCode == Keys.Delete) {
                if (_selectedNode != null) {
                    RemoveNode(_selectedNode.Name);
                    SetFocusOnNode(null);
                }
                else if (_selectedConnection != null) {
                    RemoveConnection(_selectedConnection.Item1.Name, _selectedConnection.Item2, _selectedConnection.Item3.Name, _selectedConnection.Item4);
                    _selectedConnection = null;
                }
            }
        }

        private UIGraphNode? VisualNodeByName(string name) {
            return _visualNodes.Find(x => x.Name == name);
        }

        private GraphNode GetGraphNodeOfVisualNode(UIGraphNode node) {
            return _graph.GetNodeByName(node.Name);
        }

        private void GraphingBox_MouseDown(object sender, MouseEventArgs e) {
            UIGraphNode? node = GetGraphNodeAtPosition(e.X, e.Y);
            if (node != null) {
                _nodeToDrag = node;

                _nodeDragStartPosition = node.NodeRectangle.Location;
                _mouseDragStart = e.Location;

                SetFocusOnNode(node);
            }
        }

        private void GraphingBox_MouseUp(object sender, MouseEventArgs e) {
            if (_nodeToDrag != null) {
                _nodeToDrag = null;
            }
        }

        private void GraphingBox_MouseMove(object sender, MouseEventArgs e) {
            if (_nodeToDrag != null) {
                Point newPosition = _nodeDragStartPosition.Add(e.Location.Delta(_mouseDragStart));
                MoveUIGraphNode(_nodeToDrag, newPosition);
            }

            Tuple<UIGraphNode, string>? nodeAndName = GetInputNodeAtPosition(e.X, e.Y);
            if (nodeAndName != null) {
                OnMouseHovering?.Invoke(GetGraphNodeOfVisualNode(nodeAndName.Item1).Inputs[nodeAndName.Item2]);
                return;
            }

            nodeAndName = GetOutputNodeAtPosition(e.X, e.Y);
            if (nodeAndName != null) {
                OnMouseHovering?.Invoke(GetGraphNodeOfVisualNode(nodeAndName.Item1).Outputs[nodeAndName.Item2]);
                return;
            }

            UIGraphNode? node = GetGraphNodeAtPosition(e.X, e.Y);
            if (node != null) {
                OnMouseHovering?.Invoke(GetGraphNodeOfVisualNode(node));
                return;
            }

            OnMouseHovering?.Invoke(null);
        }

        private void MoveUIGraphNode(UIGraphNode node, Point position) {
            node.MoveToPosition(position);
            RerenderGraph();
        }
    }
}
