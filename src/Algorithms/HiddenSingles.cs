using sudoku.src.GameModel;
using sudoku.src.Utils;
using System;

namespace sudoku.src.Algorithms
{
    /// <summary>
    /// Implements the Hidden Singles solving strategy.
    /// Identifies numbers that can only be placed in one specific cell within a unit (Row, Column, or Box),
    /// </summary>
    public class HiddenSingles : ISolvingStrategy
    {
        public string StrategyName => "Hidden Singles";

        private readonly int[] _counts;
        private readonly (int row, int col)[] _lastPos;

        public HiddenSingles()
        {
            // Pre-allocate arrays to avoid Unnecessary usage of repeated calls
            _counts = new int[Constants.Size + 1];
            _lastPos = new (int, int)[Constants.Size + 1];
        }

        /// <summary>
        /// Iterates through all rows, columns, and boxes to find and fill Hidden Singles.
        /// </summary>
        /// <param name="board">The board to analyze and update.</param>
        /// <returns>True if any hidden singles were found and filled.</returns>
        public bool Apply(SudokuBoard board)
        {
            bool changed = false;

            for (int i = 0; i < Constants.Size; i++)
            {
                if (FindAndFillHiddenSingle(board, board.GetRowCells(i))) changed = true;
                if (FindAndFillHiddenSingle(board, board.GetColCells(i))) changed = true;
                if (FindAndFillHiddenSingle(board, board.GetBoxCells(i))) changed = true;
            }

            if (changed) board.UpdateEmptyCellsList();
            return changed;
        }

        /// <summary>
        /// Analyzes a specific unit (row, col, or box) to determine if any number appears as a candidate exactly once.
        /// </summary>
        private bool FindAndFillHiddenSingle(SudokuBoard board, IEnumerable<(int row, int col)> unitCells)
        {
            Array.Clear(_counts, 0, _counts.Length);

            foreach (var (row, col) in unitCells)
            {
                if (!board.IsEmpty(row, col)) continue;

                int moves = board.GetValidMoves(row, col);
                while (moves > 0)
                {
                    int bit = BitOperation.GetLowestSetBit(moves);
                    int num = BitOperation.ToDigit(bit);
                    _counts[num]++;
                    _lastPos[num] = (row, col);
                    moves = BitOperation.RemoveBit(moves, bit);
                }
            }

            bool localChange = false;
            for (int num = 1; num <= Constants.Size; num++)
            {
                if (_counts[num] == 1)
                {
                    var (row, col) = _lastPos[num];
                    board.PlaceNumber(row, col, num);
                    localChange = true;
                }
            }
            return localChange;
        }
    }
}
