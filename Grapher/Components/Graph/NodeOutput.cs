using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Components.Graph
{
    public class NodeOutput
    {
        private readonly HashSet<NodeInput> _connectedInputs = new();

        public NodeOutput(string name, GraphNode owner) {
            Name = name;
            Owner = owner;
        }

        public string Name { get; }
        public GraphNode Owner { get; }
        public IEnumerable<NodeInput> ConnectedInputs => _connectedInputs.ToList();

        public void ConnectTo(NodeInput input) {
            _connectedInputs.Add(input);

            if (!input.ConnectedOutputs.Contains(this)) {
                input.ConnectTo(this);
            }
        }

        public void Disconnect(NodeInput input) {
            if (_connectedInputs.Contains(input)) {
                _connectedInputs.Remove(input);
            }

            if (input.ConnectedOutputs.Contains(this)) {
                input.Disconnect(this);
            }
        }
    }
}
