using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneGame
{
    public class Edge
    {
        public Node To { get; set; } // Nodo destino
        public int Weight { get; set; } // Peso de la conexión

        public Edge(Node to, int weight)
        {
            To = to;
            Weight = weight;
        }
    }
}
