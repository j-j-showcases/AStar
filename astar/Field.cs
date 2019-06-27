using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astar
{
    public class Field
    {
        #region variables
        private readonly int _width, _height;
        private static readonly Node[] _nodeDirections = new[]
        {
            new Node(){Row = -1, Col = -1}, //NW
            new Node(){Row = -1, Col = 0},  //N
            new Node(){Row = -1, Col = 1},  //NE
            new Node(){Row = 0, Col = -1},  //W
            new Node(){Row = 0, Col = 1},   //E
            new Node(){Row = 1, Col = -1},  //SW
            new Node(){Row = 1, Col = 0},   //S
            new Node(){Row = 1, Col = 1},   //SE
        };
        #endregion

        #region properties
        public List<Node> Nodes { get; }
        #endregion

        #region constructors
        public Field(int width, int height)
        {
            _width = width;
            _height = height;
            Nodes = new List<Node>();
        }
        #endregion

        #region private functions
        #endregion

        #region public functions
        /// <summary>
        /// Generate nodes with given percentage to be solid
        /// </summary>
        /// <param name="blockedPercentage">percentage of nodes to be solid</param>
        /// <returns>destination node</returns>
        public Node GenerateNodes(int blockedPercentage)
        {
            var rand = new Random();
            Nodes.Clear();

            //NOTE: first + last node (begin + destination) should always be non-solid
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                { 
                    Nodes.Add(new Node()
                    {
                        Solid = (x > 0 && y > 0) && (x < (_width - 1) && y < (_height - 1)) && rand.Next(100) <= blockedPercentage,
                        Col = x,
                        Row = y
                    });
                }
            }

            return Nodes.Last();
        }

        //NOTE: could also be stored in node itself
        /// <summary>
        /// Get neigbors of specific node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<Node> Neighbors(Node node)
        {
            var neighbors = new List<Node>();
            int index = Nodes.IndexOf(node);
            int col = index % _width;
            int row = index - (col * _width);

            foreach (var direction in _nodeDirections)
            {
                if (row + direction.Row >= 0 && row + direction.Row < _height - 1 && col + direction.Col >= 0 && col + direction.Col < _width - 1)
                {
                    int neighborCol = col + direction.Col;
                    int neighborRow = row + direction.Row;
                    var neighbor = Nodes[neighborCol + (neighborRow * _height)];

                    if (!neighbor.Solid)
                        neighbors.Add(neighbor);
                }
            }

            return neighbors;
        }

        /// <summary>
        /// Draw nodes in 2d grid view
        /// Path is a list of nodes which are the ones used to find the shortest route.
        /// </summary>
        /// <param name="path">found path</param>
        public void DrawField(List<Node> path)
        {
            int col = 0;
            int row = 0;
            
            // Loop extra for first horizontal wall
            for (int i = 0; i < _width + 2; i++)
                Console.Write('+');

            Console.WriteLine();
            foreach (var node in Nodes)
            {
                // Initialsing some variables.
                char text = node.Solid ? 'O' : 'X';
                ConsoleColor consoleColor;

                if (path.Contains(node))
                    consoleColor = ConsoleColor.Green;
                else if (node.Visited)
                    consoleColor = ConsoleColor.Red;
                else
                    consoleColor = ConsoleColor.White;

                Field.ResetForegroundColor();
                if (col == 0)
                    Console.Write('+');

                Field.SetForegroundColor(consoleColor);
                Console.Write(text);

                Console.ForegroundColor = ConsoleColor.White;
                if (col == _width - 1)
                {
                    // Draws the outside walls
                    Console.Write('+');
                    Console.WriteLine();
                    col = 0;
                    row++;
                }
                else
                {
                    col++;
                }
            }
            // Reset foreground
            Field.ResetForegroundColor();

            // Loop extra for last wall horizontally
            for (int i = 0; i < _width + 2; i++)
                // Draws the outside walls
                Console.Write('+');
        }

        public static void SetForegroundColor(System.ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
        }

        public static void ResetForegroundColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion
    }
}
