using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Components.Graph
{
    public class NodeInput
    {
        private readonly HashSet<NodeOutput> _connectedOutputs = new();

        public NodeInput(string name, GraphNode owner) {
            Name = name;
            Owner = owner;
        }

        public string Name { get; }
        public GraphNode Owner { get; }
        public IEnumerable<NodeOutput> ConnectedOutputs => _connectedOutputs.ToList();

        public void ConnectTo(NodeOutput output) {
            _connectedOutputs.Add(output);
            if (!output.ConnectedInputs.Contains(this)) {
                output.ConnectTo(this);
            }
        }

        public void Disconnect(NodeOutput output) {
            if (_connectedOutputs.Contains(output)) {
                _connectedOutputs.Remove(output);                
            }

            if (output.ConnectedInputs.Contains(this)) {
                output.Disconnect(this);
            }
        }
    }
}
