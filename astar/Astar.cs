using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astar
{
    public class Astar
    {
        private readonly Field _field;
        private readonly Node _destination;
        private readonly Node _startNode;

        public List<Node> Path { get; } = new List<Node>();

        public Astar(Field field, Node destination)
        {
            _field = field;
            _destination = destination;
            _startNode = _field.Nodes[0];
        }

        //NOTE: could alse be put into node itself --> and called on 
        private double Heuristic(Node node)
        {
            ////NOTE: Chebyshev distance
            const int d = 1;
            //const int d2 = 1;

            //double dx = Math.Abs(node.Col - _destination.Col);
            //double dy = Math.Abs(node.Row - _destination.Row);
            //return (d * (dx + dy)) + ((d2 - (2 * d)) * Math.Min(dx, dy));

            //NOTE: Manhatten Distance
            double dx = Math.Abs(node.Col - _destination.Col);
            double dy = Math.Abs(node.Row - _destination.Row);
            return d * (dx + dy);
        }

        //NOTE: this doesn't implement the heuristic yet!
        /// <summary>
        /// Find path on set node field form 1st node till set destination
        /// </summary>
        /// <returns>Wheter a path is found or not</returns>
        public bool FindPath()
        {
            //NOTE: openNodes should be priority queuue
            PriorityQueue<Node, double> openNodes = new PriorityQueue<Node, double>();
            openNodes.Enqueue(_startNode, 0);
            List<Node> closedNodes = new List<Node>();
            Dictionary<Node, double> costSoFar = new Dictionary<Node, double>
            {
                [_startNode] = 0
            };
            Dictionary<Node, Node> prevNode = new Dictionary<Node, Node>();


            while (openNodes.Count > 0)
            {
                var current = openNodes.Dequeue();
                closedNodes.Add(current);
                current.Visited = true;

                if (current == _destination)
                {
                    Path.Add(_startNode);
                    Path.Add(_destination);
                    Node tempNode = _destination;
                    for(int i = prevNode.Count - 1; i >= 0; i--)
                    {
                        if (tempNode != _startNode)
                        {
                            tempNode = prevNode[tempNode];
                            Path.Add(tempNode);
                        }
                    }
                    return true;
                }

                foreach (var neighbor in _field.Neighbors(current))
                {
                    double tempCost = costSoFar[current] + 1;

                    if (costSoFar.ContainsKey(neighbor) && tempCost < costSoFar[neighbor])
                        costSoFar[neighbor] = tempCost;
                    if (closedNodes.Contains(neighbor) && tempCost < costSoFar[neighbor])
                        closedNodes.Remove(neighbor);
                    if (!costSoFar.ContainsKey(neighbor) && !closedNodes.Contains(neighbor))
                    {
                        costSoFar[neighbor] = tempCost;
                        openNodes.Enqueue(neighbor, tempCost + Heuristic(neighbor));

                        prevNode[neighbor] = current;
                    }
                }
            }

            return false;
        }
    }
}
