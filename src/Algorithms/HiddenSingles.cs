using sudoku.src.GameModel;
using System;
using System.Drawing;
using System.Numerics;



namespace sudoku.src.Algorithms
{
    public class HiddenSingles : ISolvingStrategy
    {
        public string StrategyName => "Hidden Singles";

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

        private bool FindAndFillHiddenSingle(SudokuBoard board, IEnumerable<(int row, int col)> unitCells)
        {
            int[] counts = new int[Constants.Size + 1];
            (int row, int col)[] lastPos = new (int, int)[Constants.Size + 1];

            foreach (var (row, col) in unitCells)
            {
                if (!board.IsEmpty(row, col)) continue;

                int moves = board.GetValidMoves(row, col);
                while (moves > 0)
                {
                    int bit = moves & -moves;
                    int num = BitOperations.TrailingZeroCount(bit) + 1;
                    counts[num]++;
                    lastPos[num] = (row, col);
                    moves &= ~bit;
                }
            }

            bool localChange = false;
            for (int num = 1; num <= Constants.Size; num++)
            {
                if (counts[num] == 1)
                {
                    var (row, col) = lastPos[num];
                    if (board.IsEmpty(row, col))
                    {
                        board.PlaceNumber(row, col, num);
                        localChange = true;
                    }
                }
            }
            return localChange;
        }
    }
}
