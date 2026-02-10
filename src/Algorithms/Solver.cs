using sudoku.src.GameModel;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using static sudoku.src.GameModel.Constants;


namespace sudoku.src.Algorithms
{
    public static class Solver
    {
        // Applies logic (Naked/Hidden Singles) BEFORE recursion
        public static bool Solve(SudokuBoard board)
        {
            bool changed = true;
            while (changed)
            {
                changed = false;
                if (ApplyNakedSingles(board)) changed = true;
                if (ApplyHiddenSingles(board)) changed = true;
            }

            return BackTracking(board, 0);
        }

        private static bool BackTracking(SudokuBoard board, int index)
        {
            if (index >= board.EmptyCells.Count)
                return true;
            // swap to the cell with the minimum remaining values
            MRV(board, index);

            var (row, col) = board.EmptyCells[index];
            int moves = board.GetValidMoves(row, col);

            while (moves > 0)
            {
                int move = moves & -moves; // Get the lowest set bit
                int number = BitOperations.TrailingZeroCount(move) + 1;

                board.PlaceNumber(row, col, number);

                if (BackTracking(board, index + 1))
                    return true;

                board.RemoveNumber(row, col);
                moves &= ~move; // Remove the bit we just tried
            }
            return false;
        }
        public static void MRV(SudokuBoard board, int index)
        {
            // Look ahead in the list to find the cell with the fewest moves
            int bestIndex = index;
            int minMoves = Size;

            for (int i = index; i < board.EmptyCells.Count; i++)
            {
                var (row, col) = board.EmptyCells[i];
                int count = board.GetValidMovesCount(row, col);

                // If any cell has 0 moves, this path is dead immediately
                if (count == 0) break;

                if (count < minMoves)
                {
                    minMoves = count;
                    bestIndex = i;
                    if (minMoves == 1) break; // Can't get better than 1
                }
            }

            // Swap the best cell to the current index position
            if (bestIndex != index)
            {
                var temp = board.EmptyCells[index];
                board.EmptyCells[index] = board.EmptyCells[bestIndex];
                board.EmptyCells[bestIndex] = temp;
            }
        }

        private static bool ApplyNakedSingles(SudokuBoard board)
        {
            bool changed = false;
            var toFill = new List<(int row, int col, int val)>();

            foreach (var (row, col) in board.EmptyCells)
            {
                int moves = board.GetValidMoves(row, col);
                if (BitOperations.PopCount((uint)moves) == 1)
                {
                    int val = BitOperations.TrailingZeroCount(moves) + 1;
                    toFill.Add((row, col, val));
                }
            }

            foreach (var (row, col, val) in toFill)
            {
                if (board.IsEmpty(row, col))
                {
                    board.PlaceNumber(row, col, val);
                    changed = true;
                }
            }

            if (changed) board.UpdateEmptyCellsList();
            return changed;
        }

        private static bool ApplyHiddenSingles(SudokuBoard board)
        {
            bool changed = false;

            for (int i = 0; i < Size; i++)
            {
                if (FindAndFillHiddenSingle(board, board.GetRowCells(i))) changed = true;
                if (FindAndFillHiddenSingle(board, board.GetColCells(i))) changed = true;
                if (FindAndFillHiddenSingle(board, board.GetBoxCells(i))) changed = true;
            }

            if (changed) board.UpdateEmptyCellsList();
            return changed;
        }

        private static bool FindAndFillHiddenSingle(SudokuBoard board, IEnumerable<(int row, int col)> unitCells)
        {
            int[] counts = new int[Size + 1];
            (int row, int col)[] lastPos = new (int, int)[Size + 1];

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
            for (int num = 1; num <= Size; num++)
            {
                // If a number has exactly 1 valid spot in this unit, place it.
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