using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneGame
{
    public class Graph
    {
        public List<Node> Nodes { get; set; } = new List<Node>();
        public Dictionary<Node, List<Edge>> AdjacencyList { get; set; } = new Dictionary<Node, List<Edge>>();
        public void AddNode(Node node)
        {
            Nodes.Add(node);
            AdjacencyList[node] = new List<Edge>();
        }

        public void AddEdge(Node from, Node to, int weight)
        {
            AdjacencyList[from].Add(new Edge(to, weight));
        }
    }

}
