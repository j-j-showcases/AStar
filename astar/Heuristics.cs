using System;

namespace Algorithms
{
    public static class Heuristics
    {
        /// <summary>
        /// Calculates the Manhattan distance between the two points.
        /// </summary>
        /// <param name="x1">The first x coordinate.</param>
        /// <param name="x2">The second x coordinate.</param>
        /// <param name="y1">The first y coordinate.</param>
        /// <param name="y2">The second y coordinate.</param>
        /// <returns>The Manhattan distance between (x1, y1) and (x2, y2)</returns>
        public static double ManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        /// <summary>
        /// Calculates the EuclideanDistance distance between the two points.
        /// </summary>
        /// <param name="x1">The first x coordinate.</param>
        /// <param name="x2">The second x coordinate.</param>
        /// <param name="y1">The first y coordinate.</param>
        /// <param name="y2">The second y coordinate.</param>
        /// <returns>The Manhattan distance between (x1, y1) and (x2, y2)</returns>
        public static double EuclideanDistance(int x1, int x2, int y1, int y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }

        /// <summary>
        /// Calculates the ChebyshevDistance distance between the two points.
        /// </summary>
        /// <param name="x1">The first x coordinate.</param>
        /// <param name="x2">The second x coordinate.</param>
        /// <param name="y1">The first y coordinate.</param>
        /// <param name="y2">The second y coordinate.</param>
        /// <returns>The Manhattan distance between (x1, y1) and (x2, y2)</returns>
        public static double ChebyshevDistance(int x1, int x2, int y1, int y2)
        {
            var dx = Math.Abs(x2 - x1);
            var dy = Math.Abs(y2 - y1);
            return (dx + dy) - Math.Min(dx, dy);
        }
    }
}
