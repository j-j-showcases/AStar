using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    //NOTE: should be struct --> this means everything should be ref else code wont work
    public class Node
    {
        public bool Visited { get; set; }
        public bool Solid { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }

        public static bool operator == (Node a, Node b)
        {
            return a.Row == b.Row && a.Col == b.Col;
        }
        public static bool operator != (Node a, Node b)
        {
            return a.Row != b.Row || a.Col != b.Col;
        }
    }
}
