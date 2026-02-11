using sudoku.src.GameModel;
using System;
using sudoku.src.Utils;

namespace sudoku.src.Algorithms
{
    /// <summary>
    /// Implements the Naked Singles solving strategy.
    /// Identifies cells that have only one legal possibile move.
    /// </summary>
    public class NakedSingles : ISolvingStrategy
    {
        public string StrategyName => "Naked Singles";

        /// <summary>
        /// Scans all empty cells. If a cell has exactly one valid move, it is filled.
        /// </summary>
        /// <param name="board">The board to analyze and update.</param>
        /// <returns>True if any naked singles were found and filled.</returns>
        public bool Apply(SudokuBoard board)
        {
            bool changed = false;
            var toFill = new List<(int row, int col, int val)>();

            foreach (var (row, col) in board.EmptyCells)
            {
                int moves = board.GetValidMoves(row, col);
                // If only one bit is set in moves, it means there's only one valid move for that cell
                if (BitOperation.CountSetBits(moves) == 1)
                {
                    int val = BitOperation.ToDigit(moves);
                    toFill.Add((row, col, val));
                }
            }

            foreach (var (row, col, val) in toFill)
            {
                board.PlaceNumber(row, col, val);
                changed = true;
            }

            if (changed)
                board.UpdateEmptyCellsList();
            return changed;
        }
    }
}
