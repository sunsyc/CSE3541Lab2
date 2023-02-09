using System.Collections.Generic;
using System.Linq;

namespace ShareefSoftware
{
    /// Utility class to traverse a grid.
    public class GridTraversal<T>
    {

        private readonly IGridGraph<T> grid;
        private readonly List<(int Row, int Column)> frontier;
        private readonly HashSet<(int Row, int Column)> visited;

        /// Constructor
        public GridTraversal(IGridGraph<T> grid)
        {
            this.grid = grid;
            this.frontier = new List<(int Row, int Column)>();
            this.visited = new HashSet<(int Row, int Column)>();
        }

        /*
         * Replace this with your documentation
         * 
         * DO NOT change the method signature
         * Define helper methods as 'private'
         */
        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> GenerateMaze(int startRow, int startColumn)
        {
            var start = (startRow, startColumn);
            frontier.Add(start);
            visited.Add(start);

            while (frontier.Count > 0)
            {
                var current = frontier[0];
                frontier.RemoveAt(0);

                foreach (var neighbor in GetUnvisitedNeighbors(current))
                {
                    visited.Add(neighbor);
                    frontier.Add(neighbor);
                    yield return (current, neighbor);
                }
            }
        }

        private IEnumerable<(int Row, int Column)> GetUnvisitedNeighbors((int Row, int Column) current)
        {
            var (row, col) = current;
            var neighbors = new List<(int, int)>
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            return neighbors.Where(neighbor =>
                grid.IsValidNode(neighbor.Item1, neighbor.Item2) && !visited.Contains(neighbor));
        }
    }
}