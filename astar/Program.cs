using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astar
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            bool askAgain = true;
            int row = 0;
            int col = 0;
            int wallPerc = 0;
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

            Field f = new Field(row, col);
            Node dest = f.GenerateNodes(wallPerc);

            Astar astar = new Astar(f, dest);
            if (astar.FindPath())
                Console.WriteLine("Solution found!");
            else
                Console.WriteLine("No solution found!");

            f.DrawField(astar.Path);
            Console.ReadLine();
        }
    }
}
