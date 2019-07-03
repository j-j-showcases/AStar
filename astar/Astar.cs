using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Astar
    {
        private readonly Field _field;
        private readonly Node goal;
        private readonly Node start;

        // Initialise variables
        Dictionary<Node, Node> prevNode = new Dictionary<Node, Node>();
        Dictionary<Node, double> costSoFar = new Dictionary<Node, double>();

        public List<Node> Path { get; } = new List<Node>();

        public Astar(Field field, Node destination)
        {
            _field = field;
            goal = destination;
            start = _field.Nodes[0];
        }

        /// <summary>
        /// Find path on set node field form 1st node till set destination
        /// </summary>
        public void AStarSearch(string algorithm)
        {
            // openNodis is a list of key-value pairs:
            // Node, (double) priority
            PriorityQueue<Node> openNodes = new PriorityQueue<Node>();

            // Add the starting Node to the frontier with a priority of 0
            openNodes.Enqueue(start, 0);

            prevNode.Add(start, start); // is set to start, None in example
            costSoFar.Add(start, 0f);

            while (openNodes.Count > 0)
            {
                // Get the node from the periorityQueue that has the lowest
                // priority, and remove it from the queue.
                Node current = openNodes.Dequeue();

                current.Visited = true;

                // If we're at the goal Node, stop looking.
                if (current == goal) break;

                foreach (var neighbor in _field.Neighbors(current))
                {

                    // +1 can be moved into the node.
                    // Only one becau
                    double newCost = costSoFar[current] + 1;

                    // If there's no cost assigned to the neighbor yet, or if the new
                    // cost is lower than the assigned one, add newCost for this neighbor
                    if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                    {
                        // If we're replacing the previous cost, remove it
                        if (costSoFar.ContainsKey(neighbor))
                        {
                            costSoFar.Remove(neighbor);
                            prevNode.Remove(neighbor);
                        }

                        costSoFar.Add(neighbor, newCost);
                        prevNode.Add(neighbor, current);
                        double priority = newCost + CalcutateHeuristics(neighbor, goal, algorithm);
                        openNodes.Enqueue(neighbor, priority);
                    }
                }
            }
        }

        /// <summary>
        /// Find path on set node field form 1st node till set destination
        /// </summary>
        /// <returns>Return a List of Nodes representing the found path</returns>
        public List<Node> FindPath()
        {

            List<Node> path = new List<Node>();
            Node current = goal;
            // path.Add(current);

            while (!current.Equals(start))
            {
                if (!prevNode.ContainsKey(current))
                {
                    Console.WriteLine("cameFrom does not contain current.");
                    return new List<Node>();
                }
                path.Add(current);
                current = prevNode[current];
            }
            // path.Add(start);
            path.Reverse();
            return path;
        }

        /// <summary>
        /// Calculates the heuristics by using diffrent algorithms.
        /// </summary>
        /// <returns>The value of the heuristics in integer</returns>
        public double CalcutateHeuristics(Node node, Node destination, string algorithm = "ManhattanDistance")
        {
            MethodInfo algorithmFunction = typeof(Heuristics).GetMethod(algorithm);

            if (algorithmFunction == null)
            {
                throw new System.ArgumentException("The given algorithm is not implemented.", "algorithm");
            }

            return (double)algorithmFunction.Invoke(null, new object[] { node.Col, destination.Col, node.Row, destination.Row });
        }
    }
}
