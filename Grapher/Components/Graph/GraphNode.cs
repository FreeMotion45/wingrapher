using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Components.Graph
{
    public class GraphNode
    {
        private readonly List<NodeInput> _inputs = new();
        private readonly List<NodeOutput> _outputs = new();
        private readonly List<string> _configurableValues = new();

        public GraphNode(string name, IEnumerable<string> inputNames, IEnumerable<string> outputNames, IEnumerable<string> configurationValues) {
            _inputs = inputNames.Select(name => new NodeInput(name, this)).ToList();
            _outputs = outputNames.Select(name => new NodeOutput(name, this)).ToList();
            _configurableValues = configurationValues.ToList();
            Name = name;
        }

        public Dictionary<string, NodeInput> Inputs => _inputs.ToDictionary((input) => input.Name);
        public Dictionary<string, NodeOutput> Outputs => _outputs.ToDictionary((output) => output.Name);
        public IEnumerable<string> ConfigurableValues => _configurableValues.ToArray();
        public string Name { get; }
    }
}
