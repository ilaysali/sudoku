using sudoku.src.Exceptions;
using sudoku.src.GameModel;
using System;
using sudoku.src.Utils;


namespace sudoku.src.Algorithms
{
    public class NakedSingles : ISolvingStrategy
    {
        public string StrategyName => "Naked Singles";

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
