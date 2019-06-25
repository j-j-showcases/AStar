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

        public List<Node> Path { get; } = new List<Node>();

        public Astar(Field field, Node destination)
        {
            _field = field;
            _destination = destination;
        }

        //NOTE: could alse be put into node itself --> and called on 
        private double Heuristic(Node node)
        {
            //NOTE: Chebyshev distance
            const int d = 1;
            const int d2 = 1;

            double dx = Math.Abs(node.Row - _destination.Row);
            double dy = Math.Abs(node.Col - _destination.Col);
            return (d * (dx + dy)) + ((d2 - (2 * d)) * Math.Min(dx, dy));
        }

        //NOTE: this doesn't implement the heuristic yet!
        /// <summary>
        /// Find path on set node field form 1st node till set destination
        /// </summary>
        /// <returns>Wheter a path is found or not</returns>
        public bool FindPath()
        {
            //Create possible path list + add starting node (0, 0)
            Queue<Node> openNodes = new Queue<Node>();
            openNodes.Enqueue(_field.Nodes[0]);
            double cost = 0;

            while (openNodes.Count > 0)
            {
                var current = openNodes.Dequeue();
                current.Visited = true;

                if (current == _destination)
                {
                    Path.Add(current);
                    return true;
                }

                foreach(var neighbor in _field.Neighbors(current))
                {
                    double tempCost = cost + 1;

                    if(!Path.Contains(current) || tempCost < cost)
                    {
                        cost = tempCost;
                        openNodes.Enqueue(current);
                        Path.Add(current);
                    }
                }
            }

            return false;
        }
    }
}
