using System.Collections.Generic;

namespace ShareefSoftware
{
    /// Utility class to traverse a grid.
    public class GridTraversal<T>
    {

        private readonly IGridGraph<T> grid;

        /*
         * Replace this with your documentation
         * 
         * Define your instance variables here
         */

        /// Constructor
        public GridTraversal(IGridGraph<T> grid)
        {
            this.grid = grid;
        }

        /*
         * Replace this with your documentation
         * 
         * DO NOT change the method signature
         * Define helper methods as 'private'
         */
        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> GenerateMaze(int startRow, int startColumn)
        {
            /*
             * Implement your maze generation algorithm here
             * Use helper methods as needed
             */
        }
    }
}