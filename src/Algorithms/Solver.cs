using sudoku.src.GameModel;
using System;


namespace sudoku.src.Algorithms
{
    public static class Solver
    {
        // Solvind strategies list for easy extensibility in the future
        private static readonly List<ISolvingStrategy> _strategies = new List<ISolvingStrategy>
        {
            // Applies logic (Naked/Hidden Singles)
            new NakedSingles(),
            new HiddenSingles()
        };

        public static bool Solve(SudokuBoard board)
        {
            bool changed = true;
            while (changed)
            {
                changed = false;

                // Iterate through all strategies
                foreach (var strategy in _strategies)
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