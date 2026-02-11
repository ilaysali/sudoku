using sudoku.src.GameModel;
using System;


namespace sudoku.src.Algorithms
{
    /// <summary>
    /// The primary class for solving Sudoku puzzles.
    /// Combines human-like logical strategies with algorithmic backtracking.
    /// </summary>
    public static class Solver
    {
        /// <summary>
        /// Attempts to solve the board.
        /// First iteratively applies logical strategies (Naked/Hidden Singles),
        /// then tries to recursive backtracking to finish the puzzle.
        /// </summary>
        /// <param name="board">The board to solve.</param>
        /// <returns>True if the board was successfully solved.</returns>
        public static bool Solve(SudokuBoard board)
        {
            // Solvind strategies list for easy extensibility in the future
            var strategies = new List<ISolvingStrategy>
            {
                new NakedSingles(),
                new HiddenSingles()
            };

            bool changed = true;
            // Apply logical moves before resorting to backtracking
            while (changed)
            {
                changed = false;

                // Iterate through all strategies
                foreach (var strategy in strategies)
                {
                    if (strategy.Apply(board))
                    {
                        changed = true;
                    }
                }
            }

            return BackTracking.BackTrackingAlgorithm(board, 0);
        }
    }
}