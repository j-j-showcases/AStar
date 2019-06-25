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
            for (int i = 0; i < _width * _height; i++)
                Nodes.Add(new Node()
                {
                    Solid = (i > 0 && i < (_width * _height) - 1) && rand.Next(100) <= blockedPercentage,
                    Row = i % _height,
                    Col = i - (i % _height)
                }) ;

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
            int row = index % _height;
            int col = (index - (row * _height));
            foreach (var direction in _nodeDirections)
            {
                if (row + direction.Row >= 0 && row + direction.Row < _height && col + direction.Col >= 0 && col + direction.Col < _width)
                {
                    int neighborRow = row + direction.Row;
                    int neighborCol = col + direction.Col;
                    neighbors.Add(Nodes[(neighborRow * _height) + neighborCol]);
                }
            }

            return neighbors;
        }

        /// <summary>
        /// Draw nodes in 2d grid view
        /// </summary>
        /// <param name="path">found path</param>
        public void DrawField(List<Node> path)
        {
            int row = 0;
            int col = 0;

            for (int i = 0; i < _width + 2; i++)
                Console.Write('#');
            Console.WriteLine();
            foreach (var node in Nodes)
            {
                char text = node.Solid ? 'O' : 'X';
                ConsoleColor consoleColor;
                if (node.Visited)
                    consoleColor = ConsoleColor.Red;
                else if (path.Contains(node))
                    consoleColor = ConsoleColor.Green;
                else
                    consoleColor = ConsoleColor.White;

                Console.ForegroundColor = ConsoleColor.White;
                if (col == 0)
                    Console.Write('.');

                Console.ForegroundColor = consoleColor;
                Console.Write(text);

                Console.ForegroundColor = ConsoleColor.White;
                if (col == _width - 1)
                {
                    Console.Write('.');
                    Console.WriteLine();
                    col = 0;
                    row++;
                }
                else
                {
                    col++;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < _width + 2; i++)
                Console.Write('#');
        }
        #endregion
    }
}
