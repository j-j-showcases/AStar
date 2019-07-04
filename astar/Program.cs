using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            bool askAgain = true;
            int row = 0;
            int col = 0;
            int wallPerc = 0;
            int indexAlgorithm = 0;

            // List of the available Algorithms
            String[] algorithmsList = {
                "ManhattanDistance",
                "EuclideanDistance",
                "ChebyshevDistance"
            };


            while (askAgain)
            {
                Console.WriteLine("Give row count:");
                if (int.TryParse(Console.ReadLine(), out row))
                    askAgain = false;
            }
            askAgain = true;
            while (askAgain)
            {
                Console.WriteLine("Give col count:");
                if (int.TryParse(Console.ReadLine(), out col))
                    askAgain = false;
            }
            askAgain = true;
            while (askAgain)
            {
                Console.WriteLine("Give wall percentage:");
                if (int.TryParse(Console.ReadLine(), out wallPerc) && wallPerc < 100)
                    askAgain = false;
            }
            askAgain = true;
            while (askAgain)
            {
                Console.WriteLine("Choose algorithm:");
                Console.WriteLine();
                for(int i = 1; i <= algorithmsList.Length; i++)
                {
                    Console.WriteLine(i + ". " + algorithmsList[i-1]);
                }

                if (int.TryParse(Console.ReadLine(), out indexAlgorithm) && indexAlgorithm <= algorithmsList.Length && indexAlgorithm > 0)
                    askAgain = false;
            }


            Field f = new Field(col, row);
            // Generate notes always returns the last node???
            // This way generate notes has more responsebilities thans just generating nodes.
            Node dest = f.GenerateNodes(wallPerc);

            // Initialise A* algorithm
            Astar astar = new Astar(f, dest);
            // Let A* do its job:
            astar.AStarSearch(algorithmsList[indexAlgorithm-1]);
            // Get the path that A* found
            List<Node> path = astar.FindPath();

            // Present the field with the path to the user.
            f.DrawField(path);

            Console.WriteLine();
            Console.WriteLine("Again? (y/n Default: n)");

            // Check if user would like to do it again.
            if (Console.ReadLine().ToUpper().Equals("Y"))
            {
                Main(args);
            }

            return;
        }
    }
}
