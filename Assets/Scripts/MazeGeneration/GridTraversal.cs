using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;

namespace SunYinchu.Lab2
{
    /// Utility class to traverse a grid.
    public class GridTraversal<T>
    {

        private readonly IGridGraph<T> grid;    // The grid to traverse.
        private readonly HashSet<(int, int)> processedCells = new HashSet<(int, int)>(); //Hashset to keep track of processed cells.
        private readonly List<((int Row, int Column) From, (int Row, int Column) To)> walls = new List<((int Row, int Column) From, (int Row, int Column) To)>();   // List to keep track of the walls.
        private readonly Random random = new Random();  // Random object to generate random numbers.

        /// Constructor
        public GridTraversal(IGridGraph<T> grid)
        {
            this.grid = grid;
        }

        /// <summary>
        /// Process a single cell.
        /// </summary>
        /// <param name="cell"> The cell to be processed. </param>
        private void ProcessCell((int Row, int Column) cell)
        {
            processedCells.Add(cell);
            foreach (var neighbor in grid.Neighbors(cell.Row, cell.Column))
            {
                if (processedCells.Contains(neighbor)) continue;
                walls.Add((cell, neighbor));
            }
        }

         /// <summary>
         /// Get a wall from the list.
         /// </summary>
         /// <returns> Randomly selected wall. </returns>
        private ((int Row, int Column) From, (int Row, int Column) To) GetRandomWall()
        {
            int randomIndex = random.Next(0, walls.Count);
            var wall = walls[randomIndex];
            walls.RemoveAt(randomIndex);
            return wall;
        }

        /// <summary>
        /// Generate the maze.
        /// </summary>
        /// <param name="startRow"> The starting row of the maze. </param>
        /// <param name="startColumn"> The starting column of the maze.</param>
        /// <returns></returns>
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
