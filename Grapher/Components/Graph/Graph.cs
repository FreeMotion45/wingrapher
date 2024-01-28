using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Components.Graph
{
    internal class Graph : IEnumerable<GraphNode>, IEnumerator<GraphNode>
    {
        private readonly Dictionary<string, GraphNode> _graph = new();

        private IEnumerator _graphValuesEnumerator;

        public GraphNode GetNodeByName(string name) {
            return _graph[name];
        }

        public void CreateNode(GraphNode node) {
            foreach (NodeOutput output in node.Outputs.Values) {
                if (output.ConnectedInputs.Count() > 0) {
                    throw new Exception("cant add node with connected inputs");
                }
            }

            foreach (NodeInput input in node.Inputs.Values) {
                if (input.ConnectedOutputs.Count() > 0) {
                    throw new Exception("cant add node with connected outputs");
                }
            }

            _graph[node.Name] = node;
        }

        public void Connect(GraphNode node, string output, GraphNode dest, string input) {
            _graph[node.Name].Outputs[output].ConnectTo(_graph[dest.Name].Inputs[input]);
        }

        public void Disconnect(GraphNode node, string output, GraphNode dest, string input) {
            _graph[node.Name].Outputs[output].Disconnect(_graph[dest.Name].Inputs[input]);
        }

        public void Remove(GraphNode nodeToRemove) {
            if (!_graph.ContainsKey(nodeToRemove.Name)) {
                return;
            }

            GraphNode node = _graph[nodeToRemove.Name];
            _graph.Remove(node.Name);

            foreach (GraphNode otherNode in _graph.Values) {
                foreach (NodeInput otherInputNode in otherNode.Inputs.Values) {
                    foreach (NodeOutput deletedOutputNode in node.Outputs.Values) {
                        if (otherInputNode.ConnectedOutputs.Contains(deletedOutputNode)) {
                            otherInputNode.Disconnect(deletedOutputNode);
                        }
                    }
                }

                foreach (NodeOutput otherOutputNode in otherNode.Outputs.Values) {
                    foreach (NodeInput deletedInputNode in node.Inputs.Values) {
                        if (otherOutputNode.ConnectedInputs.Contains(deletedInputNode)) {
                            otherOutputNode.Disconnect(deletedInputNode);
                        }
                    }
                }
            }
        }

        public void FindRoots() {

        }

        #region IEnumerator/IEnumerable implementation
        public GraphNode Current => (GraphNode)_graphValuesEnumerator.Current;

        object IEnumerator.Current => _graphValuesEnumerator.Current;

        public bool MoveNext() {
            if (_graphValuesEnumerator == null) {
                _graphValuesEnumerator = _graph.Values.GetEnumerator();
            }

            if (!_graphValuesEnumerator.MoveNext()) {
                _graphValuesEnumerator = null;
                return false;
            };

            return true;
        }

        public void Reset() {
            _graphValuesEnumerator = null;
        }

        public void Dispose() {
            Reset();
        }

        public IEnumerator<GraphNode> GetEnumerator() {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this;
        }
        #endregion
    }
}
