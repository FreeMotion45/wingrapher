using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Components
{
    class UIGraphNode
    {
        private readonly static Size INPUT_REC_SIZE = new(30, 30);
        private int IONodeRadius = INPUT_REC_SIZE.Width / 2;

        private readonly List<Rectangle> _inputRects = new();
        private readonly List<Rectangle> _outputRects = new();
        private readonly List<string> _inputNames = new();
        private readonly List<string> _outputNames = new();        

        public UIGraphNode(string name, Rectangle nodeRectangle, IEnumerable<string> outputNames, IEnumerable<string> inputNames) {
            NodeRectangle = nodeRectangle;

            _inputNames = inputNames.ToList();
            _outputNames = outputNames.ToList();

            for (int i = 0; i < _inputNames.Count; i++) {
                int x = nodeRectangle.X - INPUT_REC_SIZE.Width - 2;
                int y = nodeRectangle.Y + i * INPUT_REC_SIZE.Height;
                _inputRects.Add(new Rectangle(new Point(x, y), INPUT_REC_SIZE));
            }

            for (int i = 0; i < _outputNames.Count; i++) {
                int x = nodeRectangle.X + nodeRectangle.Width + 1;
                int y = nodeRectangle.Y + i * INPUT_REC_SIZE.Height;
                _outputRects.Add(new Rectangle(new Point(x, y), INPUT_REC_SIZE));
            }


            Name = name;
        }

        public Rectangle NodeRectangle { get; private set; }
        public IEnumerable<Rectangle> NodeInputRectangles => _inputRects.ToArray();
        public Dictionary<string, Rectangle> InputMapping => _inputNames.Zip(_inputRects).ToDictionary((tup) => tup.First, (tup) => tup.Second);
        public IEnumerable<Rectangle> NodeOutputRectangles => _outputRects.ToArray();
        public Dictionary<string, Rectangle> OutputMapping => _outputNames.Zip(_outputRects).ToDictionary((tup) => tup.First, (tup) => tup.Second);

        public Size Size { get; }
        public string Name { get; }
        public Image? NodeImage { get; set; }

        public void MoveByDelta(Point delta) {
            NodeRectangle = NodeRectangle.MoveByDelta(delta);
            CalculateIORectangles();
        }

        public void MoveToPosition(Point position) {
            NodeRectangle = new Rectangle(position.X, position.Y, NodeRectangle.Width, NodeRectangle.Height);
            CalculateIORectangles();
        }

        public void Draw(Graphics graphics) {
            if (NodeImage != null) {
                graphics.DrawImage(NodeImage, NodeRectangle);
            } else {
                graphics.FillRectangle(Brushes.MediumOrchid, NodeRectangle);
            }

            foreach (var inputRect in _inputRects) {
                graphics.FillEllipse(Brushes.LawnGreen, inputRect);
            }

            foreach (var outputRect in _outputRects) {
                graphics.FillEllipse(Brushes.LawnGreen, outputRect);
            }
        }

        private void CalculateIORectangles() {
            _inputRects.Clear();
            _outputRects.Clear();

            for (int i = 0; i < _inputNames.Count; i++) {
                int x = NodeRectangle.X - INPUT_REC_SIZE.Width - 2;
                int y = NodeRectangle.Y + i * INPUT_REC_SIZE.Height;
                _inputRects.Add(new Rectangle(new Point(x, y), INPUT_REC_SIZE));
            }

            for (int i = 0; i < _outputNames.Count; i++) {
                int x = NodeRectangle.X + NodeRectangle.Width + 1;
                int y = NodeRectangle.Y + i * INPUT_REC_SIZE.Height;
                _outputRects.Add(new Rectangle(new Point(x, y), INPUT_REC_SIZE));
            }
        }
    }
}
