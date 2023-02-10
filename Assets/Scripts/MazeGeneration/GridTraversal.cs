using System;
using System.Collections.Generic;

namespace SunYinchu.Lab2
{
    /// Utility class to traverse a grid.
    public class GridTraversal<T>
    {

        private readonly IGridGraph<T> grid;
        private readonly HashSet<(int, int)> processedCells = new HashSet<(int, int)>();
        private readonly List<((int Row, int Column) From, (int Row, int Column) To)> walls = new List<((int Row, int Column) From, (int Row, int Column) To)>();
        private readonly Random random = new Random();

        /// Constructor
        public GridTraversal(IGridGraph<T> grid)
        {
            this.grid = grid;
        }

        private void ProcessCell((int Row, int Column) cell)
        {
            processedCells.Add(cell);
            foreach (var neighbor in grid.Neighbors(cell.Row, cell.Column))
            {
                if (processedCells.Contains(neighbor)) continue;
                walls.Add((cell, neighbor));
            }
        }

        private ((int Row, int Column) From, (int Row, int Column) To) GetRandomWall()
        {
            int randomIndex = random.Next(0, walls.Count);
            var wall = walls[randomIndex];
            walls.RemoveAt(randomIndex);
            return wall;
        }

        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> GenerateMaze(int startRow, int startColumn)
        {
            processedCells.Clear();
            walls.Clear();

            var startCell = (startRow, startColumn);
            ProcessCell(startCell);
            while (processedCells.Count < grid.NumberOfRows * grid.NumberOfColumns)
            {
                var wall = GetRandomWall();
                var neighbor = processedCells.Contains(wall.From) ? wall.To : wall.From;
                if (!processedCells.Contains(neighbor))
                {
                    yield return wall;
                    ProcessCell(neighbor);
                }
            }
        }
    }
}
