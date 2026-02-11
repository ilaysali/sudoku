using sudoku.src.GameModel;
using System;


namespace sudoku.src.Algorithms
{
    public static class Solver
    {
        public static bool Solve(SudokuBoard board)
        {
            // Solvind strategies list for easy extensibility in the future
            var strategies = new List<ISolvingStrategy>
            {
                new NakedSingles(),
                new HiddenSingles()
            };

            bool changed = true;
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